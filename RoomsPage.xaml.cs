using hotel_manag_mobile.Models;

namespace hotel_manag_mobile
{
    public partial class RoomsPage : ContentPage
    {
        public RoomsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadRoomsAsync();
        }

        private async Task LoadRoomsAsync()
        {
            var rooms = await App.Database.GetRoomsAsync();
            RoomsListView.ItemsSource = rooms;
        }

        private async void OnAddRoomClicked(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(RoomNumberEntry.Text) ||
                RoomTypePicker.SelectedItem == null ||
                string.IsNullOrWhiteSpace(RoomPriceEntry.Text))
            {
                await DisplayAlert("Error", "All fields are required.", "OK");
                return;
            }

            
            var newRoom = new Room
            {
                RoomNumber = RoomNumberEntry.Text,
                Type = RoomTypePicker.SelectedItem.ToString(),
                Price = decimal.Parse(RoomPriceEntry.Text)
            };

            
            await App.Database.SaveRoomAsync(newRoom);

            await DisplayAlert("Success", "Room added successfully.", "OK");

            
            RoomNumberEntry.Text = string.Empty;
            RoomTypePicker.SelectedItem = null;
            RoomPriceEntry.Text = string.Empty;

            await LoadRoomsAsync();
        }

        private async void OnDeleteRoomClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var room = button?.CommandParameter as Room;

            if (room != null)
            {
                bool confirm = await DisplayAlert("Confirm", $"Delete room {room.RoomNumber}?", "Yes", "No");
                if (confirm)
                {
                    await App.Database.DeleteRoomAsync(room);
                    await LoadRoomsAsync();
                }
            }
        }
    }
}
