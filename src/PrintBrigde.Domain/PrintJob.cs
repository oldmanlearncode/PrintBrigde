namespace PrintBrigde.Domain;

public enum JobStatus { Received, Validating, Queued, Rendering, SubmittedToSpooler, Completed, Failed, Cancelled, Unknown }
public enum PaperSize { A4 }
public enum PrintOrientation { Portrait, Landscape }
public enum PrintSides { OneSided }

public sealed record PrintOptions(int Copies = 1, PaperSize PaperSize = PaperSize.A4,
    PrintOrientation Orientation = PrintOrientation.Portrait, PrintSides Sides = PrintSides.OneSided);

public sealed record PrintJob(
    Guid Id, string ClientId, string ClientJobId, string PrinterId, string DocumentName,
    string ArtifactPath, string Sha256, long SizeBytes, PrintOptions Options,
    JobStatus Status, DateTimeOffset SubmittedUtc, DateTimeOffset UpdatedUtc,
    int? WindowsSpoolJobId = null, string? FailureCode = null, string? FailureMessage = null);
