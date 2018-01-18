var mainDiv,
    word,
    currentPosition = 0,
    isTranslation = false,
    translationBtn,
    counter;

window.onload = start;
function start() {

    mainDiv = document.getElementById("main");
    word = document.getElementById("word");
    translationBtn = document.getElementById("translationBtn");
    counter = document.getElementById("counter");

    mainDiv.onclick = clickHandler;
    loadWord();
    updateCounter();
}

function clickHandler(event) {
    
    var pressedElement = event.srcElement;

    if (pressedElement === document.getElementById("left"))
        moveLeft();

    else if (pressedElement === document.getElementById("right"))
        moveRight();

    else if (pressedElement === document.getElementById("translationBtn") ||
        pressedElement === document.getElementById("card"))
        showTranslation();
}

function moveLeft() {
    if (currentPosition > 0) {
        currentPosition--;
        loadWord();
        updateCounter();
        resetTranslations();
    }
}

function moveRight() {
    if (currentPosition < words.length - 1) {
        currentPosition++;
        loadWord();
        updateCounter();
        resetTranslations();
    }
}

function showTranslation()
{
    if (!isTranslation) {
        word.innerHTML = words[currentPosition].Translation;
        translationBtn.innerHTML = 'Спрятать перевод';
        isTranslation = true;
    } else {
        loadWord();
        translationBtn.innerHTML = 'Показать перевод';
        isTranslation = false;
    }
}

function loadWord() {
    word.innerHTML = words[currentPosition].OriginalWord;
}

function updateCounter() {
    counter.innerHTML = 'Все слова (' + (currentPosition + 1) + '/' + words.length + ')';
}

function resetTranslations() {
    translationBtn.innerHTML = 'Показать перевод';
    isTranslation = false;
}