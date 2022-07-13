using LinkedInApp.Models;
using LinkedInApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace LinkedInApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentPage
    {
        public MapView(JobModel selectJob)
        {
            InitializeComponent();
            PrepareJobPictureMap(selectJob);
            MapJob.Job = selectJob;
            MapJob.MoveToRegion(
                  MapSpan.FromCenterAndRadius(
                      new Position(
                          selectJob.Latitude,
                          selectJob.Longitude
                          ),
                      Distance.FromMiles(.5)
                      )
                  );

            MapJob.Pins.Add(
                  new Pin
                  {
                      Type = PinType.Place,
                      Label = selectJob.Empresa,

                      Position = new Position(
                          selectJob.Latitude,
                          selectJob.Longitude
                    )
                  }
                );
        }
        private async void PrepareJobPictureMap(JobModel job)
        {
  

            ////Guardamos la imagen localmente y regresamos la Uri donde se guardó
             job.ImagenURI = new ImageService().SaveImageFromBase64(job.Imagen, job.ID);

        }
    }
}