var changePassBtn,
    changePass,
    savePassBtn,
    isChangePassShown = false;

window.onload = start;

function start() {
    changePassBtn = document.getElementById("changePassBtn");
    changePass = document.getElementById("changePass");
    savePassBtn = document.getElementById("savePassBtn");

    changePassBtn.onclick = displayChangePass;
    savePassBtn.onclick = savePass;
}

function displayChangePass() {
    isChangePassShown = !isChangePassShown;
    if (isChangePassShown) {
        changePass.style.display = 'block';
        return;
    }
    changePass.style.display = 'none';
}

function savePass() {
    var oldPass = document.getElementById("oldPass").value;
    var newPass = document.getElementById("newPass").value;

    $.get("/Options/SetNewPassword",
        {
            oldPassword: oldPass,
            newPassword: newPass
        },
        function (data) {
            if (data === "True") {
                alert("Новый пароль успешно сохранён");
                displayChangePass();
                return;
            }

            alert("Ошибка сохранения пароля");
        });
}