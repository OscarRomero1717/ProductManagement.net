using Microsoft.EntityFrameworkCore;
using ProductManagement.Common.Collection;

namespace ProductManagement.Common.Paging
{
    public static class PaginExtension
    {
        public static async Task<DataCollection<T>> GetPageAsync<T>(this IQueryable<T>query,int page, int take)
        {
            try
            {
                var origianlPages = page;
                page--;
                if (page > 0)
                {
                    page = page * take;
                }

                var result = new DataCollection<T>
                {
                    Items =  query.Skip(page).Take(take).ToList(),
                    Total =  query.Count(),
                    Pages = origianlPages
                };


                if (result.Total > 0)
                {
                    result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));

                }
                return result;
            }
            catch (Exception ex )
            {

                throw;
            }
            


        }

    }
}