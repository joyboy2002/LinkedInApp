using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedInApp.Models
{
    public class JobModel
    {


        #region Propiedades

        public int ID { get; set; }

        public string Cargo { get; set; }

        public string Modalidad { get; set; }

        public string Ciudad { get; set; }

        public string Empresa { get; set; }

        public string Jornada { get; set; }

        public string Description { get; set; }

        public string Imagen { get; set; }

        public string ImagenURI { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }


        #endregion
    }
}
