using System;
using System.Linq;

using Xunit;

namespace Tedd.LinqUtils.Tests;

public class LinqStringTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public void StringJoinStringTest(int size)
    {
        var items = new int[size];
        var rnd = new Random();
        for (var i = 0; i < size; i++)
            items[i] = rnd.Next();

        var str = items.Select(s => s.ToString()).StringJoin(", ");
        Assert.Equal(string.Join(", ", items), str);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public void StringJoinCharTest(int size)
    {
        var items = new int[size];
        var rnd = new Random();
        for (var i = 0; i < size; i++)
            items[i] = rnd.Next();

        var str = items.Select(s => s.ToString()).StringJoin(',');
        Assert.Equal(string.Join(',', items), str);
    }
}
