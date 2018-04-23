function BookDataSource() {
	var datasource = new kendo.data.DataSource({
		transport: {
			read: {
				url: "api/ApiBook/Read",
				dataType: "json",
				type: "POST"
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
	return datasource;
}
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
            	selectable: true,
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