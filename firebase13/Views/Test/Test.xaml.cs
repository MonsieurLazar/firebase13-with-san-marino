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
    public partial class Test : ContentPage
    {
        DataRepository repository = new DataRepository();
        public Test()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            var datas = await repository.GetAll();
            DataListView.ItemsSource = datas;
        }

        private void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Task task = Navigation.PushModalAsync(new TestEntry());
        }

        private void DataListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var data = e.Item as Data;
            Navigation.PushModalAsync(new DataDetails(data));
            ((ListView)sender).SelectedItem = null;
        }

        private async void DeleteTap_Tapped(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Delete", "Do you want to delete?", "Yes", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await repository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Information", "Data has been deleted.", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Data deleted failed.", "Ok");
                }
            }

        }

        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            //DisplayAlert("Edit", "Do you want to Edit?", "Ok");

            string id = ((TappedEventArgs)e).Parameter.ToString();
            var student = await repository.GetById(id);
            if (student == null)
            {
                await DisplayAlert("Warning", "Data not found.", "Ok");
            }
            student.Id = id;
            await Navigation.PushModalAsync(new DataEdit(student));

        }
    }
}