using DataService.Models.RequestModels;
using DataService.Models.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.Services
{
    public interface IPaymentService
    {
        Payment GetByIdOrTransactionId(PaymentFilterModel filter);

        IEnumerable<Payment> GetAll();

        bool Delete(int paymentId);
    }
    public class PaymentService : BaseUnitOfWork<UnitOfWork>, IPaymentService
    {
        public PaymentService(UnitOfWork uow) : base(uow)
        {
        }

        public bool Delete(int paymentId)
        {
            Payment payment = _uow.Payment.GetByPaymentId(paymentId);
            if(payment != null)
            {
                payment.IsDelete = 1;
                _uow.Payment.Update(payment);
                _uow.Commit();
                return true;
            }
            return false;
        }

        public IEnumerable<Payment> GetAll()
        {
            return _uow.Payment.GetAll();
        }

        public Payment GetByIdOrTransactionId(PaymentFilterModel filter)
        {
            Payment result = null;
            if(filter.PaymentId > 0 && filter.TransactionId > 0)
            {
                return result;
            }else if(filter.PaymentId > 0)
            {
                result = _uow.Payment.GetByPaymentId(filter.PaymentId);
            }else
            {
                result = _uow.Payment.GetByTransactionId(filter.TransactionId);
            }
            return result;
        }
    }
}
