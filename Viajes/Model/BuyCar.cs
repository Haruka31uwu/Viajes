using System;
using System.Collections.Generic;
using System.Text;

namespace Viajes.Model
{
    public class BuyCar
    {
        public string idOfUser { get; set; }   
        public List<Services> ServiceList{ get;set;}
        public float PriceCar { get; set; }
    }
}
