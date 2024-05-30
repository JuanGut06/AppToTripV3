using Microsoft.Maui.Controls.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppToTripV3.Controles
{
    public class CustomPin : Pin
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Tag { get; set; }
        public string Label { get; set; }
        public string Position { get; set; }

        public string Icon { get; set; }

    }
}
