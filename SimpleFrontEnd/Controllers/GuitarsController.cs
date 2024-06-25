using Microsoft.AspNetCore.Mvc;
using SimpleFrontEnd.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace SimpleFrontEnd.Controllers
{
    public class GuitarsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public GuitarsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("SimpleCrudApiClient");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Guitars");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var guitars = JsonSerializer.Deserialize<List<Guitar>>(content);

                return View(guitars);
            }

            return Content("Error fetching guitars");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)               
        {
            var response = await _httpClient.GetAsync($"/api/Guitars/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var guitar = JsonSerializer.Deserialize<Guitar>(content);

                return View(guitar);
            }

            return Content("Error fetching guitar details");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Guitars/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var guitar = JsonSerializer.Deserialize<Guitar>(content);

                return View(guitar);
            }

            return Content("Error fetching guitar details");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guitar guitar)
        {
            var content = new StringContent(JsonSerializer.Serialize(guitar), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/api/Guitars", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return Content("Error updating guitar");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();            
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guitar guitar)
        {
            var content = new StringContent(JsonSerializer.Serialize(guitar), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Guitars", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return Content("Error creating guitar");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Guitars/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var guitar = JsonSerializer.Deserialize<Guitar>(content);

                return View(guitar);
            }

            return Content("Error fetching guitar details");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Guitars/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return Content("Error deleting guitar");
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
