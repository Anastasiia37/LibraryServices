// <copyright file="Genre.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.LibraryModel
{
    /// <summary>
    /// Defines different genres of books
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Gets or sets the identifier for genre
        /// </summary>
        /// <value>
        /// The identifier of genre
        /// </value>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of genre
        /// </summary>
        /// <value>
        /// The genre name
        /// </value>
        [Required(ErrorMessage = "Genre must have name!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Invalid length of genre name!")]
        public string Name
        {
            get;
            set;
        }
    }
}