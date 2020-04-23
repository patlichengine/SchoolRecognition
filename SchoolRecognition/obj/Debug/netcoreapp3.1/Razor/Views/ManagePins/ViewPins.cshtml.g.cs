#pragma checksum "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManagePins\ViewPins.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ef1c9d883afcaecabe170aab83048facde91c6a8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ManagePins_ViewPins), @"mvc.1.0.view", @"/Views/ManagePins/ViewPins.cshtml")]
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
using SchoolRecognition.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef1c9d883afcaecabe170aab83048facde91c6a8", @"/Views/ManagePins/ViewPins.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"57d0266bbca735634eabe717bec472dc819d6d99", @"/Views/_ViewImports.cshtml")]
    public class Views_ManagePins_ViewPins : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SchoolRecognition.Models.PinsViewDto>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/datatables/datatables.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ManagePins", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "GeneratePins", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-secondary text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/datatables/datatables.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManagePins\ViewPins.cshtml"
  
    var recognitionType = (RecognitionTypesDto)ViewData["RecognitionType"];
    ViewData["Title"] = recognitionType.RecognitionTypeName + " Pins";
    Layout = "~/Views/Shared/_LayoutAdmin.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("styles", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ef1c9d883afcaecabe170aab83048facde91c6a86767", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n");
            }
            );
            WriteLiteral("\r\n<!-- Content Header (Page header) -->\r\n<section class=\"content-header\">\r\n    <div class=\"container-fluid\">\r\n        <div class=\"row mb-2\">\r\n            <div class=\"col-sm-6\">\r\n                <h1>");
#nullable restore
#line 18 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManagePins\ViewPins.cshtml"
               Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n            </div>\r\n            <div class=\"col-sm-6\">\r\n                <ol class=\"breadcrumb float-sm-right\">\r\n                    <li class=\"breadcrumb-item\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef1c9d883afcaecabe170aab83048facde91c6a88644", async() => {
                WriteLiteral("Dashboard");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</li>\r\n                    <li class=\"breadcrumb-item\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef1c9d883afcaecabe170aab83048facde91c6a810263", async() => {
                WriteLiteral("Manage Pins");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</li>\r\n                    <li class=\"breadcrumb-item active\">");
#nullable restore
#line 24 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManagePins\ViewPins.cshtml"
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
#line 38 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManagePins\ViewPins.cshtml"
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
                                ");
#nullable restore
#line 45 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManagePins\ViewPins.cshtml"
                           Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </a>
                        </li>
                    </ul>
                </div>
                <div class=""card-body"">
                    <div class=""tab-content"" id=""custom-tabs-three-tabContent"">
                        <div class=""tab-pane fade active show"" id=""custom-tabs-three-home"" role=""tabpanel"" aria-labelledby=""custom-tabs-three-home-tab"">

                            <div class=""clearfix mb-3"">
                                <div class=""float-right"">
                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef1c9d883afcaecabe170aab83048facde91c6a814064", async() => {
                WriteLiteral("\r\n                                        <i class=\"fas fa-receipt\"></i>\r\n                                        Generate Pins\r\n                                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 56 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManagePins\ViewPins.cshtml"
                                                                                WriteLiteral(recognitionType.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
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

                            <table id=""datatable-1"" class=""table table-bordered table-striped dataTable"" role=""grid"" aria-describedby=""example1_info"">
                                <thead>
                                    <tr role=""row"">
                                        <td>Date Generated</td>
                                        <td>Serial Number</td>
                                        <td>Created By</td>
                                        <td>Active</td>
                                        <td>In Use</td>
                                    </tr>
                                </thead>
                                <tbody>
");
#nullable restore
#line 74 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManagePins\ViewPins.cshtml"
                                     foreach (var item in Model)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <tr role=\"row\">\r\n                                            <td>");
#nullable restore
#line 77 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManagePins\ViewPins.cshtml"
                                           Write(item.DateCreated);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 78 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManagePins\ViewPins.cshtml"
                                           Write(item.SerialNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 79 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManagePins\ViewPins.cshtml"
                                           Write(item.CreatedByUser);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>\r\n                                                <a href=\"#\"");
            BeginWriteAttribute("class", " class=\"", 3950, "\"", 4025, 4);
            WriteAttributeValue("", 3958, "btn", 3958, 3, true);
            WriteAttributeValue(" ", 3961, "btn-xs", 3962, 7, true);
            WriteAttributeValue(" ", 3968, "disabled", 3969, 9, true);
#nullable restore
#line 81 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManagePins\ViewPins.cshtml"
WriteAttributeValue(" ", 3977, item.IsActive ? "btn-success" : "btn-danger", 3978, 47, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                                                    <i class=""fas fa-check""></i>
                                                </a>
                                            </td>
                                            <td>
                                                <a href=""#""");
            BeginWriteAttribute("class", " class=\"", 4325, "\"", 4399, 4);
            WriteAttributeValue("", 4333, "btn", 4333, 3, true);
            WriteAttributeValue(" ", 4336, "btn-xs", 4337, 7, true);
            WriteAttributeValue(" ", 4343, "disabled", 4344, 9, true);
#nullable restore
#line 86 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManagePins\ViewPins.cshtml"
WriteAttributeValue(" ", 4352, item.IsInUse ? "btn-success" : "btn-danger", 4353, 46, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                                    <i class=\"fas fa-check\"></i>\r\n                                                </a>\r\n                                            </td>\r\n                                        </tr>\r\n");
#nullable restore
#line 91 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManagePins\ViewPins.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </tbody>
                            </table>

                        </div>
                    </div>
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
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef1c9d883afcaecabe170aab83048facde91c6a821409", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    <script>\r\n        $(document).ready( function () {\r\n            $(\'#datatable-1\').DataTable({\r\n                \"order\": [[ 0, \"desc\" ]]\r\n            });\r\n        } );\r\n    </script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SchoolRecognition.Models.PinsViewDto>> Html { get; private set; }
    }
}
#pragma warning restore 1591
