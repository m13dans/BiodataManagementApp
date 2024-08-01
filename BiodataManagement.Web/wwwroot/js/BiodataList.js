function GenerateData() {
  debugger;
  $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    url: "Biodata/GenerateData",
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
    url: "Biodata/DisplayData",
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
  Swal.fire({
    title: "Are you sure?",
    text: "You won't be able to revert this!",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Yes, delete it!",
  }).then((result) => {
    if (result.isConfirmed) {
      $.ajax({
        type: "DELETE",
        url: "Biodata/DeleteAllFakeData",
        data: {},
        success: function (data) {
          Swal.fire({
            title: "Biodata Deleted!",
            text: `${data} biodata, Deleted!`,
            icon: "success",
          });
          DisplayData();
        },
        error: function (response) {
          Swal.fire({
            title: "Error!",
            text: "Do you want to continue",
            icon: "error",
            confirmButtonText: "Cool",
          });
        },
      });
    }
  });
}
