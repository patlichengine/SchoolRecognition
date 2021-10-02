using System;
using System.Collections.Generic;
using System.Text;
using static SchoolRecognition.ServiceLayer.Extensions.IQueryableExtensions;

namespace SchoolRecognition.ServiceLayer.ResourceParameters
{
    public class BaseResourceParams<T> where T : class
    {
        const int maxPageSize = 200;
        //public string Code { get; set; }
        public Guid? Id { get; set; }
        public object PrimaryKey { get; set; }
        public string SearchQuery { get; set; }
        public List<FilterParameter> FilterParameters { get; set; } = new List<FilterParameter>();
        public List<string> SearchFields { get; set; } = new List<string>();
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public string OrderBy { get; set; } = "Id";
        public string Fields { get; set; }
        public T ParameterObject { get; set; }
        public BaseResourceParams()
        {

        }

        public BaseResourceParams(string orderParam)
        {
            this.OrderBy = orderParam;
        }

        public static BaseResourceParams<T> Create(T entity, string orderParam)
        {
            return new BaseResourceParams<T>(orderParam);
        }
    }
}
