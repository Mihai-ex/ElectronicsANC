using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryRepository _categoryRepository = new CategoryRepository();

        // GET: Category
        public ActionResult Index()
        {
            List<CategoryModel> categories = _categoryRepository.GetAllCategories();

            return View("Index", categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(Guid id)
        {
            CategoryModel categoryModel = _categoryRepository.GetCategoryById(id);

            return View("DetailsCategory", categoryModel);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View("CreateCategory");
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                CategoryModel categoryModel = new CategoryModel();
                UpdateModel(categoryModel);

                _categoryRepository.InsertCategory(categoryModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateCategory");
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(Guid id)
        {
            CategoryModel categoryModel = _categoryRepository.GetCategoryById(id);

            return View("EditCategory", categoryModel);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                CategoryModel categoryModel = new CategoryModel();
                UpdateModel(categoryModel);

                _categoryRepository.UpdateMember(categoryModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditCategory");
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(Guid id)
        {
            CategoryModel categoryModel = _categoryRepository.GetCategoryById(id);

            return View("DeleteCategory", categoryModel);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                _categoryRepository.DeleteCategory(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteCategory");
            }
        }
    }
}
