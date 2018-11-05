// <copyright file="BookController.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogic.LibraryModel;
using BusinessLogic.LibraryService;
using Microsoft.AspNetCore.Mvc;
using MyWebLibrary.ViewModel;

namespace Library.Controllers
{
    /// <summary>
    /// Controller for dealing with Book
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        /// <summary>
        /// The library service
        /// </summary>
        private IBookService libraryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookController"/> class
        /// </summary>
        /// <param name="libraryService">The library service</param>
        public BookController(IBookService libraryService)
        {
            this.libraryService = libraryService;
        }

        #region Book

        // GET: api/book
        /// <summary>
        /// Gets all books from the Library
        /// </summary>
        /// <returns>
        /// HTTP status code Ok() with the list of books
        /// or HTTP status code NoContent() if there are any books in library
        /// </returns>
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            int count = this.libraryService.ListOfAll.Count<Book>();
            if (count != 0)
            {
                return Ok(this.libraryService.ListOfAll);
            }

            return NoContent();
        }

        // GET: api/book/5
        /// <summary>
        /// Gets the book by its id
        /// </summary>
        /// <param name="id">The identifier of the book</param>
        /// <returns>
        /// HTTP status code Ok() with found book
        /// or HTTP status code NotFound() if there isn`t book with such id
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            Book foundBook = this.libraryService.GetById(id);
            if (foundBook == null)
            {
                return NotFound("No books with such id!");
            }

            return Ok(foundBook);
        }

        // POST: api/book
        /// <summary>
        /// Adds the new book to the book list
        /// </summary>
        /// <param name="book">The book for adding</param>
        /// <returns>
        /// HTTP status code Created() with added book`s id
        /// or HTTP status code BadRequest() if the book with such id already exists
        /// </returns>
        [HttpPost]
        public IActionResult AddNewBook([FromBody]BookFromUI book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid book input");
            }

            try
            {
                int newBookId = this.libraryService.Add(Mapper.Map<Book>(book));
                return Created("book", newBookId);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // PUT: api/book/5
        /// <summary>
        /// Updates the book
        /// </summary>
        /// <param name="bookId">The book identifier</param>
        /// <param name="book">The book information</param>
        /// <returns>
        /// HTTP status code Ok() with updated book`s id
        /// or HTTP status code NotFound() if there is no books with specified id
        /// </returns>
        [HttpPut("{bookId}")]
        public IActionResult UpdateBook(int bookId, [FromBody]BookFromUI book)
        {
            int? updatedBookId = this.libraryService.Update(bookId, Mapper.Map<Book>(book));
            if (updatedBookId == null)
            {
                return NotFound("No books with such id!");
            }

            return Ok(updatedBookId);
        }

        // DELETE: api/book/5
        /// <summary>
        /// Deletes the book from the library
        /// </summary>
        /// <param name="id">The identifier of the book</param>
        /// <returns>
        /// HTTP status code Ok() with deleted book`s id
        /// or HTTP status code NotFound() if there are any book in library with specified id
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            int? deletedBookId = this.libraryService.Remove(id);
            if (deletedBookId == null)
            {
                return NotFound("No books with such id!");
            }

            return Ok(deletedBookId);
        }

        #endregion Book

        #region BookAuthorPair

        // POST: api/book/
        /// <summary>
        /// Adds the author to the book
        /// </summary>
        /// <param name="bookId">The book identifier</param>
        /// <param name="authorId">The author identifier</param>
        /// <returns>
        /// HTTP status code Created() with id of new record in pairsBookAuthor
        /// or HTTP status code BadRequest() if the book with such id doesn`t exist
        /// or HTTP status code BadRequest() if the author with such id doesn`t exist
        /// </returns>
        [HttpPost("bookId={bookId}&authorId={authorId}")]
        public IActionResult AddAuthorToBook(int bookId, int authorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            try
            {
                int newPairBookAuthorId = this.libraryService.AddAuthorToBook(bookId, authorId);
                return Created("PairBookAuthor", newPairBookAuthorId);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // DELETE: api/book/
        /// <summary>
        /// Removes the author from book
        /// </summary>
        /// <param name="bookId">The book identifier</param>
        /// <param name="authorId">The author identifier</param>
        /// <returns>
        /// HTTP status code Ok() with removedPairBookAuthorId
        /// or HTTP status code NotFound() if there are o specified author in specified book
        /// or HTTP status code BadRequest() if there is no author or book with specified ids
        /// </returns>
        [HttpDelete("bookId={bookId}&authorId={authorId}")]
        public IActionResult RemoveAuthorFromBook(int bookId, int authorId)
        {
            try
            {
                int? removedPairBookAuthorId = this.libraryService.RemoveAuthorFromBook(bookId, authorId);
                if (removedPairBookAuthorId == null)
                {
                    return NotFound("No specified author in specified book!");
                }

                return Ok(removedPairBookAuthorId);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // GET: api/book/
        /// <summary>
        /// Get all books by specified author
        /// </summary>
        /// <param name="authorId">The author identifier</param>
        /// <returns>
        /// HTTP status code Ok() with list of found books
        /// or HTTP status code NotFound() if there are no books with such author
        /// </returns>
        [HttpGet("authorId={authorId}")]
        public IActionResult GetBooksByAuthor(int authorId)
        {
            try
            {
                List<Book> foundBooks = new List<Book>();
                foundBooks = this.libraryService.GetBooksByAuthor(authorId).ToList();

                if (foundBooks.Count == 0)
                {
                    return NotFound("No books with such author!");
                }

                return Ok(foundBooks);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        #endregion BookAuthorPair

        #region BookGenrePair

        // POST: api/book/
        /// <summary>
        /// Adds the genre to the book
        /// </summary>
        /// <param name="bookId">The book identifier</param>
        /// <param name="genreId">The genre identifier</param>
        /// <returns>
        /// HTTP status code Created() with id of new record in pairsBookGenre
        /// or HTTP status code BadRequest() if the book with such id doesn`t exist
        /// or HTTP status code BadRequest() if the genre with such id doesn`t exist
        /// </returns>
        [HttpPost("bookId={bookId}&genreId={genreId}")]
        public IActionResult AddGenreToBook(int bookId, int genreId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            try
            {
                int newPairBookGenreId = this.libraryService.AddGenreToBook(bookId, genreId);
                return Created("PairBookAuthor", newPairBookGenreId);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // DELETE: api/book/
        /// <summary>
        /// Removes the genre from book
        /// </summary>
        /// <param name="bookId">The book identifier</param>
        /// <param name="genreId">The genre identifier</param>
        /// <returns>
        /// HTTP status code Ok() with removedPairBookGenreId
        /// or HTTP status code NotFound() if there are o specified genre in specified book
        /// or HTTP status code BadRequest() if there is no author or book with specified ids
        /// </returns>
        [HttpDelete("bookId={bookId}&genreId={genreId}")]
        public IActionResult RemoveGenreFromBook(int bookId, int genreId)
        {
            try
            {
                int? removedPairBookGenreId = this.libraryService.RemoveGenreFromBook(bookId, genreId);
                if (removedPairBookGenreId == null)
                {
                    return NotFound("No specified genre in specified book!");
                }

                return Ok(removedPairBookGenreId);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // GET: api/book/
        /// <summary>
        /// Get all books by specified genre
        /// </summary>
        /// <param name="genreId">The genre identifier</param>
        /// <returns>
        /// HTTP status code Ok() with list of found books
        /// or HTTP status code NotFound() if there are no books with such genre
        /// </returns>
        [HttpGet("genreId={genreId}")]
        public IActionResult GetBooksByGenre(int genreId)
        {
            try
            {
                List<Book> foundBooks = new List<Book>();
                foundBooks = this.libraryService.GetBooksByGenre(genreId).ToList();

                if (foundBooks.Count == 0)
                {
                    return NotFound("No books with such genre!");
                }

                return Ok(foundBooks);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        #endregion BookGenrePair
    }
}