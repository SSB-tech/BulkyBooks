var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({

        ajax: {
            url: "/Admin/Product/GetAll",
            dataSrc: "sushant"
        },
        columns: [
            { "data": "title" },
            { "data": "isbn" },
            { "data": "price" },
            { "data": "author" },
            //{ "data": "category.name" }
            { "data": "category.name" },
            {
                data: "id",
                render: function (data) {
                    return `
                     <a href="/Admin/Product/Upsert?id=${data}"><i class="bi bi-pencil-square"></i></a>&nbsp;&nbsp;&nbsp;
                    <a onClick=Delete('/Admin/Product/Delete?id=${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i>Delete</a>
                    `
                }
            }
        ],
        autoWidth: true
    });
}
    function Delete (url){
        Swal.fire({
            title: "Do you want to save the changes?",
            showDenyButton: true,
            showCancelButton: true,
            confirmButtonText: "Delete",
            denyButtonText: `Don't Delete`
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $.ajax({
                    url: url,
                    type: 'DELETE',
                    success: function (res) {
                        if (res.success) {
                            dataTable.ajax.reload();
                            toastr.success(res.message);
                        }
                        else {
                            toastr.error(res.message);         
                        }
                    }

                })
            } else if (result.isDenied) {
                Swal.fire("Changes are not saved", "", "info");
            }
        });
    
}


