using PrintBrigde.Application;
using PrintBrigde.Domain;

namespace PrintBrigde.Infrastructure;

// Deliberate scaffolding: implement following requirement/04-windows-printing.md.
// Do not substitute raw PDF bytes for printer-language data.
public sealed class WindowsPdfRenderer : IPdfPrintRenderer
{
    public Task<PrintResult> RenderAndSubmitAsync(string pdfPath, PrinterInfo printer,
        PrintOptions options, CancellationToken cancellationToken)
        => throw new NotImplementedException("WIN-02..06: use a PDF renderer through installed Windows printer driver.");
}

public sealed class WindowsPrinterCatalog : IPrinterCatalog
{
    public Task<IReadOnlyList<PrinterInfo>> ListAsync(CancellationToken cancellationToken)
        => throw new NotImplementedException("WIN-01: enumerate and allowlist installed printers.");

    public Task<PrinterInfo?> GetAsync(string printerId, CancellationToken cancellationToken)
        => throw new NotImplementedException("WIN-01: resolve printer safely.");
}
