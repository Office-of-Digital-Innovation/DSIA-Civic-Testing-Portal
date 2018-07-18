using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace app.Data
{
    public class FusionDbContext : IdentityDbContext
    {
        public FusionDbContext(DbContextOptions<FusionDbContext> options)
            : base(options)
        {
        }
    }
}
