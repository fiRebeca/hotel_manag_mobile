using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace hotel_manag_mobile.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [SQLite.MaxLength(100)]
        public string Name { get; set; }
        [SQLite.MaxLength(100), Unique]
        public string Email { get; set; }

        [SQLite.MaxLength(10)]
        public string Password { get; set; }
        [SQLite.MaxLength(20)]
        public string Role { get; set; } /*employee sau guest*/


    }
}
