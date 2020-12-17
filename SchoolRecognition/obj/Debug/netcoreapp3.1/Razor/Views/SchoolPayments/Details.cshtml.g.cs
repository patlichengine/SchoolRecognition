#pragma checksum "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "852f42c817b39722d75ae768fbc8b764af1f64de"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SchoolPayments_Details), @"mvc.1.0.view", @"/Views/SchoolPayments/Details.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"852f42c817b39722d75ae768fbc8b764af1f64de", @"/Views/SchoolPayments/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99db7b73c51d84dd15942319281775a6f70cf22f", @"/Views/_ViewImports.cshtml")]
    public class Views_SchoolPayments_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SchoolPaymentsViewDto>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-toggle", new global::Microsoft.AspNetCore.Html.HtmlString("link"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "SchoolPayments", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Schools", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/linkClick.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
  
    string paymentReceiptImage = null;
    if (Model.PaymentReceiptImage != null && Model.PaymentReceiptImage.Length > 0)
    {
        var base64 = Convert.ToBase64String(Model.PaymentReceiptImage);
        paymentReceiptImage = $"data:image/jpg;base64,{base64}";
    }
    ViewData["Title"] = $"[{Model.PaymentReceiptNo}] Payment Details";
    //Layout = "~/Views/Shared/_LayoutAdmin.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<!-- Content Header (Page header) -->\r\n<section class=\"content-header\">\r\n    <div class=\"container-fluid\">\r\n        <div class=\"row mb-2\">\r\n            <div class=\"col-sm-6\">\r\n                <h1>");
#nullable restore
#line 19 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
               Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n            </div>\r\n            <div class=\"col-sm-6\">\r\n                <ol class=\"breadcrumb float-sm-right\">\r\n                    <li class=\"breadcrumb-item\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "852f42c817b39722d75ae768fbc8b764af1f64de7495", async() => {
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
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</li>\r\n                    <li class=\"breadcrumb-item\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "852f42c817b39722d75ae768fbc8b764af1f64de9197", async() => {
                WriteLiteral("Manage Payments");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</li>\r\n                    <li class=\"breadcrumb-item active\">");
#nullable restore
#line 25 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
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

<section class=""content"">
    <div class=""container-fluid"">

        
        <div class=""row"">

            <div class=""col-12"">

                <!-- Main content -->
                <div class=""invoice p-3 mb-3"">
                    <!-- this row will not appear when printing -->
                    <div class=""row no-print mb-3"">
                        <div class=""col-12 clearfix"">
                            <button id=""printPage"" class=""btn btn-default btn-lg float-right"">
                                <i class=""fas fa-print""></i> Print
                            </button>

                        </div>
                    </div>
                    <!-- title row -->
                    <div class=""row"">
                        <div class=""col-12"">
                            <h4>
                                ");
#nullable restore
#line 55 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                           Write(Model.RecognitionTypeName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <span style=\"font-weight: 300\">Payment Statement</span>\r\n                                <small class=\"float-right mt-3\">\r\n                                    <b>RECEIPT NO.</b> ");
#nullable restore
#line 57 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                                                  Write(Model.PaymentReceiptNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br>\r\n                                    <span class=\"mt-2\" style=\"font-size: 18px\"><b>Date:</b> ");
#nullable restore
#line 58 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                                                                                        Write(Model.DateCreated != null ? Model.DateCreated.Value.ToString("dd-MMMM-yyyy") : "NOT STATED");

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span>

                                </small>
                            </h4>
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- info row -->
                    <div class=""row invoice-info mt-5"">
                        <div class=""col-sm-8 invoice-col"">
                            Made For:
                            <address>
                                <strong class=""lead"" style=""font-weight: bolder"">");
#nullable restore
#line 70 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                                                                            Write(Model.SchoolName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n                                <br />\r\n                                ");
#nullable restore
#line 72 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                           Write(Model.SchoolAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br>\r\n                                Phone: ");
#nullable restore
#line 73 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                                  Write(Model.SchoolPhoneNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br>\r\n                                Email: ");
#nullable restore
#line 74 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                                  Write(Model.SchoolEmailAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </address>
                        </div>
                        <!-- /.col -->
                        <div class=""col-sm-4 invoice-col"">
                            Captured By:
                            <address>
                                <strong class=""lead"" style=""font-weight: bolder"">");
#nullable restore
#line 81 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                                                                             Write(!String.IsNullOrWhiteSpace(Model.CreatedByUser) ? Model.CreatedByUser : "NOT STATED");

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong><br>\r\n");
            WriteLiteral(@"                            </address>
                        </div>
                    </div>
                    <!-- /.row -->

                    <hr />
                    <div class=""row"">
                        <!-- /.col -->
                        <div class=""col-12"">
                            <p class=""lead"">RECEIPT NUMBER - <b>");
#nullable restore
#line 95 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                                                           Write(Model.PaymentReceiptNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></p>\r\n                            <hr />\r\n                            <p class=\"lead\">PIN ISSUED - <b>");
#nullable restore
#line 97 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                                                       Write(Model.PinSerialNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b></p>
                            <div class=""table-responsive"">
                                <table class=""table"">
                                    <tbody>
                                        <tr>
                                            <th style=""width:30%"">Recognition Type</th>
                                            <td>");
#nullable restore
#line 103 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                                           Write(Model.RecognitionTypeName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
                                        </tr>
                                        <tr>
                                            <th style=""width:30%"">School</th>
                                            <td>
                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "852f42c817b39722d75ae768fbc8b764af1f64de18457", async() => {
#nullable restore
#line 108 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                                                                                                                                                          Write(Model.SchoolName);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 108 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                                                                                                               WriteLiteral(Model.SchoolId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                            </td>
                                        </tr>
                                        <tr>
                                            <th style=""width:30%"">School Category</th>
                                            <td>
                                                ");
#nullable restore
#line 114 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                                           Write(Model.SchoolCategoryName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>Amount Paid</th>
                                            <td>");
#nullable restore
#line 119 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                                            Write(Model.AmountPaid != null ? Model.AmountPaid.Value.ToString("#,##0") : "0.00");

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        </tr>\r\n                                        <tr>\r\n                                            <th>Date</th>\r\n                                            <td>");
#nullable restore
#line 123 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
                                            Write(Model.DateCreated != null ? Model.DateCreated.Value.ToString("dd-MMMM-yyyy") : "NOT STATED");

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                        <!-- /.col -->
                    </div>
                    <!-- /.row -->
                    <!-- /.row -->

                    <div class=""row"">
                        <!-- /.col -->
                        <div class=""col-12 border"">
                            <div class=""d-flex justify-content-center"">
                                <img");
            BeginWriteAttribute("src", " src=\"", 6843, "\"", 6869, 1);
#nullable restore
#line 139 "C:\Users\15N\Desktop\henry\WAEC Program\SchoolRecognition Files\SR New\remote\SchoolRecognition\Views\SchoolPayments\Details.cshtml"
WriteAttributeValue("", 6849, paymentReceiptImage, 6849, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""img-fluid"" alt=""Receipt"">
                            </div>
                        </div>

                        <!-- /.col -->
                    </div>
                    <!-- /.row -->
                </div>
                <!-- /.invoice -->
            </div><!-- /.col -->
        </div><!-- /.row -->
    </div><!-- /.container-fluid -->
</section>


<script>
    $('#printPage').click(function(){
           window.print();
           return false;
});
</script>
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "852f42c817b39722d75ae768fbc8b764af1f64de25010", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SchoolPaymentsViewDto> Html { get; private set; }
    }
}
#pragma warning restore 1591
