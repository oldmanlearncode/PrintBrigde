# R-ARCH: System architecture

## Goal

Turn a Canon LBP2900 (USB/CAPT) attached to a Proxmox Windows VM into a LAN-accessible network printer. Clients must not need the Canon driver; ordinary Ctrl+P must ultimately work for supported applications.

## Boundaries

1. **IPP frontend (Linux, future):** a CUPS printer queue accepts jobs and print attributes; DNS-SD advertising and compliant document-format negotiation are optional until validated. A filter converts input to printable PDF; a backend submits it to the Agent with metadata and stable request/job identity.
2. **Windows Print Agent (.NET 8):** secure HTTP API, validation, local durable job store and spool files, serialized worker, renderer and Windows spooler integration.
3. **Native Windows printing:** an installed PDF application/renderer invokes the **installed Canon CAPT driver** via the OS printing pipeline. Do not implement CAPT or send a raw PDF directly to the USB device.
4. **Physical printer:** USB passthrough from Proxmox VM host to Windows VM.

## Dependency direction

Domain -> no project references. Application -> Domain. Infrastructure -> Application + Domain. Api -> Application + Infrastructure. Tests may depend on relevant production projects. Interfaces live in Application (ports), no Windows-specific APIs in Domain.

## Implementation requirements

- [P0, ARCH-01] The core job lifecycle must be independent of Win32 APIs so it can be unit-tested on Linux.
- [P0, ARCH-02] Renderer, job repository, artifact store, printer discovery, spooler and clock are replaceable interfaces.
- [P0, ARCH-03] Single active print worker per printer. Document concurrency across multiple printers separately.
- [P0, ARCH-04] Windows Agent accepts **PDF only** in MVP; conversion of other document types belongs to the frontend/client print pipeline.
- [P0, ARCH-05] Expose truthful statuses. Spooler acknowledgment is not proof of paper output.
- [P1, ARCH-06] Recover persisted jobs safely after service restart.
- [P1, ARCH-07] IPP frontend and Windows Agent may deploy to separate VMs/LXCs within a trusted LAN.

## Non-goals (MVP)

No CAPT reverse engineering; no browser dashboard; no public Internet access; no direct DOCX/XLSX rendering in Windows Agent; no automatic duplex or color printing; no assumption that IPP Anywhere/AirPrint/Mopria compliance comes free with CUPS.

## Initial milestone

First verify local printing from Windows VM (driver, USB passthrough, test page). Actual gateway implementation is subsequent work.
