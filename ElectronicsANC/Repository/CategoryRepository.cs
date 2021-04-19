using ElectronicsANC.Models;
using ElectronicsANC.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Repository
{
    public class CategoryRepository
    {
        public ElectronicsDbDataContext dbContext;

        public CategoryRepository()
        {
            dbContext = new ElectronicsDbDataContext();
        }

        public CategoryRepository(ElectronicsDbDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<CategoryModel> GetAllCategories()
        {
            List<CategoryModel> categoryList = new List<CategoryModel>();

            foreach (Category dbCategory in dbContext.Categories)
                AddDbObjectToModelCollection(categoryList, dbCategory);

            return categoryList;
        }

        public CategoryModel GetCategoryById(Guid id)
        {
            var category = dbContext.Categories.FirstOrDefault(x => x.IdCategory == id);

            return MapDbObjectToModel(category);
        }

        public CategoryModel GetCategoryByName(string name)
        {
            var category = dbContext.Categories.FirstOrDefault(x => x.CategoryName.Equals(name));

            return MapDbObjectToModel(category);
        }

        public void InsertCategory(CategoryModel category)
        {
            category.IdCategory = Guid.NewGuid();

            dbContext.Categories.InsertOnSubmit(MapModelToDbObject(category));
            dbContext.SubmitChanges();
        }

        public void UpdateMember(CategoryModel category)
        {
            Category dbCategory = dbContext.Categories.FirstOrDefault(x => x.IdCategory == category.IdCategory);

            if(dbCategory != null)
            {
                dbCategory.IdCategory = category.IdCategory;
                dbCategory.CategoryName = category.CategoryName;
                dbContext.SubmitChanges();
            }
        }

        public void DeleteCategory(Guid id)
        {
            Category dbCategory = dbContext.Categories.FirstOrDefault(x => x.IdCategory == id);

            if(dbCategory != null)
            {
                dbContext.Categories.DeleteOnSubmit(dbCategory);
                dbContext.SubmitChanges();
            }
        }

        private void AddDbObjectToModelCollection(List<CategoryModel> categoryList, Category dbCategory)
        {
            categoryList.Add(MapDbObjectToModel(dbCategory));
        }

        private CategoryModel MapDbObjectToModel(Category dbCategory)
        {
            CategoryModel category = new CategoryModel();

            if(dbCategory != null)
            {
                category.IdCategory = dbCategory.IdCategory;
                category.CategoryName = dbCategory.CategoryName;

                return category;
            }

            return null;
        }

        private Category MapModelToDbObject(CategoryModel category)
        {
            Category dbCategory = new Category();

            if(category != null)
            {
                dbCategory.IdCategory = category.IdCategory;
                dbCategory.CategoryName = category.CategoryName;

                return dbCategory;
            }

            return null;
        }
    }
}