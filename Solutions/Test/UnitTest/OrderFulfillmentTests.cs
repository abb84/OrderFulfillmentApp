
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepZio.Ofa.Classes.BusinessLibrary.Classes;
using RepZio.Ofa.Data.OrderFulfillmentDataLibrary.Classes;
using RepZio.Ofa.Services.BusinessProcessor;
using RepZio.Ofa.Services.BusinessProcessor.Interfaces;

namespace UnitTest
{
    /// <summary>
    /// Order Fulfillment Tests
    /// </summary>
    [TestClass]
    public class OrderFulfillmentTests
    {
        #region " Properties / Constants... "
        private readonly IOrderService _orderService;
        #endregion

        #region " Constructors... "
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderFulfillmentTests" /> class.
        /// </summary>
        public OrderFulfillmentTests()
        {
            _orderService = new OrderService(new InMemoryOrderRepo());
        }
        #endregion

        #region " Additional test attributes... "
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        #region " Test Methods... "
        /// <summary>
        /// Creates the order test.
        /// </summary>
        [TestMethod]
        public void CreateOrderTest()
        {
            // Arrange
            Order order = new Order();

            // Act
            _orderService.InsertOrUpdate(order);

            // Asserts
            Assert.AreNotEqual(0, _orderService.GetAll().Count);
        }

        /// <summary>
        /// Adds the order item test.
        /// </summary>
        [TestMethod]
        public void AddOrderItemTest()
        {
            // Arrange
            Order order = new Order();
            _orderService.InsertOrUpdate(order);
            OrderItem newItem = new OrderItem() 
                                    { 
                                        ItemName = "Product 1",
                                        OrderNumber = order.OrderNumber, 
                                        PricePerUnit = 12, 
                                        Quantity = 2 
                                    };

            // Act
            _orderService.AddOrderItem(order.OrderNumber, newItem);
            int expectedItemsCount = 1;
            decimal expectedProductTotal = 24;

            // Asserts
            Assert.AreEqual(expectedItemsCount, order.OrderItems.Count);
            Assert.AreEqual(expectedProductTotal, order.ProductTotal);
        }

        /// <summary>
        /// Updates the billing address test.
        /// </summary>
        [TestMethod]
        public void UpdateBillingAddressTest()
        {
            // Arrange
            Order order = new Order();
            _orderService.InsertOrUpdate(order);
            BillingAddress newAddress = new BillingAddress()
                                        {
                                            FullName = "Abbie Alonso",
                                            Address = "1758 Abbey Rd",
                                            City = "West Palm Beach",
                                            Company = "RepZio",
                                            Country = "USA",
                                            State = "FL",
                                            Email = "abbie.alonso@gmail.com",
                                            Phone = "561-997-4504"
                                        };

            // Act
            _orderService.UpdateBillingAddress(order.OrderNumber, newAddress);

            // Asserts
            Assert.AreEqual(newAddress, order.BillingAddress);
        }

        /// <summary>
        /// Updates the order item quantity test.
        /// </summary>
        [TestMethod]
        public void UpdateOrderItemQuantityTest()
        {
            // Arrange
            Order order = new Order();
            _orderService.InsertOrUpdate(order);
            OrderItem newItem = new OrderItem()
                                {
                                    ItemName = "Product 1",
                                    OrderNumber = order.OrderNumber,
                                    PricePerUnit = 12,
                                    Quantity = 2
                                };

            _orderService.AddOrderItem(order.OrderNumber, newItem);

            // Act
            _orderService.UpdateItemQuantity(order.OrderNumber, newItem.ItemNumber, 4);
            decimal expectedProductTotal = 48;

            // Asserts
            Assert.AreEqual(expectedProductTotal, order.ProductTotal);
        }
        #endregion
    }
}
