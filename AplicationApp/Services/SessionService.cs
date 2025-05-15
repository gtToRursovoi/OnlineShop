using Database.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationApp.Services
{
    public static class SessionService
    {
        public static int CurrentUserId { get; set; }
        public static string CurrentUserLogin { get; set; }
    }
}
