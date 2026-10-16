# PrintBrigde

Contract-first .NET 8 Windows Print Agent for a Canon LBP2900 USB printer attached to a Proxmox Windows VM.

**Status:** project skeleton; no real printing, persistence, IPP or document conversion implemented.

## Architecture

Client (Linux/Windows/macOS via IPP) -> CUPS frontend (future) -> authenticated HTTP PDF upload -> Windows Agent -> PDF renderer -> Windows spooler -> Canon CAPT driver -> USB printer.

The Windows agent **must not send PDF bytes as RAW spool data**; the PDF must be rendered with the installed printer driver.

## Documentation

Read [requirements index](requirement/README.md), [architecture](requirement/01-architecture.md), [jobs](requirement/02-print-jobs.md), [API](requirement/03-http-api.md), [printing](requirement/04-windows-printing.md), [CUPS compatibility](requirement/05-ipp-cups.md), [operations](requirement/06-security-operations.md), and [test cases](requirement/07-test-cases.md).

## Build

Requires .NET 8 SDK.

```bash
dotnet restore PrintBrigde.sln
dotnet build PrintBrigde.sln
dotnet test PrintBrigde.sln
```

The API starts in a **scaffold-only** state; no operational printing endpoints are wired yet. Never expose the API to the Internet.

## Roadmap

1. Install Canon CAPT and verify direct Windows printing on the VM.
2. Implement PDF validation, durable spool storage, job state machine and worker.
3. Implement renderer/spooler adapter and verify physical output.
4. Implement authenticated HTTP endpoints, cancellation, status and recovery.
5. Integrate CUPS queue, PDF conversion and IPP client discovery.
6. Validate Windows/Linux/macOS clients, then investigate AirPrint/Mopria support.

**Spelling:** GitHub repository is intentionally `PrintBrigde`; product name in docs is PrintBridge.
