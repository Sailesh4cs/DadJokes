using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dad_Jokes.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Dad_Jokes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger)
        {
            _httpClient = new HttpClient();
            _logger = logger;
        }

        public  async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://dad-jokes.p.rapidapi.com/random/joke"),
                Headers =
                    {
                        { "X-RapidAPI-Key", "4684d850abmsh8a9a3941d1d4bfbp1ad684jsn319faa41e852" },
                        { "X-RapidAPI-Host", "dad-jokes.p.rapidapi.com" },
                    },
            };
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var dadjokes = (RandomJoke)JsonConvert.DeserializeObject(body, typeof(RandomJoke));

                ViewBag.Joke = dadjokes.body.FirstOrDefault().Setup;
                ViewBag.Punchline = dadjokes.body.FirstOrDefault().Punchline;
            }
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://dad-jokes.p.rapidapi.com/joke/count"),
                Headers =
                    {
                        { "X-RapidAPI-Key", "4684d850abmsh8a9a3941d1d4bfbp1ad684jsn319faa41e852" },
                        { "X-RapidAPI-Host", "dad-jokes.p.rapidapi.com" },
                    },
            };
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var jokeCount = (JokeCount)JsonConvert.DeserializeObject(body, typeof(JokeCount));

                ViewBag.jokeCountres = jokeCount.body;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
