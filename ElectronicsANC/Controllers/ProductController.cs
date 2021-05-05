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
        public ActionResult Index(string sortOrder, string filter)
        {
            List<ProductModel> products = _productRepository.GetAllProducts();

            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ManufacturerSortParam = sortOrder == "manufacturer" ? "manufacturer_desc" : "manufacturer";
            ViewBag.PriceSortParam = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.WarrantySortParam = sortOrder == "warranty" ? "warranty_desc" : "warranty";
            ViewBag.RatingSortParam = sortOrder == "rating" ? "rating_desc" : "rating";

            products = FilterProducts(filter, ref products);
            products = SortProducts(products, sortOrder);

            return View("Index", products);
        }

        // GET: Product/Details/5
        public ActionResult Details(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);

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

        private List<ProductModel> FilterProducts(string filter, ref List<ProductModel> productModels)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                switch (filter)
                {
                    case "Corsair":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Lian":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "NZXT":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Phanteks":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Intel":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "AMD":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "be quiet!":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Cooler Master":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "MSI":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Noctua":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Thermaltake":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "ADATA":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Crucial":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "G.Skill":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Kingston":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Patriot":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Team":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Asus":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "ASRock":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Gigabyte":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "EVGA":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Angelbird":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Samsung":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Seagate":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "Western Digital":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "NVIDIA":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    default:
                        productModels = _productRepository.GetAllProducts();
                        break;
                }
            }

            return productModels;
        }

        private List<ProductModel> SortProducts(List<ProductModel> temp, string sortOrder)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    return _productRepository.OrderByDescendingParameter(temp, "Name");
                case "manufacturer_desc":
                    return _productRepository.OrderByDescendingParameter(temp, "Manufacturer");
                case "price_desc":
                    return _productRepository.OrderByDescendingParameter(temp, "Price");
                case "warranty_desc":
                    return _productRepository.OrderByDescendingParameter(temp, "Warranty");
                case "rating_desc":
                    return _productRepository.OrderByDescendingParameter(temp, "Rating");
                case "manufacturer":
                    return _productRepository.OrderByAscendingParameter(temp, "Manufacturer");
                case "price":
                    return _productRepository.OrderByAscendingParameter(temp, "Price");
                case "warranty":
                    return _productRepository.OrderByAscendingParameter(temp, "Warranty");
                case "rating":
                    return _productRepository.OrderByAscendingParameter(temp, "Rating");
                default:
                    return _productRepository.OrderByAscendingParameter(temp, "Name");
            }
        }
    }
}
