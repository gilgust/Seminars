using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Seminars.Models;
using Seminars.Repositories;

namespace Seminars.Controllers
{
    //public class FileController : Controller
    //{
    //    private AppDbContext _context;
    //    private IHostingEnvironment _hostingEnvironment;
    //    public FileController(AppDbContext context, IHostingEnvironment hostingEnvironment)
    //    {
    //        _context = context;
    //        _hostingEnvironment = hostingEnvironment;
    //    }
    //    public IActionResult Index() => View(_context.Files.ToList());

    //    [HttpPost]
    //    public async Task<IActionResult> AddFile(IFormFile uploadedFile)
    //    {
    //        if (uploadedFile != null)
    //        {
    //            //путь к папке Files
    //            var path = "/Files/" + uploadedFile.FileName;
    //            //сохраняем файл в папку Files в каталоге wwwroot
    //            using (var filStream = new FileStream(_hostingEnvironment.WebRootPath + path, FileMode.Create))
    //            {
    //                await uploadedFile.CopyToAsync(filStream);
    //            }

    //            var file = new FileModel {Name = uploadedFile.FileName, Path = path};
    //            _context.Files.Add(file);
    //            _context.SaveChanges();

    //        }
    //        return RedirectToAction(nameof(Index));
    //    }
    //}
}