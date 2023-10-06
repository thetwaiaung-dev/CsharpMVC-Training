var addIngredientModal = new bootstrap.Modal(document.getElementById('add-ingredient-modal'));
var updateIngredientModal =new bootstrap.Modal(document.getElementById('update-ingredient-modal'));
var ingredientBody = document.getElementById('ingredient-body');
var ingredientText = document.getElementById('ingredientText');
var ingredientNameError = document.getElementById('ingredientNameError');
var ingredientQuantityError = document.getElementById('ingredientQuantityError');
var ingredientUnitError = document.getElementById('ingredientUnitError');
var inputIngredientName = document.getElementById('ingredient-name');
var inputIngredientQuantity = document.getElementById('ingredient-quantity');
var inputIngredientUnit = document.getElementById('ingredient-unit');
var ingredientNameErrorUpdate = document.getElementById('ingredientNameErrorUpdate');
var ingredientQuantityErrorUpdate = document.getElementById('ingredientQuantityErrorUpdate');
var ingredientUnitErrorUpdate = document.getElementById('ingredientUnitErrorUpdate');
var updateIngredientName = document.getElementById('update-ingredient-name');
var updateIngredientQuantity = document.getElementById('update-ingredient-quantity');
var updateIngredientUnit = document.getElementById('update-ingredient-unit');
var tableRowIndex = document.getElementById('tableRowIndex');
var updateIngredientId = document.getElementById('update-ingredient-id');
var updateIngredientForm = document.getElementById('update-ingredient-form');
var duplicateNameUpdate = document.getElementById('duplicateNameUpdate');
var duplicateNameAdd = document.getElementById('duplicateNameAdd');
var recipeAddBtn = document.getElementById('recipe-add-btn');
var recipeUpdateBtn = document.getElementById('recipe-update-btn');
var recipeForm = document.getElementById('recipe-form');
var recipeUpdateForm = document.getElementById('recipe-update-form');

/* dom elements for recipe input form */
var photoError = document.getElementById('photo-error');
var titleError = document.getElementById('title-error');
var authorError = document.getElementById('author-error');
var preparationTimeError = document.getElementById('preparationTime-error');
var cookingTimeError = document.getElementById('cookingTime-error');
var categoryError = document.getElementById('category-error');
var descError = document.getElementById('desc-error');
var instrutError = document.getElementById('instruct-error');

var inputTitle = document.getElementById('input-title');
var inputAuthor = document.getElementById('input-author');
var inputPreTime = document.getElementById('input-pre-time');
var inputCookTime = document.getElementById('input-cook-time');
var inputCategory = document.getElementById('input-category');
var inputDes = document.getElementById('input-des');
var inputInstruct = document.getElementById('input-instruct');

//preview image on update
function previewImage(event) {
    var reader = new FileReader();
    reader.onload = function () {
        var image = document.getElementById('imgSrc');
        image.src = reader.result;
    }
    reader.readAsDataURL(event.target.files[0]);
}

//set image value when update
if (window.photoUrl != null) {
    var imageUrl = window.photoUrl;

    var file = new File([imageUrl], imageUrl, { type: 'image/jpeg' });
    // Get the file input element
    var fileInput = document.getElementById('photoUpload');
    var fileList = new DataTransfer();
    fileList.items.add(file);

    fileInput.files = fileList.files;
}

//retreive ingredient data list from server side when have error
if (window.ingredientName != null) {
    for (var i = 0; i < window.ingredientName.length; i++) {
        createTable(window.ingredientName[i], window.ingredientQuantity[i], window.ingredientUnit[i]);
    }
}

//to add table row when click button
function addIngre() {
    if (!validate(inputIngredientName.value, ingredientNameError, inputIngredientName, 'Name is required.') ||
        !validate(inputIngredientQuantity.value, ingredientQuantityError, inputIngredientQuantity, 'Quantity is required.') ||
        !validate(inputIngredientUnit.value, ingredientUnitError, inputIngredientUnit,'Unit is required.')) {
        return false;
    }
    var rowCount = ingredientBody.getElementsByTagName('tr').length;
    var duplicateResult = false;
    for (var i = 0; i < rowCount; i++) {
        if (document.getElementsByClassName('name-td')[i].innerText == inputIngredientName.value) {
            duplicateResult = true;
        }
    }
    if (duplicateResult) {
        error(duplicateNameAdd, 'Name has already existed.');
        return false;
    } else if (!duplicateResult) {
        createTable($('#ingredient-name').val(), $('#ingredient-quantity').val(), $('#ingredient-unit').val());
        addIngredientModal.hide();
    } else { error(duplicateNameAdd, 'Something was wrong'); return false; }
    
}

//table data create method
function createTable(nameData,quantityData,unitData) {
    const tr = document.createElement('tr');
    const nameTd = document.createElement('td');
    const quantityTd = document.createElement('td');
    const unitTd = document.createElement('td');
    const actionTd = document.createElement('td');

    /*  add class name in table row for update ingredient*/
    tr.className = "update-row";

    /*    noTd.innerText = rowCount;*/
    nameTd.innerText = nameData;
    nameTd.className = 'name-td';
    quantityTd.innerText = quantityData;
    quantityTd.className = 'quantity-td';
    unitTd.innerText = unitData;
    unitTd.className = 'unit-td';
    

    const iconDelete = document.createElement('i');
    iconDelete.className = 'fa-solid fa-trash-can';
    iconDelete.addEventListener('click', function () {
        removeIngre(this); 
    });
    const iconUpdate = document.createElement('i');
    iconUpdate.className = 'fa-solid fa-pen-to-square';
    iconUpdate.addEventListener('click', function () {
        updateIngre(this);
    })

    //to create div class row for action
    const rowDiv = document.createElement('div');
    rowDiv.className = 'row';

    const colDiv1 = document.createElement('div');
    colDiv1.className = 'col';
    colDiv1.appendChild(iconUpdate);

    const colDiv2 = document.createElement('div');
    colDiv2.className = 'col';
    colDiv2.appendChild(iconDelete);

    rowDiv.appendChild(colDiv1);
    rowDiv.appendChild(colDiv2);

    actionTd.appendChild(rowDiv);

    /*    tr.appendChild(noTd);*/
    tr.appendChild(nameTd);
    tr.appendChild(quantityTd);
    tr.appendChild(unitTd);
    tr.appendChild(actionTd);
    ingredientBody.appendChild(tr);
}

/* to add ingredients and recipe */
if (recipeAddBtn != null) {
    recipeAddBtn.addEventListener('click', function (e) {
        e.preventDefault();
        var rowCount = ingredientBody.getElementsByTagName('tr').length;
        for (var i = 0; i < rowCount; i++) {
            var ingreName = document.getElementsByClassName('name-td')[i].innerText;
            var ingreQuantity = document.getElementsByClassName('quantity-td')[i].innerText;
            var ingreUnit = document.getElementsByClassName('unit-td')[i].innerText;

            createInputBoxForIngredient(recipeForm, ingreName, ingreQuantity, ingreUnit);
        }

        recipeForm.submit();
    })
}

/* to update ingredients and recipe */
if (recipeUpdateBtn != null) {
    function updateRecipe() {
        var photoUpload = document.getElementById('photoUpload');
        var validPhoto = ["jpeg", "png", "jpg"];
        if (photoUpload.value == '') {
            errorInput(photoUpload, photoError, 'Choose image.');
            return false;
        }
        if (photoUpload.value != '') {
            var imgValid = photoUpload.value.substring(photoUpload.value.lastIndexOf('.') + 1);
            var result = false;
            for (var i = 0; i < validPhoto.length; i++) {
                if (imgValid === validPhoto[i]) {
                    result = true;
                    break;
                }

            }
            if (result === false) {
                errorInput(photoUpload, photoError, 'Selected file in not an image.');
                return false;
            }
        }
        if (
            !validateRecipeTitle() || !validateRecipeAuthor() || !validateRecipePreTime() ||
            !validateRecipeCookTime() || !validateRecipeCategory() || !validateRecipeDes() || !validateRecipeInstruct()

        ) {
            return false;
        }
        var updateIngredientRows = document.querySelectorAll(".update-row");

        updateIngredientRows.forEach(row => {
            var name = row.querySelector('td:nth-child(1)').innerHTML;
            var quantity = row.querySelector('td:nth-child(2)').innerHTML;
            var unit = row.querySelector('td:nth-child(3)').innerHTML;

            createInputBoxForIngredient(recipeUpdateForm, name, quantity, unit);
        })
        return true;
    }
}

//error show method
function error(element, message) {
    element.innerHTML = message;
    setTimeout(() => {
        element.innerHTML = null;
    }, 2000);
}

function errorInput(inputElement,element, message) {
    element.innerHTML = message;
    inputElement.classList.remove('success');
    inputElement.classList.add('error');
}

function successInput(inputElement,element) {
    element.innerHTML = null;
    inputElement.classList.remove('error');
    inputElement.classList.add('success');
}

//to remove table row 
function removeIngre(element) {
    var row = element.closest('tr');
    if (row) {
        row.remove();
    }
}

//set data when click update icon button
function updateIngre(element, id) {

    var tableRow = element.closest('tr');
    var name = tableRow.querySelector('td:nth-child(1)').innerHTML;
    var quantity = tableRow.querySelector('td:nth-child(2)').innerHTML;
    var unit = tableRow.querySelector('td:nth-child(3)').innerHTML;

    updateIngredientName.value = name;
    updateIngredientQuantity.value = quantity;
    updateIngredientUnit.value = unit;
    tableRowIndex.value = tableRow.rowIndex;
    if (id != undefined) {
        updateIngredientId.value = id;
    } else updateIngredientId.value = 0;
    updateIngredientModal.show();
}

//to update data for ingredient in table row
function updateIngreBtn() {
    if (!validate(updateIngredientName.value, ingredientNameErrorUpdate, updateIngredientName,'Name is required.') ||
        !validate(updateIngredientQuantity.value, ingredientQuantityErrorUpdate, updateIngredientQuantity,'Quantity is required.') ||
        !validate(updateIngredientUnit.value, ingredientUnitErrorUpdate, updateIngredientUnit,'Unit is required.')) {
        return false;
    }
    var rowIndex = tableRowIndex.value;
    var rowCount = ingredientBody.getElementsByTagName('tr').length;
    var duplicateResult = false;
    for (var i = 0; i < rowCount; i++) {
        if (document.getElementsByClassName('name-td')[i].innerText == updateIngredientName.value && [rowIndex-1]!=i)
        {
            duplicateResult = true;
        }
    }
    if (duplicateResult) {
        error(duplicateNameUpdate, 'Name has already existed.');
        return false;
    }
    else if (!duplicateResult) {
        if (updateIngredientId.value > 0) {
            updateIngredientForm.submit();
        } else if (updateIngredientId.value == 0) {
            document.getElementsByClassName('name-td')[rowIndex - 1].innerText = updateIngredientName.value;
            document.getElementsByClassName('quantity-td')[rowIndex - 1].innerText = updateIngredientQuantity.value;
            document.getElementsByClassName('unit-td')[rowIndex - 1].innerText = updateIngredientUnit.value;

            updateIngredientModal.hide();
        }
    } else { error(duplicateNameUpdate, 'Something was wrong.'); return false; } 
}


/* to show error when key up for ingredient */
function validateIngreName() {
    validate(inputIngredientName.value, ingredientNameError, inputIngredientName,'Name is required.');
}

function validateIngreQuantity() {
    validate(inputIngredientQuantity.value, ingredientQuantityError, inputIngredientQuantity,'Quantity is required.');
}

function validateIngreUnit() {
    validate(inputIngredientUnit.value, ingredientUnitError, inputIngredientUnit,'Unit is required.');
}

//to show error when key up
function validateIngreNameUpdate() {
    validate(updateIngredientName.value, ingredientNameErrorUpdate, updateIngredientName, 'Name is required.');
}

function validateIngreQuantityUpdate() {
    validate(updateIngredientQuantity.value, ingredientQuantityErrorUpdate, updateIngredientQuantity,'Quantity is required.');
}

function validateIngreUnitUpdate() {
    validate(updateIngredientUnit.value, ingredientUnitErrorUpdate, updateIngredientUnit,'Unit is required.');
}

/* to show error for recipe */
function validateRecipeTitle() {
    if (!validate(inputTitle.value, titleError, inputTitle, 'Title is required.')) {
        return false;
    }
    return true;
}

function validateRecipeAuthor() {
    if (!validate(inputAuthor.value, authorError, inputAuthor, 'Author is required.')) {
        return false;
    }
    return true;
}

function validateRecipePreTime() {
    if (!validate(inputPreTime.value, preparationTimeError, inputPreTime, 'Preparation time is required.')) {
        return false;
    }
    return true;
}

function validateRecipeCookTime() {
    if (!validate(inputCookTime.value, cookingTimeError, inputCookTime, 'Cooking time is required.')) {
        return false;
    }
    return true;
}

function validateRecipeCategory() {
    if (!validate(inputCategory.value, categoryError, inputCategory, 'Category is required.')) {
        return false;
    }
    return true;
}

function validateRecipeDes() {
    if(!validate(inputDes.value, descError, inputDes, 'Description is required.')){
        return false;
    }
    return true;
}

function validateRecipeInstruct() {
    if (!validate(inputInstruct.value, instrutError, inputInstruct, 'Instruction is required.')) {
        return false;
    }
    return true;
}

//validate function
function validate(data, errorElement,inputElement,message) {
    if (data == null || data.trim() == "") {
        errorInput(inputElement, errorElement, message)
        return false;
    }
    successInput(inputElement, errorElement);
    return true;
}

/* create input box to send ingredient values */
function createInputBoxForIngredient(form, name, quantity, unit) {
    var inputName = document.createElement('input');
    var inputQuantity = document.createElement('input');
    var inputUnit = document.createElement('input');

    inputName.value = name;
    inputName.name = 'ingredientName';
    inputName.type = 'hidden';

    inputQuantity.value = quantity;
    inputQuantity.name = 'ingredientQuantity';
    inputQuantity.type = 'hidden';

    inputUnit.value = unit;
    inputUnit.name = 'ingredientUnit';
    inputUnit.type = 'hidden';

    form.appendChild(inputName);
    form.appendChild(inputQuantity);
    form.appendChild(inputUnit);
}