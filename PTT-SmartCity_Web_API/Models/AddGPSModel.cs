using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class AddGPSModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        [StringLength(50)]
        public string DevEUI { get; set; }

        [Required]
        public float Latitude { get; set; }

        [Required]
        public float Longitude { get; set; }

        [Required]
        [StringLength(100)]
        public string Emergency { get; set; }

        [Required]
        public float Battery { get; set; }

        [Required]
        public float RSSI { get; set; }

        [Required]
        public float SNR { get; set; }
    }
}