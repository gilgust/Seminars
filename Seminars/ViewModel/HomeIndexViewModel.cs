using Seminars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seminars.ViewModel
{
    public class HomeIndexViewModel
    {
        public IList<Seminar> Seminars { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}
