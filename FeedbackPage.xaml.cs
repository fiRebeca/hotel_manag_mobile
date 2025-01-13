using hotel_manag_mobile.Models;

namespace hotel_manag_mobile;

public partial class FeedbackPage : ContentPage
{
	private int _currentUserID;
	public FeedbackPage(int userId)
	{
        InitializeComponent();
		_currentUserID = userId;
		LoadRoomsAsync();
	}

    private async void LoadRoomsAsync()
    {
        var rooms = await App.Database.GetRoomsAsync();
        RoomPicker.ItemsSource = rooms.Select(r => $"{r.RoomNumber} - {r.Type}").ToList();
    }

    private async void OnSubmitFeedbackClicked(object sender, EventArgs e)
    {
        if (RoomPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Error", "Please select a room.", "OK");
            return;
        }

        var feedback = new Feedback
        {
            UserID = _currentUserID,
            FeedbackText = FeedbackTextEditor.Text,
            Rating = (int)RatingSlider.Value,
        };

        
        await App.Database.SaveFeedbackAsync(feedback);

        await DisplayAlert("Success", "Feedback submitted successfully!", "OK");

       
        RoomPicker.SelectedIndex = -1;
        FeedbackTextEditor.Text = string.Empty;
        RatingSlider.Value = 3;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var feedbacks = await App.Database.GetFeedbacksByUserIdAsync(_currentUserID); // Obține feedbackurile utilizatorului curent
        FeedbackListView.ItemsSource = feedbacks;
    }

    private async void OnDeleteFeedbackClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var feedback = button?.CommandParameter as Feedback;

        if (feedback == null) return;

        var confirm = await DisplayAlert("Confirm", "Are you sure you want to delete this feedback?", "Yes", "No");
        if (confirm)
        {
            await App.Database.DeleteFeedbackAsync(feedback);
            await DisplayAlert("Success", "Feedback deleted successfully!", "OK");

            // Reîmprospătează lista de feedbackuri
            var feedbacks = await App.Database.GetFeedbacksByUserIdAsync(_currentUserID);
            FeedbackListView.ItemsSource = feedbacks;
        }
    }

    private async void OnEditFeedbackClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var feedback = button?.CommandParameter as Feedback;

        if (feedback == null)
        {
            await DisplayAlert("Error", "Feedback not found.", "OK");
            return;
        }

        // Populează câmpurile formularului cu valorile existente
        FeedbackTextEditor.Text = feedback.FeedbackText;
        RatingSlider.Value = feedback.Rating;

        // Salvează modificările
        feedback.FeedbackText = FeedbackTextEditor.Text;
        feedback.Rating = (int)RatingSlider.Value;

        await App.Database.UpdateFeedbackAsync(feedback);
        await DisplayAlert("Success", "Feedback updated successfully!", "OK");

        await LoadFeedbacksAsync(); // Reîncarcă lista feedback-urilor
    }

    private async Task LoadFeedbacksAsync()
    {
        var feedbacks = await App.Database.GetFeedbacksByUserIdAsync(_currentUserID);
        FeedbackListView.ItemsSource = feedbacks;
    }


}