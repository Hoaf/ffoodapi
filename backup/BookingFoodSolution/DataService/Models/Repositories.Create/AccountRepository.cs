using DataService.Models.Repositories.Basic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;

namespace DataService.Models.Repositories.Create
{
    public partial interface IAccountRepository : IBaseRepository<Account>
    {
        IEnumerable<Account> GetAccount(int currentId);
        Account GetAccountById(int id);
        Account CheckUsername(string username);
        Account CheckLogin(string username, string password);
    }
    public partial class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        
        public AccountRepository(BookingFoodContext Context) : base(Context)
        {
            
        }

        public Account CheckLogin(string username, string password)
        {
            return Get().Where(p => p.Username == username && p.Password == password && p.IsDelete == 0).FirstOrDefault();
        }

        public Account CheckUsername(string username)
        {
            return Get().Where(p => p.Username == username && p.Id == 0).FirstOrDefault();
        }

        public IEnumerable<Account> GetAccount(int idCurrent)
        {
           
            return Get().Where(p=> p.Id != idCurrent).Include(x => x.Store);
        }

        public Account GetAccountById(int id)
        {
            return Get().Where(p => p.Id == id && p.IsDelete == 0).Include(x => x.Store).FirstOrDefault();
        }
    }

}
