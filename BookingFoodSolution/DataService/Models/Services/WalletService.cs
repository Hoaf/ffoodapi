using DataService.Models.RequestModels;
using DataService.Models.RequestModels.PutRequests;
using DataService.Models.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService.Models.Services
{
    public interface IWalletService
    {
        Wallet Get(int id);
        IEnumerable<Wallet> Get(WalletFilterModel model);
        //Wallet CreateWallet(int AccountId);
        bool DeleteWallet(int id);
        Wallet Update(WalletPutRequestModel model);
    }

    public class WalletService : BaseUnitOfWork<UnitOfWork>, IWalletService
    {
        public WalletService(UnitOfWork uow) : base(uow) {}

        public Wallet Get(int id)
        {
            var result = _uow.Wallet.GetWalletById(id);
            if(result == null)
            {
                return null;
            }
            else
            {
                return result;
            }

          
        }

        public IEnumerable<Wallet> Get(WalletFilterModel model)
        {
            var result = _uow.Wallet.GetWallets();
            if (result == null)
            {
                return null;
            }
            else
            {
                if (model.AccountId > 0)
                {
                    result = result.Where(w => w.AccountId == model.AccountId);
                }
                return result;
            }
        }

        public IEnumerable<Wallet> Get()
        {
            var result = _uow.Wallet.GetWallets();
            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        //public Wallet CreateWallet(int AccountId)
        //{
        //    var checkWallet = _uow.Wallet.GetWalletByAccountId(AccountId);
        //    if (checkWallet != null && checkWallet.IsDelete == 1)
        //    {
        //        checkWallet.IsDelete = 0;
        //        _uow.Wallet.Update(checkWallet);
        //        _uow.Commit();
        //        return _uow.Wallet.GetWalletById(checkWallet.Id);
        //    }
        //    else if (checkWallet == null)
        //    {
        //        Wallet wallet = new Wallet
        //        {
        //            Balance = 0,
        //            AccountId = AccountId,
        //            IsDelete = 0
        //        };
        //        _uow.Wallet.Create(wallet);
        //        _uow.Commit();
        //        return _uow.Wallet.GetWalletById(wallet.Id);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public bool DeleteWallet(int id)
        {
            var wallet = _uow.Wallet.GetWalletById(id);
            if (wallet == null)
            {
                return false;
            }
            else
            {
                wallet.IsDelete = 1;
                _uow.Wallet.Update(wallet);
                _uow.Commit();
                return true;
            }
        }

        public Wallet Update(WalletPutRequestModel model)
        {
            var wallet = _uow.Wallet.GetWalletByAccountId(model.AcountId);
            if (wallet == null)
            {
                return null;
            }
            else
            {
                var currentBalance = wallet.Balance + model.Balance;
                wallet.Balance = currentBalance;
                _uow.Wallet.Update(wallet);
                _uow.Commit();
                return _uow.Wallet.GetWalletById(wallet.Id);
            }
        }

        
    }
}
