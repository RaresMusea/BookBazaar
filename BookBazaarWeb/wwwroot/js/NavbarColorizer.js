document.addEventListener('DOMContentLoaded', () => {
    const navbar = document.querySelector('nav');
    const currentPage = window.location.href;
    const isSignedIn = navbar.dataset.signedin;
    const userType = navbar.dataset.usertype;
    const color = "#ED5B2D";
    const pagesDOM = document.querySelectorAll(".NavigationLink");
    let pagesText;

    const revalidateColors = () => {
        for (let j = 0; j < pagesDOM.length; j++) {
            if (pagesDOM[j].hasAttribute('style')) {
                pagesDOM.removeAttribute('style');
            }
        }
    }

    if (isSignedIn === "False") {
        pagesText = ["BOOK BAZAAR", "Register", "Login", "OrderBasket"]
    } else {
        pagesText = ["BOOK BAZAAR", "Category", "Book", "Company", "OrderManagement", "Customer/Order", "Manage", "Customer/OrderBasket"];
    }

    if (currentPage === "https://localhost:7279/" || currentPage === ("https://localhost:7229/Home")) {
        revalidateColors();
        pagesDOM[0].setAttribute('style', `color:${color} !important`);
    } else {

        for (let i = 0; i < pagesText.length; i++) {
            if (isSignedIn === "True") {
                if (currentPage.includes(pagesText[i])) {
                    let navbarItem;

                    if (userType === "Admin" && ["Category", "Book", "Company", "OrderManagement"].includes(pagesText[i])) {
                        navbarItem = pagesDOM[1];
                    }

                    if (["Customer/Order"].includes(pagesText[i]) && i === 5 || ["Manage"].includes(pagesText[i])) {
                        if (userType === "Admin") {
                            navbarItem = pagesDOM[2];
                        } else {
                            navbarItem = pagesDOM[1];
                        }
                    }

                    if (currentPage.includes('/Customer/OrderBasket')) {
                        if (userType === "Admin") {
                            navbarItem = pagesDOM[4];
                        } else {
                            navbarItem = pagesDOM[3];
                        }
                    }

                    revalidateColors();
                    navbarItem.setAttribute(`style`, `color:${color} !important`);
                }
            } else {
                if (currentPage.includes(pagesText[i])) {
                    revalidateColors();
                    pagesDOM[i].setAttribute('style', `color:${color} !important`);
                }
            }
        }
    }
});
