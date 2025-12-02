using System.ComponentModel.DataAnnotations;

namespace RecycleBitBackEnd.Models.Request {
    public class GetAllUserReques {

        [Required]
        public int UserIdApplicant { get; set; }

        [Required]
        public string Role { get; set; }
    }
}