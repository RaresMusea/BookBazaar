document.addEventListener("DOMContentLoaded", () => {
    const inputRole = document.querySelector('#Input_Role');
    const companyFormElement = document.querySelector('.CompanyWrapper');


    inputRole.addEventListener('change', () => {
        if (inputRole.options[inputRole.selectedIndex].text === "Company") {
            companyFormElement.classList.remove('Invisible');
            companyFormElement.classList.add('Visible');
        } else {
            if (!companyFormElement.classList.contains('Invisible')) {
                companyFormElement.classList.add('Invisible');
            }
        }
    })

});