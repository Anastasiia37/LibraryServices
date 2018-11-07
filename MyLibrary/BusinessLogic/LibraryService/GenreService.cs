// <copyright file="GenreService.cs" company="Peretiatko Anastasiia">
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
    /// Genre services
    /// </summary>
    /// <seealso cref="BusinessLogic.LibraryService.LibraryService" />
    /// <seealso cref="BusinessLogic.LibraryService.IGenreService" />
    public class GenreService : IGenreService
    {
        /// <summary>
        /// The data provider (database, inMemory, etc)
        /// </summary>
        private IDataProvider dataProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenreService"/> class
        /// </summary>
        /// <param name="dataProvider">The data provider</param>
        public GenreService(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        #region GenreServices

        /// <summary>
        /// Gets all genres
        /// </summary>
        /// <returns>The list of genres in Library</returns>
        public IEnumerable<Genre> ListOfAll
        {
            get
            {
                return this.dataProvider.Genres;
            }
        }

        /// <summary>
        /// Gets the genre by identifier
        /// </summary>
        /// <param name="id">The genre identifier</param>
        /// <returns>The found genre or null if there is no such genre in library</returns>
        public Genre GetById(int id)
        {
            return this.dataProvider.Genres.FirstOrDefault(genre => genre.Id == id);
        }

        /// <summary>
        /// Adds the genre to Library
        /// </summary>
        /// <param name="genre">The genre for adding</param>
        /// <returns>The id of added genre</returns>
        /// <exception cref="System.ArgumentException">The genre with such id already exists!</exception>
        public int Add(Genre genre)
        {
            if (this.dataProvider.Genres.Any(g => g.Id == genre.Id))
            {
                throw new ArgumentException("The genre with such id already exists!");
            }

            this.dataProvider.AddGenre(genre);
            this.dataProvider.Save();
            return genre.Id;
        }

        /// <summary>
        /// Updates the genre
        /// </summary>
        /// <param name="genreId">The genre identifier</param>
        /// <param name="genre">The new genre</param>
        /// <returns>The id of updated genre or null if there is no genre with specified id</returns>
        public int? Update(int genreId, Genre genre)
        {
            Genre genreForUpdate = this.dataProvider.Genres.FirstOrDefault(g => g.Id == genreId);
            if (genreForUpdate == null)
            {
                return null;
            }

            genreForUpdate.Name = genre.Name;
            this.dataProvider.Save();
            return genreForUpdate.Id;
        }

        /// <summary>
        /// Removes the genre
        /// </summary>
        /// <param name="id">The identifier of genre for removing</param>
        /// <returns>
        /// The id of deleted genre or null if there are no such genre
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Can`t remove this genre because books with this genre exist!
        /// </exception>
        public int? Remove(int id)
        {
            Genre genreForDelete = this.dataProvider.Genres.FirstOrDefault(genre => genre.Id == id);
            if (genreForDelete != null)
            {
                if (this.dataProvider.PairsBookGenre.Any(p => p.GenreId == id))
                {
                    throw new ArgumentException("Can`t remove this genre because books with this genre exist!");
                }

                this.dataProvider.RemoveGenre(genreForDelete);
                this.dataProvider.Save();
                return genreForDelete.Id;
            }

            return null;
        }

        #endregion GenreServices
    }
}