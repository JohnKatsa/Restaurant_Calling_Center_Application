using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProject.Interfaces.IDataRetrieval
{
    public interface IDataRetrievalByRegex<T>
    {
        Task<IEnumerable<T>> GetByNameAsync(string regex);
    }
}
