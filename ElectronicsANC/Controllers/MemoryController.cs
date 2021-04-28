using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class MemoryController : Controller
    {
        private ProductRepository _productRepository = new ProductRepository();

        // GET: Memory
        public ActionResult Index(string sortOrder, string filter)
        {
            Guid id = new Guid("BDCDFA08-7874-4140-B619-829657393DBD");
            List<ProductModel> productModels = _productRepository.GetAllProductsByCategoryId(id);

            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ManufacturerSortParam = sortOrder == "manufacturer" ? "manufacturer_desc" : "manufacturer";
            ViewBag.PriceSortParam = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.WarrantySortParam = sortOrder == "warranty" ? "warranty_desc" : "warranty";
            ViewBag.RatingSortParam = sortOrder == "rating" ? "rating_desc" : "rating";

            productModels = FilterProducts(filter, id, productModels);
            productModels = SortProducts(productModels, sortOrder, id);

            return View("Index", productModels);
        }

        // GET: Memory/Details/5
        public ActionResult Details(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);

            return View("DetailsMemory", productModel);
        }
        private List<ProductModel> FilterProducts(string filter, Guid id, List<ProductModel> productModels)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                switch (filter)
                {
                    case "MSI":
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
                    default:
                        productModels = _productRepository.GetAllProductsByCategoryId(id);
                        break;
                }
            }

            return productModels;
        }

        private List<ProductModel> SortProducts(List<ProductModel> temp, string sortOrder, Guid id)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    return _productRepository.OrderByDescendingParameterWithID(temp, "Name", id);
                case "manufacturer_desc":
                    return _productRepository.OrderByDescendingParameterWithID(temp, "Manufacturer", id);
                case "price_desc":
                    return _productRepository.OrderByDescendingParameterWithID(temp, "Price", id);
                case "warranty_desc":
                    return _productRepository.OrderByDescendingParameterWithID(temp, "Warranty", id);
                case "rating_desc":
                    return _productRepository.OrderByDescendingParameterWithID(temp, "Rating", id);
                case "manufacturer":
                    return _productRepository.OrderByAscendingParameterWithID(temp, "Manufacturer", id);
                case "price":
                    return _productRepository.OrderByAscendingParameterWithID(temp, "Price", id);
                case "warranty":
                    return _productRepository.OrderByAscendingParameterWithID(temp, "Warranty", id);
                case "rating":
                    return _productRepository.OrderByAscendingParameterWithID(temp, "Rating", id);
                default:
                    return _productRepository.OrderByAscendingParameterWithID(temp, "Name", id);
            }
        }
    }
}
