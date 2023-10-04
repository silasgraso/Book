using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book
{
    public class BookRepositoryList
    {
        private int _nextId = 1;
        private readonly List<Book> _books = new();

   
        public IEnumerable<Book> Get(int? priceBelow = null, string? orderBy = null)
        {
            IEnumerable<Book> copyBooks = new List<Book>(_books);
            if (priceBelow != null)
            {
                copyBooks = copyBooks.Where(b => b.Price < priceBelow).ToList();
            }
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "title":
                    case "title_asc":
                        copyBooks = copyBooks.OrderBy(b => b.Title);
                        break;
                    case "title_dec":
                        copyBooks = copyBooks.OrderByDescending(b => b.Title);
                        break;
                    case "price":
                    case "price_asc":
                        copyBooks = copyBooks.OrderBy(b => b.Price);
                        break;
                    case "price_dec":
                        copyBooks = copyBooks.OrderByDescending(b => b.Price);
                        break;
                    default:
                        break;
                }
            }
            return copyBooks;
        }

        public Book? GetById(int id)
        {
            return _books.Find(book => book.Id == id);
        }


        public Book Add(Book book)
        {
            book.Validate();
            book.Id = _nextId++;
            _books.Add(book);
            return book;
        }

        public Book Update(int id, Book bookUpdate)
        {
            bookUpdate.Validate();
            if (GetById(id) != null)
            {
                Book book = GetById(id);
                book.Title = bookUpdate.Title;
                book.Price = bookUpdate.Price;
            }
            return bookUpdate;
        }

        public Book Delete(int id)
        {
            Book book = GetById(id);
            if (book != null)
            {
                _books.Remove(book);
            }
            return book;
        }


    }
}
