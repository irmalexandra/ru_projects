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
    public class AuthorService
    {
        private AuthorRepository _authorRepository;
        private NewsItemRepository _newsItemRepository;

        private void AddLinksToAuthorDto(HyperMediaModel a, int Id)
        {
            a.Links.AddReference("self" , new {href = $"/api/authors/{Id}"});
            a.Links.AddReference("edit" , new {href = $"/api/authors/{Id}"});
            a.Links.AddReference("delete" , new {href = $"/api/authors/{Id}"});
            a.Links.AddReference("newsItems" , new {href = $"/api/authors/{Id}/newsItems"});
            a.Links.AddListReference("newsItemsDetailed",
                _newsItemRepository.GetNewsItemsByAuthorId(Id).Select(n => new {href = $"/api/{n.Id}"}));
        }

        public AuthorService() // Constructor
        {
            
            _authorRepository = new AuthorRepository();  // instance of class
            _newsItemRepository = new NewsItemRepository();
        }
        
        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            var authors = _authorRepository.GetAllAuthors().ToList();
            authors.ForEach(author =>
            {
                AddLinksToAuthorDto(author, author.Id);
            });
            if (authors == null)
            {
                throw new ResourceNotFoundException("No authors were found.");
            }
            return authors;
        }
        
        public AuthorDetailDto GetAuthorById(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null)
            {
                throw new ResourceNotFoundException($"Author with id {id} does not exist.");
            }
            AddLinksToAuthorDto(author, author.Id);
            return author;
        }

        public AuthorDto CreateAuthor(AuthorInputModel author)
        {
            return _authorRepository.CreateAuthor(author);
        }

        public NewsItemAuthors CreateNewsItemAuthor(int authorId, int newsItemId)
        {
            if (_authorRepository.GetAuthorById(authorId) != null)
            {
                if (_newsItemRepository.GetNewsItemById(newsItemId) != null)
                {
                    if(!_authorRepository.CheckNewsItemAuthorRelation(authorId, newsItemId))
                    {
                       
                        return _authorRepository.CreateNewsItemAuthor(authorId, newsItemId);
                       
                    }
                    throw new ResourceAlreadyExistsException();
                }
                throw new ResourceNotFoundException($"News item with id {newsItemId} was not found.");
            }
            throw new ResourceNotFoundException($"Author with id {authorId} was not found.");

        }

        public bool UpdateAuthorById(AuthorInputModel author, int id)
        {
            return _authorRepository.UpdateAuthorById(author, id);
        }

        public bool DeleteAuthorById(int id)
        {
            return _authorRepository.DeleteAuthorById(id);
        }


    }
}