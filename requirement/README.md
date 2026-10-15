# Requirements index

All requirements are contracts to implement, **not a claim that code already works**.

| File | Scope |
|---|---|
| [01-architecture](01-architecture.md) | System boundaries and deliverables |
| [02-print-jobs](02-print-jobs.md) | Submission, queue, lifecycle, idempotency |
| [03-http-api](03-http-api.md) | REST endpoints and wire contracts |
| [04-windows-printing](04-windows-printing.md) | PDF rendering, Windows spooler and CAPT |
| [05-ipp-cups](05-ipp-cups.md) | CUPS/IPP network printer frontend |
| [06-security-operations](06-security-operations.md) | Security, observability and deployment |
| [07-test-cases](07-test-cases.md) | Acceptance tests and traceability |

Priority: **P0** required for MVP, **P1** next release, **P2** optional.

Acceptance target: on an Ubuntu client **without CAPT**, Ctrl+P selects PrintBridge and an A4 PDF/document prints correctly through the USB Canon LBP2900 attached to the Windows VM. Driverless behavior on Windows/macOS/mobile requires separate validation; CUPS sharing alone does not prove IPP Everywhere, AirPrint or Mopria conformance.
