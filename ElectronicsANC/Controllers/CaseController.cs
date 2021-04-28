using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class CaseController : Controller
    {
        private ProductRepository _productRepository = new ProductRepository();
        private MemberRepository _memberRepository = new MemberRepository();

        // GET: Case
        public ActionResult Index(string sortOrder, string filter)
        {
            Guid id = new Guid("0B41BE18-21BB-42B1-AADA-FDAF9F66DD69");
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

        // GET: Case/Details/5
        public ActionResult Details(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);
            var members = _memberRepository.GetAllMembers();

            if (members != null)
            {
                ViewBag.members = members;
            }

            return View("DetailsCase", productModel);
        }

        private List<ProductModel> FilterProducts(string filter, Guid id, List<ProductModel> productModels)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                switch (filter)
                {
                    case "Intel":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    case "AMD":
                        productModels = _productRepository.GetProductsByManufacturer(filter);
                        break;
                    default:
                        productModels = _productRepository.GetAllProductsByCategoryId(id);
                        break;
                }

                //_productRepository.SaveFilter(productModels, filter);
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
