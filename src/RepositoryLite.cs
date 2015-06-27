using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Configuration;
using YoutubeDL.Properties;
using System.Data;
using System.Web.Script.Serialization;
using System.Text;
using System.Data.SQLite;
using YoutubeDL;

namespace YoutubeDL.Models
{
    public class RepositoryLite
    {
        SQLiteDatabase db;
        StringBuilder script;
        bool begin_update;
        public RepositoryLite()
        {
            var connectionString = "Data Source=ytvid.db";
            db = new SQLiteDatabase(connectionString);

            script = new StringBuilder();
        }

        internal DownloadVid[] _LoadDownloadVideo(int channel_id)
        {
            var jsSer = new JavaScriptSerializer();
            DataTable dt = db.GetDataTable(
                string.Format("select jsonYDL, jsonRecord from video where {0} = 0 OR channel_id = {0}", channel_id)
                );
            return dt.AsEnumerable().Select(r => { 
                var vid = jsSer.Deserialize<DownloadVid>((string)r["jsonRecord"]);
                vid.jsonYDL = (string)r["jsonYDL"];
                return vid;
            }).ToArray();
        }
        internal void _InsertOrUpdate(DownloadVid c)
        {
            string jsonYDL = c.jsonYDL;
            c.jsonYDL = null;

            var jsSer = new JavaScriptSerializer();
            string jsonRecord = jsSer.Serialize(c);

            c.jsonYDL = jsonYDL;

            string sqlcommand = Resources.INSERT;
            if (GetVid(c.vid) != null)
                sqlcommand = Resources.UPDATE;

            script.AppendLine(string.Format(sqlcommand,
                SQLiteDatabase.Escape(c.vid),
                SQLiteDatabase.Escape(c.vidFID),
                SQLiteDatabase.Escape(c.vidUrl),
                SQLiteDatabase.Escape(c.vidFilename),
                c.vidSize,
                SQLiteDatabase.Escape(c.audFID),
                SQLiteDatabase.Escape(c.audUrl),
                SQLiteDatabase.Escape(c.audFilename),
                c.audSize,
                SQLiteDatabase.Escape(c.resolution),
                SQLiteDatabase.Escape(c.ext),
                c.status,
                SQLiteDatabase.Escape(c.title),
                c.size,
                SQLiteDatabase.Escape(jsonYDL),
                SQLiteDatabase.Escape(jsonRecord)
            ));

            if (!begin_update) Commit();
        }

        internal DownloadVid GetVid(string vid)
        {
            var jsSer = new JavaScriptSerializer();
            DataTable dt = db.GetDataTable(string.Format("select jsonRecord from video where vid == '{0}'", vid));
            return dt.AsEnumerable().Select(r => jsSer.Deserialize<DownloadVid>((string)r["jsonRecord"])).SingleOrDefault();

        }

        internal void DeleteVid(string vid)
        {
            db.ExecuteNonQuery(string.Format("DELETE FROM video WHERE vid = '{0}'", vid));
        }

        internal void BeginUpdate(){
            begin_update = true;
            script.Length = 0;
        }
        internal void Commit()
        {
            if(script.Length > 0)
                db.ExecuteNonQuery(script.ToString());

            begin_update = false;
            script.Length = 0;
        }

        internal Channel[] Get_Channel_list()
        {
            DataTable dt = db.GetDataTable("select * from channel");
            return dt.AsEnumerable().Select(r => new Channel
            {
                id = Convert.ToInt32(r["id"]),
                ytchannel_id = (string)r["ytchannel_id"],
                name = (string)r["name"],
                folder = (string)r["folder"]
            }).ToArray();
        }

        internal void _UpdateFormat(DownloadVid c)
        {
            KeyValuePair<string, object>[] parameters = new KeyValuePair<string,object>[]{
                new KeyValuePair<string, object>("vidFID", c.vidFID),
                new KeyValuePair<string, object>("vidUrl", c.vidUrl),
                new KeyValuePair<string, object>("vidFilename", c.vidFilename),
                new KeyValuePair<string, object>("vidSize", c.vidSize),

                new KeyValuePair<string, object>("audFID", c.audFID),
                new KeyValuePair<string, object>("audUrl", c.audUrl),
                new KeyValuePair<string, object>("audFilename", c.audFilename),
                new KeyValuePair<string, object>("audSize", c.audSize),

                new KeyValuePair<string, object>("resolution", c.resolution),
                new KeyValuePair<string, object>("ext", c.ext),
                new KeyValuePair<string, object>("filename", c.filename),
                new KeyValuePair<string, object>("size", c.size),
                new KeyValuePair<string, object>("status", c.status),
            };

            db.Update("video", parameters, string.Format("vid = '{0}'", c.vid));
        }

        internal void Insert(DownloadVid c)
        {
            script.AppendLine(string.Format(Resources.INSERT_FORMAT,
                SQLiteDatabase.NormalizeParam(c.vid),
                SQLiteDatabase.NormalizeParam(c.group),
                SQLiteDatabase.NormalizeParam(c.channel_id),
                SQLiteDatabase.NormalizeParam(c.date_add)
            ));

            if (!begin_update) Commit();
        }
        internal void UpdateFormat(DownloadVid c)
        {
            script.AppendLine(string.Format(Resources.UPDATE_FORMAT,
                SQLiteDatabase.NormalizeParam(c.vid),

                SQLiteDatabase.NormalizeParam(c.vidFID),
                SQLiteDatabase.NormalizeParam(c.vidUrl),
                SQLiteDatabase.NormalizeParam(c.vidFilename),
                SQLiteDatabase.NormalizeParam(c.vidSize),

                SQLiteDatabase.NormalizeParam(c.audFID),
                SQLiteDatabase.NormalizeParam(c.audUrl),
                SQLiteDatabase.NormalizeParam(c.audFilename),
                SQLiteDatabase.NormalizeParam(c.audSize),

                SQLiteDatabase.NormalizeParam(c.resolution),
                SQLiteDatabase.NormalizeParam(c.ext),
                SQLiteDatabase.NormalizeParam(c.filename),
                SQLiteDatabase.NormalizeParam(c.size),
                SQLiteDatabase.NormalizeParam(c.status)
            ));

            if (!begin_update) Commit();
        }
        internal void UpdateStatus(DownloadVid c)
        {
            script.AppendLine(string.Format(Resources.UPDATE_STATUS,
                SQLiteDatabase.NormalizeParam(c.vid),
                SQLiteDatabase.NormalizeParam(c.status)
            ));

            if (!begin_update) Commit();
        }
        internal void UpdateAfterLoading(DownloadVid c)
        {
            script.AppendLine(string.Format(Resources.UPDATE_AFTERLOADING,
                SQLiteDatabase.NormalizeParam(c.vid),

                SQLiteDatabase.NormalizeParam(c.status),
                SQLiteDatabase.NormalizeParam(c.title),
                SQLiteDatabase.NormalizeParam(c.fps60 ? 1 : 0),
                SQLiteDatabase.NormalizeParam(c.date_format),
                SQLiteDatabase.NormalizeParam(c.jsonYDL)
            ));

            if (!begin_update) Commit();
        }
        internal void UpdateAfterMerging(DownloadVid c)
        {
            script.AppendLine(string.Format(Resources.UPDATE_MERGING,
                SQLiteDatabase.NormalizeParam(c.vid),

                SQLiteDatabase.NormalizeParam(c.status),
                SQLiteDatabase.NormalizeParam(c.date_merge)
            ));

            if (!begin_update) Commit();
        }
        internal void UpdateGroup(DownloadVid c)
        {
            script.AppendLine(string.Format(Resources.UPDATE_GROUP,
                SQLiteDatabase.NormalizeParam(c.vid),

                SQLiteDatabase.NormalizeParam(c.group),
                SQLiteDatabase.NormalizeParam(c.channel_id)
            ));

            if (!begin_update) Commit();
        }

        internal void _UpdateNewField(DownloadVid c)
        {
            script.AppendLine(string.Format(Resources.UPDATE_NEWFIELD,
                SQLiteDatabase.NormalizeParam(c.vid),

                SQLiteDatabase.NormalizeParam(c.filename),
                SQLiteDatabase.NormalizeParam(c.group),
                SQLiteDatabase.NormalizeParam(c.fps60 ? 1 : 0),
                SQLiteDatabase.NormalizeParam(c.date_add),
                SQLiteDatabase.NormalizeParam(c.date_format),
                SQLiteDatabase.NormalizeParam(c.date_merge)
            ));

            if (!begin_update) Commit();
        }

        internal DownloadVid[] LoadDownloadVideo(int channel_id, string group)
        {
            var jsSer = new JavaScriptSerializer();
            DataTable dt = db.GetDataTable(
                string.Format("select * from video where ({0} = 0 OR channel_id = {0}) AND ('{1}' = '' OR [group] = '{1}' OR ('{1}' = 'zOthers' AND [group] = '')) AND status >= 0", channel_id, SQLiteDatabase.Escape(group))
                );
            return dt.AsEnumerable().Select(r => new DownloadVid
            {
                vid = (string)r["vid"],

                vidFID = r["vidFID"] is DBNull ? null : (string)r["vidFID"],
                vidUrl = r["vidUrl"] is DBNull ? null : (string)r["vidUrl"],
                vidFilename = r["vidFilename"] is DBNull ? null : (string)r["vidFilename"],
                vidSize = r["vidSize"] is DBNull ? (long?)null : Convert.ToInt64(r["vidSize"]),

                audFID = r["audFID"] is DBNull ? null : (string)r["audFID"],
                audUrl = r["audUrl"] is DBNull ? null : (string)r["audUrl"],
                audFilename = r["audFilename"] is DBNull ? null : (string)r["audFilename"],
                audSize = r["audSize"] is DBNull ? (long?)null : Convert.ToInt64(r["audSize"]),

                resolution = r["resolution"] is DBNull ? null : (string)r["resolution"],
                ext = r["ext"] is DBNull ? null : (string)r["ext"],
                filename = r["filename"] is DBNull ? null : (string)r["filename"],
                size = r["size"] is DBNull ? (long?)null : Convert.ToInt64(r["size"]),
                status = r["status"] is DBNull ? 0 : Convert.ToInt32(r["status"]),

                title = r["title"] is DBNull ? null : (string)r["title"],
                group = r["group"] is DBNull ? null : (string)r["group"],
                channel_id = r["channel_id"] is DBNull ? 0 : Convert.ToInt32(r["channel_id"]),

                fps60 = r["fps60"] is DBNull ? false : Convert.ToInt32(r["fps60"]) == 1,

                date_add = r["date_add"] is DBNull ? DateTime.MinValue : Convert.ToInt64(r["date_add"]).FromUnixTime(),
                date_format = r["date_format"] is DBNull ? (DateTime?)null : Convert.ToInt64(r["date_format"]).FromUnixTime(),
                date_merge = r["date_merge"] is DBNull ? (DateTime?)null : Convert.ToInt64(r["date_merge"]).FromUnixTime(),

                jsonYDL = r["jsonYDL"] is DBNull ? null : (string)r["jsonYDL"],
            }).ToArray();
        }
        internal DownloadVid[] LoadDeletedVideo()
        {
            var jsSer = new JavaScriptSerializer();
            DataTable dt = db.GetDataTable(
                string.Format("select vid,filename,[group],channel_id from video where status = -1")
                );
            return dt.AsEnumerable().Select(r => new DownloadVid
            {
                vid = (string)r["vid"],

                filename = r["filename"] is DBNull ? null : (string)r["filename"],
                group = r["group"] is DBNull ? null : (string)r["group"],
                channel_id = r["channel_id"] is DBNull ? 0 : Convert.ToInt32(r["channel_id"]),
            }).ToArray();
        }
    }
}