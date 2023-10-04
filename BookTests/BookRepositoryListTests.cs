using Microsoft.VisualStudio.TestTools.UnitTesting;
using Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Book.Tests
{
    [TestClass]
    public class BookRepositoryListTests
    {

        private BookRepositoryList _repo;

        [TestInitialize]
        public void Init()
        {
            _repo = new BookRepositoryList();
            _repo.Add(new Book() { Title = "Harry Potter", Price = 200 });
            _repo.Add(new Book() { Title = "The Great Gatsby", Price = 100 });
            _repo.Add(new Book() { Title = "To Kill a Mockingbird", Price = 350 });
            _repo.Add(new Book() { Title = "1984", Price = 180 });
        }


        [TestMethod]
        public void GetTest()
        {
            IEnumerable<Book> books = _repo.Get();

            Assert.AreEqual(4, books.Count());
            Assert.AreEqual(books.First().Title, "Harry Potter");

            IEnumerable<Book> sortedBooks = _repo.Get(orderBy: "title");
            Assert.AreEqual(sortedBooks.First().Title, "1984");

            IEnumerable<Book> sortedBooks2 = _repo.Get(orderBy: "price");
            Assert.AreEqual(sortedBooks2.First().Title, "The Great Gatsby");
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.IsNotNull(_repo.GetById(1));
            Assert.IsNull(_repo.GetById(100));
        }

        [TestMethod()]
        public void AddTest()
        {
            Book b = new() { Title = "Mio min mio", Price = 123 };
            
            Assert.AreEqual(5, _repo.Add(b).Id);
            Assert.AreEqual(5, _repo.Get().Count());

            Book shortTitleBook = new() { Title = "ET", Price = 37 };
            Book nullTitleBook = new() { Title = null, Price = 500 };

            Book lowPriceBook = new() { Title = "Peter Plys", Price = -1 };
            Book highPriceBook = new() { Title = "Den lille prins", Price = 1201 };

            Assert.ThrowsException<ArgumentException>(() => _repo.Add(shortTitleBook));
            Assert.ThrowsException<ArgumentNullException>(() => _repo.Add(nullTitleBook));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Add(lowPriceBook));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Add(highPriceBook));
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Assert.IsNull(_repo.Delete(100));
            Assert.AreEqual(1, _repo.Delete(1)?.Id);
            Assert.AreEqual(3, _repo.Get().Count());
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.AreEqual(4, _repo.Get().Count());
            Book b = new() {Id = 1 , Title = "Et lille liv", Price = 650 };
            
            Assert.AreEqual(1, _repo.Update(1, b)?.Id);
            Assert.AreEqual(4, _repo.Get().Count());

            Book shortTitleBook = new() { Title = "ET", Price = 37 };
            Book nullTitleBook = new() { Title = null, Price = 500 };

            Book lowPriceBook = new() { Title = "Peter Plys", Price = -1 };
            Book highPriceBook = new() { Title = "Den lille prins", Price = 1201 };

            Assert.ThrowsException<ArgumentException>(() => _repo.Update(1, shortTitleBook));
            Assert.ThrowsException<ArgumentNullException>(() => _repo.Update(1, nullTitleBook));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Update(1, lowPriceBook));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Update(1, highPriceBook));
        }
    }
}