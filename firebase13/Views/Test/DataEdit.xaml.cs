using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace firebase13.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataEdit : ContentPage
    {
        DataRepository repository = new DataRepository();
        public DataEdit(Data data)
        {
            InitializeComponent();
            TxtEmail.Text = data.Email;
            TxtName.Text = data.Name;
            TxtId.Text = data.Id;
        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            string email = TxtEmail.Text;
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Warning", "Please enter your name", "Cancel");
            }
            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Warning", "Please enter your email", "Cancel");
            }

            Data data = new Data();
            data.Id = TxtId.Text;
            data.Name = name;
            data.Email = email;
            bool isUpdated = await repository.Update(data);
            if (isUpdated)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Update failed.", "Cancel");
            }

        }


    }
}