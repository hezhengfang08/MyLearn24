using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.Infrastructure.Data.Contexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

    }
}
