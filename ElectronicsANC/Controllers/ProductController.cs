﻿using ElectronicsANC.Models;
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
        public ActionResult Create()
        {
            return View("CreateProduct");
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ProductModel productModel = new ProductModel();
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
        public ActionResult Edit(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);

            return View("EditProduct", productModel);
        }

        // POST: Product/Edit/5
        [HttpPost]
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
        public ActionResult Delete(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);

            return View("DeleteProduct", productModel);
        }

        // POST: Product/Delete/5
        [HttpPost]
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