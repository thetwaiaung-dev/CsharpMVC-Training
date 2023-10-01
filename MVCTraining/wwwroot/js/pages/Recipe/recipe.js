$(function () {
    const table = $('table#recipe_table').DataTable({
        responsive: true,
        deferRender: true,
        serverSide: true,
        autoWidth: false,
        stateSave: true,
        dataCollapse: false,
        ajax: {
            'url': '/Recipe/GetAll',
            'type': 'POST',
           'datatype': 'json',
        },
        columns: [
            {
                data: 'title',
                name: 'title',
                className: 'align-middle'
            },
            {
                data: 'author',
                name: 'author',
                className: 'align-middle'
            },
            {
                data: 'category',
                className: 'align-middle'
            },
            {
                data: 'description',
                className: 'align-middle'
            },
            {
                data: 'instruction',
                className: 'align-middle'
            },
            {
                data: 'preparationTime',
                className: 'align-middle'
            },
            {
                data: 'cookingTime',
                className: 'align-middle'
            },
            {
                data: 'createdDate',
                render: function (data) {
                    const date =data.substring(0,10);
                    return date;
                },
                className: 'align-middle'
            },
            {
                data: 'dishPhoto',
                render: function (data) {
                    return `<img style="width:100px;height:100px" class="img-thumbnail" src="${data}" /> `;
                },
                className: 'align-middle'
            },
            {
                data: 'title',
                className: "text-center",
                render: function (data) {
                    return `<div class="container"><a class="row mb-5" href="/Blog/UpdateBlogView?id=${data}">
                            <i class="fa-solid fa-pen-to-square"></i></a>
                            <a class="row" href="#" class="delete-btn" onclick="deleteBlog(${data})">
                                <i class="fa-solid fa-trash-can"></i>
                            </a></div>`;
                }
            }
            
        ],
        initComplete: function (settings, json) {
            console.log('Received JSON data:', json);
        },
        columnDefs: [
            {
                targets: [2],
                searchable: false,
                orderable: false
            },
            {
                targets: [3],
                searchable: false,
                orderable: false
            },
            {
                targets: [4],
                searchable: false,
                orderable: false
            },
            {
                targets: [5],
                searchable: false,
                orderable: false
            },
            {
                targets: [6],
                searchable: false,
                orderable: false
            },
            {
                targets: [7],
                searchable: false,
                orderable: false
            },
            {
                targets: [8],
                searchable: false,
                orderable: false
            },
            {
                targets: [9],
                searchable: false,
                orderable: false
            }
        ],
        lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
    })
})