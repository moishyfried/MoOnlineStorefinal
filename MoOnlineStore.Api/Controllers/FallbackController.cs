using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoOnlineStore.Api.Controllers
{
   
    public class FallbackController : Controller
    {
        public IActionResult Index()
        {
             return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot" ,"index.HTML"),"text/HTML");
        }
    }
}