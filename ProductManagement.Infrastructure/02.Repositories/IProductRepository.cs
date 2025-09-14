using ProductManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Producto>> GetAllAsync();

        Task<Producto> GetByNombreAsync(string nombre);

        Task<Producto> GetByIdAsync(int id);       

        Task<bool> AddAsync(Producto product);
        
        Task<bool> UpdateAsync(Producto product, bool dateDifferent);

        Task<bool> DeleteAsync(int id);
       
    }
}
