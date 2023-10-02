var errorMessage = document.getElementById('errorMessage');
var successMessage = document.getElementById('successMessage');

if (errorMessage != null) {
    if (errorMessage.innerHTML != null) {
        setTimeout(function () {
            errorMessage.style.display = 'none';
            errorMessage.innerHTML = null;
        }, 2000);
    }
}

if (successMessage != null) {
    if (successMessage.innerHTML != null) {
        showMessageAlert(successMessage);
    }
}

//to show message 
function showMessageAlert(element) {
    setTimeout(() => {
        element.style.display = 'none';
        element.innerHTML = null;
    }, 2000);
}


