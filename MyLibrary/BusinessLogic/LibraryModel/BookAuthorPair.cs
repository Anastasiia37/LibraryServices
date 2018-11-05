// <copyright file="BookAuthorPair.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.LibraryModel
{
    /// <summary>
    /// Class for Book-Author pairs
    /// </summary>
    public class BookAuthorPair
    {
        /// <summary>
        /// Gets or sets the identifier for record of matching books and authors
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
        /// Gets or sets the identifier for author
        /// </summary>
        /// <value>
        /// The identifier of author
        /// </value>
        [Required(ErrorMessage = "The identifier of author must have id!")]
        [Range(1, int.MaxValue)]
        public int AuthorId
        {
            get;
            set;
        }
    }
}