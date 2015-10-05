using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TagHelperSamples.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TagHelperSamples.Controllers
{
    public class SamplesController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AlertTagHelper()
        {
            var model = new TestModel
            {
                Header = "Unable to save form",
                Message = "Please fix the highlighted errors on the form below."
            };
            return View(model);
        }
    }
}
