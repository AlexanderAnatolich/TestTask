'use strict'
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
function SummaryDataSource() {
    var datasource = new kendo.data.DataSource({
        transport: {
            read: {
                url: 'Home/ReadSummaryKendoGrid',
                dataType: "json"
            }
        },
        schema: {
            model: {
                fields:
                {
                    Check: {},
                    Id: {},
                    Type: {},
                    Title: {},
                    Author: {},
                    YearOfPublish: { type: "date" },
                    Price: {},
                    DateInsert: { type: "date" }
                }
            }
        }
    });
    return datasource;
};
var _dateToString = function (date) {
    return kendo.toString(kendo.parseDate(date), "G");
};

function GetSummaryGrid(target) {
    Target = target;
    return $(target).kendoGrid({
        dataSource: SummaryDataSource(),
        groupable: true,
        height: '700px',
        sortable: true,
        persistSelection: true,
        change: onChange,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        toolbar: [
            {
                template: '<button type="button" class="k-button" onclick="SaveAsJSON()"><a class="k-icon k-i-save"/>Save as JSON</button>'
            },
            {
                template: '<button type="button" class="k-button" onclick="SaveAsXML()"><a class="k-icon k-i-save"/>Save as XML</button>'
            }
        ],
        columns: [{
            selectable: true,
            width: '30px'
        },
        {
            field: "Type",
            title: "Type"
        },
        {
            field: "Id",
            title: "Id",
            hidden: true
        },
        {
            field: "Title",
            title: "Title"
        },
        {
            field: "Author",
            title: "Author"
        },
        {
            field: "YearOfPublish",
            title: "Year of publish",
            format: "{0:dd/MM/yyyy}"
        },
        {
            field: "Price",
            title: "Price"
        },
        {
            field: "DateInsert",
            title: "Date Insert",
            format: "{0:dd/MM/yyyy}"
        }]
    });
};


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