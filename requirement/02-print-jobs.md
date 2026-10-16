# R-JOB: Print job lifecycle

## Data contract

PrintJob: GUID Id; ClientJobId (stable client-supplied idempotency key scoped by authenticated client); DocumentName; PrinterId; PDF path; SHA-256; bytes; Copies (1..99); PaperSize (A4 MVP); Orientation; Sides (OneSided); SubmittedUtc; UpdatedUtc; Status; optional WindowsSpoolJobId; FailureCode; FailureMessage. Credentials, API keys and uploaded PDF content must never appear in logs.

## States

`Received -> Validating -> Queued -> Rendering -> SubmittedToSpooler -> [Completed | Failed | Unknown]`. `Queued` can become `Cancelled`; `Rendering` and `SubmittedToSpooler` may accept **best-effort** cancellation. `Completed` means the chosen Windows integration reported job completion, **not verified physical output**. Status mapping and its limitations must be documented.

## Requirements

- [P0, JOB-01] Reject empty/non-PDF uploads, oversized files, malformed copies/paper/orientation, unavailable printer IDs and malicious paths before enqueue.
- [P0, JOB-02] Stream upload into private staging storage; verify size, PDF signature plus renderer validation, and SHA-256; atomically promote to permanent spool path.
- [P0, JOB-03] Persist job metadata before returning 202 Accepted.
- [P0, JOB-04] At most one active render/spool dispatch per printer; fair FIFO within printer.
- [P0, JOB-05] `ClientJobId` + client identity uniquely identifies a request. Same payload/attributes returns existing job; conflicting payload returns 409. Never silently duplicate.
- [P0, JOB-06] Preserve validated print attributes and apply copies **exactly once** (frontend OR Windows renderer, not both).
- [P0, JOB-07] Cancellation of a queued job prevents dispatch. After spool submission, outcome may remain Unknown.
- [P0, JOB-08] Record clear failure reason and transitions, with timestamp and correlation ID.
- [P1, JOB-09] On crash recovery, previously unsubmitted Queued jobs may resume; in-flight/ambiguous submissions go Unknown pending reconciliation, never auto-reprint.
- [P1, JOB-10] Configurable retention/cleanup for files and metadata; never delete an active job's PDF.
- [P1, JOB-11] Distinguish printer offline, renderer failure, timeout, invalid PDF, failed spool submission and unknown outcome.
- [P1, JOB-12] Track one logical job per requested document; reject unsupported collate/duplex rather than misrepresent.
- [P2, JOB-13] History search and aggregated stats.

## Interfaces

`IPrintJobRepository`, `IPrintJobService`, `IJobArtifactStore`, `IPrintDispatcher`, `IClock`. Implementations must be cancellation-token-aware.

## Guarantees

Exactly-once **physical** printing cannot be guaranteed after a crash or lost spooler acknowledgment. Document the resulting at-most-once automatic-retry policy for ambiguous jobs.
