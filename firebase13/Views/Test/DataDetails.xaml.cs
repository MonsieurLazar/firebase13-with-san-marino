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
    public partial class DataDetails : ContentPage
    {
        DataRepository repository = new DataRepository();
        public DataDetails(Data data)
        {
            InitializeComponent();
            LabelName.Text = data.Name;
            LabelEmail.Text = data.Email;
        }
    }
}