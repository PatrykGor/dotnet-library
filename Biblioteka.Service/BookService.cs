using Biblioteka.Models;
using Biblioteka.Models.Queries;
using Biblioteka.Repository;
using Biblioteka.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Biblioteka.Services
{
    public class BookService : IBookService
    {
        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }
        private readonly IRepository<Book> _bookRepository;
        private Book bookQueryToBook(BookQuery bookQuery)
        {
            return new Book
            {
                Title = bookQuery.Title,
                Author = bookQuery.Author,
                PublishingDate = bookQuery.PublishingDate ?? DateTime.Now
            };
        }
        public void AddBook(BookQuery book)
        {
            _bookRepository.Insert(bookQueryToBook(book));
        }

        public void DeleteBook(int id)
        {
            Book toDelete = _bookRepository.Get(b => b.Id == id);
            if (toDelete == null)
                throw new ArgumentException(nameof(id));
            _bookRepository.Delete(toDelete);
        }

        public void EditBook(Book book)
        {
            _bookRepository.Update(book);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public Book GetBook(int id)
        {
            return _bookRepository.Get(b => b.Id == id);
        }

        public IEnumerable<Book> GetBooks(BookQuery query)
        {
            return _bookRepository.GetMultiple(b => b.Title == query.Title || b.Author == query.Author || b.PublishingDate == query.PublishingDate);
        }
    }
}
