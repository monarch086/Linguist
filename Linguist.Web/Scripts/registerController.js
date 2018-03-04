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
        setRedMessage("Введите логин");
        return;
    }

    if (!validateLogin(login)) {
        setRedMessage("Введите логин в формате: example@domain.com");
        return;
    }

    $.get("/Account/DoesLoginExist",
        {
            login: login
        },
        function (data) {
            if (data === "True") {
                setRedMessage("Данный логин занят, используйте другой");
                return;
            }

            setGreenMessage("Данный логин свободен");
        });
}

function clearMessage() {
    registerWarning.innerHTML = "";
}

function setRedMessage(text) {
    registerWarning.innerHTML = text;
    registerWarning.style.color = "red";
    submitBtn.disabled = true;
}

function setGreenMessage(text) {
    registerWarning.innerHTML = text;
    registerWarning.style.color = "green";
    submitBtn.disabled = false;
}

function validateLogin(login) {
    if (login.length < 5) {
        return false;
    }

    if (!login.includes("@")) {
        return false;
    }

    if (!login.includes(".")) {
        return false;
    }

    return true;
}