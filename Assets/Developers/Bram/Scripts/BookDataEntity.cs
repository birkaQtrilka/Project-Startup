using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEditor.PlayerSettings.Switch;

namespace DataBank
{
    public class BookDataEntity
    {

        public string Title { get; private set; }
        public string Authors { get; private set; }
        public string PublishDate { get; private set; }
        public string Rating { get; private set; }
        public int RatingCount { get; private set; }
        public Byte[] Cover { get; private set; }
        public string Isbn { get; private set; }
        public int NumberOfPages { get; private set; }
        public int NumberOfChapters { get; private set; }
        public string Genre { get; private set; }
        public string Languages { get; private set; }


        public string _title;
        public string _authorName;
        public string _isbn;

        public string _anotherString;




        public BookDataEntity(string pTitle, string pAuthors, string pPublishDate, string pRating, 
            int pRatingCount, Byte[] pCover, string pISBN, int pNumberOfPages, int pNumberOfChapters,
            string pGenre, string pLanguages)
        {
            Title = pTitle;
            Authors = pAuthors;
            PublishDate = pPublishDate;
            Rating = pRating; 
            RatingCount = pRatingCount;
            Cover = pCover;
            Isbn = pISBN;
            NumberOfPages = pNumberOfPages;
            NumberOfChapters = pNumberOfChapters;
            Genre = pGenre;
            Languages = pLanguages;
            

        }





        public BookDataEntity(string pTitle, string pAuthorName, string pISBN)
        {
            _title = pTitle;
            _authorName = pAuthorName;
            _isbn = pISBN;

            _anotherString = "";
        }

        public BookDataEntity(string pTitle, string pAuthorName, string pISBN, string pAnotherString)
        {
            _title = pTitle;
            _authorName = pAuthorName;
            _isbn = pISBN;

            _anotherString = pAnotherString;
        }

        public static BookDataEntity getFakeBook()
        {
            return new BookDataEntity("Test_Title", "Test_Author", "313542");
        }
    }
}