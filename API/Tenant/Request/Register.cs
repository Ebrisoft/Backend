﻿namespace API.Tenant.Request
{
    public class Register
    {
        //  Properties
        //  ==========

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}