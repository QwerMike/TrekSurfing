using System.ComponentModel.DataAnnotations;

namespace TrekSurfing.Web.Controllers
{
    public class ReviewModel
    {
        [Required(ErrorMessage = "Score is required")]
        [Range(1, 5)]
        public double Score { get; set; }
        public string Message { get; set; }

        [Required(ErrorMessage = "Target is required")]
        public int TargetId { get; set; }
    }
}