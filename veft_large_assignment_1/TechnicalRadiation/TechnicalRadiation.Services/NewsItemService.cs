using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Exceptions;
using TechnicalRadiation.Models.HyperMedia;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Repositories;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;


namespace TechnicalRadiation.Models.Services
{
    public class NewsItemService
    {
        private NewsItemRepository _newsItemRepository;
        private AuthorRepository _authorRepository;
        private CategoryRepository _categoryRepository;
        
        public NewsItemService() // Constructor
        {
            _newsItemRepository = new NewsItemRepository();
            _authorRepository = new AuthorRepository();
            _categoryRepository = new CategoryRepository();
            
        }

        private void AddLinksToNewsItems(HyperMediaModel n, int id )
        {
   
                n.Links.AddReference("self", new {href = $"/api/{id}"});
                n.Links.AddReference("edit", new {href = $"/api/{id}"});
                n.Links.AddReference("delete", new {href = $"/api/{id}"});
                n.Links.AddListReference("authors",
                    _authorRepository.GetAuthorsByNewsItemId(id).Select(a => 
                        new {href = $"/api/authors/{a.AuthorId}"}));
                n.Links.AddListReference("categories", _categoryRepository.GetCategoryByNewsItemId(id).Select(c => 
                    new {href = $"/api/categories/{c.CategoryId}"}));
            }


        public IEnumerable GetAllNewsItems(int pageSize, int pageNumber)
        {
             
            var news = _newsItemRepository.GetAllNewsItems();
            var newsSize = news.Count();
            if ((newsSize / pageSize) < pageNumber)
            {
                throw new ArgumentOutOfRangeException("Invalid page number.");
            } 
            var newsEnvelope = new Envelope<NewsItemDto>(pageNumber, pageSize, news);
            if (newsEnvelope == null)
            {
                throw new ResourceNotFoundException("No news items were found.");
            }
            

            List<NewsItemDto> returnList = new List<NewsItemDto>();
            foreach (var newsItem in newsEnvelope.Items.ToList())
            {
                AddLinksToNewsItems(newsItem, newsItem.Id);
                returnList.Add(newsItem);
            }
            return returnList;
        }
        
        public NewsItemDetailDto GetNewsItemById(int id)
        {
            var news = _newsItemRepository.GetNewsItemById(id);
            if (news == null)
            {
                throw new ResourceNotFoundException($"News item with id {id} was not found. ");
            }
            AddLinksToNewsItems(news, news.Id);
            
            return news;
        }

        public IEnumerable<NewsItemDto> GetNewsByAuthor(int id)
        {
            var news = _newsItemRepository.GetNewsItemsByAuthorId(id).ToList();
            if (news == null)
            {
                throw new ResourceNotFoundException($"News item with id {id} was not found. ");
                
            }
            news.ForEach(n =>
            {
                AddLinksToNewsItems(n, n.Id);
            });
            return news;
        }

        public NewsItemDto CreateNewsItem(NewsItemsInputModel newsItem)
        {
            
            return _newsItemRepository.CreateNewsItem(newsItem);
        }

        public bool UpdateNewsItemById(NewsItemsInputModel newsitem, int id)
        {
            return _newsItemRepository.UpdateNewsItemById(newsitem, id);
        }

        public bool DeleteNewsItemById(in int id)
        {
            return _newsItemRepository.DeleteNewsItemById(id);
        }
    }
}