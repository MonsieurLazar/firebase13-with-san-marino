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
    public partial class TestEntry : ContentPage
    {
        DataRepository repository = new DataRepository();
        public TestEntry()
        {
            InitializeComponent();
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
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
            data.Name = name;
            data.Email = email;

            var isSaved = await repository.Save(data);
            if (isSaved)
            {
                await DisplayAlert("Information", "Data has been saved", "Ok");
                Clear();
            }
            else
            {
                await DisplayAlert("Error", "Data saved failed", "Ok");
            }
        }
        public void Clear()
        {
            TxtName.Text = string.Empty;
            TxtEmail.Text = string.Empty;
        }

    }
}