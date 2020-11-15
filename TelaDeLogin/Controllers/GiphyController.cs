using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TelaDeLogin.Data;
using TelaDeLogin.Models;

namespace TelaDeLogin.Controllers
{
    [Route("giphy")]
    public class GiphyController : Controller
    {
        private readonly IHttpClientFactory clientFactory;

        public GiphyController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        
        public async Task <IActionResult> Index()
        {

            RetornoApiGiphy retornoApiGiphy = null;
            var client = this.clientFactory.CreateClient("giphy");
            var response = await client.GetAsync("search?api_key=3wVmcvpMRsPSiyvH6nQAJj7FduI7ZfFO&q=cat&rating=R");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                retornoApiGiphy = JsonConvert.DeserializeObject<RetornoApiGiphy>(responseString);
                
            }
            Procurar procurar = new Procurar();
            procurar.RetornoApiGiphy = retornoApiGiphy;
            return View(procurar);
        }

        //[HttpGet]
        //[Route("procurar")]
        //public IActionResult Procurar()
        //{
        //    return View("Procurar");
        //}

        [HttpPost]
        [Route("Procurar")]
        public IActionResult Procurar(Procurar procurar)
        {
            //string teste = descricao.GetDescricao();
            string descricao = string.IsNullOrWhiteSpace(procurar.Giphy.Descricao) ? "all" : procurar.Giphy.Descricao;
            Procurar giphy = new Procurar();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                var configuration = builder.Build();

                string baseURL = configuration["UrlSearch:Url"];
                string key = configuration["ApiKey:Key"];
                //string palavraChave = "";
                string limit = "20";
                string offset = "5";
                string rating = "g";
                string lang = "pt";
                string random_id = "1";
                //string url = "https://api.giphy.com/v1/gifs/search?api_key=3wVmcvpMRsPSiyvH6nQAJj7FduI7ZfFO&q=pipoca&limit=20&offset=5&rating=g&lang=pt&random_id=1";
                HttpResponseMessage response = client.GetAsync(
                   baseURL +
                   $"api_key={key}&" +
                   $"q={descricao}&" +
                   $"limit={limit}&" +
                   $"offset={offset}&" +
                   $"rating={rating}&" +
                   $"lang={lang}&" +
                   $"random_id={random_id}"
                   ).Result;

                response.EnsureSuccessStatusCode();
                string conteudo =
                   response.Content.ReadAsStringAsync().Result;

                //dynamic resultado = JsonConvert.DeserializeObject(conteudo);
                //Root retorno = JsonConvert.DeserializeObject<Root>(conteudo);

                giphy.RetornoApiGiphy = JsonConvert.DeserializeObject<RetornoApiGiphy>(conteudo);

            }

            return View(giphy);
        }





    }
}
