using System;
using System.Linq;
using System.Collections.Generic;
using RepZio.Ofa.Classes.BusinessLibrary.Classes;
using RepZio.Ofa.Data.OrderFulfillmentDataLibrary.Interfaces;

namespace RepZio.Ofa.Data.OrderFulfillmentDataLibrary.Classes
{
    /// <summary>
    /// In Memory implementation of IOrderRepository interface
    /// </summary>
    public class InMemoryOrderRepo : IOrderRepository
    {
        #region " Properties / Constants... "
        private readonly List<Order> _orders;

        #endregion

        #region " Constructors... "
        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryOrderRepo" /> class.
        /// </summary>
        public InMemoryOrderRepo()
        {
            _orders = new List<Order>();
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
            return _orders.Find(o => o.OrderNumber == orderNumber);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public List<Order> GetAll()
        {
            return _orders;
        }

        /// <summary>
        /// Saves the specified order.
        /// </summary>
        /// <param name="order">The order.</param>
        public void Save(Order order)
        {
            if (order.OrderNumber == 0)
            {
                int orderNumber = 1;
                if (_orders.Count > 0)
                {
                    int lastOrderNumber = _orders.OrderByDescending(o => o.OrderNumber)
                                                 .FirstOrDefault()
                                                 .OrderNumber;
                    orderNumber = lastOrderNumber + 1;
                }

                order.OrderNumber = orderNumber;
                _orders.Add(order);
            }
        }
        #endregion
    }
}
