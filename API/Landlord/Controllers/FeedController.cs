﻿using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Landlord.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Landlord)]
    public class FeedController : APIControllerBase
    {
        //  Variables
        //  =========

        private readonly IIssueRepository issueRepository;

        //  Constructors
        //  ============

        public FeedController(IIssueRepository issueRepository)
        {
            this.issueRepository = issueRepository;
        }

        //  Methods
        //  =======

        [HttpPost(Endpoints.GetFeed)]
        public async Task<ObjectResult> GetFeed()
        {
            IEnumerable<Issue> searchResults = await issueRepository.GetAllIssues(HttpContext.User.Identity.Name!).ConfigureAwait(false);

            IEnumerable<Response.Issue> result = searchResults.Select(s => new Response.Issue
            {
                Id = s.Id,
                CreatedAt = s.CreatedAt,
                IsResolved = s.IsResolved,
                Title = s.Title,
                Content = s.Content,
                Priority = s.Priority,
                House = new Response.House
                {
                    Id = s.House.Id,
                    Name = s.House.Name
                },
                Author = new Response.ApplicationUser
                {
                    Id = s.Author.Id,
                    Name = s.Author.Name,
                    Email = s.Author.Email,
                    PhoneNumber = s.Author.PhoneNumber
                }
            });

            return Ok(result);
        }
    }
}