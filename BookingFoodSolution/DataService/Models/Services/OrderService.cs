using DataService.Models.RequestModels;
using DataService.Models.RequestModels.Filter;
using DataService.Models.RequestModels.PostRequests;
using DataService.Models.RequestModels.PutRequest;
using DataService.Models.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataService.Models.Services
{
    public interface IOrderService
    {
        List<Order> GetAll(OrderFilterModel orderFilterModel);
        Order Create(OrderPostRequestModel order);
        bool IsValidOrderUnpaid(int accountid);
        Order GetOrderById(int? id);
        bool DeleteOrderById(int orderid);
        List<Order> GetSellerHistory(int? storeid);
        Order Update(OrderPutRequestModel orderPatchRequestModel);
    }

    public class OrderService : BaseUnitOfWork<UnitOfWork>, IOrderService
    {
        public OrderService(UnitOfWork uow) : base(uow)
        {

        }

        public Order GetOrderById(int? id)
        {
            return _uow.Order.GetOrderById(id);
        }

        public List<Order> GetAll(OrderFilterModel orderFilterModel)
        {
            List<Order> result = new List<Order>();
            if(orderFilterModel.StoreOrderBooking > 0 && orderFilterModel.UnpaidAccountId > 0)
            {
                return result;
            }
            else if (orderFilterModel.UnpaidAccountId > 0)
            {
                Order order = _uow.Order.GetUnpaid(orderFilterModel.UnpaidAccountId);
                if(order != null)
                {
                    result.Add(order);
                }
            }else if(orderFilterModel.StoreOrderBooking > 0)
            {
                //var allPro =  _uow.Product.GetAllProduct();
                //allPro = allPro.Where(p => p.StoreId == orderFilterModel.StoreOrderBooking);
                IEnumerable<OrderDetail> orderDetails = _uow.OrderDetail.GetOrderDetailByStoreId(orderFilterModel.StoreOrderBooking);
                if(orderDetails != null)
                {
                    foreach(OrderDetail orderdetail in orderDetails)
                    {
                        Order order = _uow.Order.GetOrderById(orderdetail.OrderId);
                        if(order != null)
                        {
                            if (!(result.Contains(order)) && order.Status == 2)
                            {
                                result.Add(order);
                            }
                        }
                    }
                }
            }
            else
            {
                result = _uow.Order.GetAll();
            }
            
            return result;
        }

        public Order Update(OrderPutRequestModel orderPutRequestModel)
        {
            if (orderPutRequestModel.Status > 1 && orderPutRequestModel.Status < 4)
            {
                Order order = _uow.Order.GetOrderById(orderPutRequestModel.Orderid);
                if (order != null)
                {
                    if(orderPutRequestModel.payment.Equals("viffood"))
                    {
                        ////create payment here
                        //Payment payment = _uow.Payment.Create(new Payment
                        //{
                        //    Name = orderPutRequestModel.payment,
                        //    IsDelete = 0
                        //}).Entity;
                        //_uow.Commit();
                        //order.PaymentId = payment.Id;
                        order.PaymentId = 1;
                    }
                    else if (orderPutRequestModel.payment.Equals("tienmat"))
                    {
                        order.PaymentId = 2;
                    }
                    //Update
                    order.Status = orderPutRequestModel.Status;
                    _uow.Order.Update(order);
                    _uow.Commit();
                    return _uow.Order.GetOrderById(orderPutRequestModel.Orderid);
                }
            }
            return null;
        }
        //private Payment createPayment(String paymentname)
        //{
        //    Payment payment = _uow.Payment.Create(new Payment
        //    {
        //        IsDelete = 0,
        //        Name = paymentname
        //    }).Entity;

        //    _uow.Commit();
        //    return _uow.Payment.GetById(payment.Id);
        //}

        public Order Create(OrderPostRequestModel order)
        {
            Order order1 = new Order
            {
                AccountId = order.AccountId,
                TotalMoney = 0,
                Note = order.Note,
                Status = 0,
                Created = DateTime.Now,
                IsDelete = 0
            };
            _uow.Order.Create(order1);
            _uow.Commit();
            return _uow.Order.GetOrderById(order1.Id);
        }

        public bool IsValidOrderUnpaid(int accountid)
        {
            return _uow.Order.IsValidOrderUnpaid(accountid);
        }

        public bool DeleteOrderById(int orderid)
        {
            Order order = GetOrderById(orderid);
            if (order != null)
            {
                order.IsDelete = 1;
                _uow.Order.Update(order);
                _uow.Commit();
                return true;
            }
            return false;
        }

        public List<Order> GetSellerHistory(int? storeid)
        {
            IEnumerable<OrderDetail> orderDetails =_uow.OrderDetail.GetOrderDetailByStoreId(storeid);
            List<Order> result = new List<Order>();
            foreach(OrderDetail od in orderDetails)
            {
                Order order = GetOrderById(od.OrderId);
                if (order != null)
                {
                    if(order.Status == 1 && !(result.Contains(order)))
                    {
                        result.Add(order);
                    }
                }
            }
            return result;
        }
    }
}
