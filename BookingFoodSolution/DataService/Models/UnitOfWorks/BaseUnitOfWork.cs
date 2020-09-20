using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.UnitOfWorks
{
    public class BaseUnitOfWork<T>
    {
        protected T _uow;
        public BaseUnitOfWork(T uow)
        {
            _uow = uow;
        }
    }
}
