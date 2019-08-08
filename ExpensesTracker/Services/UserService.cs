using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ExpensesTracker.Services
{
    public class UserService
    {
        public static string GetById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                //httpClient.DefaultRequestHeaders.Add(RequestConstants.UserAgent, RequestConstants.UserAgentValue);

                var response = httpClient.GetStringAsync(new Uri("https://localhost:44341/api/values")).Result;

                return response;
            }
        }
    }
}