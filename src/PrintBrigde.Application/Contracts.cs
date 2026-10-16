using PrintBrigde.Domain;

namespace PrintBrigde.Application;

// See requirement/02-print-jobs.md (JOB-01..12).
public sealed record SubmitJobCommand(string ClientId, string ClientJobId, string PrinterId,
    string DocumentName, PrintOptions Options, Stream PdfContent);
public sealed record PrintResult(int? SpoolJobId, bool Submitted, string? Detail = null);
public sealed record PrinterInfo(string Id, string DisplayName, bool IsAvailable);

public interface IPrintJobService
{
    Task<PrintJob> SubmitAsync(SubmitJobCommand command, CancellationToken cancellationToken);
    Task<PrintJob?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> CancelAsync(Guid id, CancellationToken cancellationToken);
}

public interface IPrintJobRepository
{
    Task<PrintJob?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<PrintJob?> FindByClientKeyAsync(string clientId, string clientJobId, CancellationToken cancellationToken);
    Task AddAsync(PrintJob job, CancellationToken cancellationToken);
    Task UpdateAsync(PrintJob job, CancellationToken cancellationToken);
    Task<IReadOnlyList<PrintJob>> GetQueuedAsync(string printerId, CancellationToken cancellationToken);
}

public interface IJobArtifactStore
{
    // Returns private persisted path and digest; implementations validate size and PDF payload.
    Task<(string Path, string Sha256, long Size)> SavePdfAsync(Stream input, CancellationToken cancellationToken);
    Task DeleteAsync(string path, CancellationToken cancellationToken);
}

public interface IPrinterCatalog
{
    Task<IReadOnlyList<PrinterInfo>> ListAsync(CancellationToken cancellationToken);
    Task<PrinterInfo?> GetAsync(string printerId, CancellationToken cancellationToken);
}

// See requirement/04-windows-printing.md. A successful renderer exit != paper printed.
public interface IPdfPrintRenderer
{
    Task<PrintResult> RenderAndSubmitAsync(string pdfPath, PrinterInfo printer,
        PrintOptions options, CancellationToken cancellationToken);
}

public interface IWindowsPrintSpooler
{
    Task<JobStatus> GetSubmissionStatusAsync(string printerId, int spoolJobId, CancellationToken cancellationToken);
    Task<bool> TryCancelAsync(string printerId, int spoolJobId, CancellationToken cancellationToken);
}

public interface IPrintDispatcher
{
    Task DispatchNextAsync(string printerId, CancellationToken cancellationToken);
}

public interface IClock
{
    DateTimeOffset UtcNow { get; }
}
