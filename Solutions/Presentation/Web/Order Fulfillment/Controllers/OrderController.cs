
using System.Web.Mvc;
using RepZio.Ofa.Classes.BusinessLibrary.Classes;
using RepZio.Ofa.Services.BusinessProcessor.Interfaces;

namespace RepZio.Ofa.Presentation.Web.OrderFulfillment.Controllers
{
    /// <summary>
    /// Order Controller
    /// </summary>
    public class OrderController : Controller
    {
        #region " Properties / Constants... "
        private readonly IOrderService _orderService;
        #endregion

        #region " Constructors... "
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController" /> class.
        /// </summary>
        /// <param name="orderService">The order service.</param>
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        #region " Actions... "
        /// <summary>
        /// GET: Order.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(_orderService.GetAll());
        }

        /// <summary>
        /// GET: Order/Details/5.
        /// </summary>
        /// <param name="id">The OrderNumber.</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            return View(_orderService.GetById(id));
        }

        /// <summary>
        /// GET: Order/Create.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            // NOTE: I'm using Order class only for simplification. In a real application should be used a CartModel and make all modifications over the cart and only save changes when all
            // information required was completed.
            return View(new Order());
        }

        /// <summary>
        /// CPOST: Order/Create.
        /// </summary>
        /// <param name="newOrder">The new order.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Order newOrder)
        {
            // NOTE: I'm creating an Order without order items for simplification. In a real application it not should be allowed.
            _orderService.InsertOrUpdate(newOrder);

            return RedirectToAction("AddItem", new { id = newOrder.OrderNumber });
        }

        /// <summary>
        /// GET: Order/AddItem/5.
        /// </summary>
        /// <param name="id">The OrderNumber.</param>
        /// <param name="lastItemNumber">The last item number.</param>
        /// <returns></returns>
        public ActionResult AddItem(int id, int lastItemNumber = 0)
        {
            OrderItem newItem = new OrderItem() 
                                { 
                                    OrderNumber = id,
                                    ItemNumber = lastItemNumber
                                };
            
            ViewBag.OrderItems = _orderService.GetById(id).OrderItems;

            return View(newItem);
        }

        /// <summary>
        /// POST: Order/AddItem/5.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <param name="addOther">The add other.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddItem(OrderItem newItem, bool addOther)
        {
            ActionResult returnValue = RedirectToAction("Index");

            _orderService.AddOrderItem(newItem.OrderNumber, newItem);
            if (addOther)
                returnValue = RedirectToAction("AddItem", new { id = newItem.OrderNumber, lastItemNumber = newItem.ItemNumber });

            return returnValue;
        }
        #endregion
    }
}
