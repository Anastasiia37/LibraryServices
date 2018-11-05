// <copyright file="BookFromUI.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using BusinessLogic.LibraryModel;

namespace MyWebLibrary.ViewModel
{
    /// <summary>
    /// Class for getting Book data from UI
    /// </summary>
    public class BookFromUI
    {
        /// <summary>
        /// Gets or sets the name of book
        /// </summary>
        /// <value>
        /// The name of book
        /// </value>
        [Required(ErrorMessage = "Book must have name!")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Invalid length of book`s name!")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the publishing year
        /// </summary>
        /// <value>
        /// The publishing year
        /// </value>
        [Required(ErrorMessage = "Book must have the year of publishing!")]
        [Year]
        public int Year
        {
            get;
            set;
        }
    }
}