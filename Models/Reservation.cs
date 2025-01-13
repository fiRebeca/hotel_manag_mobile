using SQLite;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;

namespace hotel_manag_mobile.Models
{
    public class Reservation
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [SQLiteNetExtensions.Attributes.ForeignKey(typeof(User))]
        public int UserID { get; set; }

        [SQLiteNetExtensions.Attributes.ForeignKey(typeof(Room))]
        public int RoomID { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        [ManyToOne]
        public User User { get; set; }

        [ManyToOne]
        public Room Room { get; set; }
    }
}
