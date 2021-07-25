using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Repositories.Data;

namespace TechnicalRadiation.Models.Repositories
{
    public class AuthorRepository
    {
        private static readonly string _adminName = "TechnicalRadiationAdmin";
        private AuthorDto ToAuthorDto (Author author)
        {
            if (author != null)
            {
                return new AuthorDto()
                {
                    Id = author.Id,
                    Name = author.Name,
                };
            }

            return null;
        }

        private AuthorDetailDto ToAuthorDetailDto(Author author)
        {
            if (author != null)
            {
                return new AuthorDetailDto()
                {
                    Id = author.Id,
                    Name = author.Name,
                    Bio = author.Bio,
                    ProfileImageSource = author.ProfileImgSource
                };
            }

            return null;

        }
        
        private Author ToAuthor(AuthorInputModel author, int id)
        {
            return new Author
            {
                Id = id,
                Name = author.Name,
                Bio = author.Bio,
                ProfileImgSource = author.ProfileImgSource,
                
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

        }

        private NewsItemAuthors ToNewsItemAuthor(int authorId, int newsItemId)
        {
            return new NewsItemAuthors
            {
                AuthorId = authorId,
                NewsItemId = newsItemId    

            };
        }

        public bool CheckNewsItemAuthorRelation(int authorId, int newsItemId)
        {
            return (from relations in DataProvider.NewsItemAuthors
                where relations.NewsItemId == newsItemId && relations.AuthorId == authorId
                select relations).FirstOrDefault() != null;
        }
        
        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            var author = DataProvider.Authors.Select(a => ToAuthorDto(a));
            return author;
        }
        
        public AuthorDetailDto GetAuthorById(int id)
        {
            var author = DataProvider.Authors.FirstOrDefault(a => a.Id == id);
            return ToAuthorDetailDto(author);
        }

        public AuthorDto CreateAuthor(AuthorInputModel author)
        {
            var nextId = DataProvider.Authors.Count()+1;
            var entity = ToAuthor(author, nextId);
            DataProvider.Authors.Add(entity);
            return ToAuthorDto(entity);

        }

        public NewsItemAuthors CreateNewsItemAuthor(int authorId, int newsItemId)
        {
            var entity = ToNewsItemAuthor(authorId, newsItemId);
            DataProvider.NewsItemAuthors.Add(entity);
            return entity;
        }

        public bool UpdateAuthorById(AuthorInputModel newAuthor, int id)
        {
            Author oldAuthor = DataProvider.Authors.FirstOrDefault(author => author.Id == id);
            if (oldAuthor == null)
            {
                return false;
            }

            oldAuthor.Name = newAuthor.Name;
            oldAuthor.Bio = newAuthor.Bio;
            oldAuthor.ProfileImgSource = newAuthor.ProfileImgSource;
            oldAuthor.ModifiedBy = _adminName;
            oldAuthor.ModifiedDate = DateTime.Now;
            return true;
        }

        public bool DeleteAuthorById(int id)
        {
            return DataProvider.Authors.Remove(DataProvider.Authors.FirstOrDefault(news => news.Id == id));
        }

        public IEnumerable<NewsItemAuthors> GetAuthorsByNewsItemId(int newsItemId)
        {
            return DataProvider.NewsItemAuthors.Where(n => n.NewsItemId == newsItemId);
        }
    }
}