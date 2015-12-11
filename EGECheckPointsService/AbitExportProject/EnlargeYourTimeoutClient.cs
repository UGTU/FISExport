using System;
using System.Net;

namespace AbitExportProject
{
    public class EnlargeYourTimeoutClient : WebClient
    {
        public int Timeout { get; set; }

        public EnlargeYourTimeoutClient() : this(120000) { }

        public EnlargeYourTimeoutClient(int timeout)
        {
            this.Timeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request != null)
            {
                request.Timeout = this.Timeout;
            }
            return request;
        }
    }
}