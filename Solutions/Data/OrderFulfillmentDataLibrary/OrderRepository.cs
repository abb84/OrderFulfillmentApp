
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using Dapper;
using RepZio.Ofa.Classes.BusinessLibrary.Classes;
using RepZio.Ofa.Data.OrderFulfillmentDataLibrary.Interfaces;

namespace RepZio.Ofa.Data.OrderFulfillmentDataLibrary
{
    /// <summary>
    /// Implementation of IOrderRepository for manipulate the DataBase
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        #region " Properties / Constants... "
        #endregion

        #region " Private Methods... "
        /// <summary>
        /// Converts the order entity to class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        private Order ConvertOrderEntityToClass(dynamic entity)
        {
            Order returnValue = new Order();
            returnValue.OrderNumber = entity.OrderNumber;
            returnValue.ProductTotal = entity.ProductTotal;
            returnValue.BillingAddress = new BillingAddress()
                                            {
                                                Address = entity.BillingAddress,
                                                Address2 = entity.BillingAddress2,
                                                FullName = entity.BillingFullName,
                                                Company = entity.BillingCompany,
                                                Email = entity.BillingEmail,
                                                City = entity.BillingCity,
                                                State = entity.BillingState,
                                                Zip = entity.BillingZip,
                                                Country = entity.BillingCountry,
                                                Phone = entity.BillingPhone
                                            };

            return returnValue;
        }
        #endregion

        #region " Public Methods... "
        /// <summary>
        /// Gets the order by orderNumber.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        public Order GetById(int orderNumber)
        {
            Order result = null;
            using (IDbConnection connection = new SqlConnection(RepZio.Ofa.Data.OrderFulfillmentDataLibrary.Classes.Connection.SqlConnection))
            {
                string query = String.Format("SELECT o.*, od.* FROM dbo.[Order] AS o LEFT JOIN dbo.OrderItem AS od ON o.OrderNumber = od.OrderNumber WHERE o.OrderNumber={0}"
                                            , orderNumber);

                Dictionary<int, Order> orders = new Dictionary<int, Order>();
                result = connection.Query<dynamic, dynamic, Order>(query, (currentOrder, currentOrderItem) =>
                                                                                {
                                                                                    Order order;
                                                                                    if (!orders.TryGetValue(currentOrder.OrderNumber, out order))
                                                                                    {
                                                                                        orders.Add(currentOrder.OrderNumber, order = ConvertOrderEntityToClass(currentOrder));
                                                                                    }

                                                                                    if (order.OrderItems == null)
                                                                                        order.OrderItems = new List<OrderItem>();

                                                                                    if (currentOrderItem != null)
                                                                                        order.OrderItems.Add(new OrderItem()
                                                                                                                {
                                                                                                                    OrderNumber = currentOrderItem.OrderNumber,
                                                                                                                    ItemNumber = currentOrderItem.ItemNumber,
                                                                                                                    ItemName = currentOrderItem.ItemName,
                                                                                                                    Quantity = Convert.ToUInt32(currentOrderItem.Quantity),
                                                                                                                    PricePerUnit = currentOrderItem.PricePerUnit
                                                                                                                });
                                                                                    return order;
                                                                                }
                                                                        , splitOn: "OrderNumber").FirstOrDefault();
               
            }

            return result;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public List<Order> GetAll()
        {
            List<Order> returnValue = new List<Order>();
            using (IDbConnection connection = new SqlConnection(RepZio.Ofa.Data.OrderFulfillmentDataLibrary.Classes.Connection.SqlConnection))
            {
                string query = "SELECT o.*, od.* FROM dbo.[Order] AS o INNER JOIN dbo.OrderItem AS od ON o.OrderNumber = od.OrderNumber";

                Dictionary<int, Order> orders = new Dictionary<int, Order>();
                connection.Query<dynamic, dynamic, Order>(query
                                                          , (currentOrder, currentOrderItem) =>
                                                                  {
                                                                      Order order;
                                                                      if (!orders.TryGetValue(currentOrder.OrderNumber, out order))
                                                                      {
                                                                          orders.Add(currentOrder.OrderNumber, order = ConvertOrderEntityToClass(currentOrder));
                                                                      }
                                                                      if (order.OrderItems == null)
                                                                          order.OrderItems = new List<OrderItem>();

                                                                      order.OrderItems.Add(new OrderItem()
                                                                                              {
                                                                                                  OrderNumber = currentOrderItem.OrderNumber,
                                                                                                  ItemNumber = currentOrderItem.ItemNumber,
                                                                                                  ItemName = currentOrderItem.ItemName,
                                                                                                  Quantity = Convert.ToUInt32(currentOrderItem.Quantity),
                                                                                                  PricePerUnit = currentOrderItem.PricePerUnit
                                                                                              });
                                                                      return order;
                                                                  }
                                                            , splitOn: "OrderNumber"
                                                          );

                returnValue.AddRange(orders.Values);
            }

            return returnValue;
        }

        /// <summary>
        /// Saves the specified order.
        /// </summary>
        /// <param name="order">The order.</param>
        public void Save(Order order)
        {
            using (IDbConnection connection = new SqlConnection(RepZio.Ofa.Data.OrderFulfillmentDataLibrary.Classes.Connection.SqlConnection))
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    string query = String.Empty;
                    if (order.OrderNumber == 0)
                    {
                        query = "INSERT INTO dbo.[Order](BillingFullName, BillingCompany, BillingEmail, BillingAddress, BillingAddress2, BillingCity, "+
                                                        "BillingState, BillingZip, BillingCountry, BillingPhone, ProductTotal) " +
                                "VALUES (@BillingFullName, @BillingCompany, @BillingEmail, @BillingAddress1, @BillingAddress2, @BillingCity, @BillingState, "+
                                        "@BillingZip, @BillingCountry, @BillingPhone, @ProductTotal);" +
                                "SELECT CAST(SCOPE_IDENTITY() as int)";

                        int orderNumber = connection.Query<int>(query, new
                                                                {
                                                                    BillingFullName = order.BillingAddress.FullName,
                                                                    BillingCompany = order.BillingAddress.Company,
                                                                    BillingEmail = order.BillingAddress.Email,
                                                                    BillingAddress1 = order.BillingAddress.Address,
                                                                    BillingAddress2 = order.BillingAddress.Address2,
                                                                    BillingCity = order.BillingAddress.City,
                                                                    BillingState = order.BillingAddress.State,
                                                                    BillingZip = order.BillingAddress.Zip,
                                                                    BillingCountry = order.BillingAddress.Country,
                                                                    BillingPhone = order.BillingAddress.Phone,
                                                                    ProductTotal = order.ProductTotal
                                                                }).Single();

                        order.OrderNumber = orderNumber;
                    }
                    else
                    {
                        query = "UPDATE dbo.[Order] " +
                                "SET BillingFullName = @BillingFullName, BillingCompany = @BillingCompany, "+
                                    "BillingEmail = @BillingEmail, BillingAddress = @BillingAddress1, BillingAddress2 = @BillingAddress2, "+
                                    "BillingCity = @BillingCity, BillingState = @BillingState, BillingZip = @BillingZip, BillingCountry = @BillingCountry, "+
                                    "BillingPhone = @BillingPhone, ProductTotal = @ProductTotal " +
                                "Where OrderNumber = @OrderNumber";

                        connection.Execute(query, new
                                                    {
                                                        BillingFullName = order.BillingAddress.FullName,
                                                        BillingCompany = order.BillingAddress.Company,
                                                        BillingEmail = order.BillingAddress.Email,
                                                        BillingAddress1 = order.BillingAddress.Address,
                                                        BillingAddress2 = order.BillingAddress.Address2,
                                                        BillingCity = order.BillingAddress.City,
                                                        BillingState = order.BillingAddress.State,
                                                        BillingZip = order.BillingAddress.Zip,
                                                        BillingCountry = order.BillingAddress.Country,
                                                        BillingPhone = order.BillingAddress.Phone,
                                                        ProductTotal = order.ProductTotal,
                                                        OrderNumber = order.OrderNumber
                                                    });

                        foreach (OrderItem item in order.OrderItems)
                        {
                            bool isNew = connection.Query<int>("SELECT Count(*) FROM dbo.[OrderItem] WHERE OrderNumber=@OrderNumber AND ItemNumber = @ItemNumber"
                                                                , new 
                                                                    { 
                                                                        OrderNumber = item.OrderNumber, 
                                                                        ItemNumber = item.ItemNumber 
                                                                    }).Single() == 0;

                            if (isNew)
                                query = "INSERT INTO dbo.OrderItem (OrderNumber, ItemNumber, ItemName, Quantity, PricePerUnit) " +
                                        "VALUES (@OrderNumber, @ItemNumber, @ItemName, @Quantity, @PricePerUnit)";
                            else
                                query = "UPDATE dbo.OrderItem " +
                                        "SET ItemName = @ItemName, Quantity = @Quantity, PricePerUnit = @PricePerUnit " +
                                        "Where OrderNumber = @OrderNumber AND ItemNumber = @ItemNumber";

                            connection.Execute(query, new
                                                        {
                                                            OrderNumber = item.OrderNumber,
                                                            ItemNumber = item.ItemNumber,
                                                            ItemName = item.ItemName,
                                                            Quantity = (int)item.Quantity,
                                                            PricePerUnit = item.PricePerUnit
                                                        });
                        }
                    }

                    transactionScope.Complete();
                }
            }
        }
        #endregion
    }
}
