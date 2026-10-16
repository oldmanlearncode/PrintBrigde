# R-API: HTTP agent contract

Base: `/api/v1`. JSON errors use ProblemDetails. LAN-only authenticated requests; request size and timeouts are bounded.

## Endpoints

| Method | Path | Behavior |
|---|---|---|
| GET | /health/live | Process liveness, no secrets |
| GET | /health/ready | Persistence/queue/renderer readiness |
| GET | /printers | Installed printer IDs, capabilities and conservative status |
| GET | /printers/{id} | Printer details |
| POST | /jobs | Multipart PDF and metadata; return 202 + Location |
| GET | /jobs/{id} | Status and safe metadata |
| GET | /jobs | Paginated/filterable listing (P1) |
| DELETE | /jobs/{id} | Request cancellation; distinguish already dispatched |

## POST /jobs

Multipart form fields: `document` (application/pdf); `metadata` (application/json) with:
```json
{
  "clientJobId": "67a65489-6884-480d-bc4f-37ec0ac5aebb",
  "printerId": "canon-lbp2900",
  "documentName": "report.pdf",
  "copies": 1,
  "paperSize": "A4",
  "orientation": "Portrait",
  "sides": "OneSided"
}
```
Do not trust `documentName` as a filesystem path. Derive identity from authenticated client, not client-supplied display name.

Success: `202 Accepted`, body `{ "id": "...", "status": "Queued" }`, `Location: /api/v1/jobs/{id}`. Idempotent repeat can return 200 with existing status. Conflicting repeat -> 409.

Bad PDF/metadata -> 400/422; too large -> 413; unauthorized -> 401; forbidden -> 403; unsupported printer/settings -> 422; storage unavailable -> 503; overloaded -> 429/503. Return `traceId` where possible; never leak a local path.

## Requirements

- [P0, API-01] Validate all inputs and consistent RFC 7807 style errors (ASP.NET ProblemDetails).
- [P0, API-02] Authentication must be mandatory for write and private read endpoints, using configured client-scoped API key or stronger equivalent; constant-time key comparison, no plaintext key in repository.
- [P0, API-03] Streaming uploads with byte limit and maximum simultaneous submissions.
- [P0, API-04] Stable JSON status vocabulary documented by API.
- [P0, API-05] OpenAPI describes endpoints and error cases; don't expose unauthenticated Swagger on shared LAN by default.
- [P1, API-06] Pagination, metrics/health details, versioned authentication rotation.
- [P1, API-07] Support polling status and correlation ID propagation.

`POST /jobs` is not an IPP endpoint. IPP is handled by the frontend, not the Windows API.
