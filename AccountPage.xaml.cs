using hotel_manag_mobile.Models;

namespace hotel_manag_mobile;

public partial class AccountPage : ContentPage
{
    private User _currentUser;

    public AccountPage(User user)
    {
        InitializeComponent();
        _currentUser = user;


        NameEntry.Text = user.Name;
        EmailEntry.Text = user.Email;
        PasswordEntry.Text = user.Password;

        NavigationLabel.Text = $"Welcome, {user.Name}!";
    }

    private async void OnSaveChangesClicked(object sender, EventArgs e)
    {

        _currentUser.Name = NameEntry.Text;
        _currentUser.Email = EmailEntry.Text;
        _currentUser.Password = PasswordEntry.Text;

        await App.Database.EditUserAsync(_currentUser);
        await DisplayAlert("Success", "Changes saved!", "OK");
    }

    private async void OnDeleteAccountClicked(object sender, EventArgs e)
    {
        var confirm = await DisplayAlert("Confirm", "Are you sure you want to delete your account?", "Yes", "No");
        if (confirm)
        {
            await App.Database.DeleteUserAsync(_currentUser);
            await DisplayAlert("Deleted", "Your account has been deleted.", "OK");
            // Navighează înapoi la pagina principală
            await Navigation.PopToRootAsync();
        }
    }

    private async void OnReservationsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReservationsPage(_currentUser.ID));
    }


    private async void OnFeedbackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FeedbackPage(_currentUser.ID));
    }

    private async void OnRoomsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RoomsPage());
    }

    private async void OnFacilitiesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FacilitiesPage());
    }

 

}