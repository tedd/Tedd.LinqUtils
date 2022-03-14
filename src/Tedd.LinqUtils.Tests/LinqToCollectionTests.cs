using System;

using Xunit;

namespace Tedd.LinqUtils.Tests;

public class LinqToCollectionTests
{
    [Fact]
    public void ToQueueTest()
    {
        var i = -1;
        var items = new[] { 1, 2, 3 };
        var queue = items.ToQueue();
        while (queue.TryDequeue(out var item))
        {
            i++;
            Assert.Equal(items[i], item);
        }
    }

    [Fact]
    public void ToStackTest()
    {
        var items = new[] { 1, 2, 3 };
        var i = items.Length;
        var stack = items.ToStack();
        while (stack.TryPop(out var item))
        {
            i--;
            Assert.Equal(items[i], item);
        }
    }

    [Fact]
    public void ToHashSetTest()
    {
        var items = new[] { 1, 2, 3, 3 };
        var stack = items.ToHashSet();
        Assert.Equal(3, stack.Count);
        foreach (var item in items)
            Assert.True(stack.Contains(item));
    }

    [Fact]
    public void ToHashSetComparerTest()
    {
        var items = new ComparableObjectInt[] { new(1), new(2), new(3), new(3) };
        var stack = items.ToHashSet(new ComparableObjectInt());
        for (var i = 1; i < 4; i++)
            Assert.True(stack.Contains(new(i)));
    }
}
