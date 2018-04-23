$(document).ready(function () {
        $('.book-load').unbind('click');

        $('.book-load').click(function () {
            var grid = GetBookGrid('#KendoGridId');
        });

        $('.Summary-load').unbind('click');
        $('.Summary-load').click(function () {
            var grid = GetSummaryGrid('#KendoGridId');
        });

        $('.News-Paper').unbind('click');
        $('.News-Paper').click(function () {
            var grid = GetNewsPaperGrid('#KendoGridId');
        });

        $('.Journal-load').unbind('click');
        $('.Journal-load').click(function () {
            var grid = GetJournalGrid('#KendoGridId');
        });
})