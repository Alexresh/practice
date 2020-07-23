using practice.Common.Models;
using System.Collections.Generic;

namespace practice.Common
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
        void RewardUser(int userId, int rewardId);
        bool RemoveReward(int userId, int rewardId);
    }
}
