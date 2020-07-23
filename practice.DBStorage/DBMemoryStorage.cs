using practice.Common;
using practice.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace practice.DBStorage
{
    public class DBMemoryStorage : IStorage
    {
        private readonly string _connectionString;
        public DBMemoryStorage(string connectionString) 
        {
            _connectionString = connectionString;
        }
        public int AddReward(RewardModel reward)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString)) 
            {
                using (SqlCommand command = connection.CreateCommand()) {
                    command.Parameters.AddWithValue("Title", reward.Title);
                    command.Parameters.AddWithValue("Description", reward.Description);
                    command.CommandText = "INSERT INTO Rewards(Title,Description) output INSERTED.ID VALUES (@Title,@Description)";
                    try
                    {
                        connection.Open();
                        var result = command.ExecuteScalar();
                        return (int)(decimal)result;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return -1;
                    }
                }
            }
        }

        public int AddUser(UserModel user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("FirstName", user.FirstName);
                    command.Parameters.AddWithValue("LastName", user.LastName);
                    command.Parameters.AddWithValue("Birthdate", user.Birthdate);
                    command.CommandText = "INSERT INTO Users(FirstName,LastName,Birthdate) output INSERTED.ID VALUES (@FirstName,@LastName,@Birthdate)";
                    try
                    {
                        connection.Open();
                        var result = command.ExecuteScalar();
                        return (int)(decimal)result;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return -1;
                    }
                }
            }
        }

        public List<RewardModel> GetRewardsByUserId(int id)
        {
            List<RewardModel> rewards = new List<RewardModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("Id", id);
                    command.CommandText = "SELECT Id,Title,Description FROM Rewards LEFT JOIN UsersAndRewards ON Rewards.Id = UsersAndRewards.RewardId WHERE UserId = @Id";
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RewardModel reward = new RewardModel
                            {
                                Id = reader.GetInt32(0),
                                Title = reader[1].ToString(),
                                Description = reader[2].ToString()
                            };
                            rewards.Add(reward);
                        }
                    }
                }
            }
            return rewards;
        }

        public List<RewardModel> GetRewardsList()
        {
            List<RewardModel> rewards = new List<RewardModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id,Title,Description from Rewards";
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RewardModel reward = new RewardModel
                            {
                                Id = reader.GetInt32(0),
                                Title = reader[1].ToString(),
                                Description = reader[2].ToString()
                            };
                            rewards.Add(reward);
                        }
                    }
                }
            }
            return rewards;
        }

        public List<UserModel> GetUsersList()
        {
            List<UserModel> users = new List<UserModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id,FirstName,LastName,Birthdate from Users";
                    connection.Open();
                    using (var reader = command.ExecuteReader()) 
                    {
                        while(reader.Read())
                        {
                            UserModel user = new UserModel
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader[1].ToString(),
                                LastName = reader[2].ToString(),
                                Birthdate = (DateTime)reader[3]
                            };
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }

        public bool RemoveReward(int userId, int rewardId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@rewardId", rewardId);
                    command.CommandText = "DELETE FROM UsersAndRewards WHERE UserId=@userId AND RewardId=@rewardId";
                    try
                    {
                        connection.Open();
                        int result = (int)command.ExecuteNonQuery();
                        if (result == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return false;
                    }
                }
            }
        }

        public bool RemoveRewardById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.CommandText = "DELETE FROM Rewards WHERE Id=@Id";
                    try
                    {
                        connection.Open();
                        int result = (int)command.ExecuteNonQuery();
                        if (result == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return false;
                    }
                }
            }
        }

        public bool RemoveUserById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.CommandText = "DELETE FROM Users WHERE Id=@Id";
                    try
                    {
                        connection.Open();
                        int result = (int)command.ExecuteNonQuery();
                        if (result == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return false;
                    }
                }
            }
        }

        public void RewardUser(int userId, int rewardId)
        {
            if (userId > 0 && rewardId > 0)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@rewardId", rewardId);
                        command.CommandText = "INSERT INTO UsersAndRewards(UserId,RewardId) VALUES (@userId,@rewardId)";
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
            else 
            {
                throw new ArgumentException();
            }
        }

        public bool UpdateReward(RewardModel reward)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@Id", reward.Id);
                    command.Parameters.AddWithValue("@Title", reward.Title);
                    command.Parameters.AddWithValue("@Description", reward.Description);
                    command.CommandText = "UPDATE Rewards SET Title=@Title,Description=@Description WHERE Id=@Id";
                    try
                    {
                        connection.Open();
                        int result = (int)command.ExecuteNonQuery();
                        if (result != 0)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return false;
                    }
                }
            }
        }

        public bool UpdateUser(UserModel user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Birthdate", user.Birthdate);
                    command.CommandText = "UPDATE Users SET FirstName=@FirstName,LastName=@LastName,Birthdate=@Birthdate WHERE Id=@Id";
                    try
                    {
                        connection.Open();
                        int result = (int)command.ExecuteNonQuery();
                        if (result != 0)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return false;
                    }
                }
            }
        }
    }
}
