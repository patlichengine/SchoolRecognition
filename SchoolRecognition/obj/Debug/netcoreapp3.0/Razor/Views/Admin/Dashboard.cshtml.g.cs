#pragma checksum "C:\Users\WAEC\Desktop\ApplicationFolder\SchoolRecognition\SchoolRecognition\Views\Admin\Dashboard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6fc9590b7034388934b9a198676917d79879202d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Dashboard), @"mvc.1.0.view", @"/Views/Admin/Dashboard.cshtml")]
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
#line 1 "C:\Users\WAEC\Desktop\ApplicationFolder\SchoolRecognition\SchoolRecognition\Views\_ViewImports.cshtml"
using SchoolRecognition;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\WAEC\Desktop\ApplicationFolder\SchoolRecognition\SchoolRecognition\Views\_ViewImports.cshtml"
using SchoolRecognition.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6fc9590b7034388934b9a198676917d79879202d", @"/Views/Admin/Dashboard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"57d0266bbca735634eabe717bec472dc819d6d99", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Dashboard : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\WAEC\Desktop\ApplicationFolder\SchoolRecognition\SchoolRecognition\Views\Admin\Dashboard.cshtml"
  
    ViewData["Title"] = "Dashboard";
    Layout = "~/Views/Shared/_LayoutAdmin.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<!-- Content Header (Page header) -->\r\n<section class=\"content-header\">\r\n    <div class=\"container-fluid\">\r\n        <div class=\"row mb-2\">\r\n            <div class=\"col-sm-6\">\r\n                <h1>");
#nullable restore
#line 13 "C:\Users\WAEC\Desktop\ApplicationFolder\SchoolRecognition\SchoolRecognition\Views\Admin\Dashboard.cshtml"
               Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n            </div>\r\n            <div class=\"col-sm-6\">\r\n                <ol class=\"breadcrumb float-sm-right\">\r\n");
            WriteLiteral("                    <li class=\"breadcrumb-item active\">");
#nullable restore
#line 18 "C:\Users\WAEC\Desktop\ApplicationFolder\SchoolRecognition\SchoolRecognition\Views\Admin\Dashboard.cshtml"
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
        <div class=""col-lg-3 col-6"">
            <!-- small box -->
            <div class=""small-box bg-gradient-info text-white-50"">
                <div class=""inner"">
                    <h3>150</h3>

                    <p>New Orders</p>
                </div>
                <div class=""icon"">
                    <i class=""ion ion-bag""></i>
                </div>
                <a href=""#"" class=""small-box-footer"">More info <i class=""fas fa-arrow-circle-right""></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class=""col-lg-3 col-6"">
            <!-- small box -->
            <div class=""small-box bg-gray text-white-50"">
                <div class=""inner"">
                    <h3>53<sup style=""font-size: 20px"">%</sup></h3>

                    <p>Bounce Rate</p>
            WriteLiteral(@"
                </div>
                <div class=""icon"">
                    <i class=""ion ion-stats-bars""></i>
                </div>
                <a href=""#"" class=""small-box-footer"">More info <i class=""fas fa-arrow-circle-right""></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class=""col-lg-3 col-6"">
            <!-- small box -->
            <div class=""small-box bg-warning"">
                <div class=""inner"">
                    <h3>44</h3>

                    <p>User Registrations</p>
                </div>
                <div class=""icon"">
                    <i class=""ion ion-person-add""></i>
                </div>
                <a href=""#"" class=""small-box-footer"">More info <i class=""fas fa-arrow-circle-right""></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class=""col-lg-3 col-6"">
            <!-- small box -->
            <div class=""small-box bg-navy text-white"">
                <div class=""inner"">
   ");
            WriteLiteral(@"                 <h3>65</h3>

                    <p>Unique Visitors</p>
                </div>
                <div class=""icon"">
                    <i class=""ion ion-pie-graph text-white-50""></i>
                </div>
                <a href=""#"" class=""small-box-footer"">More info <i class=""fas fa-arrow-circle-right""></i></a>
            </div>
        </div>
        <!-- ./col -->
    </div>

    <!-- Default box -->
    <div class=""card"">
        <div class=""card-header"">
            <h3 class=""card-title"">Title</h3>
        </div>
        <div class=""card-body"">
            Start creating your amazing application!
        </div>
        <!-- /.card-body -->
        <div class=""card-footer"">
            Footer
        </div>
        <!-- /.card-footer-->
    </div>
    <!-- /.card -->



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