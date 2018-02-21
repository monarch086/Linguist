var mainDiv,
    word,
    transcription,
    currentPosition = 0,
    isTranslation = false,
    translationBtn,
    counter;

window.onload = start;
function start() {

    mainDiv = document.getElementById("main");
    word = document.getElementById("word");
    transcription = document.getElementById("transcription");
    translationBtn = document.getElementById("translationBtn");
    counter = document.getElementById("counter");

    mainDiv.onclick = clickHandler;
    loadWord();
    updateCounter();
}

function clickHandler(event) {
    
    var pressedElement = event.srcElement;

    if (pressedElement === document.getElementById("left-block"))
        moveLeft();

    else if (pressedElement === document.getElementById("right-block"))
        moveRight();

    else if (pressedElement === document.getElementById("translationBtn") ||
        pressedElement === document.getElementById("card") || 
        pressedElement === word || 
        pressedElement === transcription)
        showTranslation();
}

function moveLeft() {
    if (currentPosition > 0) {
        currentPosition--;
        loadWord();
        updateCounter();
    }
}

function moveRight() {
    if (currentPosition < words.length - 1) {
        currentPosition++;
        loadWord();
        updateCounter();
    }
}

function showTranslation()
{
    if (!isTranslation) {
        word.innerHTML = words[currentPosition].Translation;
        transcription.style.display = 'none';
        translationBtn.innerHTML = 'Спрятать перевод';
        isTranslation = true;
    } else {
        word.innerHTML = words[currentPosition].OriginalWord;
        if (transcription.innerHTML) {
            transcription.style.display = 'table-row';
        }
        translationBtn.innerHTML = 'Показать перевод';
        isTranslation = false;
    }
}

function loadWord() {
    if (!isTranslation) {
        word.innerHTML = words[currentPosition].OriginalWord;

        if (words[currentPosition].Transcription) {
            transcription.style.display = 'table-row';
            transcription.innerHTML = words[currentPosition].Transcription;
        } else {
            transcription.style.display = 'none';
            transcription.innerHTML = null;
        }
        
    } else {
        word.innerHTML = words[currentPosition].Translation;
    }
}

function updateCounter() {
    counter.innerHTML = 'Все слова (' + (currentPosition + 1) + '/' + words.length + ')';
}