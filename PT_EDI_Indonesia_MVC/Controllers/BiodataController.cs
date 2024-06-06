using Microsoft.AspNetCore.Mvc;
using PT_EDI_Indonesia_MVC.Data.IRepository;

namespace PT_EDI_Indonesia_MVC.Controllers
{
    [Route("[controller]")]
    public class BiodataController : Controller
    {
        private readonly ILogger<BiodataController> _logger;
        private readonly IBiodataRepository _bioRepo;

        public BiodataController(ILogger<BiodataController> logger, IBiodataRepository bioRepo)
        {
            _bioRepo = bioRepo;
            _logger = logger;
        }

        // [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var bioVm = await _bioRepo.GetBiodatasAsync();
                return View(bioVm);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Error();
            }
        }
        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}