using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Repositories.Data;

namespace TechnicalRadiation.Models.Repositories
{
    public class CategoryRepository
    {
        private static readonly string _adminName = "TechnicalRadiationAdmin";
        private CategoryDto ToCategoryDto (Category category)
        {
            if (category != null)
            {
                return new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Slug = category.Slug
                };
            }
            return null;
        }

        private CategoryDetailDto ToCategoryDetailDto(Category category)
        {
            if (category != null)
            {
                return new CategoryDetailDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Slug = category.Slug,
                    NumberOfNewsItems = DataProvider.NewsItemCategories.Count(c =>
                        c.CategoryId == category.Id)
                };
            }

            return null;

        }
        
        private Category ToCategoryItem(CategoryInputModel category, int id)
        {
 
            return new Category
            {
                Id = id,
                Name = category.Name,
                Slug = category.Name.ToLower().Replace(" ", "-"),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

        }
        
        private NewsItemCategories ToNewsItemCategory(int categoryId, int newsItemId)
        {
            return new NewsItemCategories
            {
                CategoryId = categoryId,
                NewsItemId = newsItemId    

            };
        }
        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var category = DataProvider.Categories.Select(c => ToCategoryDto(c));
            return category;
        }
        public CategoryDetailDto GetCategoryById(int id)
        {
            var category = DataProvider.Categories.FirstOrDefault(n => n.Id == id);
            return ToCategoryDetailDto(category);
            
        }
        public CategoryDto CreateNewCategory(CategoryInputModel categoryitem)
        {
            var nextId = DataProvider.Categories.Count()+1;

            var entity = ToCategoryItem(categoryitem, nextId);
            DataProvider.Categories.Add(entity);
            return ToCategoryDto(entity);

        }

        public bool UpdateCategoryById(CategoryInputModel category, int id)
        {
            Category oldCategory = DataProvider.Categories.FirstOrDefault(author => author.Id == id);
            if (oldCategory == null)
            {
                return false;
            }

            oldCategory.Name = category.Name;
            oldCategory.Slug = oldCategory.Name.ToLower().Replace(" ", "-");
            oldCategory.ModifiedBy = _adminName;
            oldCategory.ModifiedDate = DateTime.Now;
            return true;
        }

        public bool DeleteAuthorById(int id)
        {
            return DataProvider.Categories.Remove(DataProvider.Categories.FirstOrDefault(news => news.Id == id));
        }
        
        public IEnumerable<NewsItemCategories> GetCategoryByNewsItemId(int newsItemId)
        {
            return DataProvider.NewsItemCategories.Where(n => n.NewsItemId == newsItemId);
        }


        public bool CheckNewsItemCategoryRelation(int categoryId, int newsItemId)
        {
            return (from relations in DataProvider.NewsItemCategories
                where relations.NewsItemId == newsItemId && relations.CategoryId == categoryId
                select relations).FirstOrDefault() != null;
        }
        
        public NewsItemCategories CreateNewsItemCategory(int categoryId, int newsItemId)
        {
            var entity = ToNewsItemCategory(categoryId, newsItemId);
            DataProvider.NewsItemCategories.Add(entity);
            return entity;
        }
    }
}