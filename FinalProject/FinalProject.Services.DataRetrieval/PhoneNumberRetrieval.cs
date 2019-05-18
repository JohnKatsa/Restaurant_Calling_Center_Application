using FinalProject.Domain;
using FinalProject.Interfaces.IDataRetrieval;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Services.DataRetrieval
{
    public class PhoneNumberRetrieval : IDataRetrievalById<PersonPhone>
    {
        // Returns Employees where their names contain the given string 
        public async Task<PersonPhone> GetByIdAsync(int id)
        {
            // make a disposable connection
            using (var context = new AdventureWorks2017Context())
            {
                try
                {
                    return await context.PersonPhone.FirstOrDefaultAsync(e => e.BusinessEntityId == id);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
