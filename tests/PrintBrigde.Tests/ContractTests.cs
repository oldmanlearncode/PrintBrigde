using PrintBrigde.Domain;
using Xunit;

namespace PrintBrigde.Tests;

public sealed class ContractTests
{
    [Fact]
    public void DefaultOptionsAreA4OneSidedOneCopy()
    {
        var options = new PrintOptions();
        Assert.Equal(1, options.Copies);
        Assert.Equal(PaperSize.A4, options.PaperSize);
        Assert.Equal(PrintSides.OneSided, options.Sides);
    }

    [Fact]
    public void JobModelCanRepresentAmbiguousSpoolerOutcome()
    {
        Assert.Equal(JobStatus.Unknown, Enum.Parse<JobStatus>("Unknown"));
    }

    // TODO: T-001..016 from requirement/07-test-cases.md.
    // Add real tests as services and validation are implemented.
}
