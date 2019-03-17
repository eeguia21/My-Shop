using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {

        IRepository<Product> contextProduct;
        IRepository<ProductCategory> contextProductCategory;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            contextProduct = productContext;
            contextProductCategory = productCategoryContext;
        }

        public ActionResult Index()
        {
            List<Product> products = contextProduct.Collection().ToList();
            return View(products);
        }

        public ActionResult Details(string Id)
        {
            Product product = contextProduct.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}