# R-WIN: Windows printing integration

## Pipeline

PDF file -> installed PDF renderer (e.g. SumatraPDF) -> Windows printing/GDI pipeline -> Windows Print Spooler -> Canon CAPT driver -> USB passthrough -> LBP2900.

**Never** use WritePrinter with raw PDF bytes as if the LBP2900 natively understood PDF.

## Requirements

- [P0, WIN-01] Enumerate installed Windows printers and use an unambiguous configured printer ID; reject unknown/unavailable selections.
- [P0, WIN-02] Implement `IPdfPrintRenderer` adapter; safely construct process arguments; never evaluate an arbitrary command/shell.
- [P0, WIN-03] Honor A4, orientation, copy count once, monochrome/one-sided limits. If an option cannot be honored reliably, fail explicitly.
- [P0, WIN-04] Execute a PDF through the installed printer driver; capture exit code, timeout, standard error where available, and Windows spool job correlation if available.
- [P0, WIN-05] Serialize jobs per physical printer. An exit code from SumatraPDF alone must **not** be equated with printed paper.
- [P0, WIN-06] Support cancellation token/timeouts without orphaning processes or corrupting queued jobs.
- [P1, WIN-07] Query Windows Spooler status through `IWindowsPrintSpooler` and reconcile job completion/cancel/offline signals conservatively.
- [P1, WIN-08] Document interactive-session vs Session 0 issues. Verify that PDF renderer works in the actual deployed account/session.
- [P1, WIN-09] Handle offline USB disconnect/reconnect and shutdown/restart.
- [P1, WIN-10] Windows service installation, startup and restricted service account.

## Hardware acceptance

Windows VM can print a test page before any Agent code is considered ready. Verify multiple sequential A4 PDF jobs; Unicode accents, vector graphics, images, blank pages, mixed portrait/landscape. LBP2900 does not support automatic duplex or color.

## Implementation note

Rendering and Spooler reconciliation may require two separate adapters. Do not pretend SumatraPDF always exposes reliable spooler job IDs. Tests must use mocks until Windows hardware is available.
