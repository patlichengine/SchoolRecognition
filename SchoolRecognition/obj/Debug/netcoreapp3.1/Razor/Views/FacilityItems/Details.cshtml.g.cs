#pragma checksum "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\FacilityItems\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b5b86d9cc9e5daa7ca0c7fa658c6ea3c8e393050"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_FacilityItems_Details), @"mvc.1.0.view", @"/Views/FacilityItems/Details.cshtml")]
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
#line 1 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\_ViewImports.cshtml"
using SchoolRecognition;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\_ViewImports.cshtml"
using SchoolRecognition.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\_ViewImports.cshtml"
using SchoolRecognition.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5b86d9cc9e5daa7ca0c7fa658c6ea3c8e393050", @"/Views/FacilityItems/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99db7b73c51d84dd15942319281775a6f70cf22f", @"/Views/_ViewImports.cshtml")]
    public class Views_FacilityItems_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SchoolRecognition.Models.FacilityItemsDto>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success btn-rounded btn-sm my-0"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-rounded btn-sm my-0"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "FacilityItems", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\FacilityItems\Details.cshtml"
  
    ViewData["Title"] = "Details";
   // Layout = "~/Views/Shared/_LayoutAdmin.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<!-- Content Wrapper. Contains page content -->
<section class=""form-simple"">

    <!-- Content Header (Page header) -->
    <section class=""content-header"">
        <div class=""container-fluid"">
            <div class=""row mb-2"">
                <div class=""col-sm-6"">
                </div>
                <div class=""col-sm-6"">
                    <ol class=""breadcrumb float-sm-right"">
                        <li class=""breadcrumb-item"">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5b86d9cc9e5daa7ca0c7fa658c6ea3c8e3930506006", async() => {
                WriteLiteral("Home");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"</li>
                        <li class=""breadcrumb-item active"">Details</li>
                    </ol>
                </div>
            </div>
        </div><!-- /.container-fluid -->
    </section>

    <!-- Main content -->
    <section class=""content"">
        <div class=""container-fluid"">
            <div class=""row"">
                <div class=""col-md-12"">


                    <!-- About Me Box -->
                    <div class=""card card-primary"">
                        <div class=""card-header"">
                            <h3 class=""card-title"">Detailed Facility Type Information of ");
#nullable restore
#line 38 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\FacilityItems\Details.cshtml"
                                                                                    Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
                        </div>
                        <!-- /.card-header -->
                        <div class=""card-body"">
                            <strong><i class=""fas fa-info mr-1""></i> The Facility Type Information are: </strong>



                            <hr>

                            <strong><i class=""fas fa-award mr-1""></i> Facility Type: <span class=""tag tag-primary text-primary""> ");
#nullable restore
#line 48 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\FacilityItems\Details.cshtml"
                                                                                                                            Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span> </strong>

                            

                            <hr>

                            <strong>
                                <i class=""fas fa-toggle-on mr-1""></i> Facility Active State:   <span class=""tag tag-primary text-success"">

");
#nullable restore
#line 57 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\FacilityItems\Details.cshtml"
                                     if (@Model.IsActive)
                                    {
                                        

#line default
#line hidden
#nullable disable
            WriteLiteral("Active ");
#nullable restore
#line 59 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\FacilityItems\Details.cshtml"
                                                            
                                    }
                                    else
                                    {
                                        

#line default
#line hidden
#nullable disable
            WriteLiteral("Not Active ");
#nullable restore
#line 63 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\FacilityItems\Details.cshtml"
                                                                
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


                                </span>
                            </strong>

                           

                            <hr>
                            <strong>
                                <i class=""fas fa-toggle-on mr-1""></i> is Summary:  <span class=""tag tag-primary text-success"">
");
#nullable restore
#line 76 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\FacilityItems\Details.cshtml"
                                     if (@Model.IsSummary)
                                    {


                                        

#line default
#line hidden
#nullable disable
            WriteLiteral("Yes ");
#nullable restore
#line 80 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\FacilityItems\Details.cshtml"
                                                         
                                    }
                                    else
                                    {
                                        

#line default
#line hidden
#nullable disable
            WriteLiteral("No ");
#nullable restore
#line 84 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\FacilityItems\Details.cshtml"
                                                        
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

                                </span>
                            </strong>

                           

                            <hr>

                          
                          

                            <hr>



                            

                            <hr>

                        </div>
                        <!-- /.card-body -->
                    </div>
                    <!-- /.card -->
                </div>

            </div>
            <!-- /.row -->
        </div><!-- /.container-fluid -->
    </section>
    <!-- /.content -->
    <!-- /.content-wrapper -->
</section>

<section");
            BeginWriteAttribute("class", " class=\"", 3739, "\"", 3747, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n\r\n    <div>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5b86d9cc9e5daa7ca0c7fa658c6ea3c8e39305012829", async() => {
                WriteLiteral("<i class=\"fa fa-pen\"></i> Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 123 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\FacilityItems\Details.cshtml"
                                                             WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5b86d9cc9e5daa7ca0c7fa658c6ea3c8e39305015171", async() => {
                WriteLiteral("<i class=\"fa fa-home\"></i> Go Home");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n    </div>\r\n</section>\r\n\r\n\r\n\r\n");
            WriteLiteral("<section class=\"content\">\r\n\r\n    <!-- Default box -->\r\n    <div class=\"card\">\r\n        <div class=\"card-header\">\r\n            <h3 class=\"card-title\">");
#nullable restore
#line 137 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\FacilityItems\Details.cshtml"
                              Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>

            <div class=""card-tools"">
                <button type=""button"" class=""btn btn-tool"" data-card-widget=""collapse"" data-toggle=""tooltip"" title=""Collapse"">
                    <i class=""fas fa-minus""></i>
                </button>
                <button type=""button"" class=""btn btn-tool"" data-card-widget=""remove"" data-toggle=""tooltip"" title=""Remove"">
                    <i class=""fas fa-times""></i>
                </button>
            </div>
        </div>
        <div class=""card-body"">
            <div class=""row"">
                <div class=""col-12 col-md-12 col-lg-8 order-2 order-md-1"">
                    <div class=""row"">
                        <div class=""col-12 col-sm-4"">
                            <div class=""info-box bg-light"">
                                <div class=""info-box-content"">
                                    <span class=""info-box-text text-center text-muted"">Estimated budget</span>
                                    <span class=""info-box-number");
            WriteLiteral(@" text-center text-muted mb-0"">2300</span>
                                </div>
                            </div>
                        </div>
                        <div class=""col-12 col-sm-4"">
                            <div class=""info-box bg-light"">
                                <div class=""info-box-content"">
                                    <span class=""info-box-text text-center text-muted"">Total amount spent</span>
                                    <span class=""info-box-number text-center text-muted mb-0"">2000</span>
                                </div>
                            </div>
                        </div>
                        <div class=""col-12 col-sm-4"">
                            <div class=""info-box bg-light"">
                                <div class=""info-box-content"">
                                    <span class=""info-box-text text-center text-muted"">Estimated project duration</span>
                                    <span class=""info-box-numb");
            WriteLiteral(@"er text-center text-muted mb-0"">
                                        20 <span>
                                        </span>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-12"">
                            <h4>Item </h4>
                            <div class=""post"">
                                <div class=""user-block"">
                                    <span class=""username"">
                                        <a href=""#""></a>
                                    </span>
                                    <span class=""description""></span>
                                </div>
                                <!-- /.user-block -->
                                <p>
                                    ");
#nullable restore
#line 192 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\FacilityItems\Details.cshtml"
                               Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </p>

                                <p>
                                    <a href=""#"" class=""link-black text-sm""><i class=""fas fa-link mr-1""></i> Demo File 1 v2</a>
                                </p>
                            </div>

                            <div class=""post clearfix"">
                                <div class=""user-block"">
                                    <img class=""img-circle img-bordered-sm"" src=""../../dist/img/user7-128x128.jpg"" alt=""User Image"">
                                    <span class=""username"">
                                        <a href=""#"">Sarah Ross</a>
                                    </span>
                                    <span class=""description"">Sent you a message - 3 days ago</span>
                                </div>
                                <!-- /.user-block -->
                                <p>
                                    Lorem ipsum represents a long-held tradition for");
            WriteLiteral(@" designers,
                                    typographers and the like. Some people hate it and argue for
                                    its demise, but others ignore.
                                </p>
                                <p>
                                    <a href=""#"" class=""link-black text-sm""><i class=""fas fa-link mr-1""></i> Demo File 2</a>
                                </p>
                            </div>

                            <div class=""post"">
                                <div class=""user-block"">
                                    <img class=""img-circle img-bordered-sm"" src=""../../dist/img/user1-128x128.jpg"" alt=""user image"">
                                    <span class=""username"">
                                        <a href=""#"">Jonathan Burke Jr.</a>
                                    </span>
                                    <span class=""description"">Shared publicly - 5 days ago</span>
                                </div>
        ");
            WriteLiteral(@"                        <!-- /.user-block -->
                                <p>
                                    Lorem ipsum represents a long-held tradition for designers,
                                    typographers and the like. Some people hate it and argue for
                                    its demise, but others ignore.
                                </p>

                                <p>
                                    <a href=""#"" class=""link-black text-sm""><i class=""fas fa-link mr-1""></i> Demo File 1 v1</a>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=""col-12 col-md-12 col-lg-4 order-1 order-md-2"">
                    <h3 class=""text-primary""><i class=""fas fa-paint-brush""></i> AdminLTE v3</h3>
                    <p class=""text-muted"">Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua bu");
            WriteLiteral(@"tcher retro keffiyeh dreamcatcher synth. Cosby sweater eu banh mi, qui irure terr.</p>
                    <br>
                    <div class=""text-muted"">
                        <p class=""text-sm"">
                            Client Company
                            <b class=""d-block"">Deveint Inc</b>
                        </p>
                        <p class=""text-sm"">
                            Project Leader
                            <b class=""d-block"">Tony Chicken</b>
                        </p>
                    </div>

                    <h5 class=""mt-5 text-muted"">Project files</h5>
                    <ul class=""list-unstyled"">
                        <li>
                            <a");
            BeginWriteAttribute("href", " href=\"", 11092, "\"", 11099, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn-link text-secondary\"><i class=\"far fa-fw fa-file-word\"></i> Functional-requirements.docx</a>\r\n                        </li>\r\n                        <li>\r\n                            <a");
            BeginWriteAttribute("href", " href=\"", 11297, "\"", 11304, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn-link text-secondary\"><i class=\"far fa-fw fa-file-pdf\"></i> UAT.pdf</a>\r\n                        </li>\r\n                        <li>\r\n                            <a");
            BeginWriteAttribute("href", " href=\"", 11480, "\"", 11487, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn-link text-secondary\"><i class=\"far fa-fw fa-envelope\"></i> Email-from-flatbal.mln</a>\r\n                        </li>\r\n                        <li>\r\n                            <a");
            BeginWriteAttribute("href", " href=\"", 11678, "\"", 11685, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn-link text-secondary\"><i class=\"far fa-fw fa-image \"></i> Logo.png</a>\r\n                        </li>\r\n                        <li>\r\n                            <a");
            BeginWriteAttribute("href", " href=\"", 11860, "\"", 11867, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn-link text-secondary""><i class=""far fa-fw fa-file-word""></i> Contract-10_12_2014.docx</a>
                        </li>
                    </ul>
                    <div class=""text-center mt-5 mb-3"">
                        <a href=""#"" class=""btn btn-sm btn-primary"">Add files</a>
                        <a href=""#"" class=""btn btn-sm btn-warning"">Report contact</a>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.card-body -->
    </div>
    <!-- /.card -->

</section>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SchoolRecognition.Models.FacilityItemsDto> Html { get; private set; }
    }
}
#pragma warning restore 1591
