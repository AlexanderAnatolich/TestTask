'use strict'

$('#search').click(function () {
    var t = $("#dropdown").val();
    if (t === "Books") {
        var url = '@Url.Action("Edit", "Book")';
    }
    if (t === "Newspapers") {
        var url = '@Url.Action("GetSearchResult", "NewsPapers")';
    }
    if (t === "Journal") {
        var url = '@Url.Action("GetSearchResult", "Journal")';
    }
    var t = document.getElementById("resultSearch").options[document.getElementById("resultSearch").selectedIndex].value;
    $('#searchResults').load(url, { id: t });
})

$(function () {
    $("#SeachInput").keyup(function (e) {
        var t = $("#dropdown").val();
        var searchInput = $("#SeachInput");
        if (searchInput.val() === "") return;
        searchInput.disabled = true;
        $.ajax({
            url: "api/ApiSearch/Search",
            dataType: 'json',
            type: "POST",
            data: { 'Name': searchInput.val(), 'Type': t },
            success: function (data) {
                var select = document.getElementById("resultSearch");
                while (select.firstChild) {
                    select.removeChild(select.firstChild);
                }
                if (data.length === 0) {
                    var el = document.createElement("option");
                    el.textContent = "Don't find products";
                    el.selected = true;
                    el.disabled = true;
                    select.appendChild(el);
                    searchInput.disabled = false;
                    return;
                }
                for (var i = 0; i < data.length; i++) {
                    var opt = data[i];
                    var el = document.createElement("option");
                    el.textContent = opt.Title;
                    el.value = opt.Id;
                    select.appendChild(el);
                }
                searchInput.disabled = false;
            },
            error: function(){
                var el = document.createElement("option");
                el.textContent = "Don't find products";
                el.selected = true;
                el.disabled = true;
                select.appendChild(el);
                searchInput.disabled = false;
                return;
            }
        });
    });
});