using RecycleBitBackEnd.Util.Filters;

namespace RecycleBitBackEnd.Models.Request {
    public class LoginRequest {
        [ValidateEmail]
        public string Email { get; set; }

        [ValidatePassword]
        public string Password { get; set; }
    }
}