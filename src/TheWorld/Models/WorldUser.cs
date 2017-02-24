using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TheWorld.Models.Context
{
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}