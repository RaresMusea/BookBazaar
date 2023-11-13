document.addEventListener('DOMContentLoaded', () => {
    const navbar = document.querySelector('nav');
    const currentPage = window.location.href;
    const isSignedIn = navbar.dataset.signedin;
    const userType = navbar.dataset.usertype;
    const isInternal = navbar.dataset.isinternal;
    const color = "#ED5B2D";
    const pagesDOM = document.querySelectorAll(".NavigationLink");
    let pagesText;

    const bookCategories = ["category=All", "category=Adventure", "category=Science-Fiction", "category=Psychology",
        "category=Programming", "category=Self-Development"];

    const revalidateColors = () => {
        for (let j = 0; j < pagesDOM.length; j++) {
            if (pagesDOM[j].hasAttribute('style')) {
                pagesDOM.removeAttribute('style');
            }
        }
    }

    if (isSignedIn === "False") {
        pagesText = ["BOOK BAZAAR", "category=All", "category=Adventure", "category=Science-Fiction", "category=Psychology",
            "category=Programming", "category=Self-Development", "Register", "Login", "OrderBasket"];
    } else {
        pagesText = ["BOOK BAZAAR", "Category", "Book", "Company", "OrderManagement", "Customer/Order", "/Identity/Account/Register", "/Admin/User", "Manage", "Customer/OrderBasket"];
    }

    if (currentPage === "https://localhost:7279/" || currentPage === ("https://localhost:7229/Home")) {
        revalidateColors();
        pagesDOM[0].setAttribute('style', `color:${color} !important`);
    } else {

        for (let i = 0; i < pagesText.length; i++) {
            if (bookCategories.includes(currentPage.split("?")[1])) {
                revalidateColors();
                pagesDOM[1].setAttribute('style', `color:${color} !important`);
            }
            if (isSignedIn === "True") {
                if (currentPage.includes(pagesText[i])) {
                    let navbarItem;

                    if ((userType === "Admin" && ["Category", "Book", "Company", "OrderManagement"].includes(pagesText[i])) || (isInternal === "True")) {
                        navbarItem = pagesDOM[2];
                    }

                    if (userType === "Admin" && currentPage.includes("/Admin/User")) {
                        navbarItem = pagesDOM[2];
                    }

                    if (userType === "Admin" && currentPage.includes("/Identity/Account/Register")) {
                        navbarItem = pagesDOM[2];
                    }

                    if (["Customer/Order"].includes(pagesText[i]) && i === 5 || ["Manage"].includes(pagesText[i])) {
                        if (userType === "Admin" || isInternal === "True") {
                            navbarItem = pagesDOM[3];
                        } else {
                            navbarItem = pagesDOM[2];

                        }
                    }

                    if (currentPage.includes('/Customer/OrderBasket')) {
                        if (userType === "Admin" || isInternal === "True") {
                            navbarItem = pagesDOM[5];
                        } else {
                            navbarItem = pagesDOM[4];
                        }
                    }

                    revalidateColors();
                    navbarItem.setAttribute(`style`, `color:${color} !important`);
                }
            } else {
                if (currentPage.includes("Login")) {
                    revalidateColors();
                    pagesDOM[3].setAttribute('style', `color:${color} !important`);
                }
                if (currentPage.includes("Register")) {
                    revalidateColors();
                    pagesDOM[2].setAttribute('style', `color:${color} !important`);
                }
            }
        }
    }
});
