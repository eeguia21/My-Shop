using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.Core.ViewModels;
using MyShop.Core.Contracts;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> contextProduct;
        IRepository<ProductCategory> contextProductCategory;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            contextProduct = productContext;
            contextProductCategory = productCategoryContext;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = contextProduct.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {

            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();
            viewModel.ProductCategories = contextProductCategory.Collection();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                contextProduct.Insert(product);
                contextProduct.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = contextProduct.Find(Id);

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = contextProductCategory.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = contextProduct.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Name = product.Name;
                productToEdit.Description = product.Description;
                productToEdit.Category = product.Category;
                productToEdit.Price = product.Price;
                productToEdit.Image = product.Image;

                contextProduct.Update(productToEdit);
                contextProduct.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = contextProduct.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = contextProduct.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                contextProduct.Delete(Id);
                contextProduct.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}