using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Interfaces.IDataRetrieval
{
    public interface IDataRetrievalById<T>
    {
        Task<T> GetByIdAsync(int id);
    }
}
