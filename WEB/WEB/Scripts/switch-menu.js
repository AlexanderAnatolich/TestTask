function SwitchMenu() {
$('.book-load').click(function () {
    $('#KendoGridId').load('/Book/Index');
    });
$('.Summary-load').click(function () {
    $('#KendoGridId').load('/Base/RenderGrid');
    });
}