#pragma checksum "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManageLocations\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "faf088fb32289be17a9a2215ced9a8d8e445f493"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ManageLocations_Index), @"mvc.1.0.view", @"/Views/ManageLocations/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"faf088fb32289be17a9a2215ced9a8d8e445f493", @"/Views/ManageLocations/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99db7b73c51d84dd15942319281775a6f70cf22f", @"/Views/_ViewImports.cshtml")]
    public class Views_ManageLocations_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "States", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("small-box bg-light"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManageLocations\Index.cshtml"
  
    ViewData["Title"] = "Manage Locations";
    Layout = "~/Views/Shared/_LayoutAdmin.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<!-- Content Header (Page header) -->\r\n<section class=\"content-header\">\r\n    <div class=\"container-fluid\">\r\n        <div class=\"row mb-2\">\r\n            <div class=\"col-sm-6\">\r\n                <h1>");
#nullable restore
#line 13 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManageLocations\Index.cshtml"
               Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n            </div>\r\n            <div class=\"col-sm-6\">\r\n                <ol class=\"breadcrumb float-sm-right\">\r\n                    <li class=\"breadcrumb-item\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "faf088fb32289be17a9a2215ced9a8d8e445f4936136", async() => {
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
#line 18 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManageLocations\Index.cshtml"
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
#line 32 "C:\csharp_projects\SchoolRecognition\SchoolRecognition\Views\ManageLocations\Index.cshtml"
       Write(Vereyon.Web.FlashMessageHtmlHelper.RenderFlashMessages(Html));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            <div class=""card"">
                <div class=""card-header"">
                    <h3 class=""card-title"">Manage Locations</h3>
                </div>
                <!-- /.card-header -->
                <div class=""card-body"">

                    <div class=""row"">
                        <div class=""col-12"">
                            <!-- small card -->
                            <a");
            BeginWriteAttribute("href", " href=\"", 1381, "\"", 1388, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""small-box bg-light"">
                                <div class=""inner"">
                                    <h3 class=""mb-5"" style=""font-weight:200"">Location Types</h3>

                                </div>
                                <div class=""icon"">
                                    <i class=""fas fa-street-view""></i>
                                </div>
");
            WriteLiteral("                            </a>\r\n                        </div>\r\n\r\n                        <!-- ./col -->\r\n                        <div class=\"col-12\">\r\n                            <!-- small card -->\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "faf088fb32289be17a9a2215ced9a8d8e445f4939836", async() => {
                WriteLiteral(@"
                                <div class=""inner"">
                                    <h3 class=""mb-5"" style=""font-weight:200"">States</h3>

                                </div>
                                <div class=""icon"">
                                    <i class=""fas fa-globe-africa""></i>
                                </div>
");
                WriteLiteral("                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
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
            WriteLiteral("\r\n                        </div>\r\n                        <!-- ./col -->\r\n                        <div class=\"col-12\">\r\n                            <!-- small card -->\r\n                            <a");
            BeginWriteAttribute("href", " href=\"", 3062, "\"", 3069, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""small-box bg-light"">
                                <div class=""inner"">
                                    <h3 class=""mb-5"" style=""font-weight:200"">Local Governments</h3>

                                </div>
                                <div class=""icon"">
                                    <i class=""fas fa-map-marked-alt""></i>
                                </div>
");
            WriteLiteral(@"                            </a>
                        </div>
                        <!-- ./col -->
                    </div>

                </div>
                <!-- /.card-body -->
            </div>
            <!-- /.card -->
        </div>
        <!-- /.col -->
    </div>



</section>
<!-- /.content -->



");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591