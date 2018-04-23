'use strict'
var datasource = new kendo.data.DataSource();
var Target;
function formatData(data) {
    var result="";
    for (var key in data) {
        result += data[key];
    }
    return result;
}

function SearchBookDataSource() {
    datasource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "api/ApiSearch/SearchBooks",
                dataType: "json",
                contentType: "application/json",
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
                    Id: {
                        nullable: true,
                        bath: false
                    },
                    Title: { type: "string", validation: { required: true } },
                    Author: { type: "string", validation: { required: true } },
                    YearOfPublish: { type: "date", validation: { required: true } },
                    PublishHouse: { validation: { required: true }, defaultValue: { Id: 1 } },
                    Price: { type: "number", validation: { required: true } },
                    DateInsert: { type: "date", validation: { required: true } },
                    Genre: { validation: { required: true } }
                }
            }
        }
    });
}
function RefreshSearchBookGrid(param) {
    datasource.read(param);
    var grid = $(Target).data("kendoGrid").setDataSource(datasource);
}
function GetSearchBookGrid(target) {
    Target = target;
    $(target).empty().html("");
    var grid = $(target).kendoGrid({
        dataSource: SearchBookDataSource(),
        groupable: true,
        sortable: true,
        height: "200px",
        change: onChange,
        columns: [
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
            },
            {
                field: "Genre",
                title: "Genre",
                template: function (dataItem) {
                    var res = "";
                    for (var i = 0; i < dataItem.Genre.length; i++) {
                        if (i !== dataItem.Genre.length - 1) {
                            res += dataItem.Genre[i].Genre + ", ";
                            continue;
                        }
                        res += dataItem.Genre[i].Genre;
                    }
                    return res;
                },
                width: "110px",
                values: 'Genre'
            }
        ]
    }).data("kendoGrid");
}