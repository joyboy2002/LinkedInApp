using LinkedInApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LinkedInApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobView : ContentPage
    {
        public JobView()
        {
            InitializeComponent();
            BindingContext = new JobViewModel();
        }

        private void CollectionView_Focused(object sender, FocusEventArgs e)
        {

        }
    }
}