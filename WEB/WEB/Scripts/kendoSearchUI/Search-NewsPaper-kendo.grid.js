"use strict";
var datasource = new kendo.data.DataSource();
var Target;
function formatData(data) {
    var result = "";
    for (var key in data) {
        result += data[key];
    }
    return result;
}

function SearchNewsPaperDataSource() {
    datasource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "api/ApiSearch/SearchNewspapers",
                dataType: "json",
                type: "GET"
            },
            parameterMap: function (data, type) {
                if (type == "read") {
                    return { anotherParam: formatData(data) }
                }
            }
        },
        pageSize: 20,
        schema: {
            model: {
                id: "Id",
                fields:
                {
                    Id: { type: "number"},
                    Title: { type: "string"},
                    PrintDate: { type: "date"},
                    PublishHouse: { type: "string"},
                    Price: { type: "number"},
                    DateInsert: { type: "date"},
                }
            }
        }
    });
}
function RefreshSearchNewsPaperGrid(param) {
    datasource.read(param);
    var grid = $(Target).data("kendoGrid").setDataSource(datasource);
}
function GetSearchNewsPaperGrid(target) {
    Target = target;
    $(target).empty().html("");
    return $(target).kendoGrid({
        dataSource: SearchNewsPaperDataSource(),
        groupable: true,
        height: '200px',
        columns:[
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
            	field: "PrintDate",
            	title: "Date of Print",
            	type: "date",
            	format: "{0:dd.MM.yyyy}"
            },
            {
            	field: "PublishHouse",
            	title: "Publishing House",
            	width: "110px",
            	template: "#=PublishHouse.House#"
            },
            {
            	field: "Price",
            	title: "Price"
            },
            {
            	field: "DateInsert",
            	title: "Date Insert",
            	type: "date",
            	format: "{0:dd.MM.yyyy}"
            }]
	}).data("kendoGrid");
}