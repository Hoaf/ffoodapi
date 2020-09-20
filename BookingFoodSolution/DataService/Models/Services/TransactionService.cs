using DataService.Models.RequestModels.PostRequests;
using DataService.Models.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.Services
{
    public interface ITransactionService
    {
        Store GetStoreByOrderId(int orderid);
        Transaction CreateTransaction(TransactionPostRequest transaction);
        IEnumerable<Transaction> GetByWalletId(int walletid);
    }
    public class TransactionService : BaseUnitOfWork<UnitOfWork>, ITransactionService
    {
        public TransactionService(UnitOfWork uow) : base(uow)
        {
        }

        public Transaction CreateTransaction(TransactionPostRequest transaction)
        {
            double? totalMoney = _uow.OrderDetail.CalculateTotalMoney(transaction.Orderid);
            bool check = true;
            //update order status & total money
            Order order = _uow.Order.GetOrderById(transaction.Orderid);
            if(order != null)
            {
                order.Status = 1;
                order.TotalMoney = totalMoney;
                _uow.Order.Update(order);
            }
            else
            {
                check = false;
            }

            int accountid = int.Parse(order.AccountId.ToString());
            Wallet walletBuyer = _uow.Wallet.GetWalletByAccountId(accountid);

            //Payment payment = _uow.Payment.GetByPaymentId(order.Id);
            //if(payment != null)
            //{
                if (order.PaymentId == 1)
                {
                    //update wallet's buyer
                    if (walletBuyer != null)
                    {
                        double? newBalance = walletBuyer.Balance - totalMoney;
                        walletBuyer.Balance = newBalance;
                        _uow.Wallet.Update(walletBuyer);
                    }
                    else
                    {
                        check = false;
                    }
                    //update wallet's seller
                    Store seller = GetStoreByOrderId(order.Id);
                    if (seller != null)
                    {
                        Wallet walletSeller = _uow.Wallet.GetWalletByAccountId(int.Parse(seller.AccountId.ToString()));
                        if (walletSeller != null)
                        {
                            double? newBalance = walletSeller.Balance + totalMoney;
                            walletSeller.Balance = newBalance;
                            _uow.Wallet.Update(walletSeller);
                        }
                    }
                    else
                    {
                        check = false;
                    }
                }
            //}
            

            //create transaction
            if (check)
            {
                Transaction transaction1 = new Transaction
                {
                    OrderId = transaction.Orderid,
                    WalletId = walletBuyer.Id,
                    Money = totalMoney,
                    IsDelete = 0,
                    Created = DateTime.Now
                };

                _uow.Transaction.Create(transaction1);
                _uow.Commit();

                //UpdateTransactionInPayment(int.Parse(order.PaymentId.ToString()),transaction2.Id);

                return transaction1;
            }
            return null;
        }

        //private void UpdateTransactionInPayment(int paymentid,int transactionId)
        //{
        //    Payment payment = _uow.Payment.GetByPaymentId(paymentid);
        //    if(payment != null)
        //    {
        //        payment.TransactionId = transactionId;
        //        _uow.Payment.Update(payment);
        //        _uow.Commit();
        //    }   
        //}

        public IEnumerable<Transaction> GetByWalletId(int walletid)
        {
            return _uow.Transaction.GetByWalletId(walletid);
        }

        public Store GetStoreByOrderId(int orderid)
        {
            List<OrderDetail> orderDetails = _uow.OrderDetail.FindByOrderId(orderid);
            if(orderDetails != null)
            {
                if (orderDetails.Count > 0)
                {
                    int? storeid = _uow.Product.GetProductById(orderDetails[0].ProductId).StoreId;
                    return _uow.Store.GetStoreById(storeid);
                }
            }
            
            return null;
        }
    }
}
