﻿using System;

namespace API.Tenant.Response
{
    public class Issue
    {
        //  Properties
        //  ==========

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsResolved { get; set; }

        public ApplicationUser Author { get; set; } = null!;
    }
}