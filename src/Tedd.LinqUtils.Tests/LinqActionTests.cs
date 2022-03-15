using System;

using FluentAssertions;

using Xunit;

namespace Tedd.LinqUtils.Tests;

public class LinqActionTests
{
    [Fact]
    public void ActionTest()
    {
        var count = 0;
        var items = new[] { 1, 2, 3 };
        items.Action(n => Assert.Equal(items[count++], n));
        Assert.Equal(items.Length, count);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public void ForEachTest(int size)
    {
        var count = 0;
        var items = new int[size];
        var rnd = new Random();
        for (var i = 0; i < size; i++)
            items[i] = rnd.Next();

        items.Action(n => Assert.Equal(items[count++], n));
        Assert.Equal(size, items.Length);
        Assert.Equal(items.Length, count);
    }
}
