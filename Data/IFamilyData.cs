using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace WebApplication.Data
{
    public interface IFamilyData
    {
        Task<IList<Adult>> GetAdultsAsync();
        Task<Adult> AddAdultAsync(Adult adult);
        Task RemoveAdultAsync(int Id);
        Task<Adult> UpdateAsync(Adult adult);
        //Person Get(int Id);
    }
}