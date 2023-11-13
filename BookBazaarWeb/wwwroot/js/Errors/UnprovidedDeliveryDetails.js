const submitButton = document.getElementById('StartDeliveryButton');
const awb = document.getElementById('Awb');
const provider = document.getElementById('Provider');

submitButton.addEventListener('click', (e) => {
    if (provider.value === "" || awb.value === "") {
        e.preventDefault();
        Swal.fire({
            template: "#SwalErrModal",
            html: `<h5 class="ModalQuestion">
                       Cannot mark the order for delivery, because the order tracking number or delivery provider were not specified accordingly!
                   </h5>
                   <h6 class="text-danger">Please check those fields and retry</h6>`,
            showCancelButton: true,
            showConfirmButton: false,
            cancelButtonText: "Okay",
            cancelButtonColor: "#ED5B2D",
            icon: 'error'
        });
    }
});