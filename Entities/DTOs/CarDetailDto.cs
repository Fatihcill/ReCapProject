using System;
using Core.Entities;

namespace Entities.DTOs
{
    public class CarDetailDto:IDto
    {
        public int CarId { get; set; }
        public int BrandId{ get; set; }
        public string BrandName { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }

        //CarImages
        public int ImageId { get; set; }
        public string ImagePath { get; set; } = Environment.CurrentDirectory + @"\wwwroot\Images\default.png";
        public DateTime  Date { get; set; }= DateTime.Now;
        public string WebImagePath { get; set; }= "/Images/default.png";


    }
}