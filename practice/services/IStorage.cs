using practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace practice.services
{
    public interface IStorage
    {
        int AddReward(RewardModel reward);
        int AddUser(UserModel user);
        bool RemoveRewardById(int id);
        bool RemoveUserById(int id);
        List<RewardModel> GetRewardsList();
        List<UserModel> GetUsersList();
        bool UpdateUser(UserModel user);
        bool UpdateReward(RewardModel reward);
        List<RewardModel> GetRewardsByUserId(int id);
        bool RewardUser(int userId, int rewardId);
        bool RemoveReward(int userId, int rewardId);
    }
    public class InMemoryStorage : IStorage
    {
        private static readonly List<UserModel> _users = new List<UserModel>
        {
            new UserModel{Id = 0,FirstName = "Ivan",LastName="Borodin",Birthdate=new DateTime(2000,10,10),Rewards = new List<int>{0,1} },
            new UserModel{Id = 1,FirstName = "Victoria",LastName="Pavlova",Birthdate=new DateTime(1998,01,13),Rewards = new List<int>{1,2} },
            new UserModel{Id = 2,FirstName = "Rose",LastName="Bezrukova",Birthdate=new DateTime(1989,09,16),Rewards = new List<int>{2,3} },
            new UserModel{Id = 3,FirstName = "Lucas",LastName="Belousov",Birthdate=new DateTime(1993,08,6),Rewards = new List<int>{3,4} },
            new UserModel{Id = 4,FirstName = "Jasmine",LastName="Yermakova",Birthdate=new DateTime(1953,10,22),Rewards = new List<int>{4,5} },
            new UserModel{Id = 5,FirstName = "Nicodemus",LastName="Maslow",Birthdate=new DateTime(1975,11,12),Rewards = new List<int>{5,0} }

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
        public int AddReward(RewardModel reward)
        {
            reward.Id = _rewards.Any() ? _rewards.Max(task => task.Id) + 1 : 0;
            _rewards.Add(reward);
            return reward.Id;
        }

        public int AddUser(UserModel user)
        {
            user.Rewards = new List<int>();
            user.Id = _users.Any() ? _users.Max(task => task.Id) + 1 : 0;
            _users.Add(user);
            return user.Id;
        }

        public List<RewardModel> GetRewardsByUserId(int id)
        {
            List <RewardModel> userRewards = new List<RewardModel>();
            UserModel _user = _users.FirstOrDefault(us => us.Id == id);
            if (_user == null) 
            {
                return null;
            }
            foreach (var rewardId in _user.Rewards)
            {
                userRewards.Add(_rewards.FirstOrDefault(re => re.Id == rewardId));
            }
            return userRewards;
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
            UserModel userRemove = _users.FirstOrDefault(us => us.Id == userId);
            if (userRemove == null)
                return false;
            return userRemove.Rewards.Remove(rewardId);
        }

        public bool RewardUser(int userId, int rewardId)
        {
            UserModel user = _users.FirstOrDefault(us => us.Id == userId);
            if (user == null)
                return false;
            if (_rewards.Exists(x => x.Id == rewardId)) 
            {
                user.Rewards.Add(rewardId);
                return true;
            }
            return false;
        }

        public bool UpdateReward(RewardModel reward)
        {
            RewardModel rewardUpdate = _rewards.FirstOrDefault(re => re.Id == reward.Id);
            if (rewardUpdate == null)
                return false;
            rewardUpdate.Title = reward.Title;
            rewardUpdate.Description = reward.Description;
            return true;
        }

        public bool UpdateUser(UserModel user)
        {
            UserModel userUpdate = _users.FirstOrDefault(us => us.Id == user.Id);
            if (userUpdate == null)
                return false;
            userUpdate.FirstName = user.FirstName;
            userUpdate.LastName = user.LastName;
            userUpdate.Birthdate = user.Birthdate;
            return true;
        }
    }
}
