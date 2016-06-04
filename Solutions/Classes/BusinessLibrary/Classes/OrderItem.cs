
namespace RepZio.Ofa.Classes.BusinessLibrary.Classes
{
    /// <summary>
    /// OrderItem Class
    /// </summary>
    public class OrderItem
    {
        #region " Properties / Constants... "
        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>The order number.</value>
        public int OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the item number.
        /// </summary>
        /// <value>The item number.</value>
        public int ItemNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>The name of the item.</value>
        public string ItemName { get; set; }

        /// <summary>
        /// Gets or sets the quantity ordered.
        /// </summary>
        /// <value>The quantity ordered.</value>
        public uint Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price per unit.
        /// </summary>
        /// <value>The price per unit.</value>
        public decimal PricePerUnit { get; set; }
        #endregion
    }
}
