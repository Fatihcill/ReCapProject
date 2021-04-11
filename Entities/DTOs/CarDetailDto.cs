using System;
using Core.Entities;

namespace Entities.DTOs
{
    public class CarDetailDto:IDto
    {
        public string Description { get; set; }
        public int CarId { get; set; }
        public string BrandName { get; set; }
        public int ModelYear { get; set; }
        public string ColorName { get; set; }
        public decimal DailyPrice { get; set; }
        public string ImagePath { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int CarFindexPoint { get; set; }


    }
}