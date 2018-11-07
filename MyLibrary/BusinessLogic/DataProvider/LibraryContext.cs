// <copyright file="LibraryContext.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using BusinessLogic.LibraryModel;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.DataProvider
{
    /// <summary>
    /// Class for using database
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// <seealso cref="BusinessLogic.DataProvider.IDataProvider" />
    public class LibraryContext : DbContext, IDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryContext"/> class
        /// </summary>
        /// <param name="options">The options</param>
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets the books
        /// </summary>
        /// <value>
        /// The books
        /// </value>
        public List<Book> Books
        {
            get
            {
                return this.books.ToList();
            }
        }

        /// <summary>
        /// Gets the genres
        /// </summary>
        /// <value>
        /// The genres
        /// </value>
        public List<Genre> Genres
        {
            get
            {
                return this.genres.ToList();
            }
        }

        /// <summary>
        /// Gets the pair of book and its genre
        /// </summary>
        /// <value>
        /// The pair of book and its genre
        /// </value>
        public List<BookGenrePair> PairsBookGenre
        {
            get
            {
                return this.bookGenrePairs.ToList();
            }
        }

        /// <summary>
        /// Gets the pair of book and its author
        /// </summary>
        /// <value>
        /// The pair of book and its author
        /// </value>
        public List<BookAuthorPair> PairsBookAuthor
        {
            get
            {
                return this.bookAuthorPairs.ToList();
            }
        }

        /// <summary>
        /// Gets the authors
        /// </summary>
        /// <value>
        /// The authors
        /// </value>
        public List<Author> Authors
        {
            get
            {
                return this.authors.ToList();
            }
        }

        /// <summary>
        /// Gets or sets the books from database
        /// </summary>
        /// <value>
        /// The books
        /// </value>
        private DbSet<Book> books
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the authors from database
        /// </summary>
        /// <value>
        /// The authors
        /// </value>
        private DbSet<Author> authors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the genres from database
        /// </summary>
        /// <value>
        /// The genres
        /// </value>
        private DbSet<Genre> genres
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the book-author pairs from database
        /// </summary>
        /// <value>
        /// The book-author pairs
        /// </value>
        private DbSet<BookAuthorPair> bookAuthorPairs
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the book-genre pairs from database
        /// </summary>
        /// <value>
        /// The book genre pairs
        /// </value>
        private DbSet<BookGenrePair> bookGenrePairs
        {
            get;
            set;
        }

        /// <summary>
        /// Saves the changes in instance
        /// </summary>
        public void Save()
        {
            this.SaveChanges();
        }

        /// <summary>
        /// Adds the author
        /// </summary>
        /// <param name="author">The author</param>
        public void AddAuthor(Author author)
        {
            this.authors.Add(author);
        }

        /// <summary>
        /// Removes the author
        /// </summary>
        /// <param name="author">The author</param>
        public void RemoveAuthor(Author author)
        {
            this.authors.Remove(author);
        }

        /// <summary>
        /// Adds the book
        /// </summary>
        /// <param name="book">The book</param>
        public void AddBook(Book book)
        {
            this.books.Add(book);
        }

        /// <summary>
        /// Removes the book
        /// </summary>
        /// <param name="book">The book</param>
        public void RemoveBook(Book book)
        {
            this.books.Remove(book);
        }

        /// <summary>
        /// Adds the genre
        /// </summary>
        /// <param name="genre">The genre</param>
        public void AddGenre(Genre genre)
        {
            this.genres.Add(genre);
        }

        /// <summary>
        /// Removes the genre
        /// </summary>
        /// <param name="genre">The genre</param>
        public void RemoveGenre(Genre genre)
        {
            this.genres.Remove(genre);
        }

        /// <summary>
        /// Adds the pair book-author
        /// </summary>
        /// <param name="pairBookAuthor">The pair book-author</param>
        public void AddBookAuthorPair(BookAuthorPair pairBookAuthor)
        {
            this.bookAuthorPairs.Add(pairBookAuthor);
        }

        /// <summary>
        /// Removes the book-author pair
        /// </summary>
        /// <param name="pairBookAuthor">The pair book-author</param>
        public void RemoveBookAuthorPair(BookAuthorPair pairBookAuthor)
        {
            this.bookAuthorPairs.Remove(pairBookAuthor);
        }

        /// <summary>
        /// Adds the book-genre pair
        /// </summary>
        /// <param name="pairBookGenre">The pair book-genre</param>
        public void AddBookGenrePair(BookGenrePair pairBookGenre)
        {
            this.bookGenrePairs.Add(pairBookGenre);
        }

        /// <summary>
        /// Removes the book-genre pair
        /// </summary>
        /// <param name="pairBookGenre">The pair book-genre</param>
        public void RemoveBookGenrePair(BookGenrePair pairBookGenre)
        {
            this.bookGenrePairs.Remove(pairBookGenre);
        }
    }
}