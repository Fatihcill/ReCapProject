using System;

namespace Entities.DTOs
{
    public class RentalDetailDto
    {
        public string BrandName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int RentalId { get; set; }

        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        //Customer Table
        public string CompanyName { get; set; }
        public int CustomerFindexPoint { get; set; }

        //Color Table
        public string ColorName { get; set; }

        //Car Table
        public string CarDescription { get; set; }

        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
    }
}