using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class EndPointController : Controller
    {
        static async Task Getir()
        {
            await GetStudents();
            
        }

        static async Task GetStudents()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/Api");
                    response.EnsureSuccessStatusCode(); // HTTP 200-299 aralığında bir durum dışında hata fırlatır

                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("GET Response:");
                    Console.WriteLine(result);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
