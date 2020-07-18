using System.Linq;
using Microsoft.AspNetCore.Mvc;
using practice.Models;
using practice.services;

namespace practice.Controllers
{
    public class UserController : Controller
    {
        private readonly IStorage _storage;

        public UserController(IStorage storage)
        {
            _storage = storage;
        }
        [HttpGet]
        public ActionResult DelReward(int userid,int rewardid)
        {
            _storage.RemoveReward(userid, rewardid);
            return RedirectToAction(nameof(Index));//ну так уж вышло
        }

        public ActionResult Index()
        {
            return View(_storage.GetUsersList());
        }

        [HttpGet]
        public ActionResult AddEdit(int id)//Если передали id=-1 в Get запросе, то add, если нет, то edit
        {
            UserAndRewards userAndRewards = new UserAndRewards
            {
                user = _storage.GetUsersList().FirstOrDefault(us => us.Id == id),
                rewards = _storage.GetRewardsByUserId(id)
            };
            if (userAndRewards.user == null)
            {
                return View();
            }
            return View(userAndRewards);
        }

        [HttpPost]
        public IActionResult AddEdit(UserModel newUser)
        {
            if (newUser.Id == -1)
            {
                _storage.AddUser(newUser);
            }
            else 
            {
                _storage.UpdateUser(newUser);
            }
            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _storage.RemoveUserById(id);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult RewardUser(int id) 
        {
            UserModel awardUser = _storage.GetUsersList().FirstOrDefault(us => us.Id == id);
            if (awardUser!=null) 
            {
                UserAndRewards userAndRewards = new UserAndRewards
                {
                    user = awardUser,
                    rewards = _storage.GetRewardsList()
                };
                return View(userAndRewards);
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult RewardUser(int userid,int rewardid) 
        {
            _storage.RewardUser(userid, rewardid);
            return RedirectToAction(nameof(Index));
        }
    }
}
