﻿using Abstractions.Models.Results;
using System.Collections.Generic;

namespace SQLServer.Models.Results
{
    public class RegisterTenantResult : IRegisterTenantResult
    {
        //  Properties
        //  ==========

        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}