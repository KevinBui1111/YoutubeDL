/*
refer to https://github.com/jamesmanning/RunProcessAsTask
Added: 2018-01-19 07:20 AM
*/
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RunProcessAsTask
{
    public static partial class ProcessEx
    {
        public static Task<ProcessResults> RunAsync(ProcessStartInfo processStartInfo, System.ComponentModel.ISynchronizeInvoke synchronizingObject = null)
            => RunAsync(processStartInfo, CancellationToken.None, synchronizingObject);

        public static async Task<ProcessResults> RunAsync(ProcessStartInfo processStartInfo, CancellationToken cancellationToken, System.ComponentModel.ISynchronizeInvoke synchronizingObject = null)
        {
            // force some settings in the start info so we can capture the output
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;

            var tcs = new TaskCompletionSource<ProcessResults>();

            var standardOutput = new StringBuilder();
            var standardError = new StringBuilder();

            var process = new Process {
                StartInfo = processStartInfo,
                EnableRaisingEvents = true,
                SynchronizingObject = synchronizingObject
            };

            var standardOutputResults = new TaskCompletionSource<string>();
            process.OutputDataReceived += (sender, args) => {
                if (args.Data != null)
                    standardOutput.Append(args.Data);
                else
                    standardOutputResults.SetResult(standardOutput.ToString());
            };

            var standardErrorResults = new TaskCompletionSource<string>();
            process.ErrorDataReceived += (sender, args) => {
                if (args.Data != null)
                    standardError.Append(args.Data);
                else
                    standardErrorResults.SetResult(standardError.ToString());
            };

            var processStartTime = new TaskCompletionSource<DateTime>();

            process.Exited += async (sender, args) => {
                // Since the Exited event can happen asynchronously to the output and error events, 
                // we await the task results for stdout/stderr to ensure they both closed.  We must await
                // the stdout/stderr tasks instead of just accessing the Result property due to behavior on MacOS.  
                // For more details, see the PR at https://github.com/jamesmanning/RunProcessAsTask/pull/16/
                tcs.TrySetResult(new ProcessResults(process, await processStartTime.Task, await standardOutputResults.Task, await standardErrorResults.Task));
            };

            using (cancellationToken.Register(
                () => {
                    tcs.TrySetCanceled();
                    try {
                        if (!process.HasExited)
                            process.Kill();
                    } catch (InvalidOperationException) { }
                })) {
                cancellationToken.ThrowIfCancellationRequested();

                if (process.Start() == false)
                    tcs.TrySetException(new InvalidOperationException("Failed to start process"));
                processStartTime.SetResult(process.StartTime);

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                return await tcs.Task;
            }
        }

        // these overloads match the ones in Process.Start to make it a simpler transition for callers
        // see http://msdn.microsoft.com/en-us/library/system.diagnostics.process.start.aspx
        public static Task<ProcessResults> RunAsync(string fileName)
            => RunAsync(new ProcessStartInfo(fileName));

        public static Task<ProcessResults> RunAsync(string fileName, string arguments, System.ComponentModel.ISynchronizeInvoke synchronizingObject = null)
            => RunAsync(new ProcessStartInfo(fileName, arguments), synchronizingObject);
    }
    public sealed class ProcessResults : IDisposable
    {
        public ProcessResults(Process process, DateTime processStartTime, string standardOutput, string standardError)
        {
            Process = process;
            ExitCode = process.ExitCode;
            RunTime = process.ExitTime - processStartTime;
            StandardOutput = standardOutput;
            StandardError = standardError;
        }

        public Process Process { get; }
        public int ExitCode { get; }
        public TimeSpan RunTime { get; }
        public string StandardOutput { get; }
        public string StandardError { get; }
        public void Dispose() { Process.Dispose(); }
    }
}
