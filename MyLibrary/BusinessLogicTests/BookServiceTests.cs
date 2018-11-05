// <copyright file="BookServiceTests.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.DataProvider;
using BusinessLogic.LibraryModel;
using BusinessLogic.LibraryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogicTests
{
    /// <summary>
    /// Tests for Library Services
    /// </summary>
    [TestClass]
    public class BookServiceTests
    {
        /// <summary>
        /// Tests the method GetBookById
        /// Correct input values
        /// Returned correct book
        /// </summary>
        /// <param name="id">The book identifier</param>
        /// <param name="name">The book name</param>
        /// <param name="year">The publishing year</param>
        [TestMethod]
        [DataRow(1, "TestBook", 1955)]
        public void GetBookById_CorrectId_ReturnedCorrectBook(int id, string name, int year)
        {
            // Arrange
            Book expectedBook = new Book { Id = id, Name = name, Year = year };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Books).Returns(new List<Book> { expectedBook });
            var libraryService = new BookService(mockDataProvider.Object);

            // Act
            Book actualBook = libraryService.GetById(id);

            // Assert
            Assert.AreEqual(expectedBook.Name, actualBook.Name);
            Assert.AreEqual(expectedBook.Year, actualBook.Year);
        }

        /// <summary>
        /// Tests the method GetBookById
        /// Incorrect input value for id of book
        /// Returned null
        /// </summary>
        [TestMethod]
        public void GetBookById_IncorrectId_ReturnedNull()
        {
            // Arrange
            Book book = new Book { Id = 1, Name = "Test book", Year = 1998 };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Books).Returns(new List<Book> { book });
            var libraryService = new BookService(mockDataProvider.Object);
            int id = 100;

            // Act
            Book actualBook = libraryService.GetById(id);

            // Assert
            Assert.IsNull(actualBook);
        }

        /// <summary>
        /// Tests the method RemoveBook
        /// Correct input values
        /// Returned identifier of removed book
        /// </summary>
        /// <param name="id">The identifier of book</param>
        /// <param name="name">The name of book</param>
        /// <param name="year">The publishing year</param>
        [TestMethod]
        [DataRow(1, "TestBook", 1955)]
        public void RemoveBook_CorrectInput_ReturnedIdOfRemovedBookAndDeleteAllBookAuthorPairsAndBookGenresPairsConnectedToThisBook
            (int id, string name, int year)
        {
            // Arrange
            int? expectedId = id;
            Book book = new Book { Id = 1, Name = name, Year = year };
            List<BookAuthorPair> pairBookAuthor = new List<BookAuthorPair>()
            {
                new BookAuthorPair { Id = 1, BookId = 1, AuthorId = 2 },
                new BookAuthorPair { Id = 2, BookId = 2, AuthorId = 3 }
            };
            List<BookGenrePair> pairBookGenre = new List<BookGenrePair>()
            {
                new BookGenrePair { Id = 1, BookId = 2, GenreId = 1 },
                new BookGenrePair { Id = 2, BookId = 1, GenreId = 3 },
                new BookGenrePair { Id = 3, BookId = 1, GenreId = 2 }
            };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Books).Returns(new List<Book> { book });
            mockDataProvider.SetupGet(mock => mock.PairsBookAuthor).Returns(pairBookAuthor);
            mockDataProvider.SetupGet(mock => mock.PairsBookGenre).Returns(pairBookGenre);
            var libraryService = new BookService(mockDataProvider.Object);

            // Act
            int? actualId = libraryService.Remove(id);

            // Assert
            mockDataProvider.Verify(mock => mock.RemoveBook(book), Times.Once());
            mockDataProvider.Verify(mock =>
                mock.RemoveBookAuthorPair(It.IsAny<BookAuthorPair>()), Times.Once());
            mockDataProvider.Verify(mock =>
                mock.RemoveBookGenrePair(It.IsAny<BookGenrePair>()), Times.Exactly(2));
            Assert.AreEqual(expectedId, actualId);
        }

        /// <summary>
        /// Tests the method RemoveBook
        /// Incorrect input value for book id
        /// Returned null
        /// </summary>
        [TestMethod]
        public void RemoveBook_NotExistedBookId_ReturnedNull()
        {
            // Arrange
            int bookIdForDelete = 100;
            Book book = new Book { Id = 1, Name = "Test Book", Year = 1256 };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(b => b.Books).Returns(new List<Book> { book });
            var libraryService = new BookService(mockDataProvider.Object);

            // Act
            int? actualId = libraryService.Remove(bookIdForDelete);

            // Assert
            Assert.IsNull(actualId);
        }

        /// <summary>
        /// Tests the method AddBook
        /// Correct input values
        /// Returned correct book
        /// </summary>
        /// <param name="name">The name of book</param>
        /// <param name="year">The publishing year</param>
        [TestMethod]
        public void AddBook_CorrectInput_ReturnedCorrectBook()
        {
            // Arrange
            Book book = new Book { Id = 1, Name = "Test Book", Year = 1256 };
            Book bookForAdding = new Book { Id = 2, Name = "Test Book 2", Year = 1960 };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Books).Returns(new List<Book> { book });
            var libraryService = new BookService(mockDataProvider.Object);

            // Act
            int idOfAddedBook = libraryService.Add(bookForAdding);

            // Assert
            mockDataProvider.Verify(mock => mock.AddBook(bookForAdding), Times.Once());
            Assert.AreEqual(bookForAdding.Id, idOfAddedBook);
        }

        /// <summary>
        /// Tests the method UpdateBook
        /// Correct input values
        /// Returned correct book
        /// </summary>
        /// <param name="bookIdForUpdating">The identifier of book for updating</param>
        /// <param name="name">The name of book</param>
        /// <param name="year">The publishing year</param>
        [TestMethod]
        [DataRow(1, "TestBook", 1960)]
        public void UpdateBook_CorrectInput_ReturnedCorrectBook(int bookIdForUpdating,
            string name, int year)
        {
            // Arrange
            Book book = new Book { Id = 1, Name = "Test Book", Year = 1256 };
            Book bookForUpdating = new Book { Id = 2, Name = name, Year = year };
            Book expectedBookAfterUpdating = new Book { Id = 3, Name = name, Year = year };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Books).Returns(new List<Book> { book });
            var libraryService = new BookService(mockDataProvider.Object);

            // Act
            int? idOfUpdatedBook = libraryService.Update(bookIdForUpdating, bookForUpdating);
            Book actualUpdatedBook = libraryService.ListOfAll.FirstOrDefault(b => b.Id == idOfUpdatedBook);

            // Assert
            Assert.AreEqual(expectedBookAfterUpdating.Name, actualUpdatedBook.Name);
            Assert.AreEqual(expectedBookAfterUpdating.Year, actualUpdatedBook.Year);
            Assert.AreEqual(bookIdForUpdating, idOfUpdatedBook);
        }

        /// <summary>
        /// Tests the method UpdateBook
        /// Incorrect input values for book id
        /// Returned Null
        /// </summary>
        /// <param name="bookIdForUpdating">The identifier of book for updating</param>
        /// <param name="name">The name of book</param>
        /// <param name="year">The publishing year</param>
        [TestMethod]
        [DataRow(100, "Test Book", 1960)] // Incorrect book id
        public void UpdateBook_IncorrectInput_ReturnedNull(int bookIdForUpdating,
            string name, int year)
        {
            // Arrange
            Book book = new Book { Id = 1, Name = "Test Book", Year = 1256 };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Books).Returns(new List<Book> { book });
            var libraryService = new BookService(mockDataProvider.Object);
            Book bookForUpdating = new Book { Id = 2, Name = name, Year = year };
            Book expectedBookAfterUpdating = new Book { Id = 3, Name = name, Year = year };

            // Act
            int? idOfUpdatedBook = libraryService.Update(bookIdForUpdating, bookForUpdating);
            Book actualUpdatedBook = libraryService.ListOfAll.FirstOrDefault(b => b.Id == idOfUpdatedBook);

            // Assert
            Assert.IsNull(idOfUpdatedBook);
        }

        /// <summary>
        /// Tests adding the author to book
        /// Correct input
        /// Returned identifier of adding pair
        /// </summary>
        [TestMethod]
        public void AddAuthorToBook_CorrectInput_ReturnedIdOfAddingPair()
        {
            // Arrange
            int bookId = 1;
            int authorId = 1;
            Book book = new Book { Id = 1, Name = "Test Book", Year = 1256 };
            Author author = new Author { Id = 1, FullName = "Test author" };
            List<BookAuthorPair> pairBookAuthor = new List<BookAuthorPair>()
            {
                new BookAuthorPair { Id = 1, BookId = 1, AuthorId = 2 }
            };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Books).Returns(new List<Book> { book });
            mockDataProvider.SetupGet(mock => mock.Authors).Returns(new List<Author> { author });
            mockDataProvider.SetupGet(mock => mock.PairsBookAuthor).Returns(pairBookAuthor);
            var libraryService = new BookService(mockDataProvider.Object);

            // Act
            int actualIdOfAddedPair = libraryService.AddAuthorToBook(bookId, authorId);

            // Assert
            mockDataProvider.Verify(mock => mock.AddBookAuthorPair(It.IsAny<BookAuthorPair>()), Times.Once());
        }

        /// <summary>
        /// Tests adding the author to book
        /// Incorrect input
        /// Expected exception
        /// </summary>
        /// <param name="bookId">The book identifier</param>
        /// <param name="authorId">The author identifier</param>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(2, 1)]
        [DataRow(1, 2)]
        public void AddAuthorToBook_IncorrectInput_ExpectedException(int bookId, int authorId)
        {
            // Arrange
            Book book = new Book { Id = 1, Name = "Test Book", Year = 1256 };
            Author author = new Author { Id = 1, FullName = "Test author" };
            List<BookAuthorPair> pairBookAuthor = new List<BookAuthorPair>()
            {
                new BookAuthorPair { Id = 1, BookId = 1, AuthorId = 2 }
            };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Books).Returns(new List<Book> { book });
            mockDataProvider.SetupGet(mock => mock.Authors).Returns(new List<Author> { author });
            mockDataProvider.SetupGet(mock => mock.PairsBookAuthor).Returns(pairBookAuthor);
            var libraryService = new BookService(mockDataProvider.Object);

            // Act
            int actualIdOfAddedPair = libraryService.AddAuthorToBook(bookId, authorId);

            // Assert
            mockDataProvider.Verify(mock => mock.AddBookAuthorPair(It.IsAny<BookAuthorPair>()), Times.Never());
        }

        /// <summary>
        /// Tests deleting the author to book
        /// Correct input
        /// Returned identifier of adding pair
        /// </summary>
        [TestMethod]
        public void RemoveAuthorFromBook_CorrectInput_ReturnedIdOfRemovingPair()
        {
            // Arrange
            int bookId = 1;
            int authorId = 1;
            Book book = new Book { Id = 1, Name = "Test Book", Year = 1256 };
            Author author = new Author { Id = 1, FullName = "Test author" };
            List<BookAuthorPair> pairBookAuthor = new List<BookAuthorPair>()
            {
                new BookAuthorPair { Id = 1, BookId = 1, AuthorId = 2 },
                new BookAuthorPair { Id = 2, BookId = 1, AuthorId = 1 },
                new BookAuthorPair { Id = 3, BookId = 2, AuthorId = 1 }
            };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Books).Returns(new List<Book> { book });
            mockDataProvider.SetupGet(mock => mock.Authors).Returns(new List<Author> { author });
            mockDataProvider.SetupGet(mock => mock.PairsBookAuthor).Returns(pairBookAuthor);
            var libraryService = new BookService(mockDataProvider.Object);
            int expectedIdOfRemovedPair = 2;

            // Act
            int? actualIdOfRemovedPair = libraryService.RemoveAuthorFromBook(bookId, authorId);

            // Assert
            Assert.AreEqual(expectedIdOfRemovedPair, actualIdOfRemovedPair);
            mockDataProvider.Verify(mock => mock.RemoveBookAuthorPair(It.IsAny<BookAuthorPair>()), Times.Once());
        }

        /// <summary>
        /// Tests deleting the author to book
        /// No matches
        /// Returned Null
        /// </summary>
        [TestMethod]
        public void RemoveAuthorFromBook_NoMatches_ReturnedNull()
        {
            // Arrange
            int bookId = 1;
            int authorId = 1;
            Book book = new Book { Id = 1, Name = "Test Book", Year = 1256 };
            Author author = new Author { Id = 1, FullName = "Test author" };
            List<BookAuthorPair> pairBookAuthor = new List<BookAuthorPair>()
            {
                new BookAuthorPair { Id = 1, BookId = 1, AuthorId = 2 },
                new BookAuthorPair { Id = 2, BookId = 2, AuthorId = 1 }
            };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Books).Returns(new List<Book> { book });
            mockDataProvider.SetupGet(mock => mock.Authors).Returns(new List<Author> { author });
            mockDataProvider.SetupGet(mock => mock.PairsBookAuthor).Returns(pairBookAuthor);
            var libraryService = new BookService(mockDataProvider.Object);

            // Act
            int? actualIdOfAddedPair = libraryService.RemoveAuthorFromBook(bookId, authorId);

            // Assert
            Assert.IsNull(actualIdOfAddedPair);
            mockDataProvider.Verify(mock => mock.RemoveBookAuthorPair(It.IsAny<BookAuthorPair>()), Times.Never());
        }

        /// <summary>
        /// Tests deleting the author to book
        /// Incorrect input for id
        /// Expected Exception
        /// </summary>
        /// <param name="bookId">The book identifier</param>
        /// <param name="authorId">The author identifier</param>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(2, 1)] // Incorrect book id
        [DataRow(1, 2)] // Incorrect author id
        public void RemoveAuthorFromBook_IncorrectInput_ExpectedException(int bookId, int authorId)
        {
            // Arrange
            Book book = new Book { Id = 1, Name = "Test Book", Year = 1256 };
            Author author = new Author { Id = 1, FullName = "Test author" };
            List<BookAuthorPair> pairBookAuthor = new List<BookAuthorPair>()
            {
                new BookAuthorPair { Id = 1, BookId = 2, AuthorId = 3 },
                new BookAuthorPair { Id = 2, BookId = 1, AuthorId = 3 }
            };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Books).Returns(new List<Book> { book });
            mockDataProvider.SetupGet(mock => mock.Authors).Returns(new List<Author> { author });
            mockDataProvider.SetupGet(mock => mock.PairsBookAuthor).Returns(pairBookAuthor);
            var libraryService = new BookService(mockDataProvider.Object);

            // Act
            int? actualIdOfAddedPair = libraryService.RemoveAuthorFromBook(bookId, authorId);

            // Assert
            Assert.IsNull(actualIdOfAddedPair);
            mockDataProvider.Verify(mock => mock.RemoveBookAuthorPair(It.IsAny<BookAuthorPair>()), Times.Never());
        }

        /// <summary>
        /// Gets the books by author
        /// Correct input
        /// Returned list of books
        /// </summary>
        [TestMethod]
        public void GetBooksByAuthor_CorrectInput_ReturnedListOfBooks()
        {
            // Arrange
            int authorId = 1;
            Book book = new Book { Id = 1, Name = "Test Book", Year = 1256 };
            Author author = new Author { Id = 1, FullName = "Test author" };
            List<BookAuthorPair> pairBookAuthor = new List<BookAuthorPair>()
            {
                new BookAuthorPair { Id = 1, BookId = 2, AuthorId = 3 },
                new BookAuthorPair { Id = 2, BookId = 1, AuthorId = 1 }
            };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Books).Returns(new List<Book> { book });
            mockDataProvider.SetupGet(mock => mock.Authors).Returns(new List<Author> { author });
            mockDataProvider.SetupGet(mock => mock.PairsBookAuthor).Returns(pairBookAuthor);
            var libraryService = new BookService(mockDataProvider.Object);
            int expectedCountOfBooks = 1;

            // Act
            List<Book> books = libraryService.GetBooksByAuthor(authorId).ToList();

            // Assert
            Assert.AreEqual(expectedCountOfBooks, books.Count);
            Assert.AreEqual(book.Name, books[0].Name);
            Assert.AreEqual(book.Year, books[0].Year);
        }

        /// <summary>
        /// Gets the books by author
        /// Incorrect input for author id
        /// Expected Exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBooksByAuthor_IncorrectInputForAuthorId_ExpectedException()
        {
            // Arrange
            int authorId = 4;
            Book book = new Book { Id = 1, Name = "Test Book", Year = 1256 };
            Author author = new Author { Id = 1, FullName = "Test author" };
            List<BookAuthorPair> pairBookAuthor = new List<BookAuthorPair>()
            {
                new BookAuthorPair { Id = 1, BookId = 2, AuthorId = 3 },
                new BookAuthorPair { Id = 2, BookId = 1, AuthorId = 1 }
            };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Books).Returns(new List<Book> { book });
            mockDataProvider.SetupGet(mock => mock.Authors).Returns(new List<Author> { author });
            mockDataProvider.SetupGet(mock => mock.PairsBookAuthor).Returns(pairBookAuthor);
            var libraryService = new BookService(mockDataProvider.Object);
            int expectedCountOfBooks = 1;

            // Act
            List<Book> books = libraryService.GetBooksByAuthor(authorId).ToList();

            // Assert
            Assert.AreEqual(expectedCountOfBooks, books.Count);
            Assert.AreEqual(book.Name, books[0].Name);
            Assert.AreEqual(book.Year, books[0].Year);
        }
    }
}