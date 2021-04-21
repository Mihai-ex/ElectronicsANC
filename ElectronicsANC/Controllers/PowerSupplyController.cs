using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class PowerSupplyController : Controller
    {
        private ProductRepository _productRepository = new ProductRepository();

        // GET: PowerSupply
        public ActionResult Index()
        {
            Guid id = new Guid("0EAD7EF7-BC34-4E5E-B1AD-C42F19966387");
            List<ProductModel> productModels = _productRepository.GetAllProductsByCategoryId(id);

            return View("Index", productModels);
        }

        // GET: PowerSupply/Details/5
        public ActionResult Details(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);

            return View("DetailsPowerSupply", productModel);
        }
    }
}
