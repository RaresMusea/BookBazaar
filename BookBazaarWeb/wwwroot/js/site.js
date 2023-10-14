// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/*const buttons = document.querySelectorAll('.EditButton');
const urls = [];
buttons.forEach(button => {
    console.log(button.dataset.url);
    urls.push(button.dataset.url);
})

for (let i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener('click', async (e) => {
        fetch(urls[i])
            .then(response => response.text())
            .then(data => {
                console.log(data)
                Swal.fire({
                    html: data,
                    showCancelButton: true,
                    showConfirmButton: false,
                    /!*confirmButtonText: "Update",
                    confirmButtonColor: '#468B91'*!/
                }).then(result => {
                    if (result.isConfirmed) {
                    }
                });
            })
            .catch(console.log);
    });

    document.querySelector('.UpdateButton').addEventListener('submit', (e) => {
        e.preventDefault();
    })*/
