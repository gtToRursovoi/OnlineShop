using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationApp.Services
{
    public interface ISearchablePage
    {
        void ApplySearch(string query);
    }
}
