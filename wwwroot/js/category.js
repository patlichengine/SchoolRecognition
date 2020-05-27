var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/SchoolCategories/Get",
            "dataType":"json"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "code", "width": "15%" },
           
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/SchoolCategories/Edit/${data}" class="btn btn-success" style="cursor:pointer">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a  onclick=Delete("/SchoolCategories/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="fas fa-trash-alt text-white"></i>
                            </a>
                        </div>

                    `;
                }, "width": "40%"
            }

        ]

    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "Delete",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}