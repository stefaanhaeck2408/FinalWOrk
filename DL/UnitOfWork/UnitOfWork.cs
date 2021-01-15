using DL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.UnitOfWork
{
    public abstract class UnitOfWork: IUnitOfWork
    {
        public DataContext Context { get; }

        public UnitOfWork(DataContext context) {
            Context = context;
        }

        public void Commmit()
        {
            Context.SaveChanges();
        }
    }
}
