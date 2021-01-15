using DL.Models;
using DL.Context;
using DL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.UnitOfWork
{
    public interface IRondeVraagUnitOfWork
    {
        public DataContext Context { get; }
        void Commmit();
        public ISQLRepository<Ronde> RondeRepository { get; }
        public ISQLRepository<RondeVraagTussentabel> TussentabelRepository { get; }
        public ISQLRepository<Vraag> VraagRepository { get; }
    }
}
