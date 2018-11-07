// <copyright file="AuthorFromUI.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace MyWebLibrary.ViewModel
{
    /// <summary>
    /// Class for getting Author data from UI
    /// </summary>
    public class AuthorFromUI
    {
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