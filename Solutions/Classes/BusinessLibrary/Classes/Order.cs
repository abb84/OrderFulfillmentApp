
using System.Collections.Generic;

namespace RepZio.Ofa.Classes.BusinessLibrary.Classes
{
    /// <summary>
    /// Order Class
    /// I designed this class for meet test's requirements, so most of details required in a real application were excluded (e.g: Taxes, Shipping Charges, Discounts, etc..)
    /// </summary>
    public class Order
    {
        #region " Properties / Constants... "
        private int _sequenceCounter;

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public int OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        /// <value>The billing address.</value>
        public BillingAddress BillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the product total.
        /// </summary>
        /// <value>The product total.</value>
        public decimal ProductTotal { get; set; }

        /// <summary>
        /// Gets or sets the order items.
        /// </summary>
        /// <value>The order items.</value>
        public List<OrderItem> OrderItems { get; set; }
        #endregion

        #region " Constructors... "
        /// <summary>
        /// Initializes a new instance of the <see cref="Order" /> class.
        /// </summary>
        public Order()
        {
            OrderItems = new List<OrderItem>();
            this.BillingAddress = new BillingAddress();
            _sequenceCounter = 1;
        }
        #endregion

        #region " Private Methods... "
        /// <summary>
        /// Gets the next sequence number.
        /// </summary>
        /// <returns></returns>
        private int GetNextSequenceNumber()
        {
            int value = _sequenceCounter;
            _sequenceCounter++;
            return value;
        }

        /// <summary>
        /// Updates the sequence number.
        /// </summary>
        /// <param name="existingItemNumber">The existing item number.</param>
        private void UpdateSequenceNumber(int existingItemNumber)
        {
            if (_sequenceCounter <= existingItemNumber)
            {
                _sequenceCounter = existingItemNumber;
                _sequenceCounter++;
            }
        }
        #endregion

        #region " Public Methods... "
        /// <summary>
        /// Adds the new item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddNewItem(OrderItem item)
        {
            if (item.ItemNumber < 1)
                item.ItemNumber = GetNextSequenceNumber();
            else
                UpdateSequenceNumber(item.ItemNumber);

            OrderItems.Add(item);
            CalculateOrderTotal();
        }

        /// <summary>
        /// Calculates the order total.
        /// </summary>
        public void CalculateOrderTotal()
        {
            decimal productTotal = 0;
            foreach (OrderItem item in OrderItems)
            {
                productTotal += item.PricePerUnit * item.Quantity;
            }

            this.ProductTotal = productTotal;
        }
        #endregion
    }
}
