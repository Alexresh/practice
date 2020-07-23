using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using practice.Common;
using practice.Models;

namespace practice.Controllers
{
    public class RewardController : Controller
    {
        private readonly IStorage _storage;

        public RewardController(IStorage storage)
        {
            _storage = storage;
        }

        public ActionResult Index()
        {
            return View(_storage.GetRewardsList().ConvertAll(x=>x.ConvertToRewardViewModel()));
        }

        [HttpGet]
        public ActionResult AddEdit(int id)
        {
            RewardViewModel reward = _storage.GetRewardsList().FirstOrDefault(re => re.Id == id).ConvertToRewardViewModel();
            if (reward == null)
            {
                return View();
            }
            return View(reward);
        }

        [HttpPost]
        public IActionResult AddEdit(RewardViewModel newReward)
        {
            if (newReward.Id == -1)
            {
                _storage.AddReward(newReward.ConvertToRewardDomainModel());
            }
            else
            {
                _storage.UpdateReward(newReward.ConvertToRewardDomainModel());
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _storage.RemoveRewardById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
