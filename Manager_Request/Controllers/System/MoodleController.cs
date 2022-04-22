using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Request.Controllers.System
{
    public class MoodleController : BaseApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCourse()
        {
            var cl = new HttpClient();
            cl.BaseAddress = new Uri("https://api-aao.eiu.edu.vn/api/qtmd/w-locdsmonhoctheohocky");
            string _ContentType = "application/json";
            cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));
            var _CredentialBase64 = "TW9vZGxlOlh6MWhhMlk3UldGelNBPT0=";
            cl.DefaultRequestHeaders.Add("Authorization", String.Format("Basic {0}", _CredentialBase64));
            var param = new
            {
                Quarter = "1",
                School_Year = "2020",
                School_Short_Name = "03"
            };
            string httpClinet = Newtonsoft.Json.JsonConvert.SerializeObject(param);

            HttpContent _Body = new StringContent(httpClinet, Encoding.UTF8, "application/json");
            _Body.Headers.ContentType = new MediaTypeHeaderValue(_ContentType);
            var response = await cl.PostAsync("https://api-aao.eiu.edu.vn/api/qtmd/w-locdsmonhoctheohocky", _Body);
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResultData<Course>>(result);
            var test = data.Data.DS_Courses;
            var test2 = test.AsQueryable();
            return Ok(test);
        }
        private class ParameterMoodle
        {
            public string Quarter { get; set; }

            public string School_Year { get; set; }

            public string School_Short_Name { get; set; }
        }

        public class ResultData<T>
        {
            public virtual T Data { get; set; }

            public bool Result { get; set; }

            public int Code { get; set; }

        }

        public class Course
        {
            public List<Tesst> DS_Courses { get; set; }
        }


        public class Tesst
        {
            public string Course_ID { get; set; }

            public string Course_Short_Name { get; set; }
        }


    }


}
