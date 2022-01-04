function focusById(elementId) {
    var element = document.getElementById(elementId);

    if (element) {
        element.select();
        element.focus();        
    }
}

