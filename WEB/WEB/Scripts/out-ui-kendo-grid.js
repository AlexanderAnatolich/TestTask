'use strict'
function CteateDataSource() {
    var dataSource = new kendo.data.DataSource({
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
    return dataSource;
};

function GetDefaultSummaryGrid(target) {
    return $(target).kendoGrid({
        dataSource: CteateDataSource(),
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
                name: "SaveXML", text: "Save as XML", className: "XMLsave", iconClass: "k-icon k-i-save"
            },
            {
                name: "SaveJSON", text: "Save as JSON", className: "JSONsave", iconClass: "k-icon k-i-save"
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
    $(".JSONsave").bind('click', function () {
        var data = JSON.stringify({
            CId: CheckedId,
            CT: CheckedType
        });
        var url = '@Url.Action("SaveAsJSON", "Save")' + "?data=" + data;
        window.location.href = url;
    });
    $(".XMLsave").bind('click', function () {
        var data = JSON.stringify({
            CId: CheckedId,
            CT: CheckedType
        });
        var url = '@Url.Action("SaveAsXML", "Save")' + "?data=" + data;
        window.location.href = url;
    });
}