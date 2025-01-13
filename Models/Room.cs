using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace hotel_manag_mobile.Models
{
    public class Room
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string RoomNumber { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public string DisplayInfo => $"Room: {RoomNumber}, Type: {Type}, Price: {Price:C}";
    }
}
