using FinalProject.Domain;
using FinalProject.Interfaces.IDataRetrieval;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Services.DataRetrieval
{
    public class EmployeeRetrieval : IDataRetrievalById<Employee>
    {
        // Returns Employees where their names contain the given string 
        public async Task<Employee> GetByIdAsync(int id)
        {
            using (var context = new AdventureWorks2017Context())
            {
                try
                {
                    return await context.Employee.FirstOrDefaultAsync(e => e.BusinessEntityId == id);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
