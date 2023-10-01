var errorMessage = document.getElementById('errorMessage');
var successMessage = document.getElementById('successMessage');

if (errorMessage != null) {
    if (errorMessage.innerHTML != null) {
        console.log('Error Message =>> ' + errorMessage)
        setTimeout(function () {
            errorMessage.style.display = 'none';
            errorMessage.innerHTML = null;
        }, 2000);
    }
}

if (successMessage != null) {
    if (successMessage.innerHTML != null) {
        setTimeout(function () {
            successMessage.style.display = 'none';
            successMessage.innerHTML = null;
        }, 2000);
    }
}


