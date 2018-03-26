$(document).ready(function () {
    function SwitchMenu(target) {
        $('.book-load').unbind('click');
        $('.book-load').click(function () {
            var grid = GetBookGrid(target);
        });

        $('.Summary-load').unbind('click');
        $('.Summary-load').click(function () {
            var grid = GetSummaryGrid(target);
        });

        $('.News-Paper').unbind('click');
        $('.News-Paper').click(function () {
            var grid = GetNewsPaperGrid(target);
        });

        $('.Journal-load').unbind('click');
        $('.Journal-load').click(function () {
            var grid = GetJournalGrid(target);
        });
    }
})