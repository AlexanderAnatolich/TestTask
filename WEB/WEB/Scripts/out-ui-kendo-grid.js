'use strict'
var CheckedType = [];
var CheckedId = [];

function onChange(e) {
    CheckedType = [];
    CheckedId = [];
    var rows = e.sender.select();
    rows.each(function (e) {
        var grid = $("#KendoGridId").data("kendoGrid");
        var dataItem = grid.dataItem(this);
        CheckedType.push(dataItem['Type']);
        CheckedId.push(dataItem['Id']);
    })
};

function showDetails(e) {
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
};

$("#grid .k-grid-content").on("change", "input.chkbx", function (e) {
    var grid = $("#KendoGridId").data("kendoGrid"),
        dataItem = grid.dataItem($(e.target).closest("tr"));
    dataItem.set("Check", this.checked);
});

function SaveAsXML() {
    debugger
    var url = 'Save/SaveAsXML' + "?";

    for (var i = 0; i < CheckedId.length; ++i) {
        url += "CId=" + CheckedId[i] + "&" + "CT=" + CheckedType[i] + "&";
    }
    window.location.href = url;
};

function SaveAsJSON() {
    debugger
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
                url: 'Base/ReadSummaryKendoGrid',
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
function BookDataSource() {
    var datasource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "Book/ReadFunction",
                dataType: "json"
            }
        },
        schema: {
            model: {
                fields:
                {
                    Id: {},
                    Title: {},
                    Author: {},
                    YearOfPublish: { type: "date" },
                    PublishingHouse: {},
                    Price: {},
                    DateInsert: { type: "date" },
                    Genre: {}
                }
            }
        }
    });
    return datasource;
}
function GetBookGrid(target) {
    console.log("sdfdsf");
    return $(target).kendoGrid({
        dataSource: BookDataSource(),
        groupable: true,
        height: '700px',
        sortable: true,
        persistSelection: true,
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
            selectable: true
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
            field: "PublishingHouse",
            title: "PublishingHouse"
        },
        {
            field: "Price",
            title: "Price"
        },
        {
            field: "DateInsert",
            title: "Date Insert",
            format: "{0:dd/MM/yyyy}"
        },
        {
            field: "Genre",
            title: "Genre"
        },
        ],
    });
}
function GetSummaryGrid(target) {
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
        toolbar:[
            {
                template: '<button type="button" class="k-button" onclick="SaveAsJSON()"><a class="k-icon k-i-save"/>Save as JSON</button>'
            },
            {
                template: '<button type="button" class="k-button" onclick="SaveAsXML()"><a class="k-icon k-i-save"/>Save as XML</button>'
            }
        ],
        columns: [{
            selectable: true
        },
        {
            field: "Type",
            title: "Type",
        }, {
            field: "Id",
            title: "Id",
            hidden: true
        }, {
            field: "Title",
            title: "Title",
        }, {
            field: "Author",
            title: "Who publish"
        }, {
            field: "YearOfPublish",
            title: "Year of publish",
            format: "{0:dd/MM/yyyy}"
        }
            , {
            field: "Price",
            title: "Price"
        }
            ,
        {
            field: "DateInsert",
            title: "Date Insert",
            format: "{0:dd/MM/yyyy}"
        },
        { command: { text: "View Details", click: showDetails }, title: " " }
        ],
    });
};
