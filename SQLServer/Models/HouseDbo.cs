﻿using Abstractions.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SQLServer.Models
{
    public class HouseDbo : House
    {
        //  Properties
        //  ==========

        [Key]
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string Name { get => base.Name; set => base.Name = value; }

        public new string Pinboard { get => base.Pinboard; set => base.Pinboard = value; }

        public new ApplicationUserDbo Landlord { get => (ApplicationUserDbo)base.Landlord; set => base.Landlord = value; }
        public new IEnumerable<ApplicationUserDbo> Tenants { get => base.Tenants.Cast<ApplicationUserDbo>(); set => base.Tenants = value; }
        public new IEnumerable<IssueDbo> Issues { get => base.Issues.Cast<IssueDbo>(); set => base.Issues = value; }
        public new IEnumerable<ContactDbo> Contacts { get => base.Contacts.Cast<ContactDbo>(); set => base.Contacts = value; }
    }
}