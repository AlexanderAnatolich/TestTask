﻿function NewsPaperDataSource() {
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
    $(target).html("");
	Target = target;
	return $(target).kendoGrid({
		dataSource: NewsPaperDataSource(),
		groupable: true,
        height: '700px',
        change: onChange,
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