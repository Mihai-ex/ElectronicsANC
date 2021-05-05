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
        public ActionResult Index(string sortOrder, string filter)
        {
            List<CategoryModel> categories = _categoryRepository.GetAllCategories();

            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            categories = FilterCategories(filter, ref categories);
            categories = SortCategories(categories, sortOrder);

            return View("Index", categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(Guid id)
        {
            CategoryModel categoryModel = _categoryRepository.GetCategoryById(id);

            return View("DetailsCategory", categoryModel);
        }

        // GET: Category/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View("CreateCategory");
        }

        // POST: Category/Create
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            CategoryModel categoryModel = _categoryRepository.GetCategoryById(id);

            return View("EditCategory", categoryModel);
        }

        // POST: Category/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                CategoryModel categoryModel = new CategoryModel();
                UpdateModel(categoryModel);

                _categoryRepository.UpdateCategory(categoryModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditCategory");
            }
        }

        // GET: Category/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            CategoryModel categoryModel = _categoryRepository.GetCategoryById(id);

            return View("DeleteCategory", categoryModel);
        }

        // POST: Category/Delete/5
        [Authorize(Roles = "Admin")]
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

        private List<CategoryModel> FilterCategories(string filter, ref List<CategoryModel> categoryModels)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                categoryModels = _categoryRepository.GetCategoriesByName(filter);
            }

            return categoryModels;
        }

        private List<CategoryModel> SortCategories(List<CategoryModel> temp, string sortOrder)
        {
            if (sortOrder == "name_desc")
                return _categoryRepository.OrderByDescendingParam(temp, "Name");
            else
                return _categoryRepository.OrderByAscendingParameter(temp, "Name");
        }
    }
}
