using Microsoft.AppCenter.Data;
using MyFavouriteSign.Services;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyFavouriteSign
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private string _stateTerritorySelection = "None";
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();            
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            var listofUserData = await Data.ListAsync<string>(DefaultPartitions.UserDocuments);
            if (listofUserData.CurrentPage.Items.Count > 0)
            {
                await Data.ReplaceAsync(Authentication.userInfo.AccountId, _stateTerritorySelection, DefaultPartitions.UserDocuments);
            }
            else
            {
                await Data.CreateAsync(Authentication.userInfo.AccountId, _stateTerritorySelection, DefaultPartitions.UserDocuments);
            }
            
        }

        private void StateTerritoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                _stateTerritorySelection = (string)picker.ItemsSource[selectedIndex];
                UpdateSignImages();
            }
        }
        private async void LoadProfile_Clicked(object sender, EventArgs e)
        {
            var listofUserData = await Data.ListAsync<string>(DefaultPartitions.UserDocuments);
            if (listofUserData.CurrentPage.Items.Count > 0)
            {
                var userData = await Data.ReadAsync<string>(Authentication.userInfo.AccountId, DefaultPartitions.UserDocuments);
                _stateTerritorySelection = userData.DeserializedValue;
            }
            SignPicker.IsVisible = true;
            SignGrid.IsVisible = true;
            SaveButton.IsVisible = true;
            SignOutButton.IsVisible = true;
            DisplayName.IsVisible = true;
            LoadProfileButton.IsVisible = false;
            UpdateSignImages();
            SignPicker.SelectedItem = _stateTerritorySelection;
            DisplayName.Text = "Welcome " + Authentication.DisplayName + "!";
        }

        private async void SignOut_Clicked(object sender, EventArgs e)
        {
            _stateTerritorySelection = "None";
            Authentication.SignOut();
            SignPicker.IsVisible = false;
            SignGrid.IsVisible = false;
            SaveButton.IsVisible = false;
            SignOutButton.IsVisible = false;
            DisplayName.IsVisible = false;
            LoadProfileButton.IsVisible = true;
            await Authentication.SignInAsync();
        }

        private void UpdateSignImages()
        {
            if (_stateTerritorySelection == "None")
            {
                NoEntry.Source = "";
                NoUTurn.Source = "";
                NoLeftTurn.Source = "";
                NoRightTurn.Source = "";
            }
            else if (_stateTerritorySelection == "ACT" || _stateTerritorySelection == "NSW" || _stateTerritorySelection == "NT")
            {
                NoEntry.Source = ImageSource.FromResource("MyFavouriteSign.Images.NoEntryRTA.png");
                NoUTurn.Source = ImageSource.FromResource("MyFavouriteSign.Images.NoUTurnRTA.png");
                NoLeftTurn.Source = ImageSource.FromResource("MyFavouriteSign.Images.NoLeftTurnRTA.png");
                NoRightTurn.Source = ImageSource.FromResource("MyFavouriteSign.Images.NoRightTurnRTA.png");
            }
            else
            {
                NoEntry.Source = ImageSource.FromResource("MyFavouriteSign.Images.NoEntry.png");
                NoUTurn.Source = ImageSource.FromResource("MyFavouriteSign.Images.NoUTurn.png");
                NoLeftTurn.Source = ImageSource.FromResource("MyFavouriteSign.Images.NoLeftTurn.png");
                NoRightTurn.Source = ImageSource.FromResource("MyFavouriteSign.Images.NoRightTurn.png");
            }
        }
    }
}
