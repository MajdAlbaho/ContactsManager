using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace ContactsManager.Api.Helper
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string sort) {
            if (source == null) {
                throw new ArgumentNullException("source");
            }

            if (sort == null) {
                return source;
            }

            // split the sort string
            var lstSort = sort.Split(',');

            string completeSortExpression = "";
            foreach (var sortOption in lstSort) {
                // if the sort option starts with "-" descending, otherwise ascending
                if (sortOption.StartsWith("-")) {
                    completeSortExpression = completeSortExpression + sortOption.Remove(0, 1) + " descending,";
                }
                else {
                    completeSortExpression = completeSortExpression + sortOption + ",";
                }

            }

            if (!string.IsNullOrWhiteSpace(completeSortExpression)) {
                // order by string value from Dynamic Linq, Remove() is used for remove the last comma
                source = source.OrderBy(completeSortExpression.Remove(completeSortExpression.Count() - 1));
            }

            return source;
        }
    }
}


