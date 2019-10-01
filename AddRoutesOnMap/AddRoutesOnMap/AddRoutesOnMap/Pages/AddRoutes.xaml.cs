using AddRoutesOnMap.CustomControls;
using AddRoutesOnMap.PlacesSearchBar;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AddRoutesOnMap.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddRoutes : ContentPage
	{
        string GooglePlacesApiKey = "AIzaSyD5HZVzzHIy5NxhjHoUjMSMNXec4QR9PRw";
        public AddRoutes ()
		{
			InitializeComponent ();

            search_bar.ApiKey = GooglePlacesApiKey; //|country:ar|country:pe|country:mx|country:es country:us|country:co|country:cl|country:ar|country:pe
            search_bar.Type = PlaceType.All;
            // search_bar.Components = new Components("country:mx|country:es"); // Restrict results to Australia and New Zealand country:au|country:nz|country:in|country:co|country:cl|country:us|country:ar|country:pe|country:mx|country:es 
            search_bar.PlacesRetrieved += Search_Bar_PlacesRetrieved;
            search_bar.TextChanged += Search_Bar_TextChanged;
            search_bar.MinimumSearchText = 2;
            results_list.ItemSelected += Results_List_ItemSelected;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await pinListAsync();
        }
        void Search_Bar_PlacesRetrieved(object sender, AutoCompleteResult result)
        {
            results_list.ItemsSource = result.AutoCompletePlaces;
            spinner.IsRunning = false;
            spinner.IsVisible = false;

            if (result.AutoCompletePlaces != null && result.AutoCompletePlaces.Count > 0)
                results_list.IsVisible = true;
        }

        void Search_Bar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                placeListGrid.IsVisible = true;
                results_list.IsVisible = false;
                spinner.IsVisible = true;
                spinner.IsRunning = true;
            }
            else
            {
                placeListGrid.IsVisible = false;
                results_list.IsVisible = true;
                spinner.IsRunning = false;
                spinner.IsVisible = false;
            }
        }

        async void Results_List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem == null)
                    return;

                var prediction = (AutoCompletePrediction)e.SelectedItem;
                results_list.SelectedItem = null;
                var FullAddress = prediction.Description;
               // Helpers.Commom.PlaceName = FullAddress;
                var place = await Places.GetPlace(prediction.Place_ID, GooglePlacesApiKey);

                //if (place != null)
                //    await DisplayAlert(
                //        place.Name, string.Format("Lat: {0}\nLon: {1}", place.Latitude, place.Longitude), "OK");
                if (place != null)
                {
                    var PlaceName = place.Name;
                    var Latitude = place.Latitude;

                    var Longitude = place.Longitude;

                    search_bar.Text = PlaceName;
                }

                await OriginAddress();
                placeListGrid.IsVisible = false;
                //if (addTravelerTripPage3VM == null)
                //{
                    Helpers.Constant.originLatitude = place.Latitude.ToString();
                    Helpers.Constant.originLongitude = place.Longitude.ToString();
                //    await OriginAddress();
                //    okButton.IsVisible = true;
                //}
                //else if (addTravelerTripPage3VM != null)
                //{
                //    OrderReceiveLatitude = place.Latitude;
                //    OrderReceiveLongitude = place.Longitude;
                //    Helpers.Commom.OrderLatitude = OrderReceiveLatitude;
                //    Helpers.Commom.OrderLongitude = OrderReceiveLongitude;
                //    await DestinationAddress();
                //    okButton.IsVisible = true;
                //}
                //if (addTripPageVM.DestinationCountryName == "Destination Address")
                //{
                //    Helpers.Commom.DestinationLatitude = place.Latitude.ToString();
                //    Helpers.Commom.DestinationLongitude = place.Longitude.ToString();
                //    await DestinationAddress();
                //    okButton.IsVisible = true;
                //}
                //else
                //{
                //    address.Text= "Please Enter Your Origin Address";
                //    search_bar.Placeholder = "Origin Address";
                //    search_bar.PlaceholderColor = Color.Black;
                //    search_bar.FontSize = 13;
                //    Helpers.Commom.originLatitude = place.Latitude.ToString();
                //    Helpers.Commom.originLongitude = place.Longitude.ToString();
                //    await OriginAddress();
                //    okButton.IsVisible = true;
                //}
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public async Task pinListAsync()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }
                if (status == PermissionStatus.Granted)
                {
                    // Get Current Lattitude And Latitude
                    MyMap.CustomPins = new List<CustomPin>();
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    var position = await locator.GetLastKnownLocationAsync();
                    // Xamarin.Forms.Maps.Position position = new Xamarin.Forms.Maps.Position(location.Latitude, location.Longitude);
                    var position1 = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude); // Latitude, Longitude
                    //Helpers.Commom.originLatitude = position.Latitude.ToString();
                    //Helpers.Commom.originLongitude = position.Longitude.ToString();
                    //var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position1);
                    //var addresses = await locator.GetAddressesForPositionAsync(position, "LatLong");

                    //var address = addresses.FirstOrDefault();
                    //var throughfare = address.Thoroughfare;
                    //var subLocality = address.SubLocality;
                    //var subAdminArea = address.SubAdminArea;
                    //var adminArea = address.AdminArea;
                    //var countryName = address.CountryName;

                    //var streetNumber = address.FeatureName;
                    //var strnumber = address.SubThoroughfare;
                    ////  Helpers.Commom.ApartmentAddress = streetNumber;
                    //// addTravelerTripPageVM.ApartmentAddress = streetNumber;


                    //var FullAddress = throughfare + "," + streetNumber + "," + subLocality + "," + subAdminArea + "," + adminArea + "," + countryName;
                    MyMap.MoveToRegion(new MapSpan(position1, 1, 1));
                    //search_bar.Text = FullAddress;
                    //Helpers.Commom.PlaceName = search_bar.Text;
                    placeListGrid.IsVisible = false;
                   // okButton.IsVisible = true;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        public async Task OriginAddress()
        {
            try
            {
                // Get Current Lattitude And Latitude
                MyMap.CustomPins = new List<CustomPin>();
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                // var position = await locator.GetLastKnownLocationAsync();
                // Xamarin.Forms.Maps.Position position = new Xamarin.Forms.Maps.Position(location.Latitude, location.Longitude);
                var position1 = new Xamarin.Forms.Maps.Position(double.Parse(Helpers.Constant.originLatitude), double.Parse(Helpers.Constant.originLongitude)); // Latitude, Longitude
                MyMap.MoveToRegion(new MapSpan(position1, 1, 1));
                var pin = new CustomPin
                {
                    Pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = position1,
                        Label = "",
                        Id = "Mastercard",
                        Address = "",
                    }
                };
                pin.Id = "Mastercard";
                MyMap.CustomPins.Add(pin);
                MyMap.Pins.Add(pin.Pin);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }
    }
}