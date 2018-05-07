using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsShop.Data
{
    public class ConnectionProvider
    {
        public static string ConnectionString => $"Server=.;Database=ProductsShop;Integrated Security=True";
    }
}
