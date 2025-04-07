using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaaFramework.Config
{
    public class TestSettings
    {
        public string[] Args { get; set; }
        public float Timeout { get; set; }
        public bool Headless { get; set; }
        public int SlowMo { get; set; }
        public DriverType DriverType { get; set; }
        public string ApplicationUrl { get; set; }

    }

    public enum DriverType
    {
        Chromium,
        Chrome,
        Firefox,
        Edge
    }
}
