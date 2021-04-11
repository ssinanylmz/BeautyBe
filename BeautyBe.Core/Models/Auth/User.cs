using System;
using System.Collections.Generic;
using System.Text;

namespace BeautyBe.Core.Models.Auth
{
   public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordCrypt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Avatar { get; set; }
        public DateTime BirthDay { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public int UserTypeId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
