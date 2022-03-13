using Biblioteka.Models;
using Biblioteka.Models.Queries;
using System.Collections.Generic;

namespace Biblioteka.Services.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> GetBooks(BookQuery query);
        Book GetBook(int id);
        void DeleteBook(int id);
        void EditBook(Book book);
        void AddBook(BookQuery book);
    }
}
