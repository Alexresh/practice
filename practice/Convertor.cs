using practice.Common.Models;
using practice.Models;
using System.Collections.Generic;

namespace practice
{
    public static class Convertor
    {
        public static RewardViewModel ConvertToRewardViewModel(this RewardModel domainRewardModel)
        {
            if (domainRewardModel == null)
            {
                return null;
            }
            return new RewardViewModel
            {
                Id = domainRewardModel.Id,
                Title = domainRewardModel.Title,
                Description = domainRewardModel.Description
            };
        }
        public static RewardModel ConvertToRewardDomainModel(this RewardViewModel viewRewardModel)
        {
            return new RewardModel
            {
                Id = viewRewardModel.Id,
                Title = viewRewardModel.Title,
                Description = viewRewardModel.Description
            };
        }
        public static UserViewModel ConvertToUserViewModel(UserModel domainUserModel, List<RewardModel> rewardList)
        {
            return new UserViewModel
            {
                Id = domainUserModel.Id,
                FirstName = domainUserModel.FirstName,
                LastName = domainUserModel.LastName,
                Birthdate = domainUserModel.Birthdate,
                Rewards = rewardList.ConvertAll(x => x.ConvertToRewardViewModel())
            };
        }
        public static UserModel ConvertToUserDomainModel(this UserViewModel userViewModel)
        {
            return new UserModel
            {
                Id = userViewModel.Id,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Birthdate = userViewModel.Birthdate
            };
        }
    }
}
