function GenerateData() {
  debugger;
  $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    url: '@Url.Action("GenerateData", "Biodata")',
    data: {},
    success: function (r) {
      $(".toast").toast("show");
      DisplayData();
      debugger;
    },
    error: function (response) {
      debugger;
      alert("Submit Data Failed");
    },
  });
}

function DisplayData() {
  $.ajax({
    type: "GET",
    url: '@Url.Action("DisplayData", "Biodata")',
    data: {},
    success: function (data) {
      $("#biodataListPartial").html(data);
    },
    error: function (response) {
      alert("Display data failed");
    },
  });
}

function DeleteAllFakeData() {
  $.ajax({
    type: "DELETE",
    url: '@Url.Action("DeleteAllFakeData", "Biodata")',
    data: {},
    success: function (data) {
      alert("Deleted " + data + "Row");
    },
    error: function (response) {
      alert("Delete Failed " + response);
    },
  });
}
