namespace CityManagerApi.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        //
        public User? User { get; set; }
        public int UserId { get; set; }
        //one -to many
        public ICollection<CityImage>? CityImages { get; set; }

    }
}
