using DL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public DataContext Context { get;  }
        void Commmit();
    }
}
