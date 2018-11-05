// <copyright file="Author.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.LibraryModel
{
    /// <summary>
    /// The class for author
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        /// <value>
        /// The identifier of author
        /// </value>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the full name of author
        /// </summary>
        /// <value>
        /// The full name of author
        /// </value>
        [Required(ErrorMessage = "Author must have fullname!")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Invalid length of author fullname")]
        public string FullName
        {
            get;
            set;
        }
    }
}