using Microsoft.EntityFrameworkCore;
using System;
using UserCQRS.Infra.Data.Context;

namespace UserCQRS.Tests.Factory
{
    public class DatabaseFactory
    {        
        //Creates an in memory EF database with a new guid so each request is returned a new database
        public static UserCQRSContext Create()
        {            
            var options = new DbContextOptionsBuilder<UserCQRSContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            return new UserCQRSContext(options);
        }
    }
}
