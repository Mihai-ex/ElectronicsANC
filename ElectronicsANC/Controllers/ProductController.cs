using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository _productRepository = new ProductRepository();
        CategoryRepository _categoryRepository = new CategoryRepository();

        // GET: Product
        public ActionResult Index()
        {
            List<ProductModel> products = _productRepository.GetAllProducts();

            return View("Index", products);
        }

        // GET: Product/Details/5
        public ActionResult Details(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);
            //ProductModel productModelByCategory = _productRepository.GetProductByCategoryId(id);

            return View("DetailsProduct", productModel);
        }

        // GET: Product/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var items = _categoryRepository.GetAllCategories();
            if (items != null)
            {
                ViewBag.data = items;
            }

            return View("CreateProduct");
        }

        // POST: Product/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ProductModel productModel = new ProductModel();
                productModel.IdCategory =  Guid.Parse(Request.Form["Category"]);
                UpdateModel(productModel);

                _productRepository.InsertProduct(productModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateProduct");
            }
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);

            return View("EditProduct", productModel);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                ProductModel productModel = new ProductModel();
                UpdateModel(productModel);

                _productRepository.UpdateProduct(productModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditProduct");
            }
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);

            return View("DeleteProduct", productModel);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                _productRepository.DeleteProduct(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteProduct");
            }
        }
    }
}
