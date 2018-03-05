'use strict'
var CheckedType = [];
var CheckedId = [];
var CheckedRow = [];
var Target;

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

$(Target+" .k-grid-content").on("change", "input.chkbx", function (e) {
    var grid = $(Target).data("kendoGrid"),
        dataItem = grid.dataItem($(e.target).closest("tr"));
    dataItem.set("Check", this.checked);
});

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
            //create: {
            //    url: 'api/ApiBook/Create',
            //    dataType: "json",
            //    type: 'POST'
            //},
            //remove: function () {
            //    alert("nihera");
            //},
            //error: function (e) {
            //    alert("Status: " + e.status + "; Error message: " + e.errorThrown);
            //},
            //parameterMap: function (data, operation) {
            //    if (operation === "update" || operation === "create") {
            //        data.YearOfPublish = _dateToString(data.YearOfPublish);
            //        data.DateInsert = _dateToString(data.DateInsert);
            //        }
            //    return data;
            //}
        },

        schema: {
            model: {               
                id:"Id",
                fields:
                {
                    Id: {
                        nullable: true,
                        bath:false
                    },
                    Title: {type:"string"},
                    Author: {type:"string"},
                    YearOfPublish: { type: "date"},
                    PublishingHouse: { type: "string"},
                    Price: { type: "number" },
                    DateInsert: { type: "date"},
                    Genre: {}
                }
            }
        }
    });
    return datasource;
}
function DeleteSelected() {
    if (CheckedId.length === 0) {
        return;
    }
    $.ajax({
        url: 'api/ApiBook/Delete',
        method: 'POST',
        data: { Id: CheckedId },
        success: function (data) {
            $(Target).data("kendoGrid").refresh();
        },
        error: function () {
            alert('chet ne tak');
            $(Target).data("kendoGrid").cancelRow();
        },
    });
}
function removeSelectedRow() {
    var grid = $(Target).kendoGrid().data("kendoGrid");
    $("#grid").find("input:checked").each(function () {
        grid.removeRow($(this).closest('tr'));
    })
}
function GetBookGrid(target) {
    Target = target;
    return $(target).kendoGrid({
        dataSource: BookDataSource(),
        groupable: true,
        height: '700px',
        sortable: true,
        change: onChange,
        persistSelection: true,
        //editable: {
        //    mode: "popup"
        //},
        //edit:function(e) {
        //        e.container.find("input:first").hide();
        //        e.container.find("label:first").hide();
        //},
        //save: function (e) {
        //    var that = this;
        //    $.ajax({                
        //        success: function (data) {
        //            that.refresh();
        //        },
        //        error: function (data) { 
        //            that.cancelRow();
        //        }

        //    });
        //},
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        toolbar: [
            {
                template: "<button type='button' class='k-button' onclick='removeSelectedRow()'><a class='k-icon k-i-delete'/></span>Delete</button>"
            },
            {
                template: '<button type="button" class="k-button" onclick="DeleteSelected()"><a class="k-icon k-i-delete"/>Delete selected</button>'
            },
            {
                name:"create"
            },
            {
                template: '<button type="button" class="k-button" onclick="SaveAsJSON()"><a class="k-icon k-i-save"/>Save as JSON</button>'
            },
            {
                template: '<button type="button" class="k-button" onclick="SaveAsXML()"><a class="k-icon k-i-save"/>Save as XML</button>'
            }
        ],
        columns: [
            {                           
                template: "<input type='checkbox'/>",
                selectable:true,
                width: '30px'
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
                type: "date",
                format: "{0:dd.MM.yyyy}"
            },
            {
                field: "Genre",
                title: "Genre"
            },
            {
                command: {
                    text: "Delete",
                    click: removeSelectedRow
                }
            }
        ]
    });
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
            title: "Who publish"
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
<script src="https://demos.telerik.com/kendo-ui/content/shared/js/people.js"></script>
    <div id="example" class="k-content">
        <div id="grid"></div>

        <div id="details"></div>

        <script>

            $(document).ready(function () {
          var grid = $("#grid").kendoGrid({
                dataSource: {
                pageSize: 20,
              data: createRandomData(50)
            },
            pageable: true,
            height: 430,
            columns: [
              {field: "FirstName", title: "First Name", width: "140px" },
              {field: "LastName", title: "Last Name", width: "140px" },
              {field: "Title" },
              {
                field : "Select",
                title : "Select",
                width : "16%",
                template: "<input type='checkbox' class='sel' />"},
              {command: {text: "Delete", click: whenYourDeleteButtonIsClicked }, title: " ", width: "140px" }]
          }).data("kendoGrid");

        });

        function whenYourDeleteButtonIsClicked(){
          var grid = $("#grid").data("kendoGrid");
          $("#grid").find("input:checked").each(function(){
                grid.removeRow($(this).closest('tr'));
            })
        }
      </script>