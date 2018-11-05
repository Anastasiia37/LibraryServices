// <copyright file="AuthorServiceTests.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

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
    /// Tests for Author Services
    /// </summary>
    [TestClass]
    public class AuthorServiceTests
    {
        /// <summary>
        /// Tests the method GetAuthorById
        /// Correct input values
        /// Returned correct author
        /// </summary>
        /// <param name="id">The author identifier</param>
        /// <param name="name">The author name</param>
        [TestMethod]
        [DataRow(1, "TestAuthor")]
        public void GetAuthorById_CorrectId_ReturnedCorrectAuthor(int id, string name)
        {
            // Arrange
            Author expectedAuthor = new Author { Id = id, FullName = name };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Authors).Returns(new List<Author> { expectedAuthor });
            var libraryService = new AuthorService(mockDataProvider.Object);

            // Act
            Author actualAuthor = libraryService.GetById(id);

            // Assert
            Assert.AreEqual(expectedAuthor.FullName, actualAuthor.FullName);
        }

        /// <summary>
        /// Tests the method GetAuthorById
        /// Incorrect input value for id of author
        /// Returned null
        /// </summary>
        [TestMethod]
        public void GetAuthorById_IncorrectId_ReturnedNull()
        {
            // Arrange
            Author author = new Author { Id = 1, FullName = "Test Author" };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Authors).Returns(new List<Author> { author });
            var libraryService = new AuthorService(mockDataProvider.Object);
            int id = 100;

            // Act
            Author actualAuthor = libraryService.GetById(id);

            // Assert
            Assert.IsNull(actualAuthor);
        }

        /// <summary>
        /// Tests the method RemoveAuthor
        /// Correct input values
        /// Returned identifier of removed  author
        /// </summary>
        /// <param name="id">The identifier of author</param>
        /// <param name="name">The name of author</param>
        [TestMethod]
        [DataRow(1, "TestAuthor")]
        public void RemoveAuthor_CorrectInput_ReturnedIdOfRemovedAuthorAndDeleteAllBookAuthorPairs
            (int id, string name)
        {
            // Arrange
            int? expectedId = id;
            Author authorForRemoving = new Author { Id = id, FullName = name };
            List<BookAuthorPair> pairBookAuthor = new List<BookAuthorPair>()
            {
                new BookAuthorPair { Id = 1, BookId = 1, AuthorId = 2 },
                new BookAuthorPair { Id = 2, BookId = 1, AuthorId = 1 },
                new BookAuthorPair { Id = 3, BookId = 1, AuthorId = 1 },
                new BookAuthorPair { Id = 4, BookId = 2, AuthorId = 3 }
            };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Authors).Returns(new List<Author> { authorForRemoving });
            mockDataProvider.SetupGet(mock => mock.PairsBookAuthor).Returns(pairBookAuthor);
            var libraryService = new AuthorService(mockDataProvider.Object);

            // Act
            int? actualId = libraryService.Remove(id);

            // Assert
            mockDataProvider.Verify(mock => mock.RemoveAuthor(authorForRemoving), Times.Once());
            mockDataProvider.Verify(mock =>
                mock.RemoveBookAuthorPair(It.IsAny<BookAuthorPair>()), Times.Exactly(2));
            Assert.AreEqual(expectedId, actualId);
        }

        /// <summary>
        /// Tests the method RemoveAuthor
        /// Incorrect input value for author id
        /// Returned null
        /// </summary>
        [TestMethod]
        public void RemoveAuthor_NotExistedAuthorId_ReturnedNull()
        {
            // Arrange
            int authorIdForDelete = 100;
            Author author = new Author { Id = 1, FullName = "Test Author" };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Authors).Returns(new List<Author> { author });
            var libraryService = new AuthorService(mockDataProvider.Object);

            // Act
            int? actualId = libraryService.Remove(authorIdForDelete);

            // Assert
            Assert.IsNull(actualId);
        }

        /// <summary>
        /// Tests the method AddAuthor
        /// Correct input values
        /// Returned correct author
        /// </summary>
        /// <param name="name">The name of author</param>
        [TestMethod]
        [DataRow("Test Author")]
        public void AddAuthor_CorrectInput_ReturnedCorrectAuthor(string name)
        {
            // Arrange
            Author authorForAdding = new Author { Id = 2, FullName = "Test Author" };
            Author author = new Author { Id = 1, FullName = "TestAuthor2" };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Authors).Returns(new List<Author> { author });
            var libraryService = new AuthorService(mockDataProvider.Object);

            // Act
            int idOfAddedAuthor = libraryService.Add(authorForAdding);

            // Assert
            mockDataProvider.Verify(mock => mock.AddAuthor(authorForAdding), Times.Once());
            Assert.AreEqual(authorForAdding.Id, idOfAddedAuthor);
        }

        /// <summary>
        /// Tests the method UpdateAuthor
        /// Correct input values
        /// Returned correct author
        /// </summary>
        /// <param name="authorIdForUpdating">The identifier of author for updating</param>
        /// <param name="name">The name of author</param>
        [TestMethod]
        [DataRow(1, "TestAuthor")]
        public void UpdateAuthor_CorrectInput_ReturnedCorrectAuthor(int authorIdForUpdating, string name)
        {
            // Arrange
            Author author = new Author { Id = 1, FullName = "TestAuthorUpdate" };
            Author authorForUpdating = new Author { Id = 2, FullName = name };
            Author expectedAuthorAfterUpdating = new Author { Id = 3, FullName = name };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Authors).Returns(new List<Author> { author });
            var libraryService = new AuthorService(mockDataProvider.Object);

            // Act
            int? idOfUpdatedAuthor = libraryService.Update(authorIdForUpdating, authorForUpdating);
            Author actualUpdatedAuthor =
                libraryService.ListOfAll.FirstOrDefault(a => a.Id == idOfUpdatedAuthor);

            // Assert
            Assert.AreEqual(expectedAuthorAfterUpdating.FullName, actualUpdatedAuthor.FullName);
            Assert.AreEqual(authorIdForUpdating, idOfUpdatedAuthor);
        }

        /// <summary>
        /// Tests the method UpdateAuthor
        /// Incorrect input values for author id
        /// Returned Null
        /// </summary>
        /// <param name="authorIdForUpdating">The identifier of author for updating</param>
        /// <param name="name">The name of author</param>
        [TestMethod]
        [DataRow(100, "TestAuthor")] // Incorrect author id
        public void UpdateAuthor_IncorrectInput_ReturnedNull(int authorIdForUpdating, string name)
        {
            // Arrange
            Author author = new Author { Id = 1, FullName = "Test Author2" };
            var mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.SetupGet(mock => mock.Authors).Returns(new List<Author> { author });
            var libraryService = new AuthorService(mockDataProvider.Object);
            Author authorForUpdating = new Author { Id = 2, FullName = name };
            Author expectedAuthorAfterUpdating = new Author { Id = 3, FullName = name };

            // Act
            int? idOfUpdatedAuthor = libraryService.Update(authorIdForUpdating, authorForUpdating);
            Author actualUpdatedAuthor = libraryService.ListOfAll.FirstOrDefault(a => a.Id == idOfUpdatedAuthor);

            // Assert
            Assert.IsNull(idOfUpdatedAuthor);
        }
    }
}