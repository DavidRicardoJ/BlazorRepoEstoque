
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

function AdicionarEstiloPosFiltro() {
    var element = document.getElementsByClassName('mud-expand-panel-header');
    element[0].style.backgroundColor = 'lightgreen';
}

