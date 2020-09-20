using DataService.Models.Repositories.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService.Models.Repositories.Create
{
    public partial interface IPaymentRepository : IBaseRepository<Payment>
    {
        IEnumerable<Payment> GetAll();

        Payment GetByPaymentId(int id);
        Payment GetByTransactionId(int id);
    }
    public partial class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(BookingFoodContext Context) : base(Context)
        {

        }

        public IEnumerable<Payment> GetAll()
        {
            return Get().Where(p => p.IsDelete == 0);
        }

        public Payment GetByPaymentId(int id)
        {
            return Get().Where(p => p.IsDelete == 0 && p.Id == id).FirstOrDefault();
        }

        public Payment GetByTransactionId(int id)
        {
            return Get().Where(p => p.IsDelete == 0 && p.TransactionId == id).FirstOrDefault();
        }
    }
}
