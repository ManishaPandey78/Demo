using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace AddRoutesOnMap.CustomControls
{
    public class CustomPin : Pin
    {
        public string Id { get; set; }
        public Pin Pin { get; set; }
        public Position Position { get; set; }
        public Type Type { get; set; }
        public string Label { get; set; }
        public string Address { get; set; }
    }
}
