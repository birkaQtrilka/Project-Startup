using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DbTestBehaviourScript : MonoBehaviour
{

    //public Sprite sprite;
    public Image image;
    public Image image2;

    public bool ResetData = true;

    // Use this for initialization
    void Start()
    {

        BookDataDb mBookDataDb = new BookDataDb();

        if (ResetData)
        {
            mBookDataDb.deleteAllData();
        }

        byte[] photo = image.sprite.texture.GetRawTextureData();

        /**/
        //Add Data

        mBookDataDb.addData(new BookDataEntity("Atitle1", "Bauthor1", "2006", "4, 5",
            10, photo, "ISBN0", 100, 13,
            "Scarygenre", "English"));

        mBookDataDb.addData(new BookDataEntity("Btitle2", "Cauthor2", "2007", "3, 5",
            10, photo, "ISBN0", 100, 13,
            "Scarygenre", "English"));

        mBookDataDb.addData(new BookDataEntity("Ctitle3", "Aauthor3", "2008", "2, 5",
            10, photo, "ISBN0", 100, 13,
            "Scarygenre", "English"));
        /**/


        mBookDataDb.close();


        //Fetch All Data
        BookDataDb mBookDataDb2 = new BookDataDb();
        System.Data.IDataReader reader = mBookDataDb2.getAllData();
        int fieldCount = reader.FieldCount;
        List<BookDataEntity> myList = new List<BookDataEntity>();
        while (reader.Read())
        {

            BookDataEntity entity = new BookDataEntity(reader[0].ToString(),
                                    reader[1].ToString(),
                                    reader[2].ToString(),
                                    reader[3].ToString(), 
                                    int.Parse(reader[4].ToString()),
                                    ObjectToByteArray(reader[5]),
                                    reader[6].ToString(),
                                    int.Parse(reader[7].ToString()), 
                                    int.Parse(reader[8].ToString()),
                                    reader[9].ToString(),
                                    reader[10].ToString());

            Debug.Log("Title: " + entity.Title + " Author: " + entity.Authors + " ISBN: " + entity.Isbn);
            myList.Add(entity);
            var newTex = new Texture2D(2, 2);
            newTex.LoadImage(entity.Cover);
            image2.sprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new(.5f, .5f));
        }



        //Texture2D tex2d = image2.sprite.texture;
        
        //ImageConversion.LoadImage(tex2d, myList[0].Cover);


        //image2.sprite.texture.LoadRawTextureData(myList[0].Cover);

        // Deleting all data because otherwise error occurs
        //mLocationDb2.deleteAllData();




        /*
        LocationDb mLocationDb = new LocationDb();
        //mLocationDb.addData(new LocationEntity("0", "AR", "0.001", "0.007"));
        /**
        //Add Data
        //mLocationDb.addData(new LocationEntity("0", "AR", "0.001", "0.007"));
        mLocationDb.addData(new LocationEntity("1", "AR", "0.002", "0.006"));
        mLocationDb.addData(new LocationEntity("2", "AR", "0.003", "0.005"));
        mLocationDb.addData(new LocationEntity("3", "AR", "0.004", "0.004"));
        mLocationDb.addData(new LocationEntity("4", "AR", "0.005", "0.003"));
        mLocationDb.addData(new LocationEntity("5", "AR", "0.006", "0.002"));
        mLocationDb.addData(new LocationEntity("6", "AR", "0.007", "0.001"));/**/
        /*mLocationDb.close();


        //Fetch All Data
        LocationDb mLocationDb2 = new LocationDb();
        System.Data.IDataReader reader = mLocationDb2.getAllData();

        int fieldCount = reader.FieldCount;
        List<LocationEntity> myList = new List<LocationEntity>();
        while (reader.Read())
        {
            LocationEntity entity = new LocationEntity(reader[0].ToString(),
                                    reader[1].ToString(),
                                    reader[2].ToString(),
                                    reader[3].ToString(),
                                    reader[4].ToString());

            Debug.Log("id: " + entity._id + " lat: " + entity._Lat + " lng: " + entity._Lng);
            myList.Add(entity);
        }

        

        // Deleting all data because otherwise error occurs
        //mLocationDb2.deleteAllData();
        /**/

    }
    // Convert an object to a byte array
    public static byte[] ObjectToByteArray(object obj)
    {
        BinaryFormatter bf = new();
        using (var ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }
    public void PrintAllData()
    {
        BookDataDb mBookDataDb2 = new BookDataDb();
        System.Data.IDataReader reader = mBookDataDb2.getAllData();

        int fieldCount = reader.FieldCount;
        List<BookDataEntity> myList = new List<BookDataEntity>();
        while (reader.Read())
        {
            BookDataEntity entity = new BookDataEntity(reader[0].ToString(),
                                    reader[1].ToString(),
                                    reader[2].ToString(),
                                    reader[3].ToString(),
                                    int.Parse(reader[4].ToString()),
                                    (System.Byte[])reader[5],
                                    reader[6].ToString(),
                                    int.Parse(reader[7].ToString()),
                                    int.Parse(reader[8].ToString()),
                                    reader[9].ToString(),
                                    reader[10].ToString());

            Debug.Log("Title: " + entity.Title + " Author: " + entity.Authors + " ISBN: " + entity.Isbn);
            myList.Add(entity);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
