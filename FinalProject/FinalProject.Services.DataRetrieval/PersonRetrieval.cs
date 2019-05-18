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
    public class PersonRetrieval : IDataRetrievalByRegex<Person>
    {
        // Returns Person where their names contain the given string 
        public async Task<IEnumerable<Person>> GetByNameAsync(string regex)
        {
            using (var context = new AdventureWorks2017Context())
            {
                try
                {
                    return await context.Person.Where(p => (p.FirstName + p.MiddleName + p.LastName)
                                         .Contains(regex)).ToListAsync();
                }
                catch
                {
                    return null;
                }
            }
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            using (var context = new AdventureWorks2017Context())
            {
                try
                {
                    return await context.Person.Where(p => p.BusinessEntityId == id).FirstOrDefaultAsync();
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
