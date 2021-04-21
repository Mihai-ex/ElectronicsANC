using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class VideoCardController : Controller
    {
        private ProductRepository _productRepository = new ProductRepository();

        // GET: VideoCard
        public ActionResult Index()
        {
            Guid id = new Guid("5D453969-24EE-4F39-AE2B-A14D6620C7AF");
            List<ProductModel> productModels = _productRepository.GetAllProductsByCategoryId(id);

            return View("Index", productModels);
        }

        // GET: VideoCard/Details/5
        public ActionResult Details(Guid id)
        {
            ProductModel productModel = _productRepository.GetProdctById(id);

            return View("DetailsVideoCard", productModel);
        }
    }
}
