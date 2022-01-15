using System;
namespace BookStoreApi;
    public class Book{
        //different attributes of a book object
        public int Id{ get; set; }// the unique ID number for book
        public string Title { get; set; } // the name of the book
        public int GenreId { get; set; } // the  ID number for book genre

        public int AuthorID { get; set; }
        public int PageCount { get; set; } // total number of pages 
        public DateTime PublishDate { get; set; } //the publish date of the book
    }
