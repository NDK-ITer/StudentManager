﻿using Application.Models.ModelsOfClassroom;
using Domain.Entities;

namespace Application.Models.ModelsOfUser
{
    public class UserModel
    {
        public string? Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public string? Avatar { get; set; }
        public DateTime Birthday { get; set; }
        public List<ClassroomModel> listClassroom { get; set; }

        public UserModel()
        {

        }
        public UserModel(User user)
        {
            listClassroom = new List<ClassroomModel>();
            this.Id = user.Id;
            this.Birthday = user.Birthday;
            this.Email = user.PresentEmail;
            this.Role = user.Role.Name;
            this.PhoneNumber = user.PhoneNumber;
            this.LastName = user.LastName;
            this.FirstName = user.FirstName;
            this.CreateDate = user.CreatedDate;
            this.Avatar = $"{user.LinkAvatar}/{user.Avatar}";
            foreach (var item in user.ListClassroom)
            {
                listClassroom.Add(new ClassroomModel(item, user.Id));
            }
        }
    }
}
