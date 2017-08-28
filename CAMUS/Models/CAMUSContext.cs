using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CAMUS.Models
{
    public class CAMUSContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CAMUSContext() : base("name=CAMUSContext")
        {
            //Database.SetInitializer<CAMUSContext>(null);
        }

        public System.Data.Entity.DbSet<CAMUS.Models.News> News { get; set; }

        public System.Data.Entity.DbSet<CAMUS.Models.Banner> Banner { get; set; }

        public System.Data.Entity.DbSet<CAMUS.Models.User> User { get; set; }
    }
}
