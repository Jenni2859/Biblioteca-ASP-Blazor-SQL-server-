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

function renderLibraryChart(totalBooks, totalAuthors, totalPersons, totalLoans) {
    var ctx = document.getElementById('libraryChart').getContext('2d');

    // Destruir el gráfico anterior si existe
    if (window.myChart) {
        window.myChart.destroy();
    }

    window.myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ['Libros', 'Autores', 'Personas', 'Préstamos'],
            datasets: [{
                label: 'Estadísticas de la Biblioteca',
                data: [totalBooks, totalAuthors, totalPersons, totalLoans],
                backgroundColor: ['#005f73', '#0a9396', '#94d2bd', '#ff9f1c'],
                borderColor: ['#003f5c', '#2f4b7c', '#665191', '#a05195'],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: { beginAtZero: true }
            }
        }
    });
}


