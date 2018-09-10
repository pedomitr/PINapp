using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Phonebook.Models
{
    public class NewContactContext : DbContext
    {
        public NewContactContext (DbContextOptions<NewContactContext> options)
            : base(options)
        {
        }

		public DbSet<Phonebook.Models.NewContact> NewContact { get; set; }
    }
}
