using System;
using System.Drawing;
using System.Linq;

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
        var remaining = items.Action(n => Assert.Equal(items[count++], n)).ToArray();

        items.Should().HaveCount(count);
        items.Should().Equal(remaining);
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

        var remaining = items.Action(n => Assert.Equal(items[count++], n)).ToArray();

        items.Should().HaveCount(size);
        count.Should().Be(size);
        items.Should().Equal(remaining);
    }
}
