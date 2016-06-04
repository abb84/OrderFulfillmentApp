
using RepZio.Ofa.Classes.BusinessLibrary.Classes;
using System.Collections.Generic;

namespace RepZio.Ofa.Data.OrderFulfillmentDataLibrary.Interfaces
{
    /// <summary>
    /// IOrderRepository interface
    /// </summary>
    public interface IOrderRepository
    {
        #region " Properties / Constants... "
        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        Order GetById(int orderNumber);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        List<Order> GetAll();

        /// <summary>
        /// Saves the specified order.
        /// </summary>
        /// <param name="order">The order.</param>
        void Save(Order order);
        #endregion
    }
}
