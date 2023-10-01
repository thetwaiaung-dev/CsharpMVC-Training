var deleteAtag = document.getElementById('deleteAtag');
var deleteBlogModal = new bootstrap.Modal(
    document.getElementById("delete-blog-modal")
);

function deleteBlog(id) {
    deleteAtag.setAttribute('href', '/Blog/DeleteBlog?id=' + id);
    deleteBlogModal.show();
}

