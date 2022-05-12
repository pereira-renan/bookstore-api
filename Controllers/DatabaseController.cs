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
using bookstore_api.Context;
using Microsoft.EntityFrameworkCore;
/*
* GOOGLE API DOCUMENTATION: https://developers.google.com/books/docs/v1/using#st_params
*/
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [Produces("application/json")]
    public class DatabaseController : ControllerBase
    {
        public readonly IDatabaseBusiness _databaseBusiness;
        public DatabaseController(IDatabaseBusiness databaseBusiness)
        {
            _databaseBusiness = databaseBusiness;
        }

        [HttpPut]
        [Route("Books")]
        public async Task<BookDataResponse> InsertAll()
        {
            var result = await _databaseBusiness.InsertAll();
            return result;
        }
    }
}

