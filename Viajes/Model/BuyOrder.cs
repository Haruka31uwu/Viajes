using System;
using System.Collections.Generic;
using System.Text;

namespace Viajes.Model
{
    public class BuyOrder
    {
        public string UserId { get; set;}
        public string IdOrder { get; set; }
        public Services ServiceOrderElement { get; set; }
        public string PriceOrder { get; set; }

    }
}
