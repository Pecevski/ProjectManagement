using PMApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Services.Contracts
{
    public interface IServices<T> where T : IEntity
    {
        public Task<T> GetById(string id);
        public Task<bool> Create(T entity);
        public Task<bool> Update(string id, T entity);
        public Task<bool> Delete(string id);
    }
}
