var loginWarning,
    resetBtn,
    submitBtn;

window.onload = start;

function start() {
    loginWarning = document.getElementById("login-warning");
    resetBtn = document.getElementById("reset-btn");
    submitBtn = document.getElementById("submit-btn");

    resetBtn.onclick = resetHandler;
    submitBtn.onclick = submitHandler;

    $("form").submit(function (e) {
        e.preventDefault();
    });

    $("#login-input").on("keyup", function () {
        clearMessage();
    });
}

var resetHandler = function() {
    clearMessage();
    var login = document.getElementById("login-input").value;
    validateLogin(login, resetAction);
}

var resetAction = function() {
    var login = document.getElementById("login-input").value;
    $.get("/Options/SendResetMail",
        {
            login: login
        },
        function () {
            alert("На указанный вами адрес (логин) отправлено письмо со ссылкой для смены пароля");
        });
}

var submitHandler = function() {
    clearMessage();
    var login = document.getElementById("login-input").value;
    validateLogin(login, submitAction);
}

var submitAction = function() {
    $("form").unbind('submit').submit();
}

var clearMessage = function() {
    loginWarning.innerHTML = "";
}

var setRedMessage = function(text) {
    loginWarning.innerHTML = text;
    loginWarning.style.color = "red";
}

var checkLoginForExistence = function (login, successCallback) {
    $.get("/Account/DoesLoginExist",
        {
            login: login
        },
        function (data) {
            if (data === "True") {
                successCallback();
                return;
            }

            setRedMessage("Введённый логин не найден");
        });
}

var validateLogin = function (login, successCallback) {
    if (login === "") {
        setRedMessage("Введите логин");
        return;
    }

    checkLoginForExistence(login, successCallback);
}