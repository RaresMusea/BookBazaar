document.addEventListener("DOMContentLoaded", () => {
    const orderButtons = document.querySelectorAll('.OrderElement');
    const target = document.querySelector('.OrderDetailsWindow');

    console.log("MERGE!");
    orderButtons.forEach(orderButton => {
        orderButton.addEventListener('click', () => {
            const url = orderButton.dataset.url;
            fetch(url)
                .then(response => response.text())
                .then(data => {
                    target.innerHTML = data;
                })
                .catch(console.log);

        });
    });
});