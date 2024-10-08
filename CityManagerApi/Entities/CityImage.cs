﻿namespace CityManagerApi.Entities
{
    public class CityImage
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public bool IsMain { get; set; }
        //
        public City? City { get; set; }
        public int CityId { get; set; }
    }
}
