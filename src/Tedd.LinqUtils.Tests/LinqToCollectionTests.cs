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
        var hashSet = items.ToHashSet();
        Assert.Equal(3, hashSet.Count);
        foreach (var item in items)
            Assert.True(hashSet.Contains(item));
    }

    [Fact]
    public void ToHashSetComparerTest()
    {
        var items = new ComparableObjectInt[] { new(1), new(2), new(3), new(3) };
        var hashSet = items.ToHashSet(new ComparableObjectIntComparer());
        Assert.Equal(3, hashSet.Count);
        for (var i = 1; i < 4; i++)
            Assert.True(hashSet.Contains(new(i)));
    }

    [Fact]
    public void ToConcurrentQueueTest()
    {
        var i = -1;
        var items = new[] { 1, 2, 3 };
        var queue = items.ToConcurrentQueue();
        while (queue.TryDequeue(out var item))
        {
            i++;
            Assert.Equal(items[i], item);
        }
    }

    [Fact]
    public void ToConcurrentStackTest()
    {
        var items = new[] { 1, 2, 3 };
        var i = items.Length;
        var stack = items.ToConcurrentStack();
        while (stack.TryPop(out var item))
        {
            i--;
            Assert.Equal(items[i], item);
        }
    }

    [Fact]
    public void ToConcurrentBagTest()
    {
        var items = new[] { 1, 2, 3, 3 };
        var bag = items.ToConcurrentBag();
        Assert.Equal(4, bag.Count);
        foreach (var item in items)
            Assert.Contains(item, bag);
    }
}
