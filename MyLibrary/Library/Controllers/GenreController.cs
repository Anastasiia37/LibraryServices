// <copyright file="GenreController.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using BusinessLogic.LibraryModel;
using BusinessLogic.LibraryService;
using Microsoft.AspNetCore.Mvc;
using MyWebLibrary.ViewModel;

namespace MyWebLibrary.Controllers
{
    /// <summary>
    /// Controller for dealing with genres
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        /// <summary>
        /// The library service
        /// </summary>
        private IGenreService libraryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenreController"/> class
        /// </summary>
        /// <param name="libraryService">The library service</param>
        public GenreController(IGenreService libraryService)
        {
            this.libraryService = libraryService;
        }

        // GET: api/genre
        /// <summary>
        /// Gets all genres from the Library
        /// </summary>
        /// <returns>
        /// HTTP status code Ok() with the list of genres
        /// or HTTP status code NoContent() if there are any genres in library
        /// </returns>
        [HttpGet]
        public IActionResult GetAllGenres()
        {
            if (this.libraryService.ListOfAll != null)
            {
                return Ok(this.libraryService.ListOfAll);
            }

            return NoContent();
        }

        // GET: api/genre/5
        /// <summary>
        /// Gets the genre by its id
        /// </summary>
        /// <param name="id">The identifier of the genre</param>
        /// <returns>
        /// HTTP status code Ok() with found genre
        /// or HTTP status code NotFound() if there isn`t genre with such id
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            Genre foundGenre = this.libraryService.GetById(id);
            if (foundGenre == null)
            {
                return NotFound("No genres with such id!");
            }

            return Ok(foundGenre);
        }

        // POST: api/genre
        /// <summary>
        /// Adds the new genre to the genre list
        /// </summary>
        /// <param name="genre">The genre for adding</param>
        /// <returns>
        /// HTTP status code Created() with added genre`s id
        /// or HTTP status code BadRequest() if the genre with such id already exists
        /// </returns>
        [HttpPost]
        public IActionResult AddNewGenre([FromBody]GenreFromUI genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid genre input");
            }

            try
            {
                int newGenreId = this.libraryService.Add(Mapper.Map<Genre>(genre));
                return Created("genre", newGenreId);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // PUT: api/genre/5
        /// <summary>
        /// Updates the genre
        /// </summary>
        /// <param name="id">The genre identifier</param>
        /// <param name="genre">The genre information</param>
        /// <returns>
        /// HTTP status code Ok() with updated genre`s id
        /// or HTTP status code NotFound() if there is no genres with specified id
        /// </returns>
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody]GenreFromUI genre)
        {
            int? updatedGenreId = this.libraryService.Update(id, Mapper.Map<Genre>(genre));
            if (updatedGenreId == null)
            {
                return NotFound("No genres with such id!");
            }

            return Ok(updatedGenreId);
        }

        // DELETE: api/genre/5
        /// <summary>
        /// Deletes the genre from the library
        /// </summary>
        /// <param name="id">The identifier of the genre</param>
        /// <returns>
        /// HTTP status code Ok() with the id of deleted genre
        /// or HTTP status code NotFound() if there are any genre in library with specified id
        /// or HTTP status code BadRequest() if books with specified genre exist
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult RemoveGenre(int id)
        {
            try
            {
                int? idOfDeletedGenre = this.libraryService.Remove(id);
                if (idOfDeletedGenre == null)
                {
                    return NotFound("No genres with such id!");
                }

                return Ok(idOfDeletedGenre);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}