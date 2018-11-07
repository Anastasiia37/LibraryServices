// <copyright file="InMemoryDataProvider.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System.Collections.Generic;
using BusinessLogic.LibraryModel;

namespace BusinessLogic.DataProvider
{
    /// <summary>
    /// Data Provider for Library Service that provides data in a list
    /// </summary>
    /// <seealso cref="BusinessLogic.DataProvider.IDataProvider" />
    public class InMemoryDataProvider : IDataProvider
    {
        /// <summary>
        /// The books
        /// </summary>
        private List<Book> books;

        /// <summary>
        /// The authors
        /// </summary>
        private List<Author> authors;

        /// <summary>
        /// The genres
        /// </summary>
        private List<Genre> genres;

        /// <summary>
        /// The books with genres
        /// </summary>
        private List<BookGenrePair> pairBookGenre;

        /// <summary>
        /// The books with authors
        /// </summary>
        private List<BookAuthorPair> pairBookAuthor;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryDataProvider"/> class
        /// Fill books and authors by default values
        /// </summary>
        public InMemoryDataProvider()
        {
            this.books = new List<Book>()
            {
                new Book { Id = 1, Name = "Book 1", Year = 1950 },
                new Book { Id = 2, Name = "Book 2", Year = 1985 },
                new Book { Id = 3, Name = "Book 3", Year = 2006 },
                new Book { Id = 4, Name = "Book 4", Year = 1955 },
                new Book { Id = 5, Name = "Book 5", Year = 1850 },
                new Book { Id = 6, Name = "Book 6", Year = 1801 },
                new Book { Id = 7, Name = "Book 7", Year = 1777 }
            };

            this.authors = new List<Author>()
            {
                new Author { Id = 1, FullName = "Author 1" },
                new Author { Id = 2, FullName = "Author 2" },
                new Author { Id = 3, FullName = "Author 3" },
                new Author { Id = 4, FullName = "Author 4" },
                new Author { Id = 5, FullName = "Author 5" },  // No book with such author
                new Author { Id = 6, FullName = "Author 6" },  // No book with such author
            };

            this.genres = new List<Genre>()
            {
                new Genre { Id = 1, Name = "Genre 1" },
                new Genre { Id = 2, Name = "Genre 2" },
                new Genre { Id = 3, Name = "Genre 3" },
                new Genre { Id = 4, Name = "Genre 4" },  // No book with such genre
                new Genre { Id = 5, Name = "Genre 5" },
                new Genre { Id = 6, Name = "Genre 6" },
                new Genre { Id = 7, Name = "Genre 7" },
                new Genre { Id = 8, Name = "Genre 8" }
            };

            this.pairBookGenre = new List<BookGenrePair>()
            {
                new BookGenrePair { Id = 1, BookId = 1, GenreId = 1 },
                new BookGenrePair { Id = 1, BookId = 2, GenreId = 6 },
                new BookGenrePair { Id = 1, BookId = 2, GenreId = 1 },
                new BookGenrePair { Id = 1, BookId = 3, GenreId = 5 },
                new BookGenrePair { Id = 1, BookId = 4, GenreId = 1 },
                new BookGenrePair { Id = 1, BookId = 4, GenreId = 6 },
                new BookGenrePair { Id = 1, BookId = 5, GenreId = 3 },
                new BookGenrePair { Id = 1, BookId = 5, GenreId = 2 },
                new BookGenrePair { Id = 1, BookId = 7, GenreId = 8 },
                new BookGenrePair { Id = 1, BookId = 7, GenreId = 2 },
                new BookGenrePair { Id = 1, BookId = 7, GenreId = 6 },
                new BookGenrePair { Id = 1, BookId = 6, GenreId = 7 },
            };

            this.pairBookAuthor = new List<BookAuthorPair>()
            {
                new BookAuthorPair { Id = 1, BookId = 1, AuthorId = 2 },
                new BookAuthorPair { Id = 1, BookId = 2, AuthorId = 1 },
                new BookAuthorPair { Id = 1, BookId = 2, AuthorId = 4 },
                new BookAuthorPair { Id = 1, BookId = 4, AuthorId = 3 },
                new BookAuthorPair { Id = 1, BookId = 4, AuthorId = 2 },
                new BookAuthorPair { Id = 1, BookId = 4, AuthorId = 6 },
                new BookAuthorPair { Id = 1, BookId = 5, AuthorId = 3 },
                new BookAuthorPair { Id = 1, BookId = 6, AuthorId = 4 },
                new BookAuthorPair { Id = 1, BookId = 7, AuthorId = 4 },
            };
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
                return this.books;
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
                return this.authors;
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
                return this.genres;
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
                return this.pairBookGenre;
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
                return this.pairBookAuthor;
            }
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
        /// Adds the book
        /// </summary>
        /// <param name="book">The book</param>
        public void AddBook(Book book)
        {
            this.books.Add(book);
        }

        /// <summary>
        /// Adds the pair book-author
        /// </summary>
        /// <param name="pairBookAuthor">The pair book-author</param>
        public void AddBookAuthorPair(BookAuthorPair pairBookAuthor)
        {
            this.pairBookAuthor.Add(pairBookAuthor);
        }

        /// <summary>
        /// Adds the book-genre pair
        /// </summary>
        /// <param name="pairBookGenre">The pair book-genre</param>
        public void AddBookGenrePair(BookGenrePair pairBookGenre)
        {
            this.pairBookGenre.Add(pairBookGenre);
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
        /// Removes the author
        /// </summary>
        /// <param name="author">The author</param>
        public void RemoveAuthor(Author author)
        {
            this.authors.Remove(author);
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
        /// Removes the book-author pair
        /// </summary>
        /// <param name="pairBookAuthor">The pair book-author</param>
        public void RemoveBookAuthorPair(BookAuthorPair pairBookAuthor)
        {
            this.pairBookAuthor.Remove(pairBookAuthor);
        }

        /// <summary>
        /// Removes the book-genre pair
        /// </summary>
        /// <param name="pairBookGenre">The pair book-genre</param>
        public void RemoveBookGenrePair(BookGenrePair pairBookGenre)
        {
            this.pairBookGenre.Remove(pairBookGenre);
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
        /// Saves the changes in instance
        /// </summary>
        public void Save()
        {
        }
    }
}