
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

function openPdfInNewTab(filename, bytesBase64) {
    try {
        const byteCharacters = atob(bytesBase64);
        const byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);

        // Cria um Blob a partir dos bytes
        const blob = new Blob([byteArray], { type: 'application/pdf' });

        // Cria uma URL temporária para o Blob
        const blobUrl = URL.createObjectURL(blob);

        // Abre a URL temporária em uma nova aba
        const newWindow = window.open(blobUrl, '_blank');

        if (!newWindow) {
            // Pop-up bloqueado?
            alert("O navegador bloqueou a abertura do relatório em uma nova janela. Por favor, verifique se há bloqueadores de pop-up.");
            // Opcional: oferecer download como alternativa se a nova janela falhar
            // saveAsFile(filename, bytesBase64);
        } else {
            // Libera a URL temporária quando a nova janela for fechada (ou após um tempo razoável)
            // Esta parte é mais complexa de detectar fechar, um timeout é mais simples,
            // mas a URL ficará ativa até o timeout ou a página principal fechar.
            // Para simplificar, vamos confiar que o navegador lida bem com URLs temporárias
            // ou adicionar um timeout maior, ou omitir o revokeObjectURL aqui se for problemático.
            setTimeout(() => {
                URL.revokeObjectURL(blobUrl);
            }, 5000); // Exemplo: libera a URL após 5 segundos. Ajuste conforme necessário.
        }


    } catch (e) {
        console.error("Erro ao tentar abrir o arquivo em nova aba:", e);
        alert("Ocorreu um erro ao gerar ou exibir o relatório."); // Feedback para o usuário
    }
}

