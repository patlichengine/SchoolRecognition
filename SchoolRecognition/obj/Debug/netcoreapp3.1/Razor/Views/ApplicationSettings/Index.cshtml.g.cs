#pragma checksum "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ApplicationSettings\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6823c92dd58504df1b4fd8b47e9c0b9dd16f51e4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ApplicationSettings_Index), @"mvc.1.0.view", @"/Views/ApplicationSettings/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\_ViewImports.cshtml"
using SchoolRecognition;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\_ViewImports.cshtml"
using SchoolRecognition.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\_ViewImports.cshtml"
using SchoolRecognition.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6823c92dd58504df1b4fd8b47e9c0b9dd16f51e4", @"/Views/ApplicationSettings/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99db7b73c51d84dd15942319281775a6f70cf22f", @"/Views/_ViewImports.cshtml")]
    public class Views_ApplicationSettings_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ApplicationSettingsViewDto>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ApplicationSettings", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Update", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-light"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ApplicationSettings\Index.cshtml"
  
    ViewData["Title"] = "Application Settings";
    Layout = "~/Views/Shared/_LayoutAdmin.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
            DefineSection("styles", async() => {
                WriteLiteral("\r\n");
                WriteLiteral("\r\n");
            }
            );
            WriteLiteral("\r\n<!-- Content Header (Page header) -->\r\n<section class=\"content-header\">\r\n    <div class=\"container-fluid\">\r\n        <div class=\"row mb-2\">\r\n            <div class=\"col-sm-6\">\r\n                <h1>");
#nullable restore
#line 18 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ApplicationSettings\Index.cshtml"
               Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n            </div>\r\n            <div class=\"col-sm-6\">\r\n                <ol class=\"breadcrumb float-sm-right\">\r\n                    <li class=\"breadcrumb-item\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6823c92dd58504df1b4fd8b47e9c0b9dd16f51e46355", async() => {
                WriteLiteral("Dashboard");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</li>\r\n                    <li class=\"breadcrumb-item active\">");
#nullable restore
#line 23 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ApplicationSettings\Index.cshtml"
                                                  Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</li>
                </ol>
            </div>
        </div>
    </div><!-- /.container-fluid -->
</section>

<!-- Main content -->
<section class=""content"">


    <div class=""row"">
        <div class=""col-12"">
            <!-- /.card -->
            ");
#nullable restore
#line 37 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ApplicationSettings\Index.cshtml"
       Write(Vereyon.Web.FlashMessageHtmlHelper.RenderFlashMessages(Html));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

            <div class=""card card-primary card-outline card-outline-tabs"">
                <div class=""card-header p-0 border-bottom-0"">
                    <ul class=""nav nav-tabs"" id=""custom-tabs-three-tab"" role=""tablist"">
                        <li class=""nav-item"">
                            <a class=""nav-link active disabled"" id=""custom-tabs-three-home-tab"" data-toggle=""pill"" role=""tab"" aria-controls=""custom-tabs-three-home"" aria-selected=""true"">
                                <b>");
#nullable restore
#line 44 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ApplicationSettings\Index.cshtml"
                              Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n                            </a>\r\n                        </li>\r\n                    </ul>\r\n                </div>\r\n                <div class=\"card-body clearfix\">\r\n                    <div class=\"float-right\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6823c92dd58504df1b4fd8b47e9c0b9dd16f51e49871", async() => {
                WriteLiteral("\r\n                            <i class=\"fas fa-users-cog\"></i>\r\n                            Change Settings\r\n                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    </div>
                </div>

                <div class=""card-body"">


                    <table class=""table "">
                        <caption style=""caption-side: top"">

                            <h5 style=""font-weight: 100"">Prefered Application Settings:</h5>
                        </caption>
                        <tbody>
                            <tr>
                                <td class=""p-0"">
                                    <dl>
                                        <dt>Minimum Years a School Must Have Been Established Before Recognition</dt>
                                        <dd class=""lead"">");
#nullable restore
#line 71 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ApplicationSettings\Index.cshtml"
                                                     Write(Model != null && Model.MinimumNoOfRecogYears != null ? Model.MinimumNoOfRecogYears.Value.ToString() : "NOT SET");

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</dd class=""lead"">
                                    </dl>
                                </td>
                            </tr>
                            <tr>
                                <td class=""p-0"">
                                    <dl>
                                        <dt>Maximum Number of Pins That Can Be Generated At A Time</dt>
                                        <dd class=""lead"">");
#nullable restore
#line 79 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ApplicationSettings\Index.cshtml"
                                                     Write(Model != null && Model.MaximumNoOfPinsToGenerate != null ? Model.MaximumNoOfPinsToGenerate.Value.ToString() : "NOT SET");

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</dd class=""lead"">
                                    </dl>
                                </td>
                            </tr>
                            <tr>
                                <td class=""p-0"">
                                    <dl>
                                        <dt>Minimum Number of School Subjects</dt>
                                        <dd class=""lead"">");
#nullable restore
#line 87 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ApplicationSettings\Index.cshtml"
                                                     Write(Model != null && Model.MinimumSchoolSubjects != null ? Model.MinimumSchoolSubjects.Value.ToString() : "NOT SET");

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</dd class=""lead"">
                                    </dl>
                                </td>
                            </tr>
                            <tr>
                                <td class=""p-0"">
                                    <dl>
                                        <dt>Maximum Number of Core Subjects</dt>
                                        <dd class=""lead"">");
#nullable restore
#line 95 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ApplicationSettings\Index.cshtml"
                                                     Write(Model != null && Model.MaximumCoreSubjects != null ? Model.MaximumCoreSubjects.Value.ToString() : "NOT SET");

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</dd class=""lead"">
                                    </dl>
                                </td>
                            </tr>
                            <tr>
                                <td class=""p-0"">
                                    <dl>
                                        <dt>Minimum Number Of Trade Subjects</dt>
                                        <dd class=""lead"">");
#nullable restore
#line 103 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ApplicationSettings\Index.cshtml"
                                                     Write(Model != null && Model.MinimumTradeSubjects != null ? Model.MinimumTradeSubjects.Value.ToString() : "NOT SET");

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</dd class=""lead"">
                                    </dl>
                                </td>
                            </tr>
                        </tbody>
                    </table>


                </div>
                <!-- /.card -->
            </div>
            <!-- /.card -->
        </div>
        <!-- /.col -->
    </div>



</section>
<!-- /.content -->

");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n");
            }
            );
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ApplicationSettingsViewDto> Html { get; private set; }
    }
}
#pragma warning restore 1591
