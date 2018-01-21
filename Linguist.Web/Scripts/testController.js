var questionCard,
    questionWord,
    answerCard,
    answerWord,
    currentPosition = 0,
    totalCounter,
    rightCounter,
    wrongCounter,

    rightWords = [],
    wrongWords = [],

    isAnswerClosed = true;

window.onload = start;

function start() {

    questionCard = document.getElementById("question-card");
    questionWord = document.getElementById("question-word");
    answerCard = document.getElementById("answer-card");
    answerWord = document.getElementById("answer-word");

    totalCounter = document.getElementById("total-counter");
    rightCounter = document.getElementById("right-counter");
    wrongCounter = document.getElementById("wrong-counter");

    questionCard.onclick = pressRedButton;
    answerCard.onclick = showAnswer;

    loadWord();
    updateCounters();
}

function loadWord() {
    questionWord.innerHTML = words[currentPosition].OriginalWord;
    answerWord.innerHTML = "";
}

function updateCounters() {
    totalCounter.innerHTML = 'Все слова (' + (currentPosition + 1) + '/' + words.length + ')';
    rightCounter.innerHTML = 'Правильно: ' + rightWords.length;
    wrongCounter.innerHTML = 'Неправильно: ' + wrongWords.length;
}

function showAnswer() {
    if (isAnswerClosed) {
        answerWord.innerHTML = words[currentPosition].Translation;
        setRedGreenBackground();
        isAnswerClosed = false;
        return;
    } else {
        pressGreenButton();
    }
}

function setRedGreenBackground() {
    questionCard.style.background = '#ffb9b6';
    answerCard.style.background = '#aaefaa';
}

function resetBackground() {
    questionCard.style.background = '#ffc';
    answerCard.style.background = '#d2d2c1';
}

function pressRedButton() {
    if (!isAnswerClosed) {
        wrongWords.push(words[currentPosition].WordId);
        goToNextWord();
    }
}

function pressGreenButton() {
    if (!isAnswerClosed) {
        rightWords.push(words[currentPosition].WordId);
        goToNextWord();
    }
}

function goToNextWord() {
    if (currentPosition < words.length - 1) {
        currentPosition++;
        loadWord();
        updateCounters();
        resetBackground();
        isAnswerClosed = true;
    } else {
        endTest();
    }
}

function endTest() {
    updateCounters();
    questionCard.style.display = 'none';
    answerCard.style.display = 'none';
    saveTestResults();
}

function saveTestResults() {
    jQuery.ajaxSettings.traditional = true;

    $.get("/Test/SaveTestResults",
        {
            rightWords: rightWords,
            wrongWords: wrongWords
        });
}