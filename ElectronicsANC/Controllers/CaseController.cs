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
        public ActionResult Index(string sortOrder, string filter, string name)
        {
            Guid id = new Guid("0B41BE18-21BB-42B1-AADA-FDAF9F66DD69");
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
                    case "Corsair":
                        productModels = _productRepository.GetProductsByManufacturer(filter, id);
                        break;
                    case "Lian":
                        productModels = _productRepository.GetProductsByManufacturer(filter, id);
                        break;
                    case "NZXT":
                        productModels = _productRepository.GetProductsByManufacturer(filter, id);
                        break;
                    case "Phanteks":
                        productModels = _productRepository.GetProductsByManufacturer(filter, id);
                        break;
                    default:
                        productModels = _productRepository.GetAllProductsByCategoryId(id);
                        break;
                }

                //_productRepository.SaveFilter(productModels, filter);
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
    }
}
