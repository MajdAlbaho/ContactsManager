using ContactsManager.Constants;
using System;
using System.Net.Http;

namespace ContactsManager.Data
{
    public class Client
    {
        protected static HttpClient client { get; private set; }

        public static HttpClient GetApiClient() {
            if (client == null)
                client = new HttpClient() { BaseAddress = new Uri(Constant.ApiUri) };

            return client;
        }
    }
}
