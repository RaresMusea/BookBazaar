const modalTriggers = document.querySelectorAll('.HovarableTableData');

const displayInfoDialog = async (data) => {
    await Swal.fire({
        icon: 'info',
        html: `<h5>Description for book "${data.Title}" by ${data.Author}</h5>
                <p class="Paragraph">${data.Description}</p>`,
        showConfirmButton: false,
        showCancelButton: true,
        cancelButtonText: 'Close',
        confirmButtonColor: "#525252",
    });
}

modalTriggers.forEach(modalTrigger => {
    modalTrigger.addEventListener(`click`, async (e) => {
        const dataJson = JSON.parse(modalTrigger.dataset.json);
        await displayInfoDialog(dataJson);
    });
})