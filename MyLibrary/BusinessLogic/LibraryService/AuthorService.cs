// <copyright file="AuthorService.cs" company="Peretiatko Anastasiia">
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
    /// Author Services
    /// </summary>
    /// <seealso cref="BusinessLogic.LibraryService.LibraryService" />
    /// <seealso cref="BusinessLogic.LibraryService.IAuthorService" />
    public class AuthorService : IAuthorService
    {
        /// <summary>
        /// The data provider (database, inMemory, etc)
        /// </summary>
        private IDataProvider dataProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorService"/> class
        /// </summary>
        /// <param name="dataProvider">The data provider</param>
        public AuthorService(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        #region AuthorServices

        /// <summary>
        /// Gets all authors from the library
        /// </summary>
        /// <returns>The list of all authors</returns>
        public IEnumerable<Author> ListOfAll
        {
            get
            {
                return this.dataProvider.Authors;
            }
        }

        /// <summary>
        /// Gets the author by identifier
        /// </summary>
        /// <param name="id">The identifier of author</param>
        /// <returns>The author</returns>
        public Author GetById(int id)
        {
            return this.dataProvider.Authors.FirstOrDefault(author => author.Id == id);
        }

        /// <summary>
        /// Adds the author to the author list
        /// </summary>
        /// <param name="author">The new author</param>
        /// <returns>The id of added author</returns>
        /// <exception cref="ArgumentException">The author with such id already exists!</exception>
        public int Add(Author author)
        {
            if (this.dataProvider.Authors.Any(a => a.Id == author.Id))
            {
                throw new ArgumentException("The author with such id already exists!");
            }

            this.dataProvider.AddAuthor(author);
            this.dataProvider.Save();
            return author.Id;
        }

        /// <summary>
        /// Updates the author`s information
        /// </summary>
        /// <param name="authorId">The author identifier</param>
        /// <param name="author">The author`s information</param>
        /// <returns>
        /// The id of updated author or null if there isn`t author with specified id
        /// </returns>
        public int? Update(int authorId, Author author)
        {
            Author authorForUpdate = this.dataProvider.Authors.FirstOrDefault(a => a.Id == authorId);
            if (authorForUpdate == null)
            {
                return null;
            }

            authorForUpdate.FullName = author.FullName;
            this.dataProvider.Save();
            return authorForUpdate.Id;
        }

        /// <summary>
        /// Removes the author if there are no books of such author in the library
        /// </summary>
        /// <param name="authorId">The author identifier</param>
        /// <returns>
        /// Number of deleted books with such author
        /// or null if there isn`t author with specified id
        /// </returns>
        public int? Remove(int authorId)
        {
            Author authorForDelete = this.dataProvider.Authors.FirstOrDefault(author => author.Id == authorId);
            if (authorForDelete != null)
            {
                foreach (var pair in this.dataProvider.PairsBookAuthor.Where(pair => pair.AuthorId == authorId))
                {
                    this.dataProvider.RemoveBookAuthorPair(pair);
                    this.dataProvider.Save();
                }

                this.dataProvider.RemoveAuthor(authorForDelete);
                this.dataProvider.Save();
                return authorId;
            }

            return null;
        }

        #endregion AuthorServices
    }
}