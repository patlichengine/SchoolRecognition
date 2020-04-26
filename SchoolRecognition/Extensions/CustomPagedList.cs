using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Extensions
{
    public class CustomPagedList<T> where T : class
    {
        public Int64 LowerLimit { get; set; }
        public Int64 UpperLimit { get; set; }
        public Int64 TotalDBEntitysCount { get; set; }
        public virtual List<T> Entitys { get; set; }
        public List<int> PaginationIndices
        {
            get
            {
                //Divide rows into groups of 100
                decimal _upperLimit = 0;
                int _upperLimitCeilingValue = 0;
                //
                List<int> _paginationIndices = new List<int>();

                _upperLimit = Decimal.Divide(this.TotalDBEntitysCount, 100);

                _upperLimitCeilingValue = Convert.ToInt32(Math.Ceiling(_upperLimit));

                if (_upperLimitCeilingValue > 0)
                {
                    for (int i = 1; i <= _upperLimitCeilingValue; i++)
                    {
                        _paginationIndices.Add(i);
                    }
                }

                return _paginationIndices;

            }
        }

    }
}
