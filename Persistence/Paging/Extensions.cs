using Application.Queries;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.EF;

namespace Persistence.Paging
{
    public static class PagingExtensions
    {
        private const string CODE = "CODE";
        private const string NAME = "NAME";

        public static IQueryable<CodeNameEntity> Page(this IQueryable<CodeNameEntity> queryable, BaseQuery query)
        {

            var filtersString = query.Filters;
            var ordersString = query.Filters;

            var pagedQueryable = queryable.AsNoTracking();
            var rowsQueryable = queryable.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filtersString))
            {
                // filtersString -> filters
                var filters = new List<Filter>
                {

                };

                var predicates = new List<Expression<Func<CodeNameEntity, bool>>>();

                foreach (var filter in filters)
                {
                    switch (filter.Operator)
                    {
                        case Operator.Equals:
                            //TODO here
                            break;
                        case Operator.NotEqual:
                            break;
                        case Operator.GreaterThan:
                            break;
                        case Operator.GreaterThanOrEqual:
                            break;
                        case Operator.LessThan:
                            break;
                        case Operator.LessThanOrEqual:
                            break;
                        case Operator.Like:
                            switch (filter.Property)
                            {
                                case CODE: predicates.Add(e => Functions.Like(e.Code, $"%{filter.Value}%")); break;
                                case NAME: predicates.Add(e => Functions.Like(e.Name, $"%{filter.Value}%")); break;
                                default: break;
                            }
                            break;
                        case Operator.Contains:
                            break;
                        case Operator.NotContains:
                            break;
                        default:
                            break;
                    }
                }

                pagedQueryable = predicates.Aggregate(pagedQueryable, (current, predicate) => current.Where(predicate));
                rowsQueryable = predicates.Aggregate(rowsQueryable, (current, predicate) => current.Where(predicate));
            }

            if (!string.IsNullOrWhiteSpace(ordersString))
            {
                var orders = new List<Order>();

                var orderCount = 0; 
                foreach (var order in orders)
                {
                    if (order.Property == CODE)
                    {
                        if (orderCount == 0)
                        {
                            pagedQueryable = order.IsAscending ? pagedQueryable.OrderBy(e => e.Code) : pagedQueryable.OrderByDescending(e => e.Code);
                        }
                        else {
                            pagedQueryable = order.IsAscending ? pagedQueryable.OrderByDynamic("e => e.Code") : pagedQueryable.OrderByDescendingDynamic("e => e.Code");
                        }
                    }
                    else if (order.Property == NAME) {
                        if (orderCount == 0)
                        {
                            pagedQueryable = order.IsAscending ? pagedQueryable.OrderBy(e => e.Code) : pagedQueryable.OrderByDescending(e => e.Code);
                        }
                        else
                        {
                            pagedQueryable = order.IsAscending ? pagedQueryable.OrderBy(e => e.Code) : pagedQueryable.OrderByDescending(e => e.Code);
                        }
                    }
                    orderCount++;
                }
            }

            return queryable;
        }
    }
}

