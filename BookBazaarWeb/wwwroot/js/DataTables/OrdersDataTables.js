document.addEventListener("DOMContentLoaded", () => {
    new DataTable("#OrdersTable");

    const currentPage = window.location.href;
    const filters = document.querySelectorAll('.list-group-item');

    if (!currentPage.includes(`?`)) {
        filters[filters.length - 1].style.background = "#ED5B2D";
        filters[filters.length - 1].style.color = `white`;
    }

    filters.forEach(filter => {
        if (currentPage.includes(filter.innerHTML)) {
            console.log("contine");
            filter.style.background = "#ED5B2D";
            filter.style.color = `white`;
        }
    })
});

