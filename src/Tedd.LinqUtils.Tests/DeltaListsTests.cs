using System.Linq;
using Xunit;

namespace Tedd.LinqUtils.Tests;

public class DeltaListsTests
{
    [Fact]
    public void DeltaLists_BasicTest()
    {
        var first = new[] { 1, 2, 3, 4 };
        var second = new[] { 3, 4, 5, 6 };

        var result = first.DeltaLists(second);

        Assert.Equal(new[] { 1, 2 }, result.OnlyInFirst);
        Assert.Equal(new[] { 5, 6 }, result.OnlyInSecond);
        Assert.Equal(new[] { 3, 4, 3, 4 }, result.InBoth);
    }

    [Fact]
    public void DeltaLists_SelectorTest()
    {
        var first = new[] { 1, 2, 3, 4 };
        var second = new[] { "3", "4", "5", "6" };

        var result = first.DeltaLists(second, x => int.Parse(x));

        Assert.Equal(new[] { 1, 2 }, result.OnlyInFirst);
        Assert.Equal(new[] { "5", "6" }, result.OnlyInSecond);
        Assert.Equal(new[] { 3, 4, 3, 4 }, result.InBoth);
    }
}
