using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DataBank
{
    public class BookDataDb : SqliteHelper
    {
        private const String Tag = "Riz: BookDataDb:\t";

        private const String TABLE_NAME = "BooksAttempt2";
        private const String KEY_TITLE = "Title";
        private const String KEY_AUTHORS = "Authors";
        private const String KEY_PUBLISHDATE = "PublishDate";
        private const String KEY_RATING = "Rating";
        private const String KEY_RATINGCOUNT = "RatingCount";
        private const String KEY_COVER = "Cover";
        private const String KEY_ISBN = "ISBN";
        private const String KEY_NUMBEROFPAGES = "NumberOfPages";
        private const String KEY_NUMBEROFCHAPTERS = "NumberOfChapters";
        private const String KEY_GENRE = "Genre";
        private const String KEY_LANGUAGES = "Languages";

        //private const String KEY_ANOTHERSTR = "AnotherStr";



        private String[] COLUMNS = new String[] { KEY_TITLE, KEY_AUTHORS, KEY_PUBLISHDATE, KEY_RATING, KEY_RATINGCOUNT,
            KEY_COVER, KEY_ISBN, KEY_NUMBEROFPAGES, KEY_NUMBEROFCHAPTERS, KEY_GENRE, KEY_LANGUAGES };

        public BookDataDb() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_TITLE + " TEXT PRIMARY KEY, " +
                KEY_AUTHORS + " TEXT, " +
                KEY_PUBLISHDATE + " TEXT, " +
                KEY_RATING + " TEXT, " +
                KEY_RATINGCOUNT + " INTEGER, " +
                KEY_COVER + " BLOB, " +
                KEY_ISBN + " TEXT, " +
                KEY_NUMBEROFPAGES + " INTEGER, " +
                KEY_NUMBEROFCHAPTERS + " INTEGER, " +
                KEY_GENRE + " TEXT, " +
                KEY_LANGUAGES + " TEXT )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(BookDataEntity pBook)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_TITLE + ", " 
                + KEY_AUTHORS + ", " 
                + KEY_PUBLISHDATE + ", " 
                + KEY_RATING + ", " 
                + KEY_RATINGCOUNT + ", " 
                + KEY_COVER + ", " 
                + KEY_ISBN + ", " 
                + KEY_NUMBEROFPAGES + ", " 
                + KEY_NUMBEROFCHAPTERS + ", " 
                + KEY_GENRE + ", " 
                + KEY_LANGUAGES + " ) "

                + "VALUES ( '"
                + pBook.Title + "', '"
                + pBook.Authors + "', '"
                + pBook.PublishDate + "', '"
                + pBook.Rating + "', '"
                + pBook.RatingCount + "', '"
                + pBook.Cover + "', '"
                + pBook.Isbn + "', '"
                + pBook.NumberOfPages + "', '"
                + pBook.NumberOfChapters + "', '"
                + pBook.Genre + "', '"
                + pBook.Languages + "' )";
            dbcmd.ExecuteNonQuery();
        }
        //public void InsertImage(byte[] imageBytes) 
        //{ 
        //    using (IDbConnection dbConnection = new SqliteConnection(connectionString)) 
        //    {
        //        dbConnection.Open(); 
        //        using (IDbCommand dbCommand = dbConnection.CreateCommand()) 
        //        { 
        //            string sqlQuery = "INSERT INTO Images (image) VALUES (@image)";
        //            dbCommand.CommandText = sqlQuery; 
        //            dbCommand.Parameters.Add(new SqliteParameter("@image", imageBytes)); 
        //            dbCommand.ExecuteNonQuery();
        //        }
        //    }
        //}
        //public byte[] GetImage(int id)
        //{
        //    using (IDbConnection dbConnection = new SqliteConnection(connectionString)) 
        //    {
        //        dbConnection.Open(); 
        //        using (IDbCommand dbCommand = dbConnection.CreateCommand()) 
        //        {
        //            string sqlQuery = "SELECT image FROM Images WHERE id = @id"; 
        //            dbCommand.CommandText = sqlQuery; 
        //            dbCommand.Parameters.Add(new SqliteParameter("@id", id)); 
        //            using (IDataReader reader = dbCommand.ExecuteReader()) 
        //            {
        //                if (reader.Read()) 
        //                {
        //                    return (byte[])reader["image"];
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}

        public override IDataReader getDataById(int id)
        {
            return base.getDataById(id);
        }

        public override IDataReader getDataByString(string str)
        {
            Debug.Log(Tag + "Getting Book: " + str);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_TITLE + " = '" + str + "'";
            return dbcmd.ExecuteReader();
        }

        public override void deleteDataByString(string id)
        {
            Debug.Log(Tag + "Deleting Book: " + id);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_TITLE + " = '" + id + "'";
            dbcmd.ExecuteNonQuery();
        }

        public override void deleteDataById(int id)
        {
            base.deleteDataById(id);
        }

        public override void deleteAllData()
        {
            Debug.Log(Tag + "Deleting Table");

            base.deleteAllData(TABLE_NAME);
        }

        public override IDataReader getAllData()
        {
            return base.getAllData(TABLE_NAME);
        }


        public IDataReader getFirstInAlphabet()
        {
            Debug.Log(Tag + "Getting earliest book in alphabet");
            IDbCommand dbcmd = getDbCommand();

            string query =
                "SELECT * FROM "
                + TABLE_NAME
                + " ORDER BY " + KEY_TITLE + ") ASC LIMIT 1";

            dbcmd.CommandText = query;
            return dbcmd.ExecuteReader();
        }


        /*public IDataReader getNearestLocation(LocationInfo loc)
        {
            Debug.Log(Tag + "Getting nearest centoid from: "
                + loc.latitude + ", " + loc.longitude);
            IDbCommand dbcmd = getDbCommand();

            string query =
                "SELECT * FROM "
                + TABLE_NAME
                + " ORDER BY ABS(" + KEY_LAT + " - " + loc.latitude
                + ") + ABS(" + KEY_LNG + " - " + loc.longitude + ") ASC LIMIT 1";

            dbcmd.CommandText = query;
            return dbcmd.ExecuteReader();
        }*/

        /*public IDataReader getLatestTimeStamp()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " ORDER BY " + KEY_DATE + " DESC LIMIT 1";
            return dbcmd.ExecuteReader();
        }*/
    }
}