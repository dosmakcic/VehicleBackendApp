﻿namespace Project.Service.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string? Name { get; set; }
        public string? Abrv { get; set; }
        public VehicleMake VehicleMake { get; set; } = null!; 

    }
}
