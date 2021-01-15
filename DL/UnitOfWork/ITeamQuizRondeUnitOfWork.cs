using DL.Models;
using DL.Repositories;
using DL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.UnitOfWork
{
    public interface ITeamQuizRondeUnitOfWork
    {
        public DataContext Context { get; }
        void Commmit();
        public ISQLRepository<Quiz> QuizRepository { get; }
        public ISQLRepository<QuizTeamTussentabel> QuizTeamTussentabelRepository { get; }
        public ISQLRepository<Team> TeamRepository { get; }
        public ISQLRepository<Ronde> RondeRepository { get; }
        public ISQLRepository<QuizRondeTussentabel> QuizRondeTussentabelRepository { get; }

    }
}
