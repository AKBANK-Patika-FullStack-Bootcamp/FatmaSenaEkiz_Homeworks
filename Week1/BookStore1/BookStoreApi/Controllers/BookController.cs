using Microsoft.AspNetCore.Mvc;
namespace WebApiBookStore.AddControllers{
    
    [ApiController]
    [Route("[controller]s")]
            public class BookController : ControllerBase
            {
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
                    return bookList;
                } 

                // belirli id ile eslesen nesnelerini donen http get metodu
                [HttpGet("{id}")]
                public Book GetBooksByID(int id ){
                    var book = BookList.Where(book=> book.Id==id).SingleOrDefault();
                    return book;
                } 

                // Listedeki kitaplarla eslesen idli kitaba ait bilgileri guncelleyen put metodu 
                [HttpPut("{id}")]
                public IActionResult UpdateBook(int id,[FromBody] Book updatedBook){
                    var book = BookList.SingleOrDefault(x=> x.Id == id);
                    if(book == null)
                        return BadRequest();
                    book.Title = updatedBook.Title  != default ? updatedBook.Title : book.Title;
                    book.GenreId = updatedBook.GenreId  != default ? updatedBook.GenreId : book.GenreId;
                    book.PageCount = updatedBook.PageCount  != default ? updatedBook.PageCount : book.PageCount;
                    book.PublishDate = updatedBook.PublishDate  != default ? updatedBook.PublishDate : book.PublishDate;
                    return Ok();
                }

                
                // Listedeki kitaplarla eslesen idli kitabi listeden silen delete metodu 
                [HttpDelete("{id}")]
                public IActionResult DeleteBook(int id){
                    var book = BookList.SingleOrDefault(x=> x.Id == id);
                    if(book == null)
                        return BadRequest();
                    BookList.Remove(book);
                    return Ok();   

                }


                //Listeye yeni kitap ekleyen post metodu
                [HttpPost]
                public IActionResult AddBook([FromBodyAttribute] Book newBook){
                    var book = BookList.SingleOrDefault(x=> x.Title == newBook.Title);
                    if(book != null){
                        return BadRequest();
                    }
                    BookList.Add(newBook);
                    return Ok();
                }


            }
}