using System;
using System.Linq;

using FluentAssertions;

using Xunit;

namespace Tedd.LinqUtils.Tests;

public class LinqUtilsTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public void CountToTest(int size)
    {
        var items = new int[size];
        for (var i = 0; i < size; i++)
            items[i] = i;
        var totalCount = 0;
        var filteredCount = 0;
        var filtered = items
            .CountTo(x => totalCount = x)
            .Where(w => w >= 10)
            .CountTo(x => filteredCount = x)
            .ToArray();
        
        totalCount.Should().Be(size);
        filteredCount.Should().Be(Math.Max(0,size-10));
        filteredCount.Should().Be(filtered.Length);
    }
}