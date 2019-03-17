using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

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

        public ActionResult Index(string Category=null)
        {
            List<Product> products;
            List<ProductCategory> productCategories = contextProductCategory.Collection().ToList();

            if (Category == null)
            {
                products = contextProduct.Collection().ToList();
            }
            else
            {
                products = contextProduct.Collection().Where(p => p.Category == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = productCategories;

            return View(model);
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