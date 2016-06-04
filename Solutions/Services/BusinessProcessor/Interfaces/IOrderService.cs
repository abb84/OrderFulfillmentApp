using System.Collections.Generic;
using RepZio.Ofa.Classes.BusinessLibrary.Classes;

namespace RepZio.Ofa.Services.BusinessProcessor.Interfaces
{
    /// <summary>
    /// IOrderService Interface
    /// </summary>
    public interface IOrderService
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
        /// Updates the item quantity.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="itemNumber">The item number.</param>
        /// <param name="newQuantity">The new quantity.</param>
        void UpdateItemQuantity(int orderNumber, int itemNumber, uint newQuantity);

        /// <summary>
        /// Updates the billing address.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="newAddress">The new address.</param>
        void UpdateBillingAddress(int orderNumber, BillingAddress newAddress);

        /// <summary>
        /// Adds the order item.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="newItem">The new item.</param>
        void AddOrderItem(int orderNumber, OrderItem newItem);

        /// <summary>
        /// Inserts the or update.
        /// </summary>
        /// <param name="order">The order.</param>
        void InsertOrUpdate(Order order);
        #endregion
    }
}
