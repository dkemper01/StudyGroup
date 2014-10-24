using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace AsyncControllerActions.Models
{
    public class RequestStatistics
    {
        public Uri Uri { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public long ContentLength { get; set; }

        public static RequestStatistics Create(HttpResponseMessage response)
        {
            var obj = new RequestStatistics
            {
                Uri = response.RequestMessage.RequestUri,
                StatusCode = response.StatusCode,
                ContentLength = response.Content.Headers.ContentLength ?? 0
            };

            return obj;
        }
    }
}