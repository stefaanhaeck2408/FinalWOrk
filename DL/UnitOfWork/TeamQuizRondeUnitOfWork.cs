using DL.Context;
using DL.Models;
using DL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.UnitOfWork
{
    public class TeamQuizRondeUnitOfWork: ITeamQuizRondeUnitOfWork
    {

        public TeamQuizRondeUnitOfWork(DataContext context, ISQLRepository<Quiz> quizRepository, 
            ISQLRepository<QuizTeamTussentabel> tussentabelRepository, ISQLRepository<Team> teamRepository, ISQLRepository<Ronde> rondeRepository,
            ISQLRepository<QuizRondeTussentabel> quizRondeTussentabelRepository)
        {
            QuizRepository = quizRepository;
            QuizTeamTussentabelRepository = tussentabelRepository;
            TeamRepository = teamRepository;
            RondeRepository = rondeRepository;
            QuizRondeTussentabelRepository = quizRondeTussentabelRepository;
            Context = context;
        }
        public DataContext Context { get; }
        public ISQLRepository<Quiz> QuizRepository { get; }
        public ISQLRepository<QuizTeamTussentabel> QuizTeamTussentabelRepository { get; }
        public ISQLRepository<Team> TeamRepository { get; }
        public ISQLRepository<Ronde> RondeRepository { get; }
        public ISQLRepository<QuizRondeTussentabel> QuizRondeTussentabelRepository { get; }

        public void Commmit()
        {
            Context.SaveChanges();
        }
    }
}
