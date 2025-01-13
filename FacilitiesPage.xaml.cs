using hotel_manag_mobile.Models;

namespace hotel_manag_mobile
{
    public partial class FacilitiesPage : ContentPage
    {
        public FacilitiesPage()
        {
            InitializeComponent();
            _ = LoadFacilitiesAsync();
        }

        private async void OnAddFacilityClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FacilityNameEntry.Text) || string.IsNullOrWhiteSpace(FacilityPriceEntry.Text))
            {
                await DisplayAlert("Error", "Please fill in all the fields.", "OK");
                return;
            }

            var newFacility = new Facility
            {
                FacilityName = FacilityNameEntry.Text,
                Description = DescriptionEntry.Text,
                Price = decimal.Parse(FacilityPriceEntry.Text)
            };

            await App.Database.SaveFacilityAsync(newFacility);

            await DisplayAlert("Success", "Facility added successfully.", "OK");

        
            FacilityNameEntry.Text = string.Empty;
            DescriptionEntry.Text = string.Empty;
            FacilityPriceEntry.Text = string.Empty;

           
            await LoadFacilitiesAsync();
        }

        private async Task LoadFacilitiesAsync()
        {
            var facilities = await App.Database.GetFacilitiesAsync();
            FacilitiesListView.ItemsSource = facilities;
        }


        private async void OnDeleteFacilityClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var facility = button?.CommandParameter as Facility;

            if (facility == null)
                return;

            var confirm = await DisplayAlert("Confirm", "Are you sure you want to delete this facility?", "Yes", "No");
            if (confirm)
            {
                await App.Database.DeleteFacilityAsync(facility);
                await LoadFacilitiesAsync();
            }
        }
    }
}
