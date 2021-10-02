namespace SchoolRecognition.ServiceLayer.HelperServices
{
    public interface IPropertyCheckerService
    {
        bool TypeHasProperties<T>(string fields);
    }
}