function SwitchMenu(target) {

$('.book-load').click(function () {
    var grid = GetBookGrid(target);
    });

$('.Summary-load').click(function () {
    var grid = GetSummaryGrid(target);
});

$('.News-Paper').click(function () {
    var grid = GetNewsPaperGrid(target);
});

$('.Journal-load').click(function () {
    var grid = GetJournalGrid(target);
});
}