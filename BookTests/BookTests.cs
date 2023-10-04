using Microsoft.VisualStudio.TestTools.UnitTesting;
using Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Tests
{
    [TestClass()]
    public class BookTests
    {
        Book bookOne= new Book() { Id = 1, Title = "Harry Potter", Price = 1200 };
        Book bookTwo = new Book() { Id = 2, Title = "The Great Gatsby", Price = 300 };
        Book bookThree = new Book() { Id = 3, Title = "To Kill a Mockingbird", Price = 350 };
        Book bookFour = new Book() { Id = 4, Title = "1984", Price = 280 };
        Book bookFive = new Book() { Id = 5, Title = "The Catcher in the Rye", Price = 270 };
        
        [TestMethod()]
        public void ToStringTest()
        {
            string bookOneString = bookOne.ToString();
            Assert.AreEqual("1, Harry Potter, 1200", bookOneString);
        }

        [TestMethod()]
        public void ValidateTitleTest()
        {
            bookOne.ValidateTitle();
            
            Book bookTitleTestTwoChar = new Book() { Id = 6, Title = "Ch", Price = 500 };
            Assert.ThrowsException<ArgumentException>(() => bookTitleTestTwoChar.Validate());
            
            Book bookTitleTestNull = new Book() { Id = 7, Title = null, Price = 500 };
            Assert.ThrowsException<ArgumentNullException>(() => bookTitleTestNull.Validate());

        }

        [TestMethod()]
        public void ValidatePriceTest()
        {
            bookOne.ValidatePrice();

            Book bookPriceZero = new Book() { Id = 8, Title = "Mio min Mio", Price = 0 };
            Book bookPriceNegative = new Book() { Id = 9, Title = "Pipppi", Price = -1 };
            Book bookPriceToHigh = new Book() { Id = 9, Title = "Lord of the Rings", Price = 1201 };

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bookPriceNegative.ValidatePrice());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bookPriceToHigh.ValidatePrice());

        }

        [TestMethod()]
        public void ValidateTest()
        {
            bookOne.Validate();
            bookTwo.Validate();
            bookThree.Validate();
            bookFour.Validate();
            bookFive.Validate();
        }
    }
}