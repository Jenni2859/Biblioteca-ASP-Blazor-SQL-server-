function loadPdf(frameId, loadingId, url) {
    var iframe = document.getElementById(frameId);
    var loadingMessage = document.getElementById(loadingId);
    if (iframe) {
        iframe.onload = function () {
            iframe.style.display = "block";
            if (loadingMessage) {
                loadingMessage.style.display = "none";
            }
        };
        iframe.src = url;
    }
}

function printIframe(frameId) {
    var iframe = document.getElementById(frameId);
    if (iframe) {
        iframe.contentWindow.focus();
        iframe.contentWindow.print();
    }
}

function downloadFile(url) {
    var link = document.createElement('a');
    link.href = url;
    link.download = 'ReporteLibros.pdf'; // Establece el nombre del archivo descargado
    link.click();
}

