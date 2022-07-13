using LinkedInApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using LinkedInApp.Services;
using Newtonsoft.Json;
using LinkedInApp.Views;

namespace LinkedInApp.ViewModels
{
    public class JobViewModel : BaseViewModel
    {
        
        #region commands
        // Comandos
        private Command _NewCommand;
        public Command NewCommand => _NewCommand ?? (_NewCommand = new Command(NewAction));

        private Command _RefreshCommand;
        public Command RefreshCommand => _RefreshCommand ?? (_RefreshCommand = new Command(RefreshJobs));

        private Command _SelectCommand;
        public Command SelectCommand => _SelectCommand ?? (_SelectCommand = new Command(SelectAction));

        // Propiedades
        private List<JobModel> _Jobs;
        public List<JobModel> Jobs
        {
            get => _Jobs;
            set => SetProperty(ref _Jobs, value);
        }

        private JobModel _JobSelected;
        public JobModel JobSelected
        {
            get => _JobSelected;
            set => SetProperty(ref _JobSelected, value);
        }
       
    
        public JobViewModel()
        {
        
            LoadJobs();
        }
        #endregion

        #region metodos
        // Métodos


        public void RefreshJobs()
        {
            LoadJobs();
        }

        public async void LoadJobs()
        {
            ApiResponse response;

            IsBusy = true;
            Jobs = null;
            response = await new ApiService().GetDataAsync("Job");
            if (response == null || !response.IsSuccess)
            {
                // No hay respuesta exitosa
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("LinkedIn", $"Error al cargar los Puestos de trabajo: {response.Messege}", "Ok");
                return;
            }
            Jobs = JsonConvert.DeserializeObject<List<JobModel>>(response.Result.ToString());
            IsBusy = false;

        }

        private void NewAction()
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new JobDetailView());
        }


        private async void SelectAction(object obj)
        {
            Console.WriteLine(JobSelected);
            await Application.Current.MainPage.Navigation.PushAsync(new JobDetailView(this, true));

        }
        #endregion
    }
}
