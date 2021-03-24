using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class CarImage:IEntity
    {
        public int CarImageId { get; set; }
        public int CarId { get; set; }
        public string ImagePath { get; set; } = Environment.CurrentDirectory + @"\wwwroot\Images\default.png";
        public DateTime  Date { get; set; }= DateTime.Now;
        public string WebImagePath { get; set; }= "/Images/default.png";
        
    }
}