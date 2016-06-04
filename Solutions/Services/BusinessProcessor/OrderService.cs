using System;
using System.Linq;
using System.Collections.Generic;
using RepZio.Ofa.Classes.BusinessLibrary.Classes;
using RepZio.Ofa.Data.OrderFulfillmentDataLibrary.Interfaces;
using RepZio.Ofa.Services.BusinessProcessor.Interfaces;

namespace RepZio.Ofa.Services.BusinessProcessor
{
    /// <summary>
    /// Order Service
    /// </summary>
    public class OrderService : IOrderService
    {
        #region " Properties / Constants... "
        private readonly IOrderRepository _ordersRepository;

        #endregion

        #region " Constructors... "
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService" /> class.
        /// </summary>
        /// <param name="orderRepository">The order repository.</param>
        public OrderService(IOrderRepository orderRepository)
        {
            _ordersRepository = orderRepository;
        }

        #endregion

        #region " Public Methods... "
        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        public Order GetById(int orderNumber)
        {
            return _ordersRepository.GetById(orderNumber);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public List<Order> GetAll()
        {
            return _ordersRepository.GetAll();
        }

        /// <summary>
        /// Updates the item quantity.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="itemNumber">The item number.</param>
        /// <param name="newQuantity">The new quantity.</param>
        public void UpdateItemQuantity(int orderNumber, int itemNumber, uint newQuantity)
        {
            Order order = GetById(orderNumber);
            if(order != null)
            {
                OrderItem item = order.OrderItems.FirstOrDefault(i => i.ItemNumber == itemNumber);
                if(item != null)
                {
                    item.Quantity = newQuantity;
                    order.CalculateOrderTotal();
                    _ordersRepository.Save(order);
                }
            }
        }

        /// <summary>
        /// Updates the billing address.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="newAddress">The new address.</param>
        public void UpdateBillingAddress(int orderNumber, BillingAddress newAddress)
        {
            Order order = GetById(orderNumber);
            if (order != null)
            { 
                order.BillingAddress = newAddress;
                _ordersRepository.Save(order);
            }
        }

        /// <summary>
        /// Adds the order item.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="newItem">The new item.</param>
        public void AddOrderItem(int orderNumber, OrderItem newItem)
        {
            Order order = GetById(orderNumber);
            if (order != null)
            {
                order.AddNewItem(newItem);
                _ordersRepository.Save(order);
            }
        }

        /// <summary>
        /// Inserts the or update.
        /// </summary>
        /// <param name="order">The order.</param>
        public void InsertOrUpdate(Order order)
        {
            _ordersRepository.Save(order);
        }
        #endregion
    }
}
