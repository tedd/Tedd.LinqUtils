using System;
using System.Linq;

using Xunit;

namespace Tedd.LinqUtils.Tests;

public class LinqFilterTests
{
    [Fact]
    public void WhereNotNullTest()
    {
        var items = new string?[] { string.Empty, "", null, "  ", "A" };
        var newItems = items!.WhereIsNotNull().ToArray();

        Assert.Equal(4, newItems.Length);
        Assert.DoesNotContain(null, newItems);
    }

    [Fact]
    public void WhereNotNullOrEmptyTest()
    {
        var items = new string?[] { string.Empty, "", null, "  ", "A" };
        var newItems = items!.WhereIsNotNullOrEmpty().ToArray();

        Assert.Equal(2, newItems.Length);
        Assert.DoesNotContain(null, newItems);
        Assert.DoesNotContain("", newItems);
        Assert.DoesNotContain(string.Empty, newItems);
    }

    [Fact]
    public void WhereNotNullOrWhiteSpaceTest()
    {
        var items = new string?[] { string.Empty, "", null, "  ", "A" };
        var newItems = items!.WhereIsNotNullOrWhiteSpace().ToArray();

        Assert.Single(newItems);
        Assert.DoesNotContain(null, newItems);
        Assert.DoesNotContain("", newItems);
        Assert.DoesNotContain(string.Empty, newItems);
        Assert.DoesNotContain("  ", newItems);
    }
}
