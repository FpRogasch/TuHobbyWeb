using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuHobbyWeb.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int CountUsers { get; set; }

        public int CountGames { get; set; }

        public int CountSales { get; set; }

        public int CountNintendo { get; set; }

        public int CountPlaystation { get; set; }

        public int CountXbox { get; set; }

        public int CountPC { get; set; }

    }
}