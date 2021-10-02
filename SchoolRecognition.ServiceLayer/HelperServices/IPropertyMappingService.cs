using System.Collections.Generic;

namespace SchoolRecognition.ServiceLayer.HelperServices
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        bool ValidateMappingExistsFor<TSource, TDestination>(string fields);
    }
}