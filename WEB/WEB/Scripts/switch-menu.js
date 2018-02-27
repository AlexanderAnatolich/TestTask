function SwitchMenu(target) {
$('.book-load').click(function () {
    $(target).load('/Book/Index');
    });
$('.Summary-load').click(function () {
    $(target).load('/Base/RenderGrid');
    });
}