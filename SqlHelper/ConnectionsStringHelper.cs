using System;
namespace RandomTextCase.SqlHelper
{
    public class ConnectionsStringHelper : IConnectionsStringHelper
    {

        private readonly IConfiguration _configuration;

        public ConnectionsStringHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection").ToString();
        }
    }
}

