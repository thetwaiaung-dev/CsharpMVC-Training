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

//retreive ingredient data list from server side when have error
if (window.ingredientName != null) {
    for (var i = 0; i < window.ingredientName.length; i++) {
        createTable(window.ingredientName[i], window.ingredientQuantity[i], window.ingredientUnit[i]);
    }
}

//to add table row when click button
function addIngre() {
    if (!validateName(inputIngredientName.value, ingredientNameError) || !validateQuantity(inputIngredientQuantity.value, ingredientQuantityError) || !validateUnit(inputIngredientUnit.value, ingredientUnitError)) {
        return false;
    }
    createTable($('#ingredient-name').val(), $('#ingredient-quantity').val(), $('#ingredient-unit').val());
    addIngredientModal.hide();
}

//to show error when key up
function validateIngreName() {
    validateName(inputIngredientName.value, ingredientNameError);
}

function validateIngreQuantity() {
    validateQuantity(inputIngredientQuantity.value, ingredientQuantityError);
}

function validateIngreUnit() {
    validateUnit(inputIngredientUnit.value, ingredientUnitError);
}

//table data create method
function createTable(nameData,quantityData,unitData) {
    const tr = document.createElement('tr');
    const nameTd = document.createElement('td');
    const quantityTd = document.createElement('td');
    const unitTd = document.createElement('td');
    const actionTd = document.createElement('td');

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

document.getElementById('recipe-add-btn').addEventListener('click', function (e) {
    e.preventDefault();
    var rowCount = ingredientBody.getElementsByTagName('tr').length;
    var recipeForm = document.getElementById('recipe-form');
    for (var i = 0; i < rowCount; i++) {
        var ingreName = document.getElementsByClassName('name-td')[i].innerText;
        var ingreQuantity = document.getElementsByClassName('quantity-td')[i].innerText;
        var ingreUnit = document.getElementsByClassName('unit-td')[i].innerText;

        var inputName = document.createElement('input');
        var inputQuantity = document.createElement('input');
        var inputUnit = document.createElement('input');

        inputName.value = ingreName;
        inputName.name = 'ingredientName';
        inputName.type = 'hidden';

        inputQuantity.value = ingreQuantity;
        inputQuantity.name = 'ingredientQuantity';
        inputQuantity.type = 'hidden'; 

        inputUnit.value = ingreUnit;
        inputUnit.name = 'ingredientUnit';
        inputUnit.type = 'hidden';

        recipeForm.appendChild(inputName);
        recipeForm.appendChild(inputQuantity);
        recipeForm.appendChild(inputUnit);
        console.log('Ingredient name ==>' + ingreName);
    }

    recipeForm.submit();
})



//error show method
function error(element, message) {
    element.innerHTML = message;
    setTimeout(() => {
        element.innerHTML = '';
    }, 2000);
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
    console.log('Id =.>' + id);

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
    if (!validateName(updateIngredientName.value, ingredientNameErrorUpdate) || !validateQuantity(updateIngredientQuantity.value, ingredientQuantityErrorUpdate) || !validateUnit(updateIngredientUnit.value, ingredientUnitErrorUpdate)) {
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

//to show error when key up
function validateIngreNameUpdate() {
    validateName(updateIngredientName.value, ingredientNameErrorUpdate);
}

function validateIngreQuantityUpdate() {
    validateQuantity(updateIngredientQuantity.value, ingredientQuantityErrorUpdate);
}

function validateIngreUnitUpdate() {
    validateUnit(updateIngredientUnit.value, ingredientUnitErrorUpdate);
}

//validate ingredient name
function validateName(data,errorElement) {
    if (data == null || data.trim() == "") {
        error(errorElement, 'Name is required.')
        return false;
    }
    return true;
}

//validate ingredient quantity
function validateQuantity(data, errorElement) {
    if (data == null || data.trim() == "") {
        error(errorElement, 'Quantity is required.')
        return false;
    }
    return true;
}

//validate ingredient unit
function validateUnit(data, errorElement) {
    if (data == null || data.trim() == '') {
        error(errorElement, 'Unit is required.')
        return false;
    }
    return true;
}