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
    public partial class JobDetailView : ContentPage
    {
        public JobDetailView()
        {
            InitializeComponent();
            BindingContext = new JobDetailViewModel();
        }
        public JobDetailView(JobViewModel listViewModel,bool isEdit=false)
        {
            InitializeComponent();
            BindingContext = new JobDetailViewModel(listViewModel,isEdit);
            

        }
    }
}