const currentPage = window.location.href;
console.log(currentPage);
let pagesText;

if (currentPage.includes("Admin")) {
    pagesText = ["Home", "Privacy", "Book", "Category", "Register", "Login", "OrderBasket"];
} else {
    pagesText = ["Home", "Privacy", "Register", "Login", "OrderBasket"];
}
const pagesDOM = document.querySelectorAll(".NavigationLink");
console.log(pagesDOM);
pagesDOM.forEach(p => console.log(p));
const color = "#ED5B2D";

const revalidateColors = () => {
    for (let j = 0; j < pagesDOM.length; j++) {
        if (pagesDOM[j].hasAttribute('style')) {
            pagesDOM.removeAttribute('style');
        }
    }
}

if (currentPage === "https://localhost:7279/" || currentPage === ("https://localhost:7229/Home")) {
    revalidateColors();
    pagesDOM[0].setAttribute('style', `color:${color} !important`);
} else if (currentPage.includes("Category") || currentPage.includes("Book") || currentPage.includes("Company")) {
    revalidateColors();
    pagesDOM[2].setAttribute('style', `color:${color} !important`);
} else if (currentPage.includes("Manage")) {
    pagesDOM[3].setAttribute('style', `color:${color} !important`);
} else {
    for (let i = 0; i < pagesText.length; i++) {
        if (currentPage.includes(pagesText[i])) {
            revalidateColors();
            pagesDOM[i].setAttribute('style', `color:${color} !important`);
        }
    }
}
