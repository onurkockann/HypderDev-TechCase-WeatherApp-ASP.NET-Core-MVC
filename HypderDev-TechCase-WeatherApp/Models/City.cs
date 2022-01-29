using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HypderDev_TechCase_WeatherApp.Models
{
    public class City
    {
        [Key]
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string CoordLat { get; set; }
        public string CoordLon { get; set; }
    }
}
