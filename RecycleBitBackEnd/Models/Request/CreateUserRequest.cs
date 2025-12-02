using RecycleBitBackEnd.Config;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Util.Filters;
using System;
using System.ComponentModel.DataAnnotations;

namespace RecycleBitBackEnd.Models.Request {

    public class CreateUserRequest {

        [Required(AllowEmptyStrings = false, ErrorMessage = DictionaryError.IS_VALUE_NOT_NULL)]
        public string Name { get; set; }

        [Required]
        [ValidatePassword]
        public string Password { get; set; }

        [Required]
        [ValidateEmail]
        public string Email { get; set; }

        [ValidateStatus]
        public bool Status { get; set; }

        [Required]
        public int RoleId { get; set; }

        public string Phone { get; set; }

        [Required]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2500")]
        public DateTime DateNasc { get; set; }

        [Required]
        [ValidateCPF]
        public string CPF { get; set; }

        public AddressDto Address { get; set; }
    }
}