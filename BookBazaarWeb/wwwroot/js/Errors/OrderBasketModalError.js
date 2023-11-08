const submitButton = document.getElementById("ToCheckout");
const url = submitButton.dataset.url;
const trigger = submitButton.dataset.trigger;

submitButton.addEventListener('click', () => {

    if (trigger === "True") {
        fetch(url)
            .then(response => response.text())
            .then(data => {
                console.log(data)
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