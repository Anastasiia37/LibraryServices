// <copyright file="GenreFromUI.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace MyWebLibrary.ViewModel
{
    /// <summary>
    /// Class for getting Genre data from UI
    /// </summary>
    public class GenreFromUI
    {
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