function biodataDeleteAlert(event, url) {
  event.preventDefault();

  const deleteUrl = url;
  const deleteUrlId = deleteUrl.split("/").pop();

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
        url: deleteUrl,
        type: "DELETE",
        success: function (data) {
          Swal.fire({
            title: "Biodata Deleted!",
            text: `Biodata with Id ${deleteUrlId}, Deleted!`,
            icon: "success",
          });

          DisplayData();
        },
        error: function (response) {
          Swal.fire({
            title: "Error!",
            text: "Something went wrong",
            icon: "error",
            confirmButtonText: "Cool",
          });
        },
      });
    }
  });
}

function pendidikanDelete(element, event) {
  event.preventDefault();

  const deleteUrl = $(element).attr("href");
  const deleteUrlId = deleteUrl.split("/").pop();

  Swal.fire({
    title: "Are you sure want to delete this pendidikan terakhir item ?",
    text: "You won't be able to revert this!",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Yes, delete it!",
  }).then((result) => {
    if (result.isConfirmed) {
      $.ajax({
        url: deleteUrl,
        type: "DELETE",
        success: () => {
          Swal.fire({
            title: "Pendidikan Terakhir Deleted!",
            text: `Pendidikan Terakhir with Id ${deleteUrlId}, Deleted!`,
            icon: "success",
          }).then(() => {
            location.reload(true);
          });
        },
        error: (response) => {
          Swal.fire({
            title: "Error!",
            text: "Something went wrong",
            icon: "error",
            confirmButtonText: "Cool",
          });
        },
      });
    }
  });
}
