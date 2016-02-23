﻿namespace IReporter.Services.Data
{
    using System.Linq;

    using IReporter.Data.Models;

    public interface ICommentsService
    {
        IQueryable<Comment> GetAll();

        Comment GetById(int id);

        void Update(Comment article);

        void Create(Comment comment);

        void Save();
    }
}
