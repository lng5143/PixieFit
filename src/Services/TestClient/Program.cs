using System.Text;
using System.Text.Json;
using PixieFit.Core.Models;

byte[] imageBytes = File.ReadAllBytes("image.jpeg");

var request = new ResizingRequest
{
    PhotoBytes = imageBytes,
    Width = 0.2,
    Height = 0.2
};

var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

var client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5274/");

var response = await client.PostAsync("api/photo/resize", content);

var resizedImageBytes = await response.Content.ReadAsByteArrayAsync();

File.WriteAllBytes("resizedImage.jpeg", resizedImageBytes);

Console.WriteLine("Resized image saved to resizedImage.jpeg");

