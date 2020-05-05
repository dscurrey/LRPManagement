using LRPManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using LRPManagement.Data.Characters;
using LRPManagement.Data.Craftables;
using LRPManagement.Data.Players;
using LRPManagement.Data.Skills;
using LRPManagement.ViewModels;

namespace LRPManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICharacterRepository _characterRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICraftableRepository _itemRepository;

        public HomeController(ILogger<HomeController> logger, ICharacterRepository charRepo, ISkillRepository skillRepo, IPlayerRepository playerRepo, ICraftableRepository itemRepo)
        {
            _logger = logger;
            _characterRepository = charRepo;
            _skillRepository = skillRepo;
            _playerRepository = playerRepo;
            _itemRepository = itemRepo;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                CharacterCount = await _characterRepository.GetCount(),
                SkillCount = await _skillRepository.GetCount(),
                PlayerCount = await _playerRepository.GetCount(),
                ItemCount = await _itemRepository.GetCount()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}