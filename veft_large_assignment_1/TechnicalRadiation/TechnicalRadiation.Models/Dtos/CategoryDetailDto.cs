namespace TechnicalRadiation.Models.Dtos
{
    public class CategoryDetailDto : HyperMediaModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int NumberOfNewsItems { get; set; }
    }
    
    
}