function JournalDataSource() {
    var datasource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "api/ApiJournal/Read",
                dataType: "json",
                type: "POST"
            },
            destroy: {
                url: "api/ApiJournal/Delete",
                dataType: "json",
                type: "POST",
                data: { Id: CheckedId }
            },
            update: {
                url: 'api/ApiJournal/Update',
                dataType: "json",
                type: 'POST'
            },
            create: {
                url: 'api/ApiJournal/Create',
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
                    YearOfPublish: { type: "date", validation: { required: true } },
                    PublishHouse: { validation: { required: true }, defaultValue: { Id: 1 } },
                    Price: { type: "number", validation: { required: true } },
                    DateInsert: { type: "date", validation: { required: true } }
                }
            }
        }
    });
    return datasource;
}

function GetJournalGrid(target) {
    Target = target;
    $(target).html("");
    return $(target).kendoGrid({
        dataSource: JournalDataSource(),
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
            }
        ]
    }).data("kendoGrid");
}
