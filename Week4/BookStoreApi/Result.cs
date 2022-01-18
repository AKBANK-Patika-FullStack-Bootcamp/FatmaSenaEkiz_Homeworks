using System;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi
{
     public class Result  /*A Result class, response format*/
    {
        public int HttpStatusCode { get; set; }
        public string? Message  { get; set; }
    }
}