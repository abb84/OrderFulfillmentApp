using RepZio.Ofa.Classes.BusinessLibrary.Classes;
using RepZio.Ofa.Services.BusinessProcessor.Interfaces;
using System.Web.Mvc;

namespace RepZio.Ofa.Presentation.Web.OrderFulfillment.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        // GET: Order
        public ActionResult Index()
        {
            return View(_orderService.GetAll());
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View(_orderService.GetById(id));
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            // I'm using Order class only for simplification. In a real application should be used a CartModel and make all modifications over the cart and only save changes when all
            // information required was completed.
            return View(new Order());
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(Order newOrder)
        {
            try
            {
                // I'm creating an Order without order items for simplification. In a real application it should be allowed.
                _orderService.InsertOrUpdate(newOrder);

                return RedirectToAction("AddItem", new { id = newOrder.OrderNumber});
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/AddItem/5
        public ActionResult AddItem(int id)
        {
            OrderItem newItem = new OrderItem() 
                                { 
                                    OrderNumber = id
                                };

            ViewBag.OrderItems = _orderService.GetById(id).OrderItems;
            return View(newItem);
        }

        // POST: Order/AddItem/5
        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <param name="addOther">The add other.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddItem(OrderItem newItem, bool addOther)
        {
            ActionResult returnValue = RedirectToAction("Index");
            try
            {
                Order orderSaved = _orderService.GetById(newItem.OrderNumber);
                orderSaved.AddNewItem(newItem);

                _orderService.InsertOrUpdate(orderSaved);
                if (addOther)
                    returnValue = RedirectToAction("AddItem", new { id = newItem.OrderNumber });
            }
            catch
            {
                returnValue = View();
            }

            return returnValue;
        }
    }
}
