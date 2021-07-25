using System.Dynamic;

namespace TechnicalRadiation.Models.Dtos
{
    public class NewsItemDto : HyperMediaModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }
        public string ShortDescription { get; set; }
        
        
        
        
    }
    
    
}