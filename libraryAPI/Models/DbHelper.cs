using libraryAPI.EfCore;
namespace libraryAPI.Models;
public class DbHelper
{
    private EF_DataContext _context;
    public DbHelper(EF_DataContext context)
    {
        _context = context;
    }
    public List<RelationModel> GetEntries()
    {
        //GET
        List<RelationModel> response = new List<RelationModel>();
        List<Relation> dataList = _context.Relations.ToList();
        dataList.ForEach(row=>response.Add(new RelationModel()
        {
            book_name = row.book_name,
            book_date_of_first_publication  = row.book_date_of_first_publication ,
            author_name = row.author_name,
            author_date_of_birth = row.author_date_of_birth
        }));
        return response;
    }
    public void PostEntry(RelationModel relationModel)
    {
        //POST
        // Relation dbTable = new Relation();
        var dbTable = _context.Relations.Where(d => d.book_name==relationModel.book_name
                                                &&d.book_date_of_first_publication==relationModel.book_date_of_first_publication
                                                &&d.author_name==relationModel.author_name
                                                &&d.author_date_of_birth==relationModel.author_date_of_birth).FirstOrDefault();
        if(dbTable==null)
        {
            var book = _context.Books.Where(d=>d.name==relationModel.book_name
                                                    &&d.date_of_first_publication==relationModel.book_date_of_first_publication).FirstOrDefault();
            if(book==null){
                book = new Book();
                book.name=relationModel.book_name;
                book.date_of_first_publication=relationModel.book_date_of_first_publication;
                _context.Books.Add(book);
            }
            var author = _context.Authors.Where(d=>d.name==relationModel.author_name
                                                    &&d.date_of_birth==relationModel.author_date_of_birth).FirstOrDefault();
            if(author==null)
            {
                author = new Author();
                author.name = relationModel.author_name;
                author.date_of_birth = relationModel.author_date_of_birth;
                _context.Authors.Add(author);
            }
            dbTable = new Relation();
            dbTable.book_name=relationModel.book_name;
            dbTable.book_date_of_first_publication=relationModel.book_date_of_first_publication;
            dbTable.author_name=relationModel.author_name;
            dbTable.author_date_of_birth=relationModel.author_date_of_birth;
            _context.Relations.Add(dbTable);
        }
        _context.SaveChanges();
    }
    public void DeleteEntry(RelationModel relationModel)
    {
        //DELETE
        var entry = _context.Relations.Where(d => d.book_name==relationModel.book_name
                                                &&d.book_date_of_first_publication==relationModel.book_date_of_first_publication
                                                &&d.author_name==relationModel.author_name
                                                &&d.author_date_of_birth==relationModel.author_date_of_birth).FirstOrDefault();
        if(entry!=null)
        {
            _context.Relations.Remove(entry);
            _context.SaveChanges();
            var book = _context.Relations.Where(row=>row.book_name==relationModel.book_name
                                                    &&row.book_date_of_first_publication==relationModel.book_date_of_first_publication).FirstOrDefault();
            if(book==null){
                var deleteBook = _context.Books.Where(row=>row.name==relationModel.book_name
                                                            &&row.date_of_first_publication==relationModel.book_date_of_first_publication).FirstOrDefault();
                if(deleteBook!=null) _context.Books.Remove(deleteBook);
            }
            _context.SaveChanges();
            var author = _context.Relations.Where(row=>row.author_name==relationModel.author_name
                                                    &&row.author_date_of_birth==relationModel.author_date_of_birth).FirstOrDefault();
            if(author==null){
                var deleteAuthor = _context.Authors.Where(row=>row.name==relationModel.author_name
                                                            &&row.date_of_birth==relationModel.author_date_of_birth).FirstOrDefault();
                if(deleteAuthor!=null) _context.Authors.Remove(deleteAuthor);
            }
            _context.SaveChanges();
        }
    }
    public List<BookModel> GetBooks()
    {
        //GET
        List<BookModel> response = new List<BookModel>();
        List<Book> dataList = _context.Books.ToList();
        List<Relation> relationList = _context.Relations.ToList();
        dataList.ForEach(row=>response.Add(new BookModel()
        {
            name = row.name,
            date_of_first_publication = row.date_of_first_publication,
            edition = row.edition,
            publisher = row.publisher,
            original_language = row.original_language
        }));
        foreach(BookModel bookModel in response){
            List<Relation> authorsList = relationList.Where(row=>row.book_name==bookModel.name
                                                                &&row.book_date_of_first_publication==bookModel.date_of_first_publication).ToList();
            foreach(Relation relation in authorsList){
                bookModel.authors.Add(new AuthorId(){
                    name=relation.author_name,
                    date_of_birth=relation.author_date_of_birth
                });
            }
        }
        return response;
    }
    public List<BookModel> SearchBook(BookModel bookModel)
    {
        //GET
        List<BookModel> response = new List<BookModel>();
        List<Book> dataList = _context.Books.Where(row=>row.name==bookModel.name).ToList();
        List<Relation> relationList = _context.Relations.Where(row=>row.book_name==bookModel.name).ToList();
        dataList.ForEach(row=>response.Add(new BookModel()
        {
            name = row.name,
            date_of_first_publication = row.date_of_first_publication,
            edition = row.edition,
            publisher = row.publisher,
            original_language = row.original_language
        }));
        foreach(BookModel book in response){
            List<Relation> authorsList = relationList.Where(row=>row.book_name==book.name
                                                                &&row.book_date_of_first_publication==book.date_of_first_publication).ToList();
            foreach(Relation relation in authorsList){
                book.authors.Add(new AuthorId(){
                    name=relation.author_name,
                    date_of_birth=relation.author_date_of_birth
                });
            }
        }
        return response;
    }
    public void PostBook(BookModel bookModel)
    {
        var book = _context.Books.Where(row=>row.name==bookModel.name
                                            &&row.date_of_first_publication==bookModel.date_of_first_publication).FirstOrDefault();
        if(book==null){
            //POST
            book = new Book();
            book.name = bookModel.name;
            book.date_of_first_publication = bookModel.date_of_first_publication;
            book.edition = bookModel.edition;
            book.publisher = bookModel.publisher;
            book.original_language = bookModel.original_language;
            _context.Books.Add(book);
            _context.SaveChanges();
            foreach(AuthorId author in bookModel.authors)
            {
                var addAuthor = _context.Authors.Where(row=>row.name==author.name&&row.date_of_birth==author.date_of_birth).FirstOrDefault();
                if(addAuthor==null){
                    addAuthor = new Author(){name=author.name, date_of_birth=author.date_of_birth};
                    _context.Authors.Add(addAuthor);
                    _context.SaveChanges();
                }
                var relation = _context.Relations.Where(row=>row.book_name==bookModel.name&&row.book_date_of_first_publication==bookModel.date_of_first_publication&&row.author_name==author.name&&row.author_date_of_birth==author.date_of_birth).FirstOrDefault();
                if(relation==null){
                    relation = new Relation(){
                        book_name=bookModel.name,
                        book_date_of_first_publication=bookModel.date_of_first_publication,
                        author_name=author.name,
                        author_date_of_birth=author.date_of_birth
                    };
                    _context.Relations.Add(relation);
                    _context.SaveChanges();
                }
            }
        }
        else{
            //PUT
            book.edition = bookModel.edition;
            book.publisher = bookModel.publisher;
            book.original_language = bookModel.original_language;
            _context.SaveChanges();
            foreach(AuthorId author in bookModel.authors)
            {
                var addAuthor = _context.Authors.Where(row=>row.name==author.name&&row.date_of_birth==author.date_of_birth).FirstOrDefault();
                if(addAuthor==null){
                    addAuthor = new Author(){name=author.name, date_of_birth=author.date_of_birth};
                    _context.Authors.Add(addAuthor);
                    _context.SaveChanges();
                }
                var relation = _context.Relations.Where(row=>row.book_name==bookModel.name&&row.book_date_of_first_publication==bookModel.date_of_first_publication&&row.author_name==author.name&&row.author_date_of_birth==author.date_of_birth).FirstOrDefault();
                if(relation==null){
                    relation = new Relation(){
                        book_name=bookModel.name,
                        book_date_of_first_publication=bookModel.date_of_first_publication,
                        author_name=author.name,
                        author_date_of_birth=author.date_of_birth
                    };
                    _context.Relations.Add(relation);
                    _context.SaveChanges();
                }
            }
        }
    }
    public void DeleteBook(BookModel bookModel)
    {
        var book = _context.Books.Where(row=>row.name==bookModel.name&&row.date_of_first_publication==bookModel.date_of_first_publication).FirstOrDefault();
        if(book!=null){
            _context.Remove(book);
            _context.SaveChanges();
        }
    }
    public List<AuthorModel> GetAuthors()
    {
        //GET
        List<AuthorModel> response = new List<AuthorModel>();
        List<Author> dataList = _context.Authors.ToList();
        List<Relation> relationList = _context.Relations.ToList();
        dataList.ForEach(row=>response.Add(new AuthorModel()
        {
            name = row.name,
            date_of_birth = row.date_of_birth,
            country = row.country,
        }));
        foreach(AuthorModel authorModel in response){
            List<Relation> booksList = relationList.Where(row=>row.author_name==authorModel.name
                                                    &&row.author_date_of_birth==authorModel.date_of_birth).ToList();
            foreach(Relation relation in booksList){
                authorModel.books.Add(new BookId(){
                    name=relation.book_name,
                    date_of_first_publication=relation.book_date_of_first_publication
                });
            }
        }
        return response;
    }
    public List<AuthorModel> SearchAuthor(AuthorModel authorModel)
    {
        //GET
        List<AuthorModel> response = new List<AuthorModel>();
        List<Author> dataList = _context.Authors.Where(row=>row.name==authorModel.name).ToList();
        List<Relation> relationList = _context.Relations.Where(row=>row.author_name==authorModel.name).ToList();
        dataList.ForEach(row=>response.Add(new AuthorModel()
        {
            name = row.name,
            date_of_birth = row.date_of_birth,
            country = row.country,
        }));
        foreach(AuthorModel author in response){
            List<Relation> booksList = relationList.Where(row=>row.author_name==author.name
                                                    &&row.author_date_of_birth==author.date_of_birth).ToList();
            foreach(Relation relation in booksList){
                author.books.Add(new BookId(){
                    name=relation.book_name,
                    date_of_first_publication=relation.book_date_of_first_publication
                });
            }
        }
        return response;
    }
    public void PostAuthor(AuthorModel authorModel)
    {
        var author = _context.Authors.Where(row=>row.name==authorModel.name
                                            &&row.date_of_birth==authorModel.date_of_birth).FirstOrDefault();
        if(author==null){
            //POST
            author = new Author();
            author.name = authorModel.name;
            author.date_of_birth = authorModel.date_of_birth;
            author.country = authorModel.country;
            _context.Authors.Add(author);
            _context.SaveChanges();
            foreach(BookId book in authorModel.books)
            {
                var addBook = _context.Books.Where(row=>row.name==book.name&&row.date_of_first_publication==book.date_of_first_publication).FirstOrDefault();
                if(addBook==null){
                    addBook = new Book(){name=book.name, date_of_first_publication=book.date_of_first_publication};
                    _context.Books.Add(addBook);
                    _context.SaveChanges();
                }
                var relation = _context.Relations.Where(row=>row.book_name==book.name&&row.book_date_of_first_publication==book.date_of_first_publication&&row.author_name==authorModel.name&&row.author_date_of_birth==authorModel.date_of_birth).FirstOrDefault();
                if(relation==null){
                    relation = new Relation(){
                        book_name=book.name,
                        book_date_of_first_publication=book.date_of_first_publication,
                        author_name=authorModel.name,
                        author_date_of_birth=authorModel.date_of_birth
                    };
                    _context.Relations.Add(relation);
                    _context.SaveChanges();
                }
            }
        }
        else{
            //PUT
            author.country=authorModel.country;
            foreach(BookId book in authorModel.books)
            {
                var addBook = _context.Books.Where(row=>row.name==book.name&&row.date_of_first_publication==book.date_of_first_publication).FirstOrDefault();
                if(addBook==null){
                    addBook = new Book(){name=book.name, date_of_first_publication=book.date_of_first_publication};
                    _context.Books.Add(addBook);
                    _context.SaveChanges();
                }
                var relation = _context.Relations.Where(row=>row.book_name==book.name&&row.book_date_of_first_publication==book.date_of_first_publication&&row.author_name==authorModel.name&&row.author_date_of_birth==authorModel.date_of_birth).FirstOrDefault();
                if(relation==null){
                    relation = new Relation(){
                        book_name=book.name,
                        book_date_of_first_publication=book.date_of_first_publication,
                        author_name=authorModel.name,
                        author_date_of_birth=authorModel.date_of_birth
                    };
                    _context.Relations.Add(relation);
                    _context.SaveChanges();
                }
            }
        }
    }
    public void DeleteAuthor(AuthorModel authorModel)
    {
        var author = _context.Authors.Where(row=>row.name==authorModel.name&&row.date_of_birth==authorModel.date_of_birth).FirstOrDefault();
        if(author!=null){
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}