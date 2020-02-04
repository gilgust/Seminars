using System;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;

namespace Seminars.Areas.Admin.Controllers
{
    public class FroalaApiController : Controller
    {
        private readonly string _uploadPath = "wwwroot/uploads/";
        public IActionResult UploadImage()
        {
            try
            {
                return Json(FroalaEditor.Image.Upload(HttpContext, _uploadPath));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public IActionResult UploadVideo()
        {
            try
            {
                return Json(FroalaEditor.Video.Upload(HttpContext, _uploadPath));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public IActionResult UploadFile()
        {
            try
            {
                return Json(FroalaEditor.File.Upload(HttpContext, _uploadPath));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public IActionResult LoadImages()
        {
            try
            {
                return Json(FroalaEditor.Image.List(_uploadPath));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public IActionResult UploadImageResize()
        {
            var resizeGeometry = new MagickGeometry(300, 300) {IgnoreAspectRatio = true};

            var options = new FroalaEditor.ImageOptions
            {
                ResizeGeometry = resizeGeometry
            };

            try
            {
                return Json(FroalaEditor.Image.Upload(HttpContext, _uploadPath, options));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public IActionResult UploadImageValidation()
        {
            Func<string, string, bool> validationFunction = (filePath, mimeType) => {

                var info = new MagickImageInfo(filePath);

                return info.Width == info.Height;
            };

            var options = new FroalaEditor.ImageOptions
            {
                Fieldname = "myImage",
                Validation = new FroalaEditor.ImageValidation(validationFunction)
            };

            try
            {
                return Json(FroalaEditor.Image.Upload(HttpContext, _uploadPath, options));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public IActionResult UploadFileValidation()
        {
            var fileRoute = "wwwroot/";

            Func<string, string, bool> validationFunction = (filePath, mimeType) => {

                var size = new System.IO.FileInfo(filePath).Length;
                return size <= 10 * 1024 * 1024;
            };

            var options = new FroalaEditor.FileOptions
            {
                Fieldname = "myFile",
                Validation = new FroalaEditor.FileValidation(validationFunction)
            };

            try
            {
                return Json(FroalaEditor.Image.Upload(HttpContext, fileRoute, options));
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public IActionResult DeleteFile()
        {
            try
            {
                FroalaEditor.File.Delete(HttpContext.Request.Form["src"]);
                return Json(true);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public IActionResult DeleteVideo()
        {
            try
            {
                FroalaEditor.Video.Delete(HttpContext.Request.Form["src"]);
                return Json(true);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }


        public IActionResult DeleteImage()
        {
            try
            {
                FroalaEditor.Image.Delete(HttpContext.Request.Form["src"]);
                return Json(true);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public IActionResult S3Signature()
        {
            var config = new FroalaEditor.S3Config
            {
                Bucket = Environment.GetEnvironmentVariable("AWS_BUCKET"),
                Region = Environment.GetEnvironmentVariable("AWS_REGION"),
                KeyStart = Environment.GetEnvironmentVariable("AWS_KEY_START"),
                Acl = Environment.GetEnvironmentVariable("AWS_ACL"),
                AccessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY"),
                SecretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY"),
                Expiration = Environment.GetEnvironmentVariable("AWS_EXPIRATION") // Expiration s3 image signature #11
            };

            return Json(FroalaEditor.S3.GetHash(config));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
