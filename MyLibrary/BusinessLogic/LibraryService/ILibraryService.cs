// <copyright file="ILibraryService.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace BusinessLogic.LibraryService
{
    /// <summary>
    /// Interface for services connected to Library
    /// </summary>
    /// <typeparam name="T">Type from Library model: Book, Author or Genre</typeparam>
    public interface ILibraryService<T>
    {
        /// <summary>
        /// Gets all T entities
        /// </summary>
        /// <returns>The list of T entities</returns>
        IEnumerable<T> ListOfAll
        {
            get;
        }

        /// <summary>
        /// Gets the T entity by identifier
        /// </summary>
        /// <param name="id">The identifier of T entity</param>
        /// <returns>T entity with specified id</returns>
        T GetById(int id);

        /// <summary>
        /// Adds the T entity to the list of T entities
        /// </summary>
        /// <param name="item">The T entity</param>
        /// <returns>The id of the added T entity</returns>
        int Add(T item);

        /// <summary>
        /// Updates the T entity
        /// </summary>
        /// <param name="id">The T entity identifier</param>
        /// <param name="item">The new T entity</param>
        /// <returns>The id of updated T entity or null if there isn`t T entity with specified id</returns>
        int? Update(int id, T item);

        /// <summary>
        /// Removes the T entity
        /// </summary>
        /// <param name="id">The identifier of T entity</param>
        /// <returns>
        /// The id of removed T entity or null if there isn`t T entity with such id
        /// </returns>
        int? Remove(int id);
    }
}