// <copyright file="BookService.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.DataProvider;
using BusinessLogic.LibraryModel;

namespace BusinessLogic.LibraryService
{
    /// <summary>
    /// Book Services
    /// </summary>
    /// <seealso cref="BusinessLogic.LibraryService.LibraryService" />
    /// <seealso cref="BusinessLogic.LibraryService.IBookService" />
    public class BookService : IBookService
    {
        /// <summary>
        /// The data provider (database, inMemory, etc)
        /// </summary>
        private IDataProvider dataProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class
        /// </summary>
        /// <param name="dataProvider">The data provider</param>
        public BookService(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        #region BookServices

        /// <summary>
        /// Gets all books
        /// </summary>
        /// <returns>
        /// The list of books
        /// </returns>
        public IEnumerable<Book> ListOfAll
        {
            get
            {
                return this.dataProvider.Books;
            }
        }

        /// <summary>
        /// Gets the book by its identifier
        /// </summary>
        /// <param name="id">The identifier of the book</param>
        /// <returns>
        /// Found book or null if there isn`t book with specified id</returns>
        public Book GetById(int id)
        {
            return this.dataProvider.Books.FirstOrDefault(book => book.Id == id);
        }

        /// <summary>
        /// Adds the book to the list of books
        /// </summary>
        /// <param name="book">The book</param>
        /// <returns>The id of the added book</returns>
        /// <exception cref="ArgumentException">The book with such id already exists!</exception>
        public int Add(Book book)
        {
            if (this.dataProvider.Books.Any(b => b.Id == book.Id))
            {
                throw new ArgumentException("The book with such id already exists!");
            }

            this.dataProvider.AddBook(book);
            this.dataProvider.Save();
            return book.Id;
        }

        /// <summary>
        /// Updates the book`s information
        /// </summary>
        /// <param name="bookId">The author identifier</param>
        /// <param name="book">The book`s information</param>
        /// <returns>
        /// The id of the updated book or null if there isn`t any book with specified id
        /// </returns>
        public int? Update(int bookId, Book book)
        {
            Book bookForUpdate = this.dataProvider.Books.FirstOrDefault(b => b.Id == bookId);
            if (bookForUpdate == null)
            {
                return null;
            }

            bookForUpdate.Name = book.Name;
            bookForUpdate.Year = book.Year;
            this.dataProvider.Save();
            return bookForUpdate.Id;
        }

        /// <summary>
        /// Removes the book from the library,
        /// and all records in pairsBookAuthor and pairsBookGenre with such book id
        /// </summary>
        /// <param name="id">The identifier of the book</param>
        /// <returns>
        /// The id of removed book or null if there isn`t book with such id
        /// </returns>
        public int? Remove(int id)
        {
            Book bookForDelete = this.dataProvider.Books.FirstOrDefault(book => book.Id == id);
            if (bookForDelete == null)
            {
                return null;
            }

            foreach (var pair in this.dataProvider.PairsBookAuthor.Where(pair => pair.BookId == id))
            {
                this.dataProvider.RemoveBookAuthorPair(pair);
                this.dataProvider.Save();
            }

            foreach (var pair in this.dataProvider.PairsBookGenre.Where(pair => pair.BookId == id))
            {
                this.dataProvider.RemoveBookGenrePair(pair);
                this.dataProvider.Save();
            }

            int idOfDeletedBook = bookForDelete.Id;
            this.dataProvider.RemoveBook(bookForDelete);
            this.dataProvider.Save();
            return idOfDeletedBook;
        }

        #endregion BookServices

        #region BookAuthorPairService

        /// <summary>
        /// Adds the author to book
        /// </summary>
        /// <param name="bookId">The book identifier</param>
        /// <param name="authorId">The author identifier</param>
        /// <returns>
        /// The id of new record in pairsBookAuthor
        /// </returns>
        /// <exception cref="ArgumentException">Can`t find author with such id!
        /// or
        /// Can`t find book with such id!</exception>
        public int AddAuthorToBook(int bookId, int authorId)
        {
            if (!this.dataProvider.Authors.Any(a => a.Id == authorId))
            {
                throw new ArgumentException("Can`t find author with such id!");
            }

            if (!this.dataProvider.Books.Any(b => b.Id == bookId))
            {
                throw new ArgumentException("Can`t find book with such id!");
            }

            if (this.dataProvider.PairsBookAuthor.Any(b => b.BookId == bookId && b.AuthorId == authorId))
            {
                throw new ArgumentException("Such book has such author!");
            }

            BookAuthorPair pairForAdding = new BookAuthorPair { BookId = bookId, AuthorId = authorId };
            this.dataProvider.AddBookAuthorPair(pairForAdding);
            this.dataProvider.Save();
            return pairForAdding.Id;
        }

        /// <summary>
        /// Removes the author from book
        /// </summary>
        /// <param name="bookId">The book identifier</param>
        /// <param name="authorId">The author identifier</param>
        /// <returns>
        /// The id of removed record in Book-Author pair
        /// or null if there are any pair of specified book and author
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Can`t find author with such id!
        /// or
        /// Can`t find book with such id!
        /// </exception>
        public int? RemoveAuthorFromBook(int bookId, int authorId)
        {
            if (!this.dataProvider.Authors.Any(a => a.Id == authorId))
            {
                throw new ArgumentException("Can`t find author with such id!");
            }

            if (!this.dataProvider.Books.Any(b => b.Id == bookId))
            {
                throw new ArgumentException("Can`t find book with such id!");
            }

            BookAuthorPair pairForRemoving = this.dataProvider.PairsBookAuthor.FirstOrDefault(
                pairBookAuthor => pairBookAuthor.AuthorId == authorId
                && pairBookAuthor.BookId == bookId);
            if (pairForRemoving == null)
            {
                return null;
            }

            int removedPairId = pairForRemoving.Id;
            this.dataProvider.RemoveBookAuthorPair(pairForRemoving);
            this.dataProvider.Save();
            return removedPairId;
        }

        /// <summary>
        /// Get all books by specified author
        /// </summary>
        /// <param name="authorId">The author identifier</param>
        /// <returns>
        /// Enumeration of books
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Can`t find author with such id!
        /// </exception>
        public IEnumerable<Book> GetBooksByAuthor(int authorId)
        {
            if (!this.dataProvider.Authors.Any(a => a.Id == authorId))
            {
                throw new ArgumentException("Can`t find author with such id!");
            }

            var foundBooks = this.dataProvider.PairsBookAuthor.Where(entry => entry.AuthorId == authorId).
                Select(entry => this.GetById(entry.BookId));
            return foundBooks;
        }

        #endregion BookAuthorPairService

        #region BookGenrePairService

        /// <summary>
        /// Adds the genre to book
        /// </summary>
        /// <param name="bookId">The book identifier</param>
        /// <param name="genreId">The genre identifier</param>
        /// <returns>
        /// The id of new record in pairsBookGenre
        /// </returns>
        /// <exception cref="ArgumentException">Can`t find genre with such id!
        /// or
        /// Can`t find book with such id!</exception>
        public int AddGenreToBook(int bookId, int genreId)
        {
            if (!this.dataProvider.Genres.Any(g => g.Id == genreId))
            {
                throw new ArgumentException("Can`t find genre with such id!");
            }

            if (!this.dataProvider.Books.Any(b => b.Id == bookId))
            {
                throw new ArgumentException("Can`t find book with such id!");
            }

            if (this.dataProvider.PairsBookGenre.Any(p => p.BookId == bookId && p.GenreId == genreId))
            {
                throw new ArgumentException("Such book has such genre!!");
            }

            BookGenrePair pairForAdding = new BookGenrePair { BookId = bookId, GenreId = genreId };
            this.dataProvider.AddBookGenrePair(pairForAdding);
            this.dataProvider.Save();
            return pairForAdding.Id;
        }

        /// <summary>
        /// Removes the genre from book
        /// </summary>
        /// <param name="bookId">The book identifier</param>
        /// <param name="genreId">The genre identifier</param>
        /// <returns>
        /// The id of removed record in Book-Genre pair
        /// or null if there are any pair of specified book and genre
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Can`t find genre with such id!
        /// or
        /// Can`t find book with such id!
        /// </exception>
        public int? RemoveGenreFromBook(int bookId, int genreId)
        {
            if (!this.dataProvider.Genres.Any(g => g.Id == genreId))
            {
                throw new ArgumentException("Can`t find genre with such id!");
            }

            if (!this.dataProvider.Books.Any(b => b.Id == bookId))
            {
                throw new ArgumentException("Can`t find book with such id!");
            }

            BookGenrePair pairForRemoving = this.dataProvider.PairsBookGenre.FirstOrDefault(
                pairBookGenre => pairBookGenre.GenreId == genreId
                && pairBookGenre.BookId == bookId);
            if (pairForRemoving == null)
            {
                return null;
            }

            int removedPairId = pairForRemoving.Id;
            this.dataProvider.RemoveBookGenrePair(pairForRemoving);
            this.dataProvider.Save();
            return removedPairId;
        }

        /// <summary>
        /// Get all books by specified genre
        /// </summary>
        /// <param name="genreId">The genre identifier</param>
        /// <returns>
        /// Enumeration of books
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Can`t find genre with such id!
        /// </exception>
        public IEnumerable<Book> GetBooksByGenre(int genreId)
        {
            if (!this.dataProvider.Genres.Any(g => g.Id == genreId))
            {
                throw new ArgumentException("Can`t find genre with such id!");
            }

            var foundBooks = this.dataProvider.PairsBookGenre.Where(entry => entry.GenreId == genreId).
                Select(entry => this.GetById(entry.BookId));
            return foundBooks;
        }

        #endregion BookGenrePairService
    }
}