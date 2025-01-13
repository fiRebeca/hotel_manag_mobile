using SQLite;
using SQLiteNetExtensions.Attributes;

namespace hotel_manag_mobile.Models
{
    public class Facility
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        
        public string FacilityName { get; set; }
        [MaxLength(100)]

        public string Description { get; set; }


        public decimal Price { get; set; }
    }
}
