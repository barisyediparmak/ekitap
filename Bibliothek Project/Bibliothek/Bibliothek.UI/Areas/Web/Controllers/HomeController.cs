using Bibliothek.DAL.Context;
using Bibliothek.Model.Entity;
using Bibliothek.Service.Option;
using Bibliothek.UI.Areas.Web.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Bibliothek.UI.Areas.Web.Models;

namespace Bibliothek.UI.Areas.Web.Controllers
{
    public class HomeController : Controller
    {
        CategoryService _categoryService;
        CustomerService _customerService;
        ProductService _productService;
        OrderService _orderService;
        OrderDetailService _orderDetailService;
        

        public HomeController()
        {
            _categoryService = new CategoryService();
            _customerService = new CustomerService();
            _productService = new ProductService();
            _orderService = new OrderService();
            _orderDetailService = new OrderDetailService();
        }

        public ActionResult Index()
        {
            List<Product> productList = _productService.GetActive().ToList();
            ViewBag.RandomProducts = (from p in productList orderby Guid.NewGuid() select p).Take(9).ToList();

            return View();
        }

        public ActionResult Category()
        {
            List<Category> model = _categoryService.GetActive();
            return PartialView("_Category", model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM data)
        {
            if (ModelState.IsValid)
            {
                using (ProjectContext db = new ProjectContext())
                {
                    var user = db.Customers.FirstOrDefault(x => x.Email == data.Email && x.Password == data.Password);

                    if (user != null)
                    {
                        Session["login"] = user;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mail adresi veya şifre hatalı!");
                    }
                }
            }

            return View(data);
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home", new { Area = "Web" });
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM data, Customer model)
        {
            if (ModelState.IsValid)
            {
                using (ProjectContext db = new ProjectContext())
                {
                    var customerEmail = db.Customers.FirstOrDefault(x => x.Email == data.Email);

                    if (customerEmail == null)
                    {
                        _customerService.Add(model);
                        Session["login"] = model;
                        return RedirectToAction("RegisterOk");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mail adresi sistemde kayıtlı.");
                    }
                }
            }
            return View();
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

       
        public ActionResult MyOrders()
        {
            List<Order> model = _orderService.ListOrderHistory();
            Customer customer = null;
            if (Session["login"] != null)
            {
                customer = Session["login"] as Customer;
            }
            return View(model);
        }

        public ActionResult MyOrderDetails(Guid id)
        {
            List<OrderDetail> model = _orderDetailService.GetDefault(x => x.Order.ID == id);
            return View(model);
        }
    }
}