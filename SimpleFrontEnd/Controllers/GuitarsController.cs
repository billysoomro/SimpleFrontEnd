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
            try
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

            catch (Exception ex)
            {
                return Content($"Error fetching guitars: {ex}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)               
        {
            try
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

            catch (Exception ex)
            {
                return Content($"Error fetching guitar details: {ex}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
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

            catch (Exception ex)
            {
                return Content($"Error fetching guitar details: {ex}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guitar guitar)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(guitar), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync("/api/Guitars", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Content("Error updating guitar");
            }

            catch (Exception ex)
            {
                return Content($"Error updating guitar: {ex}");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();            
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guitar guitar)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(guitar), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/Guitars", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Content("Error creating guitar");
            }

            catch (Exception ex)
            {
                return Content($"Error creating guitar: {ex}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
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

            catch (Exception ex)
            {
                return Content($"Error fetching guitar: {ex}");
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Guitars/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                return Content("Error deleting guitar");
            }

            catch (Exception ex)
            {
                return Content($"Error deleting guitar: {ex}");
            }
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
