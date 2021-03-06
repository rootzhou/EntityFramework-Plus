﻿// Description: Entity Framework Bulk Operations & Utilities (EF Bulk SaveChanges, Insert, Update, Delete, Merge | LINQ Query Cache, Deferred, Filter, IncludeFilter, IncludeOptimize | Audit)
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum & Issues: https://github.com/zzzprojects/EntityFramework-Plus/issues
// License: https://github.com/zzzprojects/EntityFramework-Plus/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

using System;
using System.Linq;
using System.Linq.Expressions;

namespace Z.EntityFramework.Plus
{
    public partial class AuditConfiguration
    {
        /// <summary>Excludes from the audit all properties.</summary>
        /// <returns>An AuditConfiguration.</returns>
        public AuditConfiguration ExcludeProperty()
        {
            ExcludeIncludePropertyPredicates.Add((x, s) => false);
            return this;
        }

        /// <summary>
        ///     Excludes from the audit all properties from entities of 'T' type or entities which the type
        ///     derive from 'T'.
        /// </summary>
        /// <typeparam name="T">Generic type to exclude all properties.</typeparam>
        /// <returns>An AuditConfiguration.</returns>
        public AuditConfiguration ExcludeProperty<T>()
        {
            ExcludeIncludePropertyPredicates.Add((x, s) => x is T ? (bool?) false : null);
            return this;
        }

        /// <summary>
        ///     Excludes from the audit selected properties from entities of 'T' type or entities which the
        ///     type derive from 'T'.
        /// </summary>
        /// <typeparam name="T">Generic type to exclude selected properties.</typeparam>
        /// <param name="propertySelector">The property selector.</param>
        /// <returns>An AuditConfiguration.</returns>
        public AuditConfiguration ExcludeProperty<T>(Expression<Func<T, object>> propertySelector)
        {
            var propertyNames = propertySelector.GetPropertyOrFieldAccessors().Select(x => x.ToString()).ToList();

            foreach (var accessor in propertyNames)
            {
                ExcludeIncludePropertyPredicates.Add((x, s) => x is T && s == accessor ? (bool?) false : null);
            }

            return this;
        }
    }
}