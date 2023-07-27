namespace CityPopulationAPI.Models
{
    // Model reprezentujący dane miasta
    public class City
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Population { get; set; }
        public string? Country { get; set; }
    }

    // Model reprezentujący dane miasta wraz z automatycznie uzupełnionym regionem
    public class CityDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Population { get; set; }
        public string? Country { get; set; }
        public string? Region { get; set; }
    }

    // Model reprezentujący relację kraj - region
    public class Region
    {
        public string? Country { get; set; }
        public string? City { get; set; }
    }
}