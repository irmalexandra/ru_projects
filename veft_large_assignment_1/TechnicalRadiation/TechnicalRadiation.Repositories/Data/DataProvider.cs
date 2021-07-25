using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using TechnicalRadiation.Models.Entities;

namespace TechnicalRadiation.Models.Repositories.Data
{
    public static class DataProvider
    {
        private static readonly string _adminName = "TechnicalRadiationAdmin";

        public static List<Author> Authors = new List<Author>
        {
            new Author
            {
                Id = 1,
                Name = "Arnar Leifsson",
                ProfileImgSource = "https://imgur.com/a/k97qxjk",
                Bio = "I am definitely NOT Arnar Ingi Gunnarsson, We are not the exact same being, a result of a " +
                      "cloning experiment done by HR back in 1994. Check my profile picture for evidence",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            },
            new Author
            {
                Id = 5,
                Name = "Rikharður Friðgeirsson",
                ProfileImgSource = "https://allthatsinteresting.com/white-jesus",
                Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            },
            new Author
            {
                Id = 2,
                Name = "Emil Örn Kristjánsson",
                ProfileImgSource = "https://allthatsinteresting.com/white-jesus",
                Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            },
            new Author
            {
                Id = 3,
                Name = "Loki Alexander Hopkins",
                ProfileImgSource = "https://allthatsinteresting.com/white-jesus",
                Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            },
            new Author
            {
                Id = 4,
                Name = "Ingi",
                ProfileImgSource = "https://allthatsinteresting.com/white-jesus",
                Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            }

        };

        public static List<Category> Categories = new List<Category>
        {
            new Category
            {
                Id = 1,
                Name = "Personal Computers",
                Slug = "personal-computers",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            },
            new Category
            {
                Id = 2,
                Name = "Consoles",
                Slug = "consoles",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            },
            new Category
            {
                Id = 3,
                Name = "Cyborgs or Robots?",
                Slug = "cyborgs-or-robots?",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            },
            new Category
            {
                Id = 4,
                Name = "The Difficult Task of Working With Emil And Rikki",
                Slug = "the-difficult-task-of-working-with-emil-and-rikki",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            }
        };

        public static List<NewsItem> NewsItems = new List<NewsItem>
        {
            new NewsItem
            {
                Id = 1,
                Title = "When the world seems to shine like you've had too much wine",
                ImgSource = "https://www.youtube.com/watch?v=OnFlx2Lnr9Q&ab_channel=NMCatalogue",
                ShortDescription = "That's amore!",
                LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                PublishDate = DateTime.Now,
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new NewsItem
                
            {
                Id = 2,
                Title = "When you dance down the street with a cloud at your feet",
                ImgSource = "https://www.youtube.com/watch?v=OnFlx2Lnr9Q&ab_channel=NMCatalogue",
                ShortDescription = "You're in love!",
                LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                PublishDate = DateTime.Now.AddDays(-7),
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new NewsItem
                
            {
                Id = 3,
                Title = "Bells will ring ting-a-ling-a-ling, ting-a-ling-a-ling, and you'll sing",
                ImgSource = "https://www.youtube.com/watch?v=OnFlx2Lnr9Q&ab_channel=NMCatalogue",
                ShortDescription = "Vita bella!",
                LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                PublishDate = DateTime.Now.AddDays(-2),
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new NewsItem
                
            {
                Id = 4,
                Title = "Hearts will play tippy-tippy-tay, tippy-tippy-tay like a gay tarantella",
                ImgSource = "https://www.youtube.com/watch?v=OnFlx2Lnr9Q&ab_channel=NMCatalogue",
                ShortDescription = "That's amore!",
                LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                PublishDate = DateTime.Now.AddDays(-3),
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Now,    
                ModifiedDate = DateTime.Now
            },
            new NewsItem
            {
                Id = 5,
                Title = "When the stars make you drool just like a pasta e fazool",
                ImgSource = "https://www.youtube.com/watch?v=OnFlx2Lnr9Q&ab_channel=NMCatalogue",
                ShortDescription = "That's amore!",
                LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                PublishDate = DateTime.Now.AddDays(-4),
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new NewsItem
            {
                Id = 6,
                Title = "When you dance down the street with a cloud at your feet",
                ImgSource = "https://www.youtube.com/watch?v=OnFlx2Lnr9Q&ab_channel=NMCatalogue",
                ShortDescription = "That's amore!",
                LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                PublishDate = DateTime.Now.AddDays(-10),
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new NewsItem
            {
                Id = 7,
                Title = "When you walk in a dream but you know you're not dreaming signore",
                ImgSource = "https://www.youtube.com/watch?v=OnFlx2Lnr9Q&ab_channel=NMCatalogue",
                ShortDescription = "That's amore!",
                LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                PublishDate = DateTime.Now.AddDays(-11),
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new NewsItem
            {
                Id = 8,
                Title = "Scuzzi me, but you see, back in old Napoli that's amore",
                ImgSource = "https://www.youtube.com/watch?v=OnFlx2Lnr9Q&ab_channel=NMCatalogue",
                ShortDescription = "That's amore!",
                LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                PublishDate = DateTime.Now.AddDays(-20),
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new NewsItem
            {
                Id = 9,
                Title = "When the moon hits your eye like a big pizza pie",
                ImgSource = "https://www.youtube.com/watch?v=OnFlx2Lnr9Q&ab_channel=NMCatalogue",
                ShortDescription = "That's amore!",
                LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                PublishDate = DateTime.Now.AddDays(5),
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            }
        };

        public static List<NewsItemAuthors> NewsItemAuthors = new List<NewsItemAuthors>
        {
            new NewsItemAuthors
            {
                AuthorId = 1,
                NewsItemId = 1
            },
            new NewsItemAuthors
            {
                AuthorId = 3,
                NewsItemId = 2
            },
            new NewsItemAuthors
            {
                AuthorId = 3,
                NewsItemId = 5
            },
            new NewsItemAuthors
            {
                AuthorId = 3,
                NewsItemId = 3
            },
            new NewsItemAuthors
            {
                AuthorId = 2,
                NewsItemId = 4
            },
    
            new NewsItemAuthors
            {
                AuthorId = 1,
                NewsItemId = 6
            },
            new NewsItemAuthors
            {
                AuthorId = 3,
                NewsItemId = 6
            },
            new NewsItemAuthors
            {
                AuthorId = 2,
                NewsItemId = 6
            }
        };

        public static List<NewsItemCategories> NewsItemCategories = new List<NewsItemCategories>
        {
            new NewsItemCategories()
            {
                CategoryId = 1,
                NewsItemId = 1
            },
            new NewsItemCategories()
            {
                CategoryId = 3,
                NewsItemId = 2
            },
            new NewsItemCategories()
            {
                CategoryId = 2,
                NewsItemId = 2
            },
            new NewsItemCategories()
            {
                CategoryId = 4,
                NewsItemId = 2
            },
            new NewsItemCategories()
            {
                CategoryId = 4,
                NewsItemId = 1
            }
        };

    }
}