using System;
using Hopper.Data;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Tests
{
    public abstract class ContextOptionsTestBase
    {
        protected readonly DbContextOptions<ApplicationDbContext> ContextOptions =
            new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        protected readonly OptionsWrapper<OperationalStoreOptions> OperationalStoreOptions =
            new OptionsWrapper<OperationalStoreOptions>(new OperationalStoreOptions());
    }
}