"use strict";
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

function onChange(e) {
    debugger;
    CheckedType = [];
    CheckedId = [];
    var rows = e.sender.select();
    rows.each(function (e) {
        var grid = $(Target).data("kendoGrid");
        var dataItem = grid.dataItem(this);
        CheckedType.push(dataItem['Type']);
        CheckedId.push(dataItem['Id']);
        CheckedRow.push(dataItem);
        debugger;
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
function NewsPaperDataSource() {
	var datasource = new kendo.data.DataSource({
		transport: {
			read: {
				url: "api/ApiNewsPaper/Read",
				dataType: "json",
				type: "POST"
			},
			destroy: {
				url: "api/ApiNewsPaper/Delete",
				dataType: "json",
				type: "POST",
				data: { Id: CheckedId }
			},
			update: {
				url: 'api/ApiNewsPaper/Update',
				dataType: "json",
				type: 'POST'
			},
			create: {
				url: 'api/ApiNewsPaper/Create',
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
					data.PrintDate = _dateToString(data.PrintDate);
					data.DateInsert = _dateToString(data.DateInsert);
				}
				return data;
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
                	PrintDate: { type: "date", validation: { required: true } },
                    PublishHouse: { validation: { required: true },defaultValue: { Id: 1 }},               	
                	Price: { type: "number", validation: { required: true } },
                	DateInsert: { type: "date", validation: { required: true } },
                }
			}
		}
	});
	return datasource;
}
function SearchNewsPaperDataSource(id) {
    var datasource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "api/ApiNewsPaper/Read",
                dataType: "json",
                type: "POST",
                data: { Id: CheckedId }
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
                    PrintDate: { type: "date", validation: { required: true } },
                    PublishHouse: { validation: { required: true }, defaultValue: { Id: 1 } },
                    Price: { type: "number", validation: { required: true } },
                    DateInsert: { type: "date", validation: { required: true } },
                }
            }
        }
    });
    return datasource;
}
function GetNewsPaperGrid(target) {
    Target = target;
    $(target).empty().html("");
    return $(target).kendoGrid({
        dataSource: NewsPaperDataSource(),
        groupable: true,
        height: '700px',
        change:onChange,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        editable: {
            confirmation: false           
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
            	field: "PrintDate",
            	title: "Date of Print",
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
            }]
	}).data("kendoGrid");
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