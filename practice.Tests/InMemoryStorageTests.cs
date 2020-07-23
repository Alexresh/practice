using Moq;
using practice.Common.Models;
using practice.services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace practice.Tests
{
    public class InMemoryStorageTests
    {
        [Fact]
        public void ShouldBeAbleToReturnIdWhenAddingReward()
        {
            RewardModel testReward = new RewardModel
            {
                Title = "1",
                Description="1"
            };
            InMemoryStorage storage = new InMemoryStorage();
            int expectedId = 6;

            int actualId = storage.AddReward(testReward);

            Assert.Equal(expectedId, actualId);
        }
        [Fact]
        public void ShouldBeAbleToThrowExeptionWhenRewardIsNull()
        {
            InMemoryStorage storage = new InMemoryStorage();

            Assert.Throws<ArgumentNullException>(() => storage.AddReward(null));
        }
        [Fact]
        public void ShouldBeAbleToReturnIdWhenAddingUser()
        {
            var mockedStorage = new Mock<UserModel>();
            InMemoryStorage storage = new InMemoryStorage();
            int expectedId = 6;

            int actualId = storage.AddUser(mockedStorage.Object);

            Assert.Equal(expectedId, actualId);
        }
        [Fact]
        public void ShouldBeAbleToThrowExeptionWhenUserIsNull()
        {
            InMemoryStorage storage = new InMemoryStorage();

            Assert.Throws<ArgumentNullException>(() => storage.AddUser(null));
        }
        [Fact]
        public void ShouldBeAbleToReturnNotNullRewardListWhenUserIdIsNegative()
        {
            InMemoryStorage storage = new InMemoryStorage();

            var rewards = storage.GetRewardsByUserId(-1);

            Assert.NotNull(rewards);
        }
        [Fact]
        public void ShouldBeAbleToReturnTrueWhenRemovingRewardById()
        {
            InMemoryStorage storage = new InMemoryStorage();

            var result = storage.RemoveRewardById(1);

            Assert.True(result);
        }
        [Fact]
        public void ShouldBeAbleToReturnFalseWhenRemovingRewardById()
        {
            InMemoryStorage storage = new InMemoryStorage();

            var result = storage.RemoveRewardById(-1);

            Assert.False(result);
        }
        [Fact]
        public void ShouldBeAbleToReturnTrueWhenRemovingUserById()
        {
            InMemoryStorage storage = new InMemoryStorage();

            var result = storage.RemoveUserById(1);

            Assert.True(result);
        }
        [Fact]
        public void ShouldBeAbleToReturnFalseWhenRemovingUserById()
        {
            InMemoryStorage storage = new InMemoryStorage();

            var result = storage.RemoveUserById(-1);

            Assert.False(result);
        }
        [Fact]
        public void ShouldBeAbleToReturnFalseWhenTakeAwayTheAward()
        {
            InMemoryStorage storage = new InMemoryStorage();

            var result = storage.RemoveReward(-1,-1);

            Assert.False(result);
        }
        [Fact]
        public void ShouldBeAbleToReturnTrueWhenTakeAwayTheAward()
        {
            InMemoryStorage storage = new InMemoryStorage();

            var result = storage.RemoveReward(1, 1);

            Assert.True(result);
        }
        [Fact]
        public void ShouldBeAbleToThrowArgumentExeptionWhenSomeIdIsLessThan1()
        {
            InMemoryStorage storage = new InMemoryStorage();

            Assert.Throws<ArgumentException>(() => storage.RewardUser(0,1));
        }
        [Fact]
        public void ShouldBeAbleToUpdateRewardsTitle()
        {
            InMemoryStorage storage = new InMemoryStorage();
            int testedRewardId=1;
            string testedTitle = "test";
            RewardModel testReward = new RewardModel
            {
                Id = testedRewardId,
                Title = testedTitle,
                Description = ""
            };

            bool result = storage.UpdateReward(testReward);
            var changedReward = storage.GetRewardsList().FirstOrDefault(x => x.Id == testedRewardId);

            Assert.True(result);
            Assert.NotNull(changedReward);
            Assert.Equal(changedReward.Title, testedTitle);

        }
        [Fact]
        public void ShouldBeAbleToThrowArgumentNullExeptionWhenUpdatingReward()
        {
            InMemoryStorage storage = new InMemoryStorage();

            Assert.Throws<ArgumentNullException>(() => storage.UpdateReward(null));
        }
        [Fact]
        public void ShouldBeAbleToThrowArgumentExeptionWhenUpdatingReward()
        {
            InMemoryStorage storage = new InMemoryStorage();
            RewardModel illegalReward = new RewardModel
            {
                Id = -1,
                Title = "1",
                Description = "1"
            };

            Assert.Throws<ArgumentException>(() => storage.UpdateReward(illegalReward));
        }
        [Fact]
        public void ShouldBeAbleToUpdateUsersFirstName()
        {
            InMemoryStorage storage = new InMemoryStorage();
            int testedUserdId = 5;
            string testedFirstName = "test";
            UserModel testUser = new UserModel
            {
                Id = testedUserdId,
                FirstName = testedFirstName,
                LastName = "",
                Birthdate = DateTime.Now
            };

            var result = storage.UpdateUser(testUser);
            var changedUser= storage.GetUsersList().FirstOrDefault(x => x.Id == testedUserdId);

            Assert.True(result);
            Assert.NotNull(changedUser);
            Assert.Equal(changedUser.FirstName, testedFirstName);
        }
        [Fact]
        public void ShouldBeAbleToThrowArgumentNullExeptionWhenUpdatingUser()
        {
            InMemoryStorage storage = new InMemoryStorage();

            Assert.Throws<ArgumentNullException>(() => storage.UpdateUser(null));
        }
        [Fact]
        public void ShouldBeAbleToThrowArgumentExeptionWhenUpdatingUser()
        {
            InMemoryStorage storage = new InMemoryStorage();
            UserModel illegalUser = new UserModel
            {
                Id = -1,
                FirstName = "1",
                LastName = "1",
                Birthdate = DateTime.Now
            };

            Assert.Throws<ArgumentException>(() => storage.UpdateUser(illegalUser));
        }

    }
}
