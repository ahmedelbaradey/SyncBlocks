using Entities.DataModels;
using Repository.Utility;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions
{
    public static class RepositoryJournalExtensions
    {
         
        public static IQueryable<Journal> Sort(this IQueryable<Journal> journals, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return journals.OrderBy(e => e.Timestamp);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Journal>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return journals.OrderBy(e => e.Timestamp);
            return journals.OrderBy(orderQuery);
        }
    }
}
