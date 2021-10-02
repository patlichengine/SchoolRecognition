using SchoolRecognition.ServiceLayer.HelperServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace SchoolRecognition.ServiceLayer.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy, 
            Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if(source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if(mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            if(string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            var orderByString = string.Empty;

            //the orderBy string is separated by ",", so we need to split it
            var orderByAfterSplit = orderBy.Split(',');

            //apply each oderby clause in reverse order - otherwise the 
            //IQueryable will be ordered in the wrong order
            foreach (var orderByClause in orderByAfterSplit)
            {
                //trin the order by clause, as it might contain leading spaces 
                // or trailing spaces. Cant trim the var in foreach,
                //do use another var
                var trimmedOrderByClause = orderByClause.Trim();

                //if the sort option ends with desc then order
                var orderDescending = trimmedOrderByClause.EndsWith(" desc");

                //remove " asc" or " desc" from the orderBy clause
                //get the property name to look for in the mapping dictionary
                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedOrderByClause : trimmedOrderByClause.Remove(indexOfFirstSpace);

                //check if the mapping dictionary contains the key in the mapping name
                if(!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentNullException($"Key mapping for {propertyName} is missing");
                }

                //get the PropertyMappingValue
                var propertyMappingValue = mappingDictionary[propertyName];

                if(propertyMappingValue == null)
                {
                    throw new ArgumentNullException(nameof(propertyMappingValue)); 
                }

                //Run through the property names
                //so the orderby clauses are supplied in the correct order
                foreach (var destinationProperty in propertyMappingValue.DestinationProperties)
                {
                    //revert sort order if necessary
                    if(propertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }

                    orderByString = orderByString +
                        (string.IsNullOrWhiteSpace(orderByString) ? string.Empty : ", ")
                        + destinationProperty
                        + (orderDescending ? " descending" : " ascending");

                }
            }

            return source.OrderBy(orderByString);

        }

        public class FilterParameter
        {
            public string FilterBy { get; set; }
            public object FilterValue { get; set; }
            public OperatorComparer OperatorComparer { get; set; }
        }

        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> source, List<FilterParameter> filterParameters)
        {
            string _propertyName = null;
            object searchConstant;
            Type type;

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }


            foreach (var filterParam in filterParameters)
            {
                if (string.IsNullOrWhiteSpace(filterParam.FilterBy))
                {
                    continue;
                }

                if (filterParam.FilterValue == null || string.IsNullOrWhiteSpace(filterParam.FilterValue.ToString()))
                {
                    continue;
                }

                //check if the object contains the property
                foreach (var objectProperty in typeof(T).GetProperties())
                {
                    if (objectProperty.Name == filterParam.FilterBy)
                    {
                        _propertyName = objectProperty.Name;
                    }
                    else
                    {
                        continue;
                    }

                }

                if (String.IsNullOrWhiteSpace(_propertyName))
                {
                    continue;
                }

                //covert the filterParam.FilterValue to the same type as the property
                type = typeof(T).GetProperty(_propertyName).PropertyType;
                if (type == typeof(Guid?))
                {
                    searchConstant = new Guid(filterParam.FilterValue.ToString());
                }
                else if (type == typeof(Guid))
                {
                    searchConstant = new Guid(filterParam.FilterValue.ToString());
                }
                else
                {
                    searchConstant = Convert.ChangeType(filterParam.FilterValue.ToString(), type);
                }



                var predicate = ExpressionBuilder.BuildPredicate<T>(searchConstant, filterParam.OperatorComparer, _propertyName);
                source = source.Where(predicate).Cast<T>();
            }

            return source;
        }

        public static IQueryable<T> ResolveSearchParams<T>(this IQueryable<T> source, string searchQuery, List<string> searchFields)
        {

            //Search
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {

                searchQuery = searchQuery.Trim().ToUpper();

                //dbResult = dbResult.Where(
                //    a => (a.CountryName != null ? a.CountryName : null).ToUpper().Contains(searchQuery)
                //);
                //Check if search field is not null
                for (int i = 0; i < searchFields.Count(); i++)
                {
                    var searchField = searchFields[i];

                    if (string.IsNullOrWhiteSpace(searchField))
                    {
                        continue;
                    }


                    string _propertyName = null;
                    //check if the object contains the property
                    for (int j = 0; j < typeof(T).GetProperties().Length; j++)
                    {
                        var objectProperty = typeof(T).GetProperties()[j];

                        if (objectProperty.Name == searchField)
                        {
                            _propertyName = objectProperty.Name;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (String.IsNullOrWhiteSpace(_propertyName))
                    {
                        continue;
                    }

                    //covert the filterParam.FilterValue to the same type as the property

                    var predicate = ExpressionBuilder.BuildPredicate<T>(_propertyName, OperatorComparer.Contains, searchQuery);
                    source = source.Where(predicate).Cast<T>();

                }
            }

            return source;
        }



    }



}
