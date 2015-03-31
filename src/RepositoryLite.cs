﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Configuration;
using YoutubeDL.Properties;
using System.Data;
using System.Web.Script.Serialization;

namespace YoutubeDL.Models
{
    public class RepositoryLite
    {
        SQLiteDatabase db;
        public RepositoryLite()
        {
            var connectionString = "Data Source=ytvid.db";
            db = new SQLiteDatabase(connectionString);
        }

        internal DownloadVid[] LoadDownloadVideo()
        {
            var jsSer = new JavaScriptSerializer();
            DataTable dt = db.GetDataTable("select jsonYDL, jsonRecord from video");
            return dt.AsEnumerable().Select(r => { 
                var vid = jsSer.Deserialize<DownloadVid>((string)r["jsonRecord"]);
                vid.jsonYDL = (string)r["jsonYDL"];
                return vid;
            }).ToArray();
        }


        internal void InsertOrUpdate(DownloadVid c)
        {
            string jsonYDL = c.jsonYDL;
            c.jsonYDL = null;

            var jsSer = new JavaScriptSerializer();
            string jsonRecord = jsSer.Serialize(c);

            c.jsonYDL = jsonYDL;

            string sqlcommand = Resources.INSERT;
            if (GetVid(c.vid) != null)
                sqlcommand = Resources.UPDATE;

            db.ExecuteNonQuery(string.Format(sqlcommand,
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
    }
}