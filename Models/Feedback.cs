using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_manag_mobile.Models
{
    public class Feedback
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [SQLiteNetExtensions.Attributes.ForeignKey(typeof(User))]
        public int UserID { get; set; }

        public string FeedbackText { get; set; }

        public int Rating { get; set; } 

        [ManyToOne]
        public User User { get; set; }
    }
}
