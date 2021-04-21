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
        public ActionResult Index()
        {
            Guid id = new Guid("BDCDFA08-7874-4140-B619-829657393DBD");
            List<ProductModel> productModels = _productRepository.GetAllProductsByCategoryId(id);

            return View("Index", productModels);
        }

        // GET: Memory/Details/5
        public ActionResult Details(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);

            return View("DetailsMemory", productModel);
        }
    }
}
