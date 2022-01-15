using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace BookStoreApi.AddControllers{
    
    
    [ApiController]
    [Route("[controller]s")]
            public class BookController : ControllerBase{
                Logger logger = new Logger(); //created the logger class
    
                // a list of different books from different genres
                private static List<Book> BookList= new List<Book>(){
                    new Book{
                        Id = 1,
                        Title = "Harry Potter",
                        GenreId = 1,// Fantasy- Fiction
                        PageCount = 390,
                        PublishDate = new DateTime(1998,08,23)
                    },
                    new Book{
                        Id = 2,
                        Title = "Grapes of Wrath",
                        GenreId = 2,// Drama
                        PageCount = 230,
                        PublishDate = new DateTime(1967,09,03)
                    },
                    new Book{
                        Id = 3,
                        Title = "Herland",
                        GenreId = 3,// Science- Fiction
                        PageCount = 490,
                        PublishDate = new DateTime(1988,12,09)
                    },
                    new Book{
                        Id = 4,
                        Title = "Think and Grow Rich",
                        GenreId = 4,// Personal Growth
                        PageCount = 190,
                        PublishDate = new DateTime(2004,01,19)
                    }
                };
                //tum book sinifi nesnelerini donen http get metodu
                [HttpGet]
                public List<Book> GetBooks(){
                    var bookList = BookList.OrderBy(x=> x.Id).ToList<Book>();
                    logger.createLog("Get operation: Book list is fetched successfully!");
                    return bookList;
                } 

                // belirli id ile eslesen nesnelerini donen http get metodu
                [HttpGet("{id}")]
                public Book GetBooksByID(int id ){
                    var book = BookList.Where(book=> book.Id==id).SingleOrDefault();
                    logger.createLog("GetById operation: The book with "+book.Id +" is fetched successfully!");
                    return book;
                } 

                //Listeye yeni kitap ekleyen post metodu
                [HttpPost]
                public Result AddBook([FromBody] Book newBook){
                    Result result = new Result();
                    var book = BookList.SingleOrDefault(x=> x.Title == newBook.Title);
                    if(book != null){
                        result.HttpStatusCode = BadRequest().StatusCode;
                        result.Message= "The book with "+ book.Id + " ID already exists.";
                        logger.createLog("Error while adding: "+ result.Message +"\tStatus Code: "+ result.HttpStatusCode);
                        return result;
                    }
                    BookList.Add(newBook);

                    result.HttpStatusCode = Ok().StatusCode;
                    result.Message= "New book with "+ newBook.Id + " ID is added successfully.";
                    logger.createLog("Success on adding: "+ result.Message +"\tStatus Code: "+ result.HttpStatusCode);
                    return result;
                    
                }

                // Listedeki kitaplarla eslesen idli kitaba ait bilgileri guncelleyen put metodu 
                [HttpPut("{id}")]
                public Result UpdateBook(int id,[FromBody] Book updatedBook){
                    Result result = new Result();
                    var book = BookList.SingleOrDefault(x=> x.Id == id);
                    if(book == null){
                        result.HttpStatusCode = BadRequest().StatusCode;
                        result.Message= "The book with "+ book.Id + " ID does not exists.";
                        logger.createLog("Error while updating: "+ result.Message +"\tStatus Code: "+ result.HttpStatusCode);
                        return result;
                    }
                    book.Title = updatedBook.Title  != default ? updatedBook.Title : book.Title;
                    book.GenreId = updatedBook.GenreId  != default ? updatedBook.GenreId : book.GenreId;
                    book.PageCount = updatedBook.PageCount  != default ? updatedBook.PageCount : book.PageCount;
                    book.PublishDate = updatedBook.PublishDate  != default ? updatedBook.PublishDate : book.PublishDate;
                    
                    result.HttpStatusCode = Ok().StatusCode;
                    result.Message= "The book with "+ book.Id + " ID is updated successfully.";
                    logger.createLog("Success on updating: "+ result.Message +"\tStatus Code: "+ result.HttpStatusCode);
                    return result;
                }

                
                // Listedeki kitaplarla eslesen idli kitabi listeden silen delete metodu 
                [HttpDelete("{id}")]
                public Result DeleteBook(int id){
                  Result result = new Result();

                    var book = BookList.SingleOrDefault(x=> x.Id == id);
                    if(book == null){
                        result.HttpStatusCode = BadRequest().StatusCode;
                        result.Message= "The book with "+ book.Id + " ID does not exists.";
                        logger.createLog("Error while deleting: "+ result.Message +"\tStatus Code: "+ result.HttpStatusCode);
                        return result;
                    }
                        
                    BookList.Remove(book);
                   
                   result.HttpStatusCode = Ok().StatusCode;
                    result.Message= "The book with "+ book.Id + " ID is deleted successfully.";
                    logger.createLog("Success on deleting: "+ result.Message +"\tStatus Code: "+ result.HttpStatusCode);
                    return result;

                }


                


            }
}