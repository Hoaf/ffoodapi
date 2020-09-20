using DataService.Models.Repositories.Basic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DataService.Models.Repositories.Create
{
    public partial interface ITransactionRepository : IBaseRepository<Transaction>
    {
        IEnumerable<Transaction> GetByWalletId(int walletid);
    }
    public partial class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BookingFoodContext Context) : base(Context)
        {

        }

        public IEnumerable<Transaction> GetByWalletId(int walletid)
        {
            return Get().Where(t => t.IsDelete == 0 && t.WalletId == walletid);
        }


    }
}
