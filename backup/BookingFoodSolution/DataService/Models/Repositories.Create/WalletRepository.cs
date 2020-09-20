using DataService.Models.Repositories.Basic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService.Models.Repositories.Create
{
    public partial interface IWalletRepository : IBaseRepository<Wallet>
    {
        IEnumerable<Wallet> GetWallets();
        Wallet GetWalletById(int id);
        Wallet GetWalletByAccountId(int id);
    }
    public partial class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(BookingFoodContext Context) : base(Context)
        {

        }

        public Wallet GetWalletByAccountId(int id)
        {
            return Get().Where(w => w.AccountId == id && w.IsDelete == 0).FirstOrDefault();
        }

        public Wallet GetWalletById(int id)
        {
            return Get().Where(w => w.Id == id && w.IsDelete == 0).FirstOrDefault();
        }

        public IEnumerable<Wallet> GetWallets()
        {
            return Get().Where(w => w.IsDelete == 0);
        }
    }
}
