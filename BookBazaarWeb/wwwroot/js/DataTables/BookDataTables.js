document.addEventListener("DOMContentLoaded", () => {
    new DataTable("#BooksTable");
    const label = document.querySelector(".dataTables_filter").attributes;
    console.log(label);
});
