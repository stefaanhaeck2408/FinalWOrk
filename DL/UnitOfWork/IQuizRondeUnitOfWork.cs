using DL.Context;
using DL.Models;
using DL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.UnitOfWork
{
    public interface IQuizRondeUnitOfWork
    {
        public DataContext Context { get; }
        void Commmit();

        public ISQLRepository<Quiz> QuizRepository { get; }
        public ISQLRepository<QuizRondeTussentabel> TussentabelRepository { get; }
        public ISQLRepository<Ronde> RondeRepository { get; }
    }
}
