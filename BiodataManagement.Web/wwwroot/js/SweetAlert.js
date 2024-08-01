function onDeleteAlert() {

  $(this).click(function (event) {
    var url = $(this).attr('href');
    Swal.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, delete it!"
    }).then((result) => {
      if (result.isConfirmed) {
        Swal.fire({
          title: `${url} Deleted!`,
          text: "Your file has been deleted.",
          icon: "success"
        });
      }
    });
  })
}