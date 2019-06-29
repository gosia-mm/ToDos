using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace ToDos.Services
{
    public class ToDosRestService
    {
        public HttpClient Client { get; set; }

        public ToDosRestService()
        {
            Client = new HttpClient(); // klient REST
        }
    }
}
