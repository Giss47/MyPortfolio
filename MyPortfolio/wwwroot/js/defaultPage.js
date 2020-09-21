(function defaultHeaderTyping() {
    const text = 'Welcome To My Humble Portfolio';
    defaultHeader = document.querySelector("header h1");
    defaultHeaderAfter = document.queryCommandEnabled("header h1::after");
    let index = 0;
    let currentText = '';
    if (screen.width < 760) {
        defaultHeader.textContent = text;
        defaultHeaderAfter.style.border = "solid 5px red";
    }
    else {
        (function type() {
            currentText = text.slice(0, ++index);
            defaultHeader.textContent = currentText;
            if (currentText !== text) {
                setTimeout(type, 100);
            }
        }());
    }
}());