'use strict'
$(function () {
    $("#SeachInput").keyup(function (e) {      
        var inputValue = $("#SeachInput").val();
        if (inputValue === "") return;

        var typeValue = $('#dropdown').val();
        if (typeValue === "Books") {
            var grid = GetSearchBookGrid('#searchResults');
            RefreshSearchBookGrid(inputValue);
        }
        if (typeValue === "Newspapers") {
            var grid = GetSearchNewsPaperGrid('#searchResults');
            RefreshSearchNewsPaperGrid(inputValue);
        }
        if (typeValue === "Journal") {
            var grid = GetSearchJournalGrid('#searchResults');
            RefreshSearchJournalGrid(inputValue);
        }
    });
});
