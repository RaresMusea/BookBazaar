const buttons = document.querySelectorAll('.DeleteButton');
const urls = [];

buttons.forEach(button => {
    console.table(button, button.dataset.url);
    urls.push(button.dataset.url);
})

const displaySuccesDialog = (entityName) => {
    Swal.fire({
        icon: 'success',
        html: `<h4>The ${entityName} was deleted successfully!.</h4>`,
        showConfirmButton: true,
        confirmButtonText: "OK",
        confirmButtonColor: "#ED5B2D",
    }).then(result => {
        if (result.isConfirmed) {
            location.reload();
        }
    })
}

const displayFailureDialog = (entityName) => {
    Swal.fire({
        icon: 'error',
        html: `<h4>An error occurred while attempting to delete the ${entityName}.</h4>`,
        showConfirmButton: true,
        confirmButtonText: "Retry",
        confirmButtonColor: "#545454",
    }).then(result => {
        if (result.isConfirmed) {
            location.reload();
        }
    })
}

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
                            console.log(document.querySelector(".ModalQuestion").dataset.payload);
                            const payloadJson = JSON.parse(document.querySelector(".ModalQuestion").dataset.payload);
                            $.ajax({
                                url: '/Admin/Category/Delete',
                                type: 'POST',
                                data: payloadJson,
                                success: () => {
                                    displaySuccesDialog('category');
                                },
                                error: () => {
                                    displayFailureDialog('category');
                                }
                            });
                        }
                    }
                );
            })
            .catch(console.log);
    });
}