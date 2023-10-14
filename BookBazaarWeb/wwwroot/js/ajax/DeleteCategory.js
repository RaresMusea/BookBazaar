const buttons = document.querySelectorAll('.DeleteButton');
const urls = [];

buttons.forEach(button => {
    console.table(button, button.dataset.url);
    urls.push(button.dataset.url);
})

for (let i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener('click', async (e) => {
        fetch(urls[i])
            .then(response => response.text())
            .then(data => {
                console.log(data)
                Swal.fire({
                    template: "#SwalDelModal",
                    html: data,
                    showCancelButton: true,
                    showConfirmButton: true,
                    confirmButtonText: "Delete",
                    confirmButtonColor: "red",
                    icon: 'question',
                }).then(result => {
                        if (result.isConfirmed) {
                            $.ajax({
                                url: e.target.dataset.url,
                                type: 'POST',
                                success: () => {
                                    Swal.fire({
                                        icon: 'success',
                                        html: '<h4>The category was deleted successfully!.</h4>',
                                        showConfirmButton: true,
                                        confirmButtonText: "OK",
                                        confirmButtonColor: "#ED5B2D",
                                    }).then(result => {
                                        if (result.isConfirmed) {
                                            window.location.reload();
                                        }
                                    })
                                },
                                error: () => {
                                    Swal.fire({
                                        icon: 'error',
                                        html: '<h4>An error occurred while attempting to delete the entity.</h4>',
                                        showConfirmButton: true,
                                        confirmButtonText: "Retry",
                                        confirmButtonColor: "#545454",
                                    }).then(result => {
                                        if (result.isConfirmed) {
                                            window.location.reload();
                                        }
                                    })
                                }
                            });
                        }
                    }
                );
            })
            .catch(console.log);
    });
}