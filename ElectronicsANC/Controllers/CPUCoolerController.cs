using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class CPUCoolerController : Controller
    {
        private ProductRepository _productRepository = new ProductRepository();

        // GET: CPUCooler
        public ActionResult Index()
        {
            Guid id = new Guid("0AD3E33F-8839-4619-BCEF-CAF82D2D1496");
            List<ProductModel> productModels = _productRepository.GetAllProductsByCategoryId(id);

            return View("Index", productModels);
        }

        // GET: CPUCooler/Details/5
        public ActionResult Details(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);

            return View("DetailsCPUCooler", productModel);
        }
    }
}
