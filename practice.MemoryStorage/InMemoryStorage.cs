using practice.Common;
using practice.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace practice.services
{
    public class InMemoryStorage : IStorage
    {
        private static readonly List<UserModel> _users = new List<UserModel>
        {
            new UserModel{Id = 0,FirstName = "Ivan",LastName="Borodin",Birthdate=new DateTime(2000,10,10) },
            new UserModel{Id = 1,FirstName = "Victoria",LastName="Pavlova",Birthdate=new DateTime(1998,01,13) },
            new UserModel{Id = 2,FirstName = "Rose",LastName="Bezrukova",Birthdate=new DateTime(1989,09,16) },
            new UserModel{Id = 3,FirstName = "Lucas",LastName="Belousov",Birthdate=new DateTime(1993,08,6) },
            new UserModel{Id = 4,FirstName = "Jasmine",LastName="Yermakova",Birthdate=new DateTime(1953,10,22) },
            new UserModel{Id = 5,FirstName = "Nicodemus",LastName="Maslow",Birthdate=new DateTime(1975,11,12) }

        };
        private static readonly List<RewardModel> _rewards = new List<RewardModel>
        {
            new RewardModel{Id=0,Title="Best seller",Description="Be the best seller for a month" },
            new RewardModel{Id=1,Title="Employee of the year",Description="Get the least amount of fines per year" },
            new RewardModel{Id=2,Title="1 year project",Description="Worked on one project for 1 year" },
            new RewardModel{Id=3,Title="5 year project",Description="Worked on one project for 5 year" },
            new RewardModel{Id=4,Title="Stop this",Description="Worked on one project for 15 year" },
            new RewardModel{Id=5,Title="Hmm",Description="Reward for beautiful eyes" }
        };
        private static readonly List<UserAndRewardModel> _userAndRewards = new List<UserAndRewardModel>
        {
            new UserAndRewardModel{UserId=0, RewardId=0},
            new UserAndRewardModel{UserId=0, RewardId=1},
            new UserAndRewardModel{UserId=1, RewardId=1}
        };
        public int AddReward(RewardModel reward)
        {
            if (reward == null)
            {
                throw new ArgumentNullException();
            }
            reward.Id = _rewards.Any() ? _rewards.Max(task => task.Id) + 1 : 0;
            _rewards.Add(reward);
            return reward.Id;
        }

        public int AddUser(UserModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            user.Id = _users.Any() ? _users.Max(task => task.Id) + 1 : 0;
            _users.Add(user);
            return user.Id;
        }

        public List<RewardModel> GetRewardsByUserId(int id)
        {
            List<RewardModel> rewards = new List<RewardModel>();
            List<UserAndRewardModel> userRewards = _userAndRewards.FindAll(x => x.UserId == id);
            foreach (var item in userRewards)
            {
                rewards.Add(_rewards.FirstOrDefault(x => x.Id == item.RewardId));
            }
            return rewards;
        }

        public List<RewardModel> GetRewardsList()
        {
            return _rewards;
        }

        public List<UserModel> GetUsersList()
        {
            return _users;
        }

        public bool RemoveRewardById(int id)
        {
            RewardModel rewardRemove = _rewards.FirstOrDefault(re => re.Id == id);
            if (rewardRemove == null)
                return false;
            return _rewards.Remove(rewardRemove);
        }

        public bool RemoveUserById(int id)
        {
            UserModel userRemove = _users.FirstOrDefault(us => us.Id == id);
            if (userRemove == null)
                return false;
            return _users.Remove(userRemove);
        }

        public bool RemoveReward(int userId, int rewardId)
        {
            UserAndRewardModel userAndRewardModel = _userAndRewards.FirstOrDefault(x => (x.UserId == userId && x.RewardId == rewardId));
            return _userAndRewards.Remove(userAndRewardModel);
        }

        public void RewardUser(int userId, int rewardId)
        {
            if (userId > 0 && rewardId > 0)
            {
                UserAndRewardModel userAndRewardModel = new UserAndRewardModel
                {
                    UserId = userId,
                    RewardId = rewardId
                };
                _userAndRewards.Add(userAndRewardModel);
            }
            else 
            {
                throw new ArgumentException();
            }
        }

        public bool UpdateReward(RewardModel reward)
        {
            if (reward == null)
            {
                throw new ArgumentNullException();
            }
            var rewardUpdate = _rewards.FirstOrDefault(re => re.Id == reward.Id);
            if (rewardUpdate == null)
                throw new ArgumentException();
            rewardUpdate.Title = reward.Title;
            rewardUpdate.Description = reward.Description;
            return true;
        }

        public bool UpdateUser(UserModel user)
        {
            if (user == null) 
            {
                throw new ArgumentNullException();
            }
            var userUpdate = _users.FirstOrDefault(us => us.Id == user.Id);
            if (userUpdate == null)
            { 
                throw new ArgumentException(); 
            }
            userUpdate.FirstName = user.FirstName;
            userUpdate.LastName = user.LastName;
            userUpdate.Birthdate = user.Birthdate;
            return true;
        }
    }
}
