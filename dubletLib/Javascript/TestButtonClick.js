function clickDesiredButtonByInnerText(btnInnerText) {

    //let buttons = document.getElementByTagName('button');
    let buttons = document.querySelectorAll('button');
    let i = 0;
    let result = null;

    if (buttons) {

        for (i = 0; i < buttons.length; i++) {
            //window.chrome.webview.postMessage("button[" + i + "].innerText: " + buttons[i].innerText);

            if (buttons[i].innerText === btnInnerText) {
                buttons[i].click();
                result = btnInnerText + ' clicked';
                break; //exit loop
            }
        }
    }

    //window.chrome.webview.postMessage("result:" + result);
    return result;
}