using DL.Context;
using DL.Models;
using DL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.UnitOfWork
{
    public class RondeVraagUnitOfWork: IRondeVraagUnitOfWork
    {
        public RondeVraagUnitOfWork(DataContext context, ISQLRepository<Ronde> rondeRepository,
            ISQLRepository<RondeVraagTussentabel> tussentabelRepository, ISQLRepository<Vraag> vraagRepository)
        {
            RondeRepository = rondeRepository;
            TussentabelRepository = tussentabelRepository;
            VraagRepository = vraagRepository;
            Context = context;

        }

        public ISQLRepository<Ronde> RondeRepository { get; }
        public ISQLRepository<RondeVraagTussentabel> TussentabelRepository { get; }
        public ISQLRepository<Vraag> VraagRepository { get; }
        public DataContext Context { get; }

        public void Commmit()
        {
            Context.SaveChanges();
        }
    }
}
