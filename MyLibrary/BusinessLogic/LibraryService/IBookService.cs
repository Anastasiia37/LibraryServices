// <copyright file="IBookService.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System.Collections.Generic;
using BusinessLogic.LibraryModel;

namespace BusinessLogic.LibraryService
{
    /// <summary>
    /// Interface for services connected to Book
    /// </summary>
    public interface IBookService : ILibraryService<Book>
    {
        /// <summary>
        /// Adds the author to book
        /// </summary>
        /// <param name="bookId">The book identifier</param>
        /// <param name="authorId">The author identifier</param>
        /// <returns>
        /// The id of new record in pairsBookAuthor
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Can`t find author with such id!
        /// or
        /// Can`t find book with such id!
        /// </exception>
        int AddAuthorToBook(int bookId, int authorId);

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
        int? RemoveAuthorFromBook(int bookId, int authorId);

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
        IEnumerable<Book> GetBooksByAuthor(int authorId);

        /// <summary>
        /// Adds the genre to book
        /// </summary>
        /// <param name="bookId">The book identifier</param>
        /// <param name="genreId">The genre identifier</param>
        /// <returns>
        /// The id of new record in pairsBookGenre
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Can`t find genre with such id!
        /// or
        /// Can`t find book with such id!
        /// </exception>
        int AddGenreToBook(int bookId, int genreId);

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
        int? RemoveGenreFromBook(int bookId, int genreId);

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
        IEnumerable<Book> GetBooksByGenre(int genreId);
    }
}