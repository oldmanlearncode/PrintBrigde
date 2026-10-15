using Xunit;

namespace PrintBrigde.Tests;

/// <summary>
/// Executable backlog: remove Skip only once the required component and assertion exist.
/// See requirement/07-test-cases.md for Given/When/Then and hardware cases.
/// These are NOT passing tests or implementations.
/// </summary>
public sealed class PendingAcceptanceTests
{
    [Theory(Skip = "Pending implementation. See requirement/07-test-cases.md.")]
    [InlineData("T-001", "Valid A4 job accepted")]
    [InlineData("T-002", "Reject copies outside 1..99")]
    [InlineData("T-003", "Reject duplex")]
    [InlineData("T-004", "Reject missing printer")]
    [InlineData("T-005", "Idempotent duplicate does not print twice")]
    [InlineData("T-006", "Conflicting idempotency payload returns 409")]
    [InlineData("T-007", "Serialize same-printer jobs")]
    [InlineData("T-008", "Independent queues for separate printers")]
    [InlineData("T-009", "Queued cancellation prevents dispatch")]
    [InlineData("T-010", "Crash while submitting never causes blind retry")]
    [InlineData("T-011", "Unauthorized submissions rejected")]
    [InlineData("T-012", "Invalid or oversized PDF rejected")]
    [InlineData("T-013", "Renderer timeout handled and process cleaned")]
    [InlineData("T-014", "Spooler outage produces reliable status")]
    [InlineData("T-015", "Unsafe paths rejected")]
    [InlineData("T-016", "Health response does not leak secrets")]
    public void PendingAcceptanceScenario(string id, string description)
    {
        Assert.Fail($"Implement {id}: {description}");
    }
}
