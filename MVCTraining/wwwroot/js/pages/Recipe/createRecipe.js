var addIngredientModal = new bootstrap.Modal(document.getElementById('add-ingredient-modal'));
var ingredientBody = document.getElementById('ingredient-body');
var ingredientText = document.getElementById('ingredientText');

//retreive ingredient data list from controller when have error
if (window.ingredientName != null) {
    for (var i = 0; i < window.ingredientName.length; i++) {
        createTable(window.ingredientName[i], window.ingredientQuantity[i], window.ingredientUnit[i]);
    }
}
document.getElementById('ingre-btn').addEventListener('click', function () {
    createTable($('#ingredient-name').val(), $('#ingredient-quantity').val(), $('#ingredient-unit').val());
})

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
    actionTd.className = 'text-end';

    const icon = document.createElement('i');
    icon.className = 'fa-solid fa-trash ms-3 ';
    actionTd.appendChild(icon);

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
