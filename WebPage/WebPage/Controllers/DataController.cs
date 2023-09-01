using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebPage.Models;
using WebPage.ViewModel;

namespace WebPage.Controllers
{
    public class DataController : Controller
    {
        private readonly ApiResumeContext _context;
        public DataController( ApiResumeContext conte) { _context = conte; }

        public IActionResult Index(string title)
        {
            var datos = from dl in _context.Datos select dl;

            if (!string.IsNullOrEmpty(title))
            {
                datos = datos.Where(d => d.Title.Contains(title));
            }
           
            var dt = new List<DatosViewModel>();

            foreach ( var datosItem in datos)
            {
                dt.Add(new DatosViewModel
                {
                    Date = datosItem.Date,
                    Id = datosItem.Id,
                    Link = datosItem.Link,
                    Title = datosItem.Title,
                    Content = datosItem.Content,
                });
                
            }

            dt.OrderByDescending(x=> x.Date);
            return View(dt);
            
        }
    }
}
