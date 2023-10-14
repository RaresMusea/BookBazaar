document.addEventListener("DOMContentLoaded", () => {
    const message = document.querySelector('.Dummy').dataset.toast
    Toastify({
        text: message,
        duration: 3500,
        close: true,
        stopOnFocus: true,
        gravity: "top",
        position: "right",
        style: {
            background: "#ED5B2D",
            fontFamily: "'Exo 2', sans-serif",
            width: "450px",
        }
    }).showToast();
});