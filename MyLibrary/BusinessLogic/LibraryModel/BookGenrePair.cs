// <copyright file="BookGenrePair.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.LibraryModel
{
    /// <summary>
    /// Matches books and related genres
    /// </summary>
    public class BookGenrePair
    {
        /// <summary>
        /// Gets or sets the identifier for record of matching books and genres
        /// </summary>
        /// <value>
        /// The identifier of book
        /// </value>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the identifier for book
        /// </summary>
        /// <value>
        /// The identifier of book
        /// </value>
        [Required(ErrorMessage = "The identifier of book must have id!")]
        [Range(1, int.MaxValue)]
        public int BookId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the identifier for genre
        /// </summary>
        /// <value>
        /// The identifier of genre
        /// </value>
        [Required(ErrorMessage = "The identifier of genre must have id!")]
        [Range(1, int.MaxValue)]
        public int GenreId
        {
            get;
            set;
        }
    }
}