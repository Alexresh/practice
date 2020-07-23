using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using practice.Common;
using practice.Common.Models;
using practice.Models;

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
            List<UserModel> domainUsers = _storage.GetUsersList();
            List<UserViewModel> viewUsers = new List<UserViewModel>();
            foreach (var domainUser in domainUsers)
            {
                viewUsers.Add(Convertor.ConvertToUserViewModel(domainUser, new List<RewardModel>()));
            }
            return View(viewUsers);
        }

        [HttpGet]
        public ActionResult AddEdit(int id)//Если передали id=-1 в Get запросе, то add, если нет, то edit
        {
            UserModel userDomain = _storage.GetUsersList().FirstOrDefault(us => us.Id == id);

            if (userDomain == null)
            {
                return View();
            }
            else
            {
                UserViewModel userView = Convertor.ConvertToUserViewModel(userDomain, _storage.GetRewardsByUserId(id));
                return View(userView);
            }
        }

        [HttpPost]
        public IActionResult AddEdit(UserViewModel newUser)
        {
            if (newUser.Id == -1)
            {
                _storage.AddUser(newUser.ConvertToUserDomainModel());
            }
            else 
            {
                _storage.UpdateUser(newUser.ConvertToUserDomainModel());
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
            UserModel userModel = _storage.GetUsersList().FirstOrDefault(us => us.Id == id);
            if (userModel != null) 
            {
                UserViewModel awardUser = Convertor.ConvertToUserViewModel(userModel, _storage.GetRewardsList());
                return View(awardUser);
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
