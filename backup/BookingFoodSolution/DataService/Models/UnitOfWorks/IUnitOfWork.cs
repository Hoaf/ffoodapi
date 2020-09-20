using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
