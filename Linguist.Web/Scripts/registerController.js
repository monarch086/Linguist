var registerWarning,
    submitBtn;

window.onload = start;

function start() {
    registerWarning = document.getElementById("register-warning");
    submitBtn = document.getElementById("submit-btn");

    $("#login-input").on("keyup", function () {
        checkLogin();
    });
}

function checkLogin() {
    var login = document.getElementById("login-input").value;
    clearMessage();

    if (login === "") {
        setRedWarningMessage("Введите логин");
        return;
    }

    $.get("/Account/DoesLoginExist",
        {
            login: login
        },
        function (data) {
            if (data === "True") {
                setRedWarningMessage("Данный логин занят, используйте другой");
                return;
            }

            setGreenWarningMessage("Данный логин свободен");
        });
}

function clearMessage() {
    registerWarning.innerHTML = "";
}

function setRedWarningMessage(text) {
    registerWarning.innerHTML = text;
    registerWarning.style.color = "red";
    submitBtn.disabled = true;
}

function setGreenWarningMessage(text) {
    registerWarning.innerHTML = text;
    registerWarning.style.color = "green";
    submitBtn.disabled = false;
}