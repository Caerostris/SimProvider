using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimProvider.Save
{
    [Serializable]
    public class Save
    {
        public List<SimProvider.Battery> _battery = new List<SimProvider.Battery>();
        public List<SimProvider.Engine> _engine = new List<SimProvider.Engine>();
        public List<SimProvider.Tire> _tire = new List<SimProvider.Tire>();
        public List<SimProvider.Bike> _bike = new List<SimProvider.Bike>();
    }
}
