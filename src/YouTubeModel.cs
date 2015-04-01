using System.Collections.Generic;
using System.Text;
using System;

public class YoutubeDlInfo
{
    public string Id { get; set; }
    public string Upload_Date { get; set; }
    public string Title { get; set; }
    public int Duration { get; set; }
    public string Url { get; set; }
    public int? Age_Limit { get; set; }
    public string Format_Id { get; set; }
    public List<Formats> Formats { get; set; }

    public string error_message { get; set; }
    public bool error { get; set; }

    public static string[] GetVideoIDFromUrl(string url)
    {
        url = url.Substring(url.IndexOf("?") + 1);
        char[] delimiters = { '&', '#', '\r', '\n', '?' };
        string[] props = url.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        StringBuilder videoids = new StringBuilder();
        foreach (string prop in props)
        {
            if (prop.StartsWith("v="))
                videoids.AppendLine(prop.Substring(2));
        }

        return videoids.ToString().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
    }

}

public class Formats
{
    public string Format_Id { get; set; }
    public string Format { get; set; }
    public string Vcodec { get; set; }
    public string Acodec { get; set; }
    public string Format_Note { get; set; }
    public string Url { get; set; }
    public string Ext { get; set; }
    public long? FileSize { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
    public int? Fps { get; set; }
    public int? Preference { get; set; }
    public int? Abr { get; set; }
}

public class DownloadVid
{
    public string vid { get; set; }
    public string vidFID { get; set; }
    public string vidUrl { get; set; }
    public string vidFilename { get; set; }
    public long? vidSize { get; set; }
    public string audFID { get; set; }
    public string audUrl { get; set; }
    public string audFilename { get; set; }
    public long? audSize { get; set; }
    public string ext { get; set; }

    public long? size { get; set; }
    public string resolution  { get; set; }

    public string filename { get; set; }
    public string title { get; set; }
    public int status { get; set; }
    public string group { get; set; }
    public string jsonYDL { get; set; }

}