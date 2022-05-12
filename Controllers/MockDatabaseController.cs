using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Diagnostics;
/*
* GOOGLE API DOCUMENTATION: https://developers.google.com/books/docs/v1/using#st_params
*/
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [Produces("application/json")]
    public class MockDatabaseController : ControllerBase
    {
        public HttpClient ApiClient { get; private set; }

        [HttpGet]
        [Route("ListAll")]
        public async Task<BookDataResponse> SearchBooks(string searchTerms = "google story", int startAtResult = 0, int resultsPerPage = 10)
        {
            string url = "https://www.googleapis.com/books/v1/volumes?q=" + searchTerms + "&langRestrict=en&startIndex=" + startAtResult + "&maxResults=" + resultsPerPage;

            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (HttpResponseMessage response = await new HttpClient().GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    BookDataResponse result = await response.Content.ReadFromJsonAsync<BookDataResponse>();

                    ApiClient.Dispose();
                    return result;
                }
                else
                {
                    ApiClient.Dispose();
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        [HttpGet]
        [Route("{BookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<BookData> BookDetails(string BookId = "zyTCAlFPjgYC")
        {
            string url = "https://www.googleapis.com/books/v1/volumes/" + BookId;

            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (HttpResponseMessage response = await new HttpClient().GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    BookData result = await response.Content.ReadFromJsonAsync<BookData>();

                    ApiClient.Dispose();
                    return result;
                }
                else
                {
                    ApiClient.Dispose();
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}

