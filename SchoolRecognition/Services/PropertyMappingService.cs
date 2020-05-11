using SchoolRecognition.Entities;
using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {


        private Dictionary<string, PropertyMappingValue> _pinHistoriesPropertyMapping =
          new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
          {
               //{ "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
               { "DateActive", new PropertyMappingValue(new List<string>() { "DateActive" } )},
               { "DateActiveDesc", new PropertyMappingValue(new List<string>() { "DateActive" } , true)},
               //{ "Name", new PropertyMappingValue(new List<string>() { "FirstName", "LastName" }) }
          };

        private Dictionary<string, PropertyMappingValue> _pinsPropertyMapping =
          new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
          {
               //{ "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
               { "SerialNumber", new PropertyMappingValue(new List<string>() { "SerialPin" } )},
               { "SerialNumberDesc", new PropertyMappingValue(new List<string>() { "SerialPin" } , true)},
               { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated" } ) },
               { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated" }, true ) },
               { "RecognitionTypeName", new PropertyMappingValue(new List<string>() { "RecognitionType" } ) },
               //{ "Age", new PropertyMappingValue(new List<string>() { "DateOfBirth" } , true) },
               //{ "Name", new PropertyMappingValue(new List<string>() { "FirstName", "LastName" }) }
          };

        private Dictionary<string, PropertyMappingValue> _recognitionTypesPropertyMapping =
          new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
          {
               //{ "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
               { "RecognitionTypeName", new PropertyMappingValue(new List<string>() { "Name" } )},
               { "RecognitionTypeNameDesc", new PropertyMappingValue(new List<string>() { "Name" }, true )},
               { "RecognitionTypeCode", new PropertyMappingValue(new List<string>() { "Code" } ) },
               //{ "Age", new PropertyMappingValue(new List<string>() { "DateOfBirth" } , true) },
               //{ "Name", new PropertyMappingValue(new List<string>() { "FirstName", "LastName" }) }
          };

        private Dictionary<string, PropertyMappingValue> _schoolPaymentsPropertyMapping =
          new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
          {
               //{ "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
               { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated" } )},
               { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated" } , true)},
               { "AmountPaid", new PropertyMappingValue(new List<string>() { "AmountPaid" } )},
               { "AmountPaidDesc", new PropertyMappingValue(new List<string>() { "AmountPaid" } , true)},
               //{ "Name", new PropertyMappingValue(new List<string>() { "FirstName", "LastName" }) }
          };

        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<PinHistoriesViewDto, PinHistories>(_pinHistoriesPropertyMapping));
            _propertyMappings.Add(new PropertyMapping<PinsViewDto, Pins>(_pinsPropertyMapping));
            _propertyMappings.Add(new PropertyMapping<RecognitionTypesViewDto, RecognitionTypes>(_recognitionTypesPropertyMapping));
            _propertyMappings.Add(new PropertyMapping<SchoolPaymentsViewDto, SchoolPayments>(_schoolPaymentsPropertyMapping));
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            // the string is separated by ",", so we split it.
            var fieldsAfterSplit = fields.Split(',');

            // run through the fields clauses
            foreach (var field in fieldsAfterSplit)
            {
                // trim
                var trimmedField = field.Trim();

                // remove everything after the first " " - if the fields 
                // are coming from an orderBy string, this part must be 
                // ignored
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                // find the matching property
                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;
        }


        public Dictionary<string, PropertyMappingValue> GetPropertyMapping
           <TSource, TDestination>()
        {
            // get matching mapping
            var matchingMapping = _propertyMappings
                .OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()._mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance " +
                $"for <{typeof(TSource)},{typeof(TDestination)}");
        }
    }
}
