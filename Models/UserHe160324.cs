using System;
using System.Collections.Generic;

namespace PRN_Project2.Models
{
    public partial class UserHe160324
    {
        public UserHe160324()
        {
            OrderHe160324s = new HashSet<OrderHe160324>();
        }

        public UserHe160324(int userId, string userName, string password, bool? gender, string? address, string ingameId, string? ingameName, string? phone, string email, string? facebook, int? roll, bool? status, ICollection<OrderHe160324> orderHe160324s)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            Gender = gender;
            Address = address;
            IngameId = ingameId;
            IngameName = ingameName;
            Phone = phone;
            Email = email;
            Facebook = facebook;
            Roll = roll;
            Status = status;
            OrderHe160324s = orderHe160324s;
        }

        public UserHe160324(int userId, string userName, string password, bool? gender, string? address, string ingameId, string? ingameName, string? phone, string email, string? facebook, int? roll, bool? status)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            Gender = gender;
            Address = address;
            IngameId = ingameId;
            IngameName = ingameName;
            Phone = phone;
            Email = email;
            Facebook = facebook;
            Roll = roll;
            Status = status;
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool? Gender { get; set; }
        public string? Address { get; set; }
        public string IngameId { get; set; } = null!;
        public string? IngameName { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; } = null!;
        public string? Facebook { get; set; }
        public int? Roll { get; set; }
        public bool? Status { get; set; }


        public virtual ICollection<OrderHe160324> OrderHe160324s { get; set; }
    }
}
