var prevScrollsPos = window.pageYOffset;
window.onscroll = () =>
{
    var currentScrollsPos = window.pageYOffset;
    console.log(currentScrollsPos);
    if (prevScrollsPos > currentScrollsPos)
    {
        document.getElementById("HeaderScroll").style.top = "0";
        prevScrollsPos = currentScrollsPos;

    }
    else
    {
        document.getElementById("HeaderScroll").style.top = "-70px";
        prevScrollsPos = currentScrollsPos;
    }
}
async function isNumber(n) { return !isNaN(parseFloat(n)) && !isNaN(n - 0) }

async function Increment(price)
{
    var num = document.getElementById("num").value;
    if (isNumber(num) && num < 100)
    {
        num++;
    }
    document.getElementById("num").value = num;
    document.getElementById("PriceProduct").textContent = "Общая стоимость " +  (num * price).toFixed(2) + " pб";
}
async function Decrement(price) {
    var num = document.getElementById("num").value;
    if (isNumber(num) && num > 0) {
        num--;
    }
    document.getElementById("num").value = num;
    document.getElementById("PriceProduct").textContent = "Общая стоимость " + (num * price).toFixed(2) + " pб";
}
async function CalculatePrice(price) {
    var num = document.getElementById("num").value;
    document.getElementById("PriceProduct").textContent = "Общая стоимость " + (num * price).toFixed(2) + " pб";
}