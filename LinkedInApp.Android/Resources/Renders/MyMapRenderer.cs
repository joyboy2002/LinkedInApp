using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LinkedInApp.Droid.Resources.Renders;
using LinkedInApp.Models;
using LinkedInApp.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyMap), typeof(MyMapRenderer))]
namespace LinkedInApp.Droid.Resources.Renders
{
    public class MyMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        JobModel Job;

        public MyMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                this.Job = (e.NewElement as MyMap).Job;
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(Job.Latitude, Job.Longitude));
            marker.SetTitle(Job.Cargo);
            marker.SetSnippet($"Ciudad: {Job.Ciudad}°");
            marker.SetSnippet($"Empresa: {Job.Empresa}°");
            return marker;
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;
                view = inflater.Inflate(Resource.Layout.MapWindow, null);
                var infoImage = view.FindViewById<ImageView>(Resource.Id.MapWindowImage);
                var infoName = view.FindViewById<TextView>(Resource.Id.MapWindowName);
                var infoCategory = view.FindViewById<TextView>(Resource.Id.MapWindowCategory);
                var infoDuration = view.FindViewById<TextView>(Resource.Id.MapWindowDuration);


                if (infoImage != null) infoImage.SetImageBitmap(BitmapFactory.DecodeFile(Job.ImagenURI));
                if (infoName != null) infoName.Text = Job.Cargo;
                if (infoCategory != null) infoCategory.Text = ($"Ciudad: {Job.Ciudad}°");
                if (infoDuration != null) infoDuration.Text = ($"Empresa: {Job.Empresa}°");


                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }
    }
}