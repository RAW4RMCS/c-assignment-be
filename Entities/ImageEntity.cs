namespace AccountApi.Entities
{
    public class ImageEntity: BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
