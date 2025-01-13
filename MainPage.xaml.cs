using hotel_manag_mobile.Models;
using Microsoft.Maui.Storage;

namespace hotel_manag_mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnAddUserClicked(object sender, EventArgs e)
        {
            AddUserForm.IsVisible = true; // Afișează formularul
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            var users = await App.Database.GetUsersAsync();
            UsersListView.ItemsSource = users;
        }

        async void OnGetUsersClicked(object sender, EventArgs e)
        {
            var users = await App.Database.GetUserAsync();
            UsersListView.ItemsSource = users;
        }

        private async void OnSaveUserClicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text;

            // Verifică dacă utilizatorul există deja în baza de date
            var existingUser = await App.Database.GetUserByEmailAsync(email);
            if (existingUser != null)
            {
                await DisplayAlert("Error", "Mail already used!", "OK");
                return;
            }

            // Creează un utilizator nou pe baza datelor introduse în formular
            var user = new User
            {
                Name = NameEntry.Text,
                Email = email,
                Password = PasswordEntry.Text,
                Role = RolePicker.SelectedItem?.ToString() ?? "Guest" // Implicit rolul este "Guest"
            };

            // Salvează utilizatorul în baza de date
            await App.Database.SaveUserAsync(user);

            // Golește câmpurile formularului și ascunde-l
            NameEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            RolePicker.SelectedItem = null;
            AddUserForm.IsVisible = false;

            await DisplayAlert("Success", "User added successfully!", "OK");

            // Reîmprospătează lista utilizatorilor
            await LoadUsersAsync();
        }


        private void OnCancelAddUserClicked(object sender, EventArgs e)
        {
            AddUserForm.IsVisible = false; // Ascunde formularul fără a salva
        }


        private async void OnUserLoginClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var user = button?.CommandParameter as User;

            if (user == null)
            {
                await DisplayAlert("Error", "User not found.", "OK");
                return;
            }

            // Solicită parola 
            string password = await DisplayPromptAsync("Login", $"Enter password for {user.Name}:", "OK");

            if (password == user.Password)
            {
                await DisplayAlert("Success", $"Welcome, {user.Name}!", "OK");

                await Navigation.PushAsync(new AccountPage(user));
                     }
            else
            {
                await DisplayAlert("Error", "Incorrect password.", "OK");
            }
        }

    }

}
