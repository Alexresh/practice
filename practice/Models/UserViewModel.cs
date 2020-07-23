using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace practice.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public List<RewardViewModel> Rewards { get; set; }

        public UserViewModel() 
        {
            Rewards = new List<RewardViewModel>();
        }
    }
}
