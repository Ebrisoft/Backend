﻿using System;
using System.Collections.Generic;

namespace Abstractions.Models
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
        public int Priority { get; set; }
        public House House { get; set; } = null!;
        public ApplicationUser Author { get; set; } = null!;
        public IEnumerable<Comment> Comments { get; set; } = null!;
    }
}