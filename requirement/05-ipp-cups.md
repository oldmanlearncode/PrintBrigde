# R-IPP: Driverless CUPS frontend (separate deployment)

## Scope

The CUPS frontend is NOT implemented in the .NET skeleton. It is tracked here as a separate Linux integration milestone.

Client application -> CUPS/IPP queue -> document conversion/filter -> custom HTTP backend -> Windows Agent PDF API.

## Requirements

- [P0, IPP-01] User prints via ordinary Ctrl+P; no manual export/upload of PDF.
- [P0, IPP-02] Ubuntu, Fedora, Arch and Debian clients should be able to add a network printer without Canon CAPT. Confirm on real clean installations.
- [P0, IPP-03] CUPS queue must receive supported client formats and reliably emit PDF with correct font embedding, rasterization, layouts and page selection.
- [P0, IPP-04] Preserve print attributes (A4, orientation, copies, page range). Prevent duplicate copies or page selection being applied twice.
- [P0, IPP-05] Backend forwards PDF and metadata with client-scoped authentication and stable job ID; handle Windows Agent outage without unsafe duplicate printing.
- [P0, IPP-06] Job errors/cancellation must be surfaced honestly to CUPS where possible; submitted-to-spooler is not guaranteed physical completion.
- [P1, IPP-07] DNS-SD/Avahi discovery and IPP Everywhere-compatible advertised capabilities/document formats must be **validated**, not assumed.
- [P1, IPP-08] Evaluate PWG Raster/PDF input and conversion as needed for generic Windows/macOS IPP clients.
- [P2, IPP-09] AirPrint and Mopria capability/profile validation for iOS/Android.

## Compatibility matrix

- Ubuntu/Fedora/Arch/Debian CUPS: P0 acceptance
- Windows 10/11 IPP class driver: P1 acceptance
- macOS: P1 acceptance
- iOS AirPrint / Android Mopria: P2 exploratory

## Deployment notes

Recommend a small Debian/Ubuntu VM or LXC for CUPS; USB is attached **only** to Windows VM. Restrict port 631 to trusted LAN; keep Agent port similarly private. Verify discovery across VLANs rather than assuming mDNS traverses routers.

## Exit criteria

No CAPT package on client. Choose PrintBridge in app print dialog, print web page, text, PDF and image, compare physical pages, validate options and failure cases.
