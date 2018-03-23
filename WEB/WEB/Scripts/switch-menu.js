function SwitchMenu(target) {
$('.book-load').click(function () {
    $(target).load('/Book/Index');
    });
$('.Summary-load').click(function () {
    $(target).load('/Base/RenderGrid');
});
$('.News-Paper').click(function () {
    $(target).load('/NewsPapers/Index');
});
$('.Journal-load').click(function () {
    $(target).load('/Journal/Index');
});
}