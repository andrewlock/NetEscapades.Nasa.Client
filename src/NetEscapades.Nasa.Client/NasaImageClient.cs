using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NetEscapades.Nasa
{
    public partial class NasaImageClient : INasaImageClient, IDisposable
    {
        const string DefaultApiAddress = "https://images-api.nasa.gov/";
        private readonly HttpClient _apiClient;

        public NasaImageClient(string host, HttpMessageHandler handler)
        {
            var baseUri = new Uri(host);
            _apiClient = new HttpClient(handler)
            {
                BaseAddress = baseUri,
            };
        }

        public NasaImageClient(string host) : this(host, new HttpClientHandler()) { }

        public NasaImageClient() : this(DefaultApiAddress) { }


        #region IDisposable Support
        private bool _isDisposing = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposing)
            {
                if (disposing)
                {
                    _apiClient.Dispose();
                }

                _isDisposing = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
