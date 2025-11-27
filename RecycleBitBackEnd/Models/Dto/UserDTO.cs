using System.ComponentModel.DataAnnotations;

namespace RailwayWeighingBackEnd.models.dto {

    /// <summary>
    /// Informations about the user
    /// </summary>
    public class UserDTO {

        /// <summary>
        /// User ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// User user
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string User { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// User status is active or not
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Indicates if the user is a weighing user
        /// </summary>
        public bool WeighingUser { get; set; }

        /// <summary>
        /// Indicates if the user is a supervisory user
        /// </summary>
        public bool SendToMRS { get; set; }

        /// <summary>
        /// Access roles for the user, used to determine what actions the user can perform
        /// </summary>
        public string AccessRoles { get; set; }

        //public static implicit operator UserDTO(DUACT_USER user) {
        //    return new UserDTO() {
        //        Id = user.DUAC_USER_ID,
        //        User = user.DUAC_USER,
        //        Name = user.DUAC_USER_NAME,
        //        Email = user.DUAC_USER_EMAIL,
        //        Type = user.DUAC_TYPE,
        //        Role = user.DUAC_ROLE,
        //        Active = user.DUAC_ACTIVE == YesNoEnum.Yes,
        //        WeighingUser = user.DUAC_WEIGHING_USER,
        //        AccessRoles = user.DUAC_ACCESS_ROLES
        //    };
        //}

        /// <summary>
        /// Converts a list of DUACT_USER entities to a list of UserDTO objects.
        /// </summary>
        /// <param name="userTableList"></param>
        /// <returns></returns>
        //public static List<UserDTO> parseToUserDtoList(List<DUACT_USER> userList) {
        //    List<UserDTO> userResponseList = new();
        //    userList.ForEach(user => userResponseList.Add(user));
        //    return userResponseList;
        //}
    }
}