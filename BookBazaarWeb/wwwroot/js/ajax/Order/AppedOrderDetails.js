document.addEventListener("DOMContentLoaded", async () => {
    const orderButtons = document.querySelectorAll('.OrderElement');
    const target = document.querySelector('.OrderDetailsWindow');
    const fetchData = async (url) => {
        fetch(url)
            .then(response => response.text())
            .then(data => {
                target.innerHTML = data;
            })
            .catch(console.log);
    }

    const resetColors = () => {
        orderButtons.forEach(button => {
            button.style.background = `white`;
            button.style.color = 'black';
        });
    }

    const setStyling = (element) => {
        element.style.background = `#ED5B2D`;
        element.style.color = `white`;
    }

    if (location.href.includes("/Index/")) {
        const tokens = location.href.split("/");
        const id = tokens[tokens.length - 1];

        orderButtons.forEach((button) => {
            if (button.dataset.id === id) {
                const url = button.dataset.url;
                resetColors();
                setStyling(button);
                fetchData(url);
            }
        })

    }

    orderButtons.forEach(orderButton => {
        orderButton.addEventListener('click', async () => {
            resetColors();
            setStyling(orderButton);
            const url = orderButton.dataset.url;
            await fetchData(url);
        });
    });
});
