using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class StorageController : Controller
    {
        private ProductRepository _productRepository = new ProductRepository();

        // GET: Storage
        public ActionResult Index()
        {
            Guid id = new Guid("D40D7133-727C-4BE5-98A3-96AB74B07D7F");
            List<ProductModel> productModels = _productRepository.GetAllProductsByCategoryId(id);

            return View("Index", productModels);
        }

        // GET: Storage/Details/5
        public ActionResult Details(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);

            return View("DetailsStorage", productModel);
        }
    }
}
