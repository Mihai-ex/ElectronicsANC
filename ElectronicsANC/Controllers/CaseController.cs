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

        // GET: Case
        public ActionResult Index()
        {
            Guid id = new Guid("0B41BE18-21BB-42B1-AADA-FDAF9F66DD69");
            List<ProductModel> productModels = _productRepository.GetAllProductsByCategoryId(id);

            return View("Index", productModels);
        }

        // GET: Case/Details/5
        public ActionResult Details(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);

            return View("DetailsCase", productModel);
        }
    }
}
