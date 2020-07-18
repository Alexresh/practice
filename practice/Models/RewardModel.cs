using System.ComponentModel.DataAnnotations;

namespace practice.Models
{
    public class RewardModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
