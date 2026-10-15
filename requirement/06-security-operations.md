# R-OPS: Security, deployment, observability

## Security

- [P0, SEC-01] Never expose Windows Agent to the public Internet; firewall inbound to CUPS frontend and administrators only.
- [P0, SEC-02] Authenticate every submission and private job retrieval; store hashed/secured client credentials, rotate without downtime where possible.
- [P0, SEC-03] Bound upload size (default 50 MiB), PDF validation, concurrency, timeouts, and spool storage quotas; reject path traversal.
- [P0, SEC-04] No arbitrary shell commands or caller-selected executable/driver paths; printer allowlist.
- [P0, SEC-05] Secrets only from runtime configuration/secret store; logs never include PDF content or credentials.
- [P1, SEC-06] TLS between frontend and Agent when traffic crosses non-isolated networks. A private LAN alone does not encrypt HTTP.

## Reliability/operations

- [P0, OPS-01] Structured logs with timestamp, job ID, client ID, trace ID, transition and error code.
- [P0, OPS-02] Readiness distinguishes server alive from printer online and from spooler functioning.
- [P0, OPS-03] Graceful shutdown finishes/persists current state without accidental retry.
- [P1, OPS-04] SQLite store for jobs and private on-disk artifacts, cleanup policy and backup/restore documentation.
- [P1, OPS-05] Configuration: printer allowlist, maximum PDF size, spool root, job TTL, renderer binary path, render timeout, API bind address, auth keys.
- [P1, OPS-06] Start automatically with Windows VM; document Windows Service vs logged-in worker tradeoffs.
- [P1, OPS-07] Expose actionable offline/out-of-paper/spooler errors when Windows reports them; tolerate unsupported status reports.

## Deployment checklist

Proxmox USB passthrough; Windows VM networking on trusted bridge; Canon CAPT driver installed; successful local test print; dedicated restricted service account; Windows Defender firewall rules; fixed LAN address; backup and snapshots; no RDP exposure to Internet.
