using LinkedInApp.Models;
using LinkedInApp.Services;
using LinkedInApp.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LinkedInApp.ViewModels
{
    public class JobDetailViewModel : BaseViewModel
    {
        //Variables locales

        public readonly JobViewModel ListViewModel;
        private readonly bool isEdit;
        #region Comandos
        //Comandos básicos
        private Command _CancelCommand;
        public Command CancelCommand => _CancelCommand ?? (_CancelCommand = new Command(CancelAction));

        private Command _SaveCommand;
        public Command SaveCommand => _SaveCommand ?? (_SaveCommand = new Command(SaveAction));

        private Command _DeleteCommand;
        public Command DeleteCommand => _DeleteCommand ?? (_DeleteCommand = new Command(DeleteAction));

        // Comandos de fotografía
        private Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        private Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

        //Comando Mapa
        private Command _MapCommand;
        public Command MapCommand => _MapCommand ?? (_MapCommand = new Command(MapAction));

        //Comando Ubicacion
        private Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationActionAsync));
        #endregion
        #region Propiedades
        //Aquí se guardó el modelo que se seleccionó
        private JobModel _JobModel;
        public JobModel JobModel
        {
            get => _JobModel;
            set => SetProperty(ref _JobModel, value);
        }

        // Propiedades
        private int _JobID;
        public int JobID
        {
            get => _JobID;
            set => SetProperty(ref _JobID, value);
        }

        //Cargo
        private string _JobCargo;
        public string JobCargo
        {
            get => _JobCargo;
            set => SetProperty(ref _JobCargo, value);
        }
        //Modalidad
        private string _JobModalidad;
        public string JobModalidad
        {
            get => _JobModalidad;
            set => SetProperty(ref _JobModalidad, value);
        }
        //Ciudad
        private string _JobCiudad;
        public string JobCiudad
        {
            get => _JobCiudad;
            set => SetProperty(ref _JobCiudad, value);
        }
        //Empresa
        private string _JobEmpresa;
        public string JobEmpresa
        {
            get => _JobEmpresa;
            set => SetProperty(ref _JobEmpresa, value);
        }
        //Jornada
        private string _JobJornada;
        public string JobJornada
        {
            get => _JobJornada;
            set => SetProperty(ref _JobJornada, value);
        }
        //Descripcion
        private string _JobeDescripcion;
        public string JobDescripcion
        {
            get => _JobeDescripcion;
            set => SetProperty(ref _JobeDescripcion, value);
        }
        //Latitud
        private double _JobLatitude;
        public double JobLatitude
        {
            get => _JobLatitude;
            set => SetProperty(ref _JobLatitude, value);
        }
        //Longitud
        private double _JobLongitude;
        public double JobLongitude
        {
            get => _JobLongitude;
            set => SetProperty(ref _JobLongitude, value);
        }

        //Imagen

        private string _JobPicture;
        public string JobPicture
        {
            get => _JobPicture;
            set => SetProperty(ref _JobPicture, value);
        }   

        #endregion
        #region Contructores
        //Constructores

        public JobDetailViewModel()
        {
            //instanciamos
            JobModel = new JobModel();
 
        }

        public JobDetailViewModel(JobViewModel listViewModel,bool isEdit=false)
        {
            ListViewModel = listViewModel;
            this.isEdit = isEdit;
            if (listViewModel.JobSelected == null)
            {
                this.JobID = 0;
            }
            else
            {

                //Es un trabajo selccionado existente
                JobID = listViewModel.JobSelected.ID;
                JobCargo = listViewModel.JobSelected.Cargo;
                JobModalidad = listViewModel.JobSelected.Modalidad;
                JobCiudad = listViewModel.JobSelected.Ciudad;
                JobEmpresa = listViewModel.JobSelected.Empresa;
                JobJornada = listViewModel.JobSelected.Jornada;
                JobDescripcion = listViewModel.JobSelected.Description;
                JobLongitude = listViewModel.JobSelected.Longitude;
                JobLatitude = listViewModel.JobSelected.Latitude;
                JobPicture = listViewModel.JobSelected.Imagen;
            }
        }
        #endregion
        #region Métodos
        //Métodos
        private void CancelAction()
        {


            //Limpiamos la lista
            JobCargo = "";
            JobModalidad = "";
            JobCiudad = "";
            JobEmpresa = "";
            JobJornada = "";
            JobDescripcion = "";
            JobLongitude = 0;
            JobLatitude = 0;
            JobPicture = "";
            //Actualizar
            ListViewModel.RefreshJobs();

            //Cerrar vista actual
            Application.Current.MainPage.Navigation.PopAsync();

        }

        private async void SaveAction()
        {

            try
            {
                ApiResponse response;

                IsBusy = true;

               
                JobModel model = new JobModel
                {
                    ID = JobID,
                    Cargo = JobCargo,
                    Modalidad = JobModalidad,
                    Ciudad = JobCiudad,
                    Empresa = JobEmpresa,
                    Jornada = JobJornada,
                    Description = JobDescripcion,
                    Longitude = JobLongitude,
                    Latitude = JobLatitude,
                    Imagen = JobPicture
                };

                if (!isEdit)
                {
                    response = await new ApiService().PostDataAsync("Job", model);
                }
                else
                {
                    response = await new ApiService().PutDataAsync("Job", model,model.ID);
                    Console.WriteLine(model.ID);
                }
      
                if (response == null || !response.IsSuccess)
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("AppLinkedIn", $"Se generó un error : {response.Messege}", "Ok");
                    return;
                }
                if (response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Guardado Existoso", "Se guardó de manera exitosa", "Ok");
                    CancelAction();
                    IsBusy = false;

                }


                //Actualizar los puestos de trabajo
                ListViewModel.RefreshJobs();

                //Cierra la vista actual
                await Application.Current.MainPage.Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AppLinkedIn", $"Se generó un error al guardar el puesto: {ex.Message}", "Ok");
            }


        }

        private async void DeleteAction()
        {
            ApiResponse response;
            try
            {
                if (JobID > 0)
                {
                    response = await new ApiService().DeleteDataAsync("Job", JobID);
                    ListViewModel.JobSelected = null;
                    //Actualizar
                    ListViewModel.RefreshJobs();

                    //Cerrar vista actual
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AppLinkedIn", $"Se generó un error al borrar el puesto: {ex.Message}", "ok");
            }
            //Actualizar
            ListViewModel.RefreshJobs();

            //Cerrar vista actual
            await Application.Current.MainPage.Navigation.PopAsync();

        }

        private async void TakePictureAction()
        {
            // Abre la cámara
            await CrossMedia.Current.Initialize();

            // Si la cámara no está disponible o no está soportada lanza un mensaje y termina este método
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            // Toma la fotografía y la regresa en el objeto file 
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Job",
                Name = "cam_picture.jpg"
            });

            // Si el objeto file es null termina este método
            if (file == null)
                return;
            IsBusy = true;
            JobPicture = await new ImageService().ConvertImageFileToBase64(file.Path);

        }



        private async void SelectPictureAction()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("AppLinkedIn", "No es posible seleccionar fotografías del dispositivo", "Ok");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });

                if (file == null)
                    return;

                JobPicture = await new ImageService().ConvertImageFileToBase64(file.Path);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AppLinkedIn", $"Se generó un error al tomar la fotografía ({ex.Message})", "Ok");
            }


        }

        
        private async void MapAction()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new MapView(ListViewModel.JobSelected));
            }
            catch (Exception exc)
            {

                await Application.Current.MainPage.DisplayAlert("AppLinkedIn", $"Error al procesar el mapa {exc.Message}", "Ok");
            }

        }
        private async void GetLocationActionAsync(object obj)
        {
            try
            {
                JobLatitude = JobLongitude = 0;
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {             
                    JobLatitude = (float)location.Latitude;
                    JobLongitude = (float)location.Longitude;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                await Application.Current.MainPage.DisplayAlert("AppLinkedIn", $"El GPS no está soportado en el dispositivo ({fnsEx.Message})", "Ok");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                await Application.Current.MainPage.DisplayAlert("AppLinkedIn", $"El GPS no está activado en el dispositivo ({fneEx.Message})", "Ok");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                await Application.Current.MainPage.DisplayAlert("AppLinkedIn", $"No se pudo obtener el permiso para las coordenadas ({pEx.Message})", "Ok");
            }
            catch (Exception ex)
            {
                // Unable to get location
                await Application.Current.MainPage.DisplayAlert("AppLinkedIn", $"Se generó un error al obtener las coordenadas del dispositivo ({ex.Message})", "Ok");
            }
        }

        #endregion

    }
}
