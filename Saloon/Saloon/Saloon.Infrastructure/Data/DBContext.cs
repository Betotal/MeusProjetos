using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saloon.Infrastructure.Data
{
    public class DBContext
    {
        public string ContextoDB()
        {
            return @"Server=(localdb)\MSSQLLocalDB;Database=Saloon;Trusted_Connection=True;";
        }
    }
}