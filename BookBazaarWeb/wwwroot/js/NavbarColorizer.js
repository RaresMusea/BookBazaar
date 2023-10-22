const currentPage = window.location.href;
console.log(currentPage);
const pagesText = ["Home", "Privacy", "Book", "Category"];
const pagesDOM = document.querySelectorAll(".NavigationLink");
pagesDOM.forEach(p => console.log(p));
const color = "#ED5B2D";

const revalidateColors = () => {
    for (let j = 0; j < pagesDOM.length - 1; j++) {
        if (pagesDOM[j].hasAttribute('style')) {
            console.log(pagesDOM[j]);
            pagesDOM.removeAttribute('style');
        }
    }
}

if (currentPage === "https://localhost:7279/" || currentPage === ("https://localhost:7229/Home")) {
    revalidateColors();
    pagesDOM[0].setAttribute('style', `color:${color} !important`);
} else {
    for (let i = 1; i < pagesText.length; i++) {
        if (currentPage.includes(pagesText[i])) {
            revalidateColors();
            pagesDOM[i > 2 ? (i - 1) : i].setAttribute('style', `color:${color} !important`);
        }
    }
}
