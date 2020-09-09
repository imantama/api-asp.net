var table = null;
var arrDepart = [];

$(document).ready(function () {
    table = $("#division").DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "/divisions/LoadDiv",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            { "data": null,
               "width": "50px",
               "sClass": "text-center",
               "orderable": false,},
            { "data": "name" },
            { "data": "department_name" },
            {
                "sortable": false,
                "render": function (data, type, row) {
                    //console.log(row);
                    $('[data-toggle="tooltip"]').tooltip();
                    return '<button class="btn btn-outline-warning btn-circle" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + row.id + ')" ><i class="fa fa-lg fa-edit"></i></button>'
                        + '&nbsp;'
                        + '<button class="btn btn-outline-danger btn-circle" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + row.id + ')" ><i class="fa fa-lg fa-times"></i></button>'
                }
            }
        ]
    });
    table.on('order.dtable search.dtable', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});

function ClearScreen() {
    $('#Id').val('');
    $('#Name').val('');
    $('#update').hide();
    $('#add').show();
}

function LoadDepart(element) {
    //debugger;
    if (arrDepart.length === 0) {
        $.ajax({
            type: "Get",
            url: "/departments/LoadDepart",
            success: function (data) {
                arrDepart = data;
                renderDepart(element);
            }
        });
    }
    else {
        renderDepart(element);
    }
}

function renderDepart(element) {
    //debugger;
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Department').hide());
    $.each(arrDepart, function (i, val) {
        $option.append($('<option/>').val(val.id).text(val.name))
    });
}

LoadDepart($('#DepartOption'))

function GetById(id) {
    debugger;
    $.ajax({
        url: "/divisions/GetById/",
        data: { id: id }
    }).then((result) => {
        debugger;
        $('#Id').val(result.id);
        $('#Name').val(result.name);
        $('#DepartOption').val(result.department_id)
        $('#add').hide();
        $('#update').show();
        $('#myModal').modal('show');
    })
}

function Save() {
    //debugger;
    var Div = new Object();
    Div.id = 0;
    Div.name = $('#Name').val();
    Div.department_id = $('#DepartOption').val();
    $.ajax({
        type: 'POST',
        url: "/divisions/InsertOrUpdate/",
        cache: false,
        dataType: "JSON",
        data: Div
    }).then((result) => {
        if (result.StatusCode == 200) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Data inserted Successfully',
                showConfirmButton: false,
                timer: 1500,
            })
            table.ajax.reload(null, false);
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            ClearScreen();
        }
    })
}

function Update() {
    //debugger;
    var Div = new Object();
    Div.id = $('#Id').val();
    Div.name = $('#Name').val();
    Div.department_id = $('#DepartOption').val();
    $.ajax({
        type: 'POST',
        url: "/divisions/InsertOrUpdate/",
        cache: false,
        dataType: "JSON",
        data: Div
    }).then((result) => {
        //debugger;
        if (result.StatusCode == 200) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Data Updated Successfully',
                showConfirmButton: false,
                timer: 1500,
            });
            table.ajax.reload(null, false);
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            ClearScreen();
        }
    })
}

function Delete(id) {
    //debugger;
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!',
    }).then((resultSwal) => {
        if (resultSwal.value) {
            //debugger;
            $.ajax({
                url: "/divisions/Delete/",
                data: { id: id }
            }).then((result) => {
                //debugger;
                if (result.StatusCode == 200) {
                    //debugger;
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Delete Successfully',
                        showConfirmButton: false,
                        timer: 1500,
                    });
                    table.ajax.reload(null, false);
                } else {
                    Swal.fire('Error', 'Failed to Delete', 'error');
                    ClearScreen();
                }
            })
        };
    });
}