namespace Module03Exercise01.View;

public partial class AddEmployeePage : ContentPage
{
	public AddEmployeePage()
	{
		InitializeComponent();
	}

    private async void OnGetLocationClicked(object sender, EventArgs e)
    {
        try
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.High
                });
            }

            if (location != null)
            {

                emp_coordinates.Text = $"Latitude: {location.Latitude},\nLongitude: {location.Longitude}";

                // Get Geocoding - Get Address from Lat and long
                var placemarks = await Geocoding.Default.GetPlacemarksAsync(location.Latitude, location.Longitude);
                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    emp_municipality.Text = $"{placemark.Locality}";
                    emp_province.Text = $"{placemark.AdminArea}";
                }
                else
                {
                    emp_coordinates.Text = "Unable to determine the address";
                }
            }
            else
            {
                emp_coordinates.Text = "Unable to get location";
            }
        }
        catch (Exception ex)
        {
            emp_coordinates.Text = $"Error: {ex.Message}";
        }
    }

    private async void OnCapturePhotoClicked(object sender, EventArgs e)
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                // Capture a photo using MediaPicker
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo != null)
                {
                    await LoadPhotoAsync(photo);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    // Load photo and display it in the Image control
    private async Task LoadPhotoAsync(FileResult photo)
    {
        if (photo == null)
            return;

        Stream stream = await photo.OpenReadAsync();
        CaptureImage.Source = ImageSource.FromStream(() => stream);
    }

}