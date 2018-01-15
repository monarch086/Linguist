var addedCategories,
    removedCategories,
    addBtn,
    removeBtn;

window.onload = start;

function start() {

    addBtn = document.getElementById("addCategory");
    removeBtn = document.getElementById("removeBtn");

    addBtn.onclick = addAction;
    //removeBtn.onclick = removeAction;
    //$(".removeCategory").click(removeAction);
}

function addAction() {
    alert("Added new category");
}

function removeAction() {
    alert("Removed category: ");
}