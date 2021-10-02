using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.ServiceLayer.HelperServices
{
    public class PropertyMappingService : IPropertyMappingService
    {

//        #region ApplicationUser

//        private Dictionary<string, PropertyMappingValue> _applicationUserPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "FullNames", new PropertyMappingValue(new List<string>() { "FullNames"}) },
//                { "FullNamesDesc", new PropertyMappingValue(new List<string>() { "FullNames"}, true )},

//                { "Lpno", new PropertyMappingValue(new List<string>() { "Lpno"}) },
//                { "LpnoDesc", new PropertyMappingValue(new List<string>() { "Lpno"}, true )},

//                { "Gender", new PropertyMappingValue(new List<string>() { "Gender"}) },
//                { "GenderDesc", new PropertyMappingValue(new List<string>() { "Gender"}, true )},

//                { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )}
//            };

//        #endregion

//        #region AuditTrail

//        private Dictionary<string, PropertyMappingValue> _auditTrailPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "Action", new PropertyMappingValue(new List<string>() { "Action"}) },
//                { "ActionDesc", new PropertyMappingValue(new List<string>() { "Action"}, true )},

//                { "Description", new PropertyMappingValue(new List<string>() { "Description"}) },
//                { "DescriptionDesc", new PropertyMappingValue(new List<string>() { "Description"}, true )},

//                { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )}
//            };

//        #endregion

//        #region BroadSheetEvidence

//        private Dictionary<string, PropertyMappingValue> _broadSheetEvidencePropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "FileName", new PropertyMappingValue(new List<string>() { "FileName"}) },
//                { "FileNameDesc", new PropertyMappingValue(new List<string>() { "FileName"}, true )},

//                { "CentreNo", new PropertyMappingValue(new List<string>() { "CentreNo"}) },
//                { "CentreNoDesc", new PropertyMappingValue(new List<string>() { "CentreNo"}, true )},
                
//                { "MalpracticeNo", new PropertyMappingValue(new List<string>() { "MalpracticeNo"}) },
//                { "MalpracticeNoDesc", new PropertyMappingValue(new List<string>() { "MalpracticeNo"}, true )},

//                { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )}
//            };

//        #endregion

//        #region BroadSheetItem

//        private Dictionary<string, PropertyMappingValue> _broadSheetItemPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "FileName", new PropertyMappingValue(new List<string>() { "FileName"}) },
//                { "FileNameDesc", new PropertyMappingValue(new List<string>() { "FileName"}, true )},

//                { "CentreNo", new PropertyMappingValue(new List<string>() { "CentreNo"}) },
//                { "CentreNoDesc", new PropertyMappingValue(new List<string>() { "CentreNo"}, true )},
                
//                { "PaperNo", new PropertyMappingValue(new List<string>() { "PaperNo"}) },
//                { "PaperNoDesc", new PropertyMappingValue(new List<string>() { "PaperNo"}, true )},
                
//                { "MalpracticeNo", new PropertyMappingValue(new List<string>() { "MalpracticeNo"}) },
//                { "MalpracticeNoDesc", new PropertyMappingValue(new List<string>() { "MalpracticeNo"}, true )},

//                { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )}
            
//            };

//        #endregion

//        #region CandidateBiodata

//        private Dictionary<string, PropertyMappingValue> _candidateBiodataPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "CandidateNo", new PropertyMappingValue(new List<string>() { "CandidateNo"}) },
//                { "CandidateNoDesc", new PropertyMappingValue(new List<string>() { "CandidateNo"}, true )},

//                { "CandName", new PropertyMappingValue(new List<string>() { "CandName"}) },
//                { "CandNameDesc", new PropertyMappingValue(new List<string>() { "CandName"}, true )},

    
//                { "Dob", new PropertyMappingValue(new List<string>() { "Dob"}) },
//                { "DobDesc", new PropertyMappingValue(new List<string>() { "Dob"}, true )},

//                { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )},


//            };

//        #endregion

//        #region CandidatePhoto

//        private Dictionary<string, PropertyMappingValue> _candidatePhotoPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )},

    
//            };

//        #endregion

//        #region Centre

//        private Dictionary<string, PropertyMappingValue> _centrePropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "CentreNo", new PropertyMappingValue(new List<string>() { "CentreNo"}) },
//                { "CentreNoDesc", new PropertyMappingValue(new List<string>() { "CentreNo"}, true )},

//                { "CentreName", new PropertyMappingValue(new List<string>() { "CentreName"}) },
//                { "CentreNameDesc", new PropertyMappingValue(new List<string>() { "CentreName"}, true )},

    
//            };

//        #endregion
        
//        #region CentreImage

//        private Dictionary<string, PropertyMappingValue> _centreImagePropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "CentreNo", new PropertyMappingValue(new List<string>() { "CentreNo"}) },
//                { "CentreNoDesc", new PropertyMappingValue(new List<string>() { "CentreNo"}, true )},

//                { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )}


//            };

//        #endregion

//        #region CentreLocation

//        private Dictionary<string, PropertyMappingValue> _centreLocationPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "CentreNo", new PropertyMappingValue(new List<string>() { "CentreNo"}) },
//                { "CentreNoDesc", new PropertyMappingValue(new List<string>() { "CentreNo"}, true )},

//                 { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )}


//            };

//        #endregion

//        #region ContactType

//        private Dictionary<string, PropertyMappingValue> _contactTypePropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "Description", new PropertyMappingValue(new List<string>() { "Description"}) },
//                { "DescriptionDesc", new PropertyMappingValue(new List<string>() { "Description"}, true )},

             

//            };

//        #endregion

//        #region Country

//        private Dictionary<string, PropertyMappingValue> _countryPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "CountryName", new PropertyMappingValue(new List<string>() { "CountryName"}) },
//                { "CountryNameDesc", new PropertyMappingValue(new List<string>() { "CountryName"}, true )},

//                { "CountryCode", new PropertyMappingValue(new List<string>() { "CountryCode"}) },
//                { "CountryCodeDesc", new PropertyMappingValue(new List<string>() { "CountryCode"}, true )},

             

//            };

//        #endregion        

//        #region CountryPhotoSpec

//        private Dictionary<string, PropertyMappingValue> _countryPhotoSpecPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "CountryCode", new PropertyMappingValue(new List<string>() { "CountryCode"}) },
//                { "CountryCodeDesc", new PropertyMappingValue(new List<string>() { "CountryCode"}, true )},

            
             

//            };

//        #endregion

//        #region CountrySubject

//        private Dictionary<string, PropertyMappingValue> _countrySubjectPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "CountryCode", new PropertyMappingValue(new List<string>() { "CountryCode"}) },
//                { "CountryCodeDesc", new PropertyMappingValue(new List<string>() { "CountryCode"}, true )},

//                { "SubjectCode", new PropertyMappingValue(new List<string>() { "SubjectCode"}) },
//                { "SubjectCodeDesc", new PropertyMappingValue(new List<string>() { "SubjectCode"}, true )},

             

//            };

//        #endregion

//        #region CountryYearSpec

//        private Dictionary<string, PropertyMappingValue> _countryYearSpecPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "CountryCode", new PropertyMappingValue(new List<string>() { "CountryCode"}) },
//                { "CountryCodeDesc", new PropertyMappingValue(new List<string>() { "CountryCode"}, true )},



//            };

//        #endregion

//        #region Disability

//        private Dictionary<string, PropertyMappingValue> _disabilityPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "DisabilityDescription", new PropertyMappingValue(new List<string>() { "DisabilityDescription"}) },
//                { "DisabilityDescriptionDesc", new PropertyMappingValue(new List<string>() { "DisabilityDescription"}, true )},

//                { "DisabilityCode", new PropertyMappingValue(new List<string>() { "DisabilityCode"}) },
//                { "DisabilityCodeDesc", new PropertyMappingValue(new List<string>() { "DisabilityCode"}, true )},

             

//            };

//        #endregion

//        #region ExamParameter

//        private Dictionary<string, PropertyMappingValue> _examParameterPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "ExamYear", new PropertyMappingValue(new List<string>() { "ExamYear"}) },
//                { "ExamYearDesc", new PropertyMappingValue(new List<string>() { "ExamYear"}, true )},
                
//                { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )}
//            };

//        #endregion

//        #region ExamType

//        private Dictionary<string, PropertyMappingValue> _examTypePropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "Title", new PropertyMappingValue(new List<string>() { "Title"}) },
//                { "TitleDesc", new PropertyMappingValue(new List<string>() { "Title"}, true )},

//                { "Code", new PropertyMappingValue(new List<string>() { "Code"}) },
//                { "CodeDesc", new PropertyMappingValue(new List<string>() { "Code"}, true )},


//                { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )}
//            };

//        #endregion

//        #region HandlingOffice

//        private Dictionary<string, PropertyMappingValue> _handlingOfficePropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "Description", new PropertyMappingValue(new List<string>() { "Description"}) },
//                { "DescriptionDesc", new PropertyMappingValue(new List<string>() { "Description"}, true )},
//            };

//        #endregion

//        #region Lga

//        private Dictionary<string, PropertyMappingValue> _lgaPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "LgaCode", new PropertyMappingValue(new List<string>() { "LgaCode"}) },
//                { "LgaCodeDesc", new PropertyMappingValue(new List<string>() { "LgaCode"}, true )},
            
//                { "LgaName", new PropertyMappingValue(new List<string>() { "LgaName"}) },
//                { "LgaNameDesc", new PropertyMappingValue(new List<string>() { "LgaName"}, true )},
             
//                { "StateCode", new PropertyMappingValue(new List<string>() { "StateCode"}) },
//                { "StateCodeDesc", new PropertyMappingValue(new List<string>() { "StateCode"}, true )},
            
//            };

//        #endregion

//        #region MalpracticeCode


//        private Dictionary<string, PropertyMappingValue> _malpracticeCodePropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {
//                { "KeyCode", new PropertyMappingValue(new List<string>() { "KeyCode"}) },
//                { "KeyCodeDesc", new PropertyMappingValue(new List<string>() { "KeyCode"}, true )},

//                { "SingularCode", new PropertyMappingValue(new List<string>() { "SingularCode"}) },
//                { "SingularCodeDesc", new PropertyMappingValue(new List<string>() { "SingularCode"}, true )},

//                { "PluralCode", new PropertyMappingValue(new List<string>() { "PluralCode"}) },
//                { "PluralCodeDesc", new PropertyMappingValue(new List<string>() { "PluralCode"}, true )},

//                { "MalpracticeDescription", new PropertyMappingValue(new List<string>() { "MalpracticeDescription"}) },
//                { "MalpracticeDescriptionDesc", new PropertyMappingValue(new List<string>() { "MalpracticeDescription"}, true )},

//                { "RuleCode", new PropertyMappingValue(new List<string>() { "RuleCode"}) },
//                { "RuleCodeDesc", new PropertyMappingValue(new List<string>() { "RuleCode"}, true )},

//                { "SourceCode", new PropertyMappingValue(new List<string>() { "SourceCode"}) },
//                { "SourceCodeDesc", new PropertyMappingValue(new List<string>() { "SourceCode"}, true )},
//            };

//        #endregion

//        #region MalpracticeReportSource


//        private Dictionary<string, PropertyMappingValue> _malpracticeReportSourcePropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {
//                { "ReportSourceCode", new PropertyMappingValue(new List<string>() { "ReportSourceCode"}) },
//                { "ReportSourceCodeDesc", new PropertyMappingValue(new List<string>() { "ReportSourceCode"}, true )},

//                { "SourceName", new PropertyMappingValue(new List<string>() { "SourceName"}) },
//                { "SourceNameDesc", new PropertyMappingValue(new List<string>() { "SourceName"}, true )},

//                { "SourceCode", new PropertyMappingValue(new List<string>() { "SourceCode"}) },
//                { "SourceCodeDesc", new PropertyMappingValue(new List<string>() { "SourceCode"}, true )},

//            };

//        #endregion

//        #region Office

//        private Dictionary<string, PropertyMappingValue> _officePropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {
//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "OfficeTitle", new PropertyMappingValue(new List<string>() { "OfficeTitle"}) },
//                { "OfficeTitleDesc", new PropertyMappingValue(new List<string>() { "OfficeTitle"}, true )},

//                { "ContactAddress", new PropertyMappingValue(new List<string>() { "ContactAddress"}) },
//                { "ContactAddressDesc", new PropertyMappingValue(new List<string>() { "ContactAddress"}, true )},

//                { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )},
//            };

//        #endregion

//        #region OfficeCentre

//        private Dictionary<string, PropertyMappingValue> _officeCentrePropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "CentreNo", new PropertyMappingValue(new List<string>() { "CentreNo"}) },
//                { "CentreNoDesc", new PropertyMappingValue(new List<string>() { "CentreNo"}, true )},

//                { "DateAssigned", new PropertyMappingValue(new List<string>() { "DateAssigned"}) },
//                { "DateAssignedDesc", new PropertyMappingValue(new List<string>() { "DateAssigned"}, true )},
//            };

//        #endregion

//        #region OfficeContact

//        private Dictionary<string, PropertyMappingValue> _officeContactPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {
//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "ContactDetails", new PropertyMappingValue(new List<string>() { "ContactDetails"}) },
//                { "ContactDetailsDesc", new PropertyMappingValue(new List<string>() { "ContactDetails"}, true )},


//                { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )}
//,
//            };

//        #endregion

//        #region OfficeDesignation

//        private Dictionary<string, PropertyMappingValue> _officeDesignationPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {
//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "DesignationTitle", new PropertyMappingValue(new List<string>() { "OfficeDesignationTitle"}) },
//                { "DesignationTitleDesc", new PropertyMappingValue(new List<string>() { "OfficeDesignationTitle"}, true )},

//            };

//        #endregion

//        #region OfficeImage

//        private Dictionary<string, PropertyMappingValue> _officeImagePropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {
//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )}
//            };

//        #endregion

//        #region OfficeRank

//        private Dictionary<string, PropertyMappingValue> _officeRankPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {
//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "RankTitle", new PropertyMappingValue(new List<string>() { "OfficeRankTitle"}) },
//                { "RankTitleDesc", new PropertyMappingValue(new List<string>() { "OfficeRankTitle"}, true )},

//                { "RankShortName", new PropertyMappingValue(new List<string>() { "ShortName"}) },
//                { "RankShortNameDesc", new PropertyMappingValue(new List<string>() { "ShortName"}, true )}

//            };

//        #endregion

//        #region OnlineService

//        private Dictionary<string, PropertyMappingValue> _onlineServicePropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {
//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "ServiceTitle", new PropertyMappingValue(new List<string>() { "OnlineServiceTitle"}) },
//                { "ServiceTitleDesc", new PropertyMappingValue(new List<string>() { "OnlineServiceTitle"}, true )},

//                { "Url", new PropertyMappingValue(new List<string>() { "ShortName"}) },
//                { "UrlDesc", new PropertyMappingValue(new List<string>() { "ShortName"}, true )},
                
//                { "Description", new PropertyMappingValue(new List<string>() { "ShortName"}) },
//                { "DescriptionDesc", new PropertyMappingValue(new List<string>() { "ShortName"}, true )},

//                { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )}
//            };

//        #endregion

//        #region Paper

//        private Dictionary<string, PropertyMappingValue> _paperPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {
 
//                { "PaperName", new PropertyMappingValue(new List<string>() { "PaperName"}) },
//                { "PaperNameDesc", new PropertyMappingValue(new List<string>() { "PaperName"}, true )},

//                { "PaperCode", new PropertyMappingValue(new List<string>() { "PaperCode"}) },
//                { "PaperCodeDesc", new PropertyMappingValue(new List<string>() { "PaperCode"}, true )},

//            };

//        #endregion

//        #region RankCategory

//        private Dictionary<string, PropertyMappingValue> _rankCategoryPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {
//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "CategoryTitle", new PropertyMappingValue(new List<string>() { "RankCategoryTitle"}) },
//                { "CategoryTitleDesc", new PropertyMappingValue(new List<string>() { "RankCategoryTitle"}, true )}
//            };

//        #endregion

//        #region ReportSource

//        private Dictionary<string, PropertyMappingValue> _reportSourcePropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "SourceName", new PropertyMappingValue(new List<string>() { "SourceName"}) },
//                { "SourceNameDesc", new PropertyMappingValue(new List<string>() { "SourceName"}, true )},

//                { "SourceCode", new PropertyMappingValue(new List<string>() { "SourceCode"}) },
//                { "SourceCodeDesc", new PropertyMappingValue(new List<string>() { "SourceCode"}, true )},
//            };

//        #endregion

//        #region RequestCategory

//        private Dictionary<string, PropertyMappingValue> _requestCategoryPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "Description", new PropertyMappingValue(new List<string>() { "Description"}) },
//                { "DescriptionDesc", new PropertyMappingValue(new List<string>() { "Description"}, true )},
//            };

//        #endregion

//        #region RequestTicket

//        private Dictionary<string, PropertyMappingValue> _requestTicketPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "Description", new PropertyMappingValue(new List<string>() { "Description"}) },
//                { "DescriptionDesc", new PropertyMappingValue(new List<string>() { "Description"}, true )},

//                { "Narration", new PropertyMappingValue(new List<string>() { "Narration"}) },
//                { "NarrationDesc", new PropertyMappingValue(new List<string>() { "Narration"}, true )},


//                { "DateCreated", new PropertyMappingValue(new List<string>() { "DateCreated"}) },
//                { "DateCreatedDesc", new PropertyMappingValue(new List<string>() { "DateCreated"}, true )}

//            };

//        #endregion

//        #region RequestTicketItem

//        private Dictionary<string, PropertyMappingValue> _requestTicketItemPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                //{ "Narration", new PropertyMappingValue(new List<string>() { "Narration"}) },
//                //{ "NarrationDesc", new PropertyMappingValue(new List<string>() { "Narration"}, true )},

//                { "Description", new PropertyMappingValue(new List<string>() { "Description"}) },
//                { "DescriptionDesc", new PropertyMappingValue(new List<string>() { "Description"}, true )},


//                { "CreateDate", new PropertyMappingValue(new List<string>() { "CreateDate"}) },
//                { "CreateDateDesc", new PropertyMappingValue(new List<string>() { "CreateDate"}, true )}
//            };

//        #endregion

//        #region State

//        private Dictionary<string, PropertyMappingValue> _statePropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {
//                { "StateCode", new PropertyMappingValue(new List<string>() { "StateCode"}) },
//                { "StateCodeDesc", new PropertyMappingValue(new List<string>() { "StateCode"}, true )},

//                { "StateName", new PropertyMappingValue(new List<string>() { "StateName"}) },
//                { "StateNameDesc", new PropertyMappingValue(new List<string>() { "StateName"}, true )},
//            };

//        #endregion

//        #region Subject

//        private Dictionary<string, PropertyMappingValue> _subjectPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {
        
//                { "SubjectCode", new PropertyMappingValue(new List<string>() { "SubjectCode"}) },
//                { "SubjectCodeDesc", new PropertyMappingValue(new List<string>() { "SubjectCode"}, true )},

//                { "SubjectName", new PropertyMappingValue(new List<string>() { "SubjectName"}) },
//                { "SubjectNameDesc", new PropertyMappingValue(new List<string>() { "SubjectName"}, true )},
//            };

//        #endregion

//        #region SubjectProgram

//        private Dictionary<string, PropertyMappingValue> _subjectProgramPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {
        
//                { "ProgramCode", new PropertyMappingValue(new List<string>() { "ProgramCode"}) },
//                { "ProgramCodeDesc", new PropertyMappingValue(new List<string>() { "ProgramCode"}, true )},

//                { "ProgramDescription", new PropertyMappingValue(new List<string>() { "ProgramDescription"}) },
//                { "ProgramDescriptionDesc", new PropertyMappingValue(new List<string>() { "ProgramDescription"}, true )},
//            };

//        #endregion

//        #region SubjectProgramSubject

//        private Dictionary<string, PropertyMappingValue> _subjectProgramSubjectPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {
        
//                { "ProgramCode", new PropertyMappingValue(new List<string>() { "ProgramCode"}) },
//                { "ProgramCodeDesc", new PropertyMappingValue(new List<string>() { "ProgramCode"}, true )},

//                { "SubjectCode", new PropertyMappingValue(new List<string>() { "SubjectCode"}) },
//                { "SubjectCodeDesc", new PropertyMappingValue(new List<string>() { "SubjectCode"}, true )},
//            };

//        #endregion

//        #region TicketStatus

//        private Dictionary<string, PropertyMappingValue> _ticketStatusPropertyMapping =
//            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
//            {

//                { "Id", new PropertyMappingValue(new List<string>() { "Id"}) },
//                { "IdDesc", new PropertyMappingValue(new List<string>() { "Id"}, true )},

//                { "Description", new PropertyMappingValue(new List<string>() { "Description"}) },
//                { "DescriptionDesc", new PropertyMappingValue(new List<string>() { "Description"}, true )},
//            };

//        #endregion



        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            //_propertyMappings.Add(new PropertyMapping<ApplicationUserDto, ApplicationUser>(_applicationUserPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<AuditTrailDto, AuditTrail>(_auditTrailPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<BroadSheetEvidenceDto, BroadSheetEvidence>(_broadSheetEvidencePropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<BroadSheetItemDto, BroadSheetItem>(_broadSheetItemPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<CandidateBiodataDto, CandidateBiodata>(_candidateBiodataPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<CandidatePhotoDto, CandidatePhoto>(_candidatePhotoPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<CentreDto, Centre>(_centrePropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<CentreImageDto, CentreImage>(_centreImagePropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<CentreLocationDto, CentreLocation>(_centreLocationPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<CountryDto, Country>(_countryPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<CountryPhotoSpecDto, CountryPhotoSpec>(_countryPhotoSpecPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<CountrySubjectDto, CountrySubject>(_countrySubjectPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<CountryYearSpecDto, CountryYearSpec>(_countryYearSpecPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<DisabilityDto, Disability>(_disabilityPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<ExamParameterDto, ExamParameter>(_examParameterPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<ExamTypeDto, ExamType>(_examTypePropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<HandlingOfficeDto, HandlingOffice>(_handlingOfficePropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<LgaDto, Lga>(_lgaPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<MalpracticeCodeDto, MalpracticeCode>(_malpracticeCodePropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<MalpracticeReportSourceDto, MalpracticeReportSource>(_malpracticeReportSourcePropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<OfficeDto, Office>(_officePropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<OfficeCentreDto, OfficeCentre>(_officeCentrePropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<OfficeContactDto, OfficeContact>(_officeContactPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<OfficeDesignationDto, OfficeDesignation>(_officeDesignationPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<OfficeImageDto, OfficeImage>(_officeImagePropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<OfficeRankDto, OfficeRank>(_officeRankPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<OnlineServiceDto, OnlineService>(_onlineServicePropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<PaperDto, Paper>(_paperPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<RankCategoryDto, RankCategory>(_rankCategoryPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<ReportSourceDto, ReportSource>(_reportSourcePropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<RequestCategoryDto, RequestCategory>(_requestCategoryPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<RequestTicketDto, RequestTicket>(_requestTicketPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<RequestTicketItemDto, RequestTicketItem>(_requestTicketItemPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<StateDto, State>(_statePropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<SubjectDto, Subject>(_subjectPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<SubjectProgramDto, SubjectProgram>(_subjectProgramPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<SubjectProgramSubjectDto, SubjectProgramSubject>(_subjectProgramSubjectPropertyMapping));
            //_propertyMappings.Add(new PropertyMapping<TicketStatusDto, TicketStatus>(_ticketStatusPropertyMapping));

        }

        //Add a method to validate the sorting parameters mapping
        public bool ValidateMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if(string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            //the string is separated by ",", so we split it
            var fieldsAfterSplit = fields.Split(',');

            //then we run through all the fileds
            foreach (var field in fieldsAfterSplit)
            {
                //trim
                var trimmedField = field.Trim();

                //remove everything after the first " " - if the fields 
                //are coming from an orderBy string, this part must be
                //ignored
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                //find the matching property
                if(!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }

            return true;
        }
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            //get the matching mapping
            var matchingMapping = _propertyMappings
                .OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()._mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance " +
                $"for <{typeof(TSource)}, {typeof(TDestination)}");
        }
    }
}
