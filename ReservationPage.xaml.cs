using hotel_manag_mobile.Models;

namespace hotel_manag_mobile;

public partial class ReservationsPage : ContentPage
{
    private int _currentUserID;

    public ReservationsPage(int userId)
    {
        InitializeComponent();
        _currentUserID = userId;

        LoadRoomsAsync();
        LoadReservationsAsync();
    }

    private async void LoadRoomsAsync()
    {
        var rooms = await App.Database.GetRoomsAsync(); // Obține camerele din baza de date
        RoomPicker.ItemsSource = rooms;
        RoomPicker.ItemDisplayBinding = new Binding("RoomNumber"); // Afișează RoomNumber în Picker
    }

    private async void LoadReservationsAsync()
    {
        var reservations = await App.Database.GetReservationsByUserIdAsync(_currentUserID); // Obține rezervările utilizatorului curent
        ReservationsListView.ItemsSource = reservations;
    }

    private async void OnSubmitReservationClicked(object sender, EventArgs e)
    {
        if (RoomPicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "Please select a room.", "OK");
            return;
        }

        var selectedRoom = RoomPicker.SelectedItem as Room;

        var reservation = new Reservation
        {
            UserID = _currentUserID,
            RoomID = selectedRoom.ID,
            CheckInDate = CheckInDatePicker.Date,
            CheckOutDate = CheckOutDatePicker.Date
        };

        await App.Database.SaveReservationAsync(reservation);
        await DisplayAlert("Success", "Reservation submitted successfully!", "OK");

        // Reîmprospătează lista rezervărilor
        LoadReservationsAsync();
    }
}
