const processButton = document.querySelector(".ProcessingButton");
const url = processButton.dataset.url;
const trigger = processButton.dataset.trigger;

processButton.addEventListener('click', () => {

    if (trigger === "True") {
        fetch(url)
            .then(response => response.text())
            .then(data => {
                Swal.fire({
                    template: "#SwalErrModal",
                    html: data,
                    showCancelButton: true,
                    showConfirmButton: false,
                    cancelButtonText: "Okay",
                    cancelButtonColor: "#ED5B2D",
                    icon: 'error'
                });
            })
            .catch(console.log);
    }
});