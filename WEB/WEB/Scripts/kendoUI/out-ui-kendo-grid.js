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
var _dateToString = function (date) {
    return kendo.toString(kendo.parseDate(date), "G");
};
function BookDataSource() {
    var datasource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "api/ApiBook/Read",
                dataType: "json",
                type:"POST"
            },
            destroy: {
                url: "api/ApiBook/Delete",
                dataType: "json",
                type: "POST",
                data: { Id: CheckedId }
            },
            update: {
                url: 'api/ApiBook/Update',
                dataType: "json",
                type: 'POST'
            },
            create: {
                url: 'api/ApiBook/Create',
                dataType: "json",
                type: 'POST'
            },
            remove: function () {
                alert("nihera");
            },
            error: function (e) {
                alert("Status: " + e.status + "; Error message: " + e.errorThrown);
            },
            parameterMap: function (data, operation) {
                if (operation === "update" || operation === "create") {
                    data.YearOfPublish = _dateToString(data.YearOfPublish);
                    data.DateInsert = _dateToString(data.DateInsert);
                    }
                return data;
            }
        },
        pageSize: 20,
        schema: {
            model: {               
                id:"Id",
                fields:
                {
                    Id: {
                        nullable: true,
                        bath:false
                    },
                    Title: { type: "string", validation: { required: true }},
                    Author: { type: "string", validation: { required: true }},
                    YearOfPublish: { type: "date", validation: { required: true }},
                    PublishHouse: { validation: { required: true }, defaultValue: { Id: 1}},
                    Price: { type: "number", validation: { required: true }},
                    DateInsert: { type: "date", validation: { required: true }},
                    Genre: { validation: { required: true }}
                }
            }
        }
    });
    return datasource;
}
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
        },
        {
            command:
            {
                text: "View Details",
                click: showDetails
            },
            title: " "
        }
        ]
    });
};

function GetBookGrid(target) {
    Target = target;
    var grid = $(target).kendoGrid({
        dataSource: BookDataSource(),
        groupable: true,
        height: '700px',
        sortable: true,
        change: onChange,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        editable: {
            confirmation: false           
        },
        saveChanges: function (e) {
            if (!confirm("Are you sure you want to save all changes?")) {
                e.preventDefault();
            }
        },
        toolbar: [
            {
                template: '<button type="button" class="k-button" onclick="whenYourDeleteButtonIsClicked()"><a class="k-icon k-i-delete"/>Delete selected</button>'
            },
            "create",
            "save",
            {
                template: '<button type="button" class="k-button" onclick="SaveAsJSON()"><a class="k-icon k-i-save"/>Save as JSON</button>'
            },
            {
                template: '<button type="button" class="k-button" onclick="SaveAsXML()"><a class="k-icon k-i-save"/>Save as XML</button>'
            }
        ],
        columns: [
            {
                selectable:true,
                width: "30px",
                template: "<input type='checkbox'/>"
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
                type: "date",
                format: "{0:dd.MM.yyyy}"
            },
            {
                field: "PublishHouse",
                title: "Publishing House",
                width: "110px",
                template: "#=PublishHouse.House#",
                editor: PubHousEditor,
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
                editor: multiselectEditor,
                width: "110px",
                values: 'Genre'
            }
        ]
    }).data("kendoGrid");
}
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

function showDetails(e) {
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
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