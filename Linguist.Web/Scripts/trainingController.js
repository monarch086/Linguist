var mainDiv,
    word,
    transcription,
    currentPosition = 0,
    isTranslation = false,
    translationBtn,
    counter,
    resultsSaved = false,
    xDown = null,
    xUp = null;

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
    
    document.addEventListener('touchstart', handleTouchStart, false);
    document.addEventListener('touchmove', handleTouchMove, false);
    document.addEventListener('touchend', handleTouchEnd, false);
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

    if (currentPosition === words.length - 1) {
        saveTrainingResults();
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

function saveTrainingResults() {
    if (!resultsSaved) {
        jQuery.ajaxSettings.traditional = true;

        $.get("/Training/SaveTrainingResults",
            {
                wordsIds: words.map(function (word) {
                    return word.WordId;
                })
            });

        resultsSaved = true;
    }
}

function handleTouchStart(evt) {
    xDown = evt.touches[0].clientX;
};

function handleTouchMove(evt) {
    if (!xDown) {
        return;
    }
    xUp = evt.touches[0].clientX;
};

function handleTouchEnd(evt) {
    if (!xUp) {
        return;
    }

    var xDiff = xDown - xUp;

    if (Math.abs(xDiff) > 100) {
        if (xDiff > 0) {
            moveRight();
        } else {
            moveLeft();
        }
    }
    
    xUp = null;
    xDown = null;
}