using DL.Context;
using DL.Models;
using DL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.UnitOfWork
{
    public class QuizRondeUnitOfWork: IQuizRondeUnitOfWork
    {
        public QuizRondeUnitOfWork(DataContext context, ISQLRepository<Quiz> quizRepository,
            ISQLRepository<QuizRondeTussentabel> tussentabelRepository, ISQLRepository<Ronde> rondeRepository)
        {
            QuizRepository = quizRepository;
            TussentabelRepository = tussentabelRepository;
            RondeRepository = rondeRepository;
            Context = context;
        }
        public DataContext Context { get; }
        public ISQLRepository<Quiz> QuizRepository { get; }
        public ISQLRepository<QuizRondeTussentabel> TussentabelRepository { get; }
        public ISQLRepository<Ronde> RondeRepository { get; }

        public void Commmit()
        {
            Context.SaveChanges();
        }
    }
}
