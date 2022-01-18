using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DAL.Model;
using EFLibCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers{
    
    
    [ApiController]
    [Route("[controller]s")]
            public class BookController : ControllerBase
            {
                Logger logger = new Logger(); //created the logger class
    
                // a list of different books from different genres
               List<Book> BooksList = new List<Book>();
               //db connection
               DBOperations dBOperations = new DBOperations();
                //tum book sinifi nesnelerini donen http get metodu
                [HttpGet]
                public List<Book> GetBooks(){
                    logger.createLog("Get operation: Book list is fetched successfully!");
                    return dBOperations.GetBooks();
                } 

                // belirli id ile eslesen nesnelerini donen http get metodu
                [HttpGet("{id}")]
                public Book GetBooksByID(int id ){
                    List<Book> bookList= dBOperations.GetBooks();
                    Book? bookSample=new Book();
                    bookSample= bookList.Find(a=> a.Id ==id);
                    if(bookSample == null)

                        logger.createLog("Error with GetById operation: There is no book with "+id +" id.");
                    else
                        logger.createLog("GetById operation: The book with "+ id +" id is fetched successfully!");
                    return bookSample;
                } 

                //Listeye yeni kitap ekleyen post metodu
                [HttpPost]
                public Result AddBook([FromBody] Book newBook){
                    Result result = new Result();

                    Book book = dBOperations.FindBook(newBook.Title,newBook.Id);
                    bool ctrl = (book!=null) ? true : false;
                    if(ctrl == false)
                    {
                            if (dBOperations.AddModel(newBook) == true)
                            {
                                //dBOperations.AddModel(newBook);
                                /*If there is no same id in list, add new profile*/
                                result.HttpStatusCode = Ok().StatusCode;//added successfully
                                result.Message= "New book with "+ newBook.Id + " ID is added successfully.";
                                logger.createLog("Success on adding: "+ result.Message +"\tStatus Code: "+ result.HttpStatusCode);
                            }
                            else
                            {
                                /*If there is an error while adding*/
                                result.HttpStatusCode = BadRequest().StatusCode;
                                result.Message= "The book with "+ newBook.Id + " ID could not added.";
                                logger.createLog("Error while adding: "+ result.Message +"\tStatus Code: "+ result.HttpStatusCode);
                                   
                            }
                                    
                                    
                    }
                    else{
                                result.HttpStatusCode = BadRequest().StatusCode;
                                result.Message= "The book with "+ newBook.Id + " ID already exists."; 
                    }
                    return result;
                    
                }

                // Listedeki kitaplarla eslesen idli kitaba ait bilgileri guncelleyen put metodu 
                [HttpPut("{id}")]
                public Result UpdateBook(int id,[FromBody] Book updatedBook){
                    Result result = new Result();
                    List<Book> bookList = dBOperations.GetBooks();
                    Book? _oldBook = bookList.Find(b=> b.Id == id);
                   
                    if(_oldBook == null){
                        result.HttpStatusCode = BadRequest().StatusCode;
                        result.Message= "The book with "+ id + " ID does not exists.";
                        logger.createLog("Error while updating: "+ result.Message +"\tStatus Code: "+ result.HttpStatusCode);
                        
                    }
                    else{
                        
                        dBOperations.DeleteModel(id);
                        dBOperations.AddModel(updatedBook);
                    
                        result.HttpStatusCode = Ok().StatusCode;
                        result.Message= "The book with "+ id + " ID is updated successfully.";
                        logger.createLog("Success on updating: "+ result.Message +"\tStatus Code: "+ result.HttpStatusCode);
                    
                    }
                    return result;
                }

                
                // Listedeki kitaplarla eslesen idli kitabi listeden silen delete metodu 
                [HttpDelete("{id}")]
                public Result DeleteBook(int id){
                  Result result = new Result();

                    if(!dBOperations.DeleteModel(id)){
                        result.HttpStatusCode = BadRequest().StatusCode;
                        result.Message= "The book with "+ id+ " ID does not exists.";
                        logger.createLog("Error while deleting: "+ result.Message +"\tStatus Code: "+ result.HttpStatusCode);
                        
                    }
                    else{
                        result.HttpStatusCode = Ok().StatusCode;
                        result.Message= "The book with "+ id + " ID is deleted successfully.";
                        logger.createLog("Success on deleting: "+ result.Message +"\tStatus Code: "+ result.HttpStatusCode);
                    }
                
                    return result;
                }

            }
}