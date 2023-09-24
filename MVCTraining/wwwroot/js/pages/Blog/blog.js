$(document).ready(function () {
    dataTable();
})
function dataTable() {
    memberTable = $('#blog_table').DataTable({
        responsive: true,
        deferRender: true,
        serverSide: true,
        autoWidth: false,
        stateSave: true,
        dataCollapse: false,
        ajax: {
            'url': '/Blog/GetAllBlog',
            'type': 'POST',
            'datatype': "json",
        },
        columns: [
            {
                data: 'blog_Title',
                name: 'blog_Title'
            },
            {
                data: 'blog_Author',
                name: 'blog_Author'
            },
            {
                data: 'blog_Content',
                name: 'blog_Content'
            },
            {
                data: 'blog_Id',
                className: "text-center",
                render: function (data) {
                    return '<a href="' + data + '">' +
                        '<i class="fa-solid fa-pen-to-square"></i></a>';
                }
            },
            {
                data: 'blog_Id',
                className: "text-center",
                render: function (data) {
                    return '<a href="' + data + '">' +
                        '<i class="fa-solid fa-trash-can"></i></a>';
                }
            },],
        columnDefs: [
            {
                targets: [3],
                searchable: false,
                orderable: false
            }],
        lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
    });
}