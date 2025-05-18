using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreMvc.Models;
using Microsoft.Extensions.FileProviders;

namespace AspNetCoreMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IFileProvider _fileProvider;

    public HomeController(ILogger<HomeController> logger, IFileProvider fileProvider)
    {
        _logger = logger;
        _fileProvider = fileProvider;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult SaveImage()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> SaveImage(IFormFile file)
    {
        if (file is { Length: > 0 })
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            ViewBag.Message = "Image saved successfully!";
        }
        else
        {
            ViewBag.Message = "Please select an image to upload.";
        }
        return View();
    }
    
    public IActionResult ImageShow()
    {
        var images = _fileProvider.GetDirectoryContents("wwwroot/images")
            .AsEnumerable().Select(x =>x.Name );
        
        if (!images.Any()) ViewBag.Message = "Resim bulunamadı.";
        return View(images);
    }

    [HttpPost]
    public IActionResult ImageShow(string name)
    {
        var file = _fileProvider.GetDirectoryContents("wwwroot/images")
            .FirstOrDefault(x => x.Name == name);

        if (file == null) return RedirectToAction("ImageShow");
        if (file.PhysicalPath != null)
            System.IO.File.Delete(file.PhysicalPath);

        return RedirectToAction("ImageShow");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}