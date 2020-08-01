using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Customer.Microservice.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Models.Customer> Customers { get; set; }

        Task<int> SaveChanges();
    }
}