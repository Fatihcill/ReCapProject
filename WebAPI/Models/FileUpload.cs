using Microsoft.AspNetCore.Http;

namespace WebAPI.Models
{
    public class FileUpload
    {
        public IFormFile files { get; set; }
    }
}