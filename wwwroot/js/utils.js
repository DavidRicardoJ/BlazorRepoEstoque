
function MoveScroll(xValue) {
    window.scrollBy(0, xValue);
}

function selecionarValorInputNumber(id) {
    console.log("ok " + id)
    const element = document.getElementById(id);
    if (element != null) {
        element.select();
    }    
}

