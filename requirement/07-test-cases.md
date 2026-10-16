# R-TEST: Test plan and acceptance

## Unit / contract tests

| ID | Given | When | Expected |
|---|---|---|---|
| T-001 | Valid A4 1-copy job | Validate | Accepted |
| T-002 | Copies 0 or 100 | Validate | Rejected |
| T-003 | Unsupported duplex | Validate | Rejected |
| T-004 | Missing printer ID | Validate | Rejected |
| T-005 | Same client/key/payload | Submit twice | Same job, no duplicate |
| T-006 | Same client/key/different payload | Submit | 409 conflict |
| T-007 | Two jobs same printer | Dispatch | Never run concurrently |
| T-008 | Two different printers | Dispatch | Independent printer locks |
| T-009 | Queued job | Cancel | Never sent to renderer |
| T-010 | Job submitted to spooler | Crash/restart | Unknown until reconciled; never blind retry |
| T-011 | Unauthorized request | Submit | 401/403, no spool file |
| T-012 | Oversize or corrupt PDF | Submit | Rejected, partial temp file cleaned |
| T-013 | Renderer timeout | Dispatch | Bounded failure/unknown, process cleanup |
| T-014 | Spooler unavailable | Dispatch | Actionable error; preserve ambiguity policy |
| T-015 | Job file path containing ../ | Submit | Sanitized/rejected |
| T-016 | API health | Call | No credentials or local paths leaked |

These are requirement-driven cases. Current tests are **initial placeholder/validation tests**, not proof of end-to-end printing.

## Integration tests (hardware required)

| ID | Scenario | Acceptance |
|---|---|---|
| H-001 | Direct test page on Windows VM | Correct A4 output |
| H-002 | Windows agent prints PDF with Vietnamese diacritics | Correct glyphs |
| H-003 | PDF with pictures and charts | No clipping/loss |
| H-004 | Two sequential jobs | Ordered, once each |
| H-005 | Disconnect/reconnect USB | Diagnosed, recoverable |
| H-006 | Printer paper empty/offline | Visible reliable failure/unknown; no duplicate reprint |
| H-007 | Windows restart while job in progress | No blind retry |
| H-008 | Ubuntu clean client Firefox Ctrl+P | In correct layout without CAPT |
| H-009 | Fedora and Arch CUPS clients | Same |
| H-010 | Windows IPP class driver | Test as separately scoped P1 |
| H-011 | macOS/iOS/Android clients | Test separately; not inferred from IPP alone |
| H-012 | Multiple clients submit jobs | FIFO, no loss, no duplicate |

## Definition of Done (MVP)

1. Windows direct printing works.
2. PDF posted to Windows Agent is rendered through Canon driver, one physical job; metadata validates.
3. Safe queue, no simultaneous same-printer job, idempotency, bounded upload.
4. Errors/statuses accurate about limitations.
5. Ubuntu without CAPT uses Ctrl+P via CUPS queue; PDF + website + document test cases pass.
6. Tests/build run in CI and hardware-only tests are documented/manual.
