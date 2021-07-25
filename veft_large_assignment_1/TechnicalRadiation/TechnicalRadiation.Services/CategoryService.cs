using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.Exceptions;
using TechnicalRadiation.Models.HyperMedia;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Repositories;

namespace TechnicalRadiation.Models.Services
{
    public class CategoryService
    {
        private CategoryRepository _categoryRepository;
        private NewsItemRepository _newsItemRepository;
        
        public CategoryService() // Constructor
        {
            _categoryRepository = new CategoryRepository();  // instance of class
            _newsItemRepository = new NewsItemRepository();
        }

        private void AddLinksToCategory(HyperMediaModel category, int id)
        {
            category.Links.AddReference("self", new {href = $"/api/categories/{id})"});
            category.Links.AddReference("edit", new {href = $"/api/categories/{id})"});
            category.Links.AddReference("delete", new {href = $"/api/categories/{id})"});
        }
        
        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var categories = _categoryRepository.GetAllCategories().ToList();
            if (categories == null)
            {
                throw new ResourceNotFoundException("No categories were found.");
            }
            categories.ForEach(c =>
            {
                AddLinksToCategory(c, c.Id);
            });
            return categories;
        }
        
        public CategoryDetailDto GetCategoryById(int id)
        {
            
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                throw new ResourceNotFoundException($"Category with id {id} was not found.");
            }
            AddLinksToCategory(category, category.Id);
            return category;
        }

        public CategoryDto CreateCategory(CategoryInputModel category)
        {
            return _categoryRepository.CreateNewCategory(category);
        }

        public bool UpdateCategoryById(CategoryInputModel category, int id)
        {
            return _categoryRepository.UpdateCategoryById(category, id);
        }

        public bool DeleteCategoryById(in int id)
        {
            return _categoryRepository.DeleteAuthorById(id);
        }

        public NewsItemCategories CreateNewsItemCategory(int categoryId,int newsItemId)
        {
            if (_categoryRepository.GetCategoryById(categoryId) != null)
            {
                if (_newsItemRepository.GetNewsItemById(newsItemId) != null)
                {
                    if(!_categoryRepository.CheckNewsItemCategoryRelation(categoryId, newsItemId))
                    {
                       
                        return _categoryRepository.CreateNewsItemCategory(categoryId, newsItemId);
                       
                    }
                    throw new ResourceAlreadyExistsException();
                }
                throw new ResourceNotFoundException($"News item with id {newsItemId} was not found.");
            }
          throw new ResourceNotFoundException($"Category with id {categoryId} was not found.");
            
        }
    }
}