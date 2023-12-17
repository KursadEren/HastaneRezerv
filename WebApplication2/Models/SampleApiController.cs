// SampleApiController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HastaneRezerv.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]/[action]")]
[ApiController]
public class SampleApiController : ControllerBase
{
    private HastaneContext _context = new HastaneContext();
    private IHttpClientFactory _httpClientFactory;

    public SampleApiController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        using (var httpClient = new HttpClient())
        {
            var apiUrl = "https://localhost:44312/api/user"; // API endpoint'i

            var response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<List<Kullanici>>();
                return Ok(users);
            }

            // Hata durumunu ele alabilirsiniz
            return BadRequest();
        }
    }
}
