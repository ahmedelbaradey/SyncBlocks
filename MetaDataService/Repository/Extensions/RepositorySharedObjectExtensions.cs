using Entities.DataModels;
using Repository.Utility;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions
{
    public static class RepositorySharedObjectExtensions
    {
         
        public static IQueryable<SharedObject> Sort(this IQueryable<SharedObject> sharedObjects, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return sharedObjects.OrderBy(e => e.Id/*.Journals.OrderBy(c=>c.Timestamp)*/);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Journal>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return sharedObjects.OrderBy(e => e.Id);
            return sharedObjects.OrderBy(orderQuery);
        }
    }
}
