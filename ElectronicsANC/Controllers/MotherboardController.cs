using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class MotherboardController : Controller
    {
        private ProductRepository _productRepository = new ProductRepository();

        // GET: Motherboard
        public ActionResult Index(string sortOrder, string filter, string name)
        {
            Guid id = new Guid("c9ff7c9f-21bd-44f9-a188-b391cbf9d35a");
            List<ProductModel> productModels = _productRepository.GetAllProductsByCategoryId(id);

            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ManufacturerSortParam = sortOrder == "manufacturer" ? "manufacturer_desc" : "manufacturer";
            ViewBag.PriceSortParam = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.WarrantySortParam = sortOrder == "warranty" ? "warranty_desc" : "warranty";
            ViewBag.RatingSortParam = sortOrder == "rating" ? "rating_desc" : "rating";

            productModels = FilterProducts(filter, id, productModels);
            productModels = FilterByName(name, id, productModels);
            productModels = SortProducts(productModels, sortOrder);

            return View("Index", productModels);
        }

        private List<ProductModel> FilterProducts(string filter, Guid id, List<ProductModel> productModels)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                switch (filter)
                {
                    case "MSI":
                        productModels = _productRepository.GetProductsByManufacturer(filter, id);
                        break;
                    case "Asus":
                        productModels = _productRepository.GetProductsByManufacturer(filter, id);
                        break;
                    case "ASRock":
                        productModels = _productRepository.GetProductsByManufacturer(filter, id);
                        break;
                    case "Gigabyte":
                        productModels = _productRepository.GetProductsByManufacturer(filter, id);
                        break;
                    default:
                        productModels = _productRepository.GetAllProductsByCategoryId(id);
                        break;
                }
            }

            return productModels;
        }

        private List<ProductModel> FilterByName(string filter, Guid id, List<ProductModel> productModels)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                productModels = _productRepository.GetProductsByNameWithCategoryID(filter, id);
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

        // GET: Motherboard/Details/5
        public ActionResult Details(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);

            return View("DetailsMotherboard", productModel);
        }

        //public ActionResult Create()
        //{
        //    return View("CreateToCart");
        //}

        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        ProductModel productModel = new ProductModel();
        //        productModel.IdCategory = Guid.Parse(Request.Form["Category"]);
        //        UpdateModel(productModel);

        //        _productRepository.InsertProduct(productModel);

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View("CreateToCart");
        //    }
        //}
    }
}
