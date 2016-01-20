using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using YoutubeDL.Properties;

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

        internal void Insert(DownloadVid c)
        {
            string query = string.Format(Resources.INSERT_FORMAT,
                SQLiteDatabase.NormalizeParam(c.vid),
                SQLiteDatabase.NormalizeParam(c.group),
                SQLiteDatabase.NormalizeParam(c.channel_id),
                SQLiteDatabase.NormalizeParam(c.date_add)
            );

            if (!begin_update) db.ExecuteNonQuery(query);
            else script.AppendLine(query);
        }
        internal void UpdateFormat(DownloadVid c)
        {
            string query = string.Format(Resources.UPDATE_FORMAT,
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
            );

            if (!begin_update) db.ExecuteNonQuery(query);
            else script.AppendLine(query);
        }
        internal void UpdateStatus(DownloadVid c)
        {
            string query = string.Format(Resources.UPDATE_STATUS,
                SQLiteDatabase.NormalizeParam(c.vid),
                SQLiteDatabase.NormalizeParam(c.status)
            );

            if (!begin_update) db.ExecuteNonQuery(query);
            else script.AppendLine(query);
        }
        internal void UpdateAfterLoading(DownloadVid c)
        {
            string query = string.Format(Resources.UPDATE_AFTERLOADING,
                SQLiteDatabase.NormalizeParam(c.vid),

                SQLiteDatabase.NormalizeParam(c.status),
                SQLiteDatabase.NormalizeParam(c.title),
                SQLiteDatabase.NormalizeParam(c.fps60 ? 1 : 0),
                SQLiteDatabase.NormalizeParam(c.date_format),
                SQLiteDatabase.NormalizeParam(c.jsonYDL)
            );

            if (!begin_update) db.ExecuteNonQuery(query);
            else script.AppendLine(query);
        }
        internal void UpdateAfterMerging(DownloadVid c)
        {
            string query = string.Format(Resources.UPDATE_MERGING,
                SQLiteDatabase.NormalizeParam(c.vid),

                SQLiteDatabase.NormalizeParam(c.status),
                SQLiteDatabase.NormalizeParam(c.date_merge)
            );

            if (!begin_update) db.ExecuteNonQuery(query);
            else script.AppendLine(query);
        }
        internal void UpdateGroup(DownloadVid c)
        {
            string query = string.Format(Resources.UPDATE_GROUP,
                SQLiteDatabase.NormalizeParam(c.vid),

                SQLiteDatabase.NormalizeParam(c.group),
                SQLiteDatabase.NormalizeParam(c.channel_id)
            );

            if (!begin_update) db.ExecuteNonQuery(query);
            else script.AppendLine(query);
        }
        internal void UpdateFilename(DownloadVid c)
        {
            string query = string.Format(Resources.UPDATE_FILENAME,
                SQLiteDatabase.NormalizeParam(c.vid),
                SQLiteDatabase.NormalizeParam(c.filename)
            );

            if (!begin_update) db.ExecuteNonQuery(query);
            else script.AppendLine(query);
        }

        internal void DeleteVid(string vid)
        {
            db.ExecuteNonQuery(string.Format("DELETE FROM video WHERE vid = '{0}'", vid));
        }

        internal DownloadVid[] LoadDownloadVideo(int channel_id, string group, bool showCompleted)
        {
            DataTable dt = db.GetDataTable(
                string.Format("select * " +
                              "from video " +
                              "where ({0} = 0 OR channel_id = {0}) " +
                                    "AND ('{1}' = 'All' OR [group] = '{1}' OR ('{1}' = '' AND ([group] = '' OR [group] is null))) " +
                                    (showCompleted ? "AND status = 4 " : "AND status >= 0"),
                            channel_id, SQLiteDatabase.Escape(group))
                );
            return dt.AsEnumerable().Select(r => MapRowToVideo(r)).ToArray();
        }
        internal DownloadVid[] LoadDeletedVideo()
        {
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
        internal DownloadVid GetVideo(string vid)
        {
            DataTable dt = db.GetDataTable(
                string.Format("select * from video where vid = {0} ", SQLiteDatabase.NormalizeParam(vid))
                );
            return dt.AsEnumerable().Select(r => MapRowToVideo(r)).FirstOrDefault();
        }

        internal void SaveImage(string vid, byte[] imagen)
        {
            SQLiteConnection con = new SQLiteConnection("Data Source=imagelist.db");
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO image (key, imagebin) VALUES (@p0, @p1);"
                , con);
            cmd.Parameters.Add(new SQLiteParameter("@p0", vid));
            cmd.Parameters.Add(new SQLiteParameter("@p1", imagen));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        internal List<FlatImage> LoadImage()
        {
            List<FlatImage> list = new List<FlatImage>();

            SQLiteConnection con = new SQLiteConnection("Data Source=imagelist.db");
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM image;", con);
            con.Open();
            IDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new FlatImage
                {
                    _key = (string)rdr[0],
                    _image = Helper.ByteToImage((System.Byte[])rdr[1])
                });
            }
            con.Close();

            return list;
        }

        private DownloadVid MapRowToVideo(DataRow r)
        {
            return new DownloadVid
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
            };
        }
    }
}