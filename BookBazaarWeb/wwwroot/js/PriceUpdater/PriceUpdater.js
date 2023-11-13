const quantitySelector = document.querySelector(".QuantitySelector");
const validation = document.getElementById("ItemSelectorValidation");
const price = document.getElementById("InitialPrice");
const discountAmount = document.querySelector('.DiscountAmount');
const element = document.querySelector(".Discount");

quantitySelector.addEventListener('change', () => {
    validation.innerHTML = ``;
    const availableAmount = parseInt(quantitySelector.dataset.maxvalue);
    const selectedAmount = parseInt(quantitySelector.value);
    const pricePerUnit = parseFloat(price.dataset.unitaryprice).toFixed(2);

    if (selectedAmount > availableAmount || selectedAmount <= 0) {
        quantitySelector.value = quantitySelector.dataset.maxvalue;
        validation.innerHTML = "You selected a quantity which exceeds the available stock for this product!";
        price.innerHTML = amount * pricePerUnit + "&euro;";
        return;
    }

    price.innerHTML = (selectedAmount * pricePerUnit).toFixed(2) + "&euro;"
    calculatePrice(selectedAmount, pricePerUnit);
});

const calculatePrice = (amount, pricePerUnit) => {
    let newPrice = 0;
    const priceBeforeDiscount = (amount * pricePerUnit).toFixed(2);

    if (amount < 10) {
        newPrice = priceBeforeDiscount;
        resetDiscount();
    }
    if (amount >= 10 && amount < 25) {
        newPrice = (priceBeforeDiscount - 0.1 * priceBeforeDiscount).toFixed(2);
        displayDiscount(newPrice, 10, amount);
    }
    if (amount >= 25 && amount < 50) {
        newPrice = (priceBeforeDiscount - 0.15 * priceBeforeDiscount).toFixed(2);
        displayDiscount(newPrice, 15, amount);
    }
    if (amount >= 50) {
        newPrice = (priceBeforeDiscount - 0.30 * priceBeforeDiscount).toFixed(2);
        displayDiscount(newPrice, 30, amount);
    }
}

const displayDiscount = (newPrice, discount, selectedAmount) => {
    price.style.textDecoration = 'line-through';

    discountAmount.innerHTML = `-${discount}% off because you selected ${selectedAmount} books!`;
    discountAmount.style.color = '#ED5B2D';
    discountAmount.classList.add('VisibleBlock');
    element.innerHTML = newPrice + '&euro;';
    element.classList.add('VisibleBlock');

}

const resetDiscount = () => {
    price.style.textDecoration = 'none';
    discountAmount.innerHTML = ``;
    discountAmount.classList.remove('VisibleBlock');
    discountAmount.classList.add('Invisible');
    element.title = ``;
    element.innerHTML = ``;
    element.classList.remove('VisibleBlock');
    element.classList.add('Invisible');
}

