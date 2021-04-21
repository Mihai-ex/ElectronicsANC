using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class CPUController : Controller
    {
        private ProductRepository _productRepository = new ProductRepository();

        // GET: CPU
        public ActionResult Index()
        {
            Guid id = new Guid("79F0BF31-EF5B-4D26-B551-2E4C2A033B5A");
            List<ProductModel> productModels = _productRepository.GetAllProductsByCategoryId(id);

            return View("Index", productModels);
        }

        // GET: CPU/Details/5
        public ActionResult Details(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);

            return View("DetailsCPU", productModel);
        }
    }
}
