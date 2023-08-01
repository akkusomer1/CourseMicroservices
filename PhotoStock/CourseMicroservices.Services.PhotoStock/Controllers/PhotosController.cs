using CourseMicroservices.Services.PhotoStock.Models;
using CourseMicroservices.Shared.ControllerBases;
using CourseMicroservices.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseMicroservices.Services.PhotoStock.Controllers
{
    [Authorize] 
    public class PhotosController : BaseController
    {
     
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo == null || photo.Length <= 0)
                return CreateActionResult(ResponseDto<NoContentDto>.Fail("Please You can add photo!", 400));


            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Photos", photo.FileName);

            using FileStream stream = new FileStream(path, FileMode.Create);
            await photo.CopyToAsync(stream, cancellationToken);

            PhotoDto photoDto = new PhotoDto()
            {
                Url =Path.Combine("Photos", photo.FileName)
            };

            return CreateActionResult(ResponseDto<PhotoDto>.Success(photoDto, 200));
        }

       
        [HttpDelete]
        public IActionResult DeletePhoto(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photoUrl);
            if (!System.IO.File.Exists(path))
                return CreateActionResult(ResponseDto<NoContentDto>.Fail("Photo Not Found", 404));

            System.IO.File.Delete(path);

            return CreateActionResult(ResponseDto<NoContentDto>.Success(204));

        }
    }
}
