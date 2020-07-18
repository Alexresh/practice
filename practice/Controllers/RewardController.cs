using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practice.Models;
using practice.services;

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
            return View(_storage.GetRewardsList());
        }

        [HttpGet]
        public ActionResult AddEdit(int id)
        {
            RewardModel reward = _storage.GetRewardsList().FirstOrDefault(re => re.Id == id);
            if (reward == null)
            {
                return View();
            }
            return View(reward);
        }

        [HttpPost]
        public IActionResult AddEdit(RewardModel newReward)
        {
            if (newReward.Id == -1)
            {
                _storage.AddReward(newReward);
            }
            else
            {
                _storage.UpdateReward(newReward);
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
