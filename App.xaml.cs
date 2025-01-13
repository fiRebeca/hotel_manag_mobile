using System;
using hotel_manag_mobile;
using System.IO;
using hotel_manag_mobile.Data;

namespace hotel_manag_mobile
{
    public partial class App : Application
    {
        static HotelAppDatabase database;

        public static HotelAppDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new HotelAppDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "hotel_manag_mobile.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
