using CSharpUtility.Algorithms;
using System;
using System.Collections.Generic;
using System.Net;

namespace CSharpUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            //Helpers.RunningObjectTableHelper.ROTHelper.GetIDEInstances(true, Helpers.RunningObjectTableHelper.ROTHelper.IDE.VisualStudio);

            // Make a microservice to keep Heroku Dyno Alive
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://ardahan.herokuapp.com");
            //request.AllowAutoRedirect = false; // find out if this site is up and don't follow a redirector
            //request.Method = System.Net.WebRequestMethods.Http.Head;
            //try
            //{
            //    WebResponse response = request.GetResponse();
            //    // do something with response.Headers to find out information about the request
            //}
            //catch (WebException wex)
            //{
            //    //set flag if there was a timeout or some other issues
            //}

            Algorithms.Algorithms algoCls = new Algorithms.Algorithms();
            int[] src = { 1, 2, 3, 7, 5 };
            int sum = 12;
            int[] result = algoCls.findLongestSubarrayBySum(src, sum);

            
        }


    }
}
