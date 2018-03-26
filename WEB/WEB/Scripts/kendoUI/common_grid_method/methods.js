var CheckedType = [];
var CheckedId = [];
var CheckedRow = [];
var Target;

function SaveAsXML() {
    var url = 'Save/SaveAsXML' + "?";

    for (var i = 0; i < CheckedId.length; ++i) {
        url += "CId=" + CheckedId[i] + "&" + "CT=" + CheckedType[i] + "&";
    }
    window.location.href = url;
};

function SaveAsJSON() {
    var url = 'Save/SaveAsJSON' + "?";

    for (var i = 0; i < CheckedId.length; ++i) {
        url += "CId=" + CheckedId[i] + "&" + "CT=" + CheckedType[i] + "&";
    }
    window.location.href = url;
};

var _dateToString = function (date) {
    return kendo.toString(kendo.parseDate(date), "G");
};
function multiselectEditor(container, options) {
    console.log(options);
    $('<select multiple= "multiple" name="' + options.field + '"/>')
        .appendTo(container)
        .kendoMultiSelect({
            placeholder: "Select genre...",
            dataTextField: "Genre",
            dataValueField: "Id",
            autoBind: true,
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: {
                        url: "api/ApiGenre/Read",
                        dataType: "json",
                        type: "POST"
                    }
                },
                schema: {
                    id: "Id",
                    fields: {
                        Genre: { type: "string" }
                    }
                }
            }),
        });
}
function PubHousEditor(container, options) {
    $('<input required name="' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            autoBind: false,
            dataTextField: "House",
            dataValueField: "Id",

            dataSource: {
                transport: {
                    read: {
                        url: "api/ApiPublishHouse/Read",
                        dataType: "json",
                        type: "POST"
                    }
                }
            }
        });
}
function onChange(e) {
    CheckedType = [];
    CheckedId = [];
    var rows = e.sender.select();
    rows.each(function (e) {
        var grid = $(Target).data("kendoGrid");
        var dataItem = grid.dataItem(this);
        CheckedType.push(dataItem['Type']);
        CheckedId.push(dataItem['Id']);
        CheckedRow.push(dataItem);
    });
};
function whenYourDeleteButtonIsClicked() {
    if (CheckedId.length === 0) {
        alert("You don't select anything");
    }
    var grid = $(Target).data("kendoGrid");
    var finde = $(Target).find("input:checked");
    finde.each(function () {
        var row = $(this).closest('tr');
        grid.removeRow(row);
    })
}