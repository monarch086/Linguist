var addedCategories = [],
    addBtn;

window.onload = start;

function start() {
    addBtn = document.getElementById("addCategory");
    addBtn.onclick = addAction;

    $("form").submit(function(e) {
        e.preventDefault();
    });

    setCurrentCategories();
}

function setCurrentCategories() {
    var categoryLabels = $('.label-info');

    Array.prototype.forEach.call(categoryLabels, child => {
        var category = { name: child.innerHTML, id: child.id };
        addedCategories.push(category);
    });
}

function addAction() {
    var dropdown = document.getElementById("CategoryId");

    var name = dropdown.options[dropdown.selectedIndex].text;
    var id = dropdown.value;

    if (id !== "" && !duplicatesCheck(name)) {
        appendHtmlCategory(name, id);
        var category = {name:name, id:id};
        addedCategories.push(category);
    }
}

function duplicatesCheck(name) {
    var found = false;
    for (var i = 0; i < addedCategories.length; i++) {
        if (addedCategories[i].name === name) {
            found = true;
            alert("Словарь \"" + name + "\"уже добавлен в список");
            break;
        }
    }
    return found;
}

function removeAction(id) {
    $("#" + id).remove();
    $("#" + id).remove();
    removeCategoryFromArray(id);
}

function appendHtmlCategory(name, id) {
    var html2Add = "<h3><span class='label label-info' id='" + id + "'>" +
        name +
        "</span>" +
        "<div style='display:inline; margin-left:8px;' id='" + id + "' onClick='removeAction(this.id)'>" +
        "<img src='/Resources/Remove-icon16px.png'/>" +
        "</div>" +
        "</h3>";
    $('#current-categories').append(html2Add);
}

function removeCategoryFromArray(id) {
    for (var i = 0; i < addedCategories.length; i++) {
        if (addedCategories[i].id === id) {
            addedCategories.splice(i, 1);
            break;
        }
    }
}

function saveAction(wordId) {
    var categoryIds = addedCategories.map(function (category) {
        return category.id;
    });

    console.log(categoryIds);

    jQuery.ajaxSettings.traditional = true;

    $.get("/Word/UpdateWordCategories",
        {
            wordId: wordId,
            categoryIds: categoryIds
        },
        function (data, status) {
            $("form").unbind('submit').submit();
        });
}