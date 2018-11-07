// <copyright file="IDataProvider.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System.Collections.Generic;
using BusinessLogic.LibraryModel;

namespace BusinessLogic.DataProvider
{
    /// <summary>
    /// Interface for data providers
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Gets the books
        /// </summary>
        /// <value>
        /// The books
        /// </value>
        List<Book> Books
        {
            get;
        }

        /// <summary>
        /// Gets the authors
        /// </summary>
        /// <value>
        /// The authors
        /// </value>
        List<Author> Authors
        {
            get;
        }

        /// <summary>
        /// Gets the genres
        /// </summary>
        /// <value>
        /// The genres
        /// </value>
        List<Genre> Genres
        {
            get;
        }

        /// <summary>
        /// Gets the pair of book and its genre
        /// </summary>
        /// <value>
        /// The pair of book and its genre
        /// </value>
        List<BookGenrePair> PairsBookGenre
        {
            get;
        }

        /// <summary>
        /// Gets the pair of book and its author
        /// </summary>
        /// <value>
        /// The pair of book and its author
        /// </value>
        List<BookAuthorPair> PairsBookAuthor
        {
            get;
        }

        /// <summary>
        /// Adds the author
        /// </summary>
        /// <param name="author">The author</param>
        void AddAuthor(Author author);

        /// <summary>
        /// Removes the author
        /// </summary>
        /// <param name="author">The author</param>
        void RemoveAuthor(Author author);

        /// <summary>
        /// Adds the book
        /// </summary>
        /// <param name="book">The book</param>
        void AddBook(Book book);

        /// <summary>
        /// Removes the book
        /// </summary>
        /// <param name="book">The book</param>
        void RemoveBook(Book book);

        /// <summary>
        /// Adds the genre
        /// </summary>
        /// <param name="genre">The genre</param>
        void AddGenre(Genre genre);

        /// <summary>
        /// Removes the genre
        /// </summary>
        /// <param name="genre">The genre</param>
        void RemoveGenre(Genre genre);

        /// <summary>
        /// Adds the pair book-author
        /// </summary>
        /// <param name="pairBookAuthor">The pair book-author</param>
        void AddBookAuthorPair(BookAuthorPair pairBookAuthor);

        /// <summary>
        /// Removes the book-author pair
        /// </summary>
        /// <param name="pairBookAuthor">The pair book-author</param>
        void RemoveBookAuthorPair(BookAuthorPair pairBookAuthor);

        /// <summary>
        /// Adds the book-genre pair
        /// </summary>
        /// <param name="pairBookGenre">The pair book-genre</param>
        void AddBookGenrePair(BookGenrePair pairBookGenre);

        /// <summary>
        /// Removes the book-genre pair
        /// </summary>
        /// <param name="pairBookGenre">The pair book-genre</param>
        void RemoveBookGenrePair(BookGenrePair pairBookGenre);

        /// <summary>
        /// Saves the changes in instance
        /// </summary>
        void Save();
    }
}