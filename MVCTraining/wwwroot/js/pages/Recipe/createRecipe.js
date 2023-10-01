var addIngredientModal = new bootstrap.Modal(document.getElementById('add-ingredient-modal'));
var ingredientBody = document.getElementById('ingredient-body');
var ingredientText = document.getElementById('ingredientText');

document.getElementById('ingre-btn').addEventListener('click', function () {
    var rowCount = ingredientBody.getElementsByTagName('tr').length;
    //console.log(rowCount);
    const tr = document.createElement('tr');
    const noTd = document.createElement('td');
    const nameTd = document.createElement('td');
    const quantityTd = document.createElement('td');
    const unitTd = document.createElement('td');
    const actionTd = document.createElement('td');

/*    noTd.innerText = rowCount;*/
    nameTd.innerText = $('#ingredient-name').val();
    nameTd.className = 'name-td';
    quantityTd.innerText = $('#ingredient-quantity').val();
    unitTd.innerText = $('#ingredient-unit').val();
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

    for (var i = 0; i < rowCount; i++) {
        var ingreName = document.getElementsByClassName('name-td')[i].innerText;
        console.log('Ingredient name ==>' + ingreName);
    }
})
