﻿namespace ProductManagementAPI.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string TwoFactorSecret { get; set; }
    }

}
