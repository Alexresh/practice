using Microsoft.AspNetCore.Mvc;
using Moq;
using practice.Common;
using practice.Common.Models;
using practice.Controllers;
using practice.Models;
using System.Collections.Generic;
using Xunit;

namespace practice.Tests
{
    public class RewardControllerTests
    {
        [Fact]
        public void ShouldBeAbleToReturnIdWhenAddingReward()
        {
            var mockedStorage = new Mock<IStorage>();
            mockedStorage.Setup(x => x.GetRewardsList()).Returns(new List<RewardModel>());
            var controller = new RewardController(mockedStorage.Object);
            ViewResult result = (ViewResult)controller.Index();
            int expectedCount = 0;

            List<RewardViewModel> resultModel = (List<RewardViewModel>)result.Model;

            Assert.NotNull(result);
            Assert.NotNull(resultModel);
            Assert.Equal(resultModel.Count, expectedCount);
        }
        [Fact]
        public void ShouldBeAbleToReturnViewWithoutRewards()
        {
            var mockedStorage = new Mock<IStorage>();
            List<RewardModel> rewards = new List<RewardModel>
            {
                new RewardModel{Id=1,Title="1",Description="1" },
                new RewardModel{Id=2,Title="2",Description="2" },
                new RewardModel{Id=3,Title="3",Description="3" },
            };
            mockedStorage.Setup(x => x.GetRewardsList()).Returns(rewards);
            var controller = new RewardController(mockedStorage.Object);
            ViewResult result = (ViewResult)controller.AddEdit(-1);

            List<RewardViewModel> resultModel = (List<RewardViewModel>)result.Model;

            Assert.NotNull(result);
            Assert.Null(resultModel);
        }
        [Fact]
        public void ShouldBeAbleToReturnViewWithRewards()
        {
            var mockedStorage = new Mock<IStorage>();
            List<RewardModel> rewards = new List<RewardModel>
            {
                new RewardModel{Id=1,Title="1",Description="1" },
                new RewardModel{Id=2,Title="2",Description="2" },
                new RewardModel{Id=3,Title="3",Description="3" },
            };
            mockedStorage.Setup(x => x.GetRewardsList()).Returns(rewards);
            var controller = new RewardController(mockedStorage.Object);
            ViewResult result = (ViewResult)controller.AddEdit(1);

            RewardViewModel resultModel = (RewardViewModel)result.Model;

            Assert.NotNull(result);
            Assert.NotNull(resultModel);
        }
    }
}
