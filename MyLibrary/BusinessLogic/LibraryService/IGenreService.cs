// <copyright file="IGenreService.cs" company="Peretiatko Anastasiia">
// Copyright (c) Peretiatko Anastasiia. All rights reserved.
// </copyright>

using BusinessLogic.LibraryModel;

namespace BusinessLogic.LibraryService
{
    /// <summary>
    /// Interface for services connected to Genre
    /// </summary>
    public interface IGenreService : ILibraryService<Genre>
    {
    }
}