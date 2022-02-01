
function ClickElement(elementID) {
    var element = document.getElementById(elementID);
    if (element) {
        element.click();
    }
}

function ClickElementByClass(classID) {
    var element = document.getElementsByClassName(classID);
    if (element) {        
        element[0].click();
    }
}