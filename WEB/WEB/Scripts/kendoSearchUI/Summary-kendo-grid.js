'use strict'
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

function GetSummaryGrid(target) {
    Target = target;
    $(target).empty().html("");
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
        columns: [
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