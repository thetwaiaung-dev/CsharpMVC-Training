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

//retreive ingredient data list from controller when have error
if (window.ingredientName != null) {
    for (var i = 0; i < window.ingredientName.length; i++) {
        createTable(window.ingredientName[i], window.ingredientQuantity[i], window.ingredientUnit[i]);
    }
}

//to add table row when click button
function addIngre() {
    if (!validateIngreName() || !validateIngreQuantity() || !validateIngreUnit() ) {
        return false;
    }
    createTable($('#ingredient-name').val(), $('#ingredient-quantity').val(), $('#ingredient-unit').val());
    addIngredientModal.hide();
}

//validate ingredient name
function validateIngreName() {
    if (inputIngredientName.value == null || inputIngredientName.value.trim() == "") {
        error(ingredientNameError, 'Name is required.')
        return false;
    }
    return true;
}

//validate ingredient quantity
function validateIngreQuantity() {
    if (inputIngredientQuantity.value == null || inputIngredientQuantity.value.trim() == "") {
        error(ingredientQuantityError, 'Quantity is required.')
        return false;
    }
    return true;
}

//validate ingredient unit
function validateIngreUnit() {
    if (inputIngredientUnit.value == null || inputIngredientUnit.value.trim() == '') {
        error(ingredientUnitError, 'Unit is required.')
        return false;
    }
    return true;
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

function updateIngre(element) {
    var tableRow = element.closest('tr');
    var name = tableRow.querySelector('td:nth-child(1)').innerHTML;
    var quantity = tableRow.querySelector('td:nth-child(2)').innerHTML;
    var unit = tableRow.querySelector('td:nth-child(3)').innerHTML;

    document.getElementById('update-ingredient-name').value = name;
    document.getElementById('update-ingredient-quantity').value = quantity;
    document.getElementById('update-ingredient-unit').value = unit;
    updateIngredientModal.show();
}
        