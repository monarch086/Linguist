var wordsTable,
    pager,
    word,
    isSearchResultsPager = false;

window.onload = start;

function start() {

    wordsTable = document.getElementById("wordsTable");

    $("#searchInput").on("keyup", function () {
        word = $(this).val().toLowerCase();

        updateWords(word, 1);

        isSearchResultsPager = true;

        updatePager(word, 1);
    });

    $("#pager a").on("click", pagerHandler);
}

function updateWords(word, page) {
    $.get("/Home/SearchWords",
        {
            word: word,
            page: page
        },
        function (data) {
            $("#wordsTable").html(data);
        });
}

function updatePager(word, page) {
    $.get("/Home/SearchPager",
        {
            word: word,
            page: page
        },
        function (data) {
            $("#pager").html(data);
            $("#pager a").on("click", pagerHandler);
        });
}

function pagerHandler() {
    var page = this.innerHTML;

    if (isSearchResultsPager) {
        updateWords(word, page);
        updatePager(word, page);
    }
}