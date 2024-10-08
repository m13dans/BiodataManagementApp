﻿function biodataDeleteAlert(event, url) {
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
        success: (response) => {
          console.log(response);
          Swal.fire({
            title: "Pendidikan Terakhir Deleted!",
            text: `Pendidikan Terakhir with Id ${deleteUrlId}, ${response.namaInstitusiAkademik} Deleted!`,
            icon: "success",
          }).then(() => {
            location.reload(true);
          });
        },
        error: (response, msg, error) => {
          console.log(response);
          console.log(msg);
          console.log(error);

          Swal.fire({
            title: "Error!",
            text: `Something went wrong : ${msg}`,
            icon: "error",
            confirmButtonText: "Cool",
          });
        },
      });
    }
  });
}

function pekerjaanDelete(element, event) {
  event.preventDefault();

  const deleteUrl = $(element).attr("href");
  const deleteUrlId = deleteUrl.split("/").pop();

  Swal.fire({
    title: "Are you sure want to delete this riwayat pekerjaan item ?",
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
        success: (response) => {
          console.log(response);
          Swal.fire({
            title: "Riwayat Pekerjaan Deleted!",
            text: `Riwayat Pekerjaan with Id ${deleteUrlId}, ${response.namaPerusahaan} Deleted!`,
            icon: "success",
          }).then(() => {
            location.reload(true);
          });
        },
        error: (response, msg, error) => {
          console.log(response);
          console.log(msg);
          console.log(error);

          Swal.fire({
            title: "Error!",
            text: `Something went wrong : ${msg}`,
            icon: "error",
            confirmButtonText: "Cool",
          });
        },
      });
    }
  });
}

function pelatihanDelete(element, event) {
  event.preventDefault();

  const deleteUrl = $(element).attr("href");
  const deleteUrlId = deleteUrl.split("/").pop();

  Swal.fire({
    title: "Are you sure want to delete this Riwayat Pelatihan item ?",
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
        success: (response) => {
          console.log(response);
          Swal.fire({
            title: "Riwayat Pelatihan Deleted!",
            text: `Riwayat Pelatihan with Id ${deleteUrlId}, ${response.namaKursus} Deleted!`,
            icon: "success",
          }).then(() => {
            location.reload(true);
          });
        },
        error: (response, msg, error) => {
          console.log(response);
          console.log(msg);
          console.log(error);

          Swal.fire({
            title: "Error!",
            text: `Something went wrong : ${msg}`,
            icon: "error",
            confirmButtonText: "Cool",
          });
        },
      });
    }
  });
}
