using FinalProject.Domain;
using FinalProject.Interfaces.IDataRetrieval;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Services.DataRetrieval
{
    public class AddressRetrieval : IDataRetrievalById<Address>
    {
        public async Task<Address> GetByIdAsync(int Id)
        {
            using(var context = new AdventureWorks2017Context())
            {
                return await context.Person.Where(p => p.BusinessEntityId == Id)
                    .Join(context.BusinessEntityAddress, p => p.BusinessEntityId, b => b.BusinessEntityId, (p, b) => b)
                    .Join(context.Address, b => b.AddressId, a => a.AddressId, (b, a) => a)
                    .FirstOrDefaultAsync();
            }
        }
    }
}
