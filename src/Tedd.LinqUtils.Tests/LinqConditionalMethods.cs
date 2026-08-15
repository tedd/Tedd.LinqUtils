using System.Linq;

using Xunit;

namespace Tedd.LinqUtils.Tests;

public class LinqConditionalMethods
{
    [Fact]
    public void IfTest()
    {
        var items = new int[] { 1, 2, 3, 4 };
        var filtered = items

            .If(true, x => x.Select(s => s + 1))
            .Else(x => x.Select(s => s + 2))

            .ToArray();

        for (var i = 0; i < items.Length; i++)
            Assert.Equal(items[i] + 1, filtered[i]);
    }

    [Fact]
    public void ElseIfTest()
    {
        var items = new int[] { 1, 2, 3, 4 };
        var filtered = items

            .If(false, x => x.Select(s => s + 10))
            .ElseIf(true, x => x.Select(s => s + 100))
            .Else(x => x.Select(s => s + 2))

            .ToArray();

        for (var i = 0; i < items.Length; i++)
            Assert.Equal(items[i] + 100, filtered[i]);
    }

    [Fact]
    public void IfIfElseIfElseTest()
    {
        var items = new int[] { 1, 2, 3, 4 };
        var filtered = items

            .If(false, x => x.Select(s => s + 1000))
            .ElseIf(false, x => x.Select(s => s + 10000))
            .ElseIf(false, x => x.Select(s => s + 100000))
            .Else(x => x.Select(s => s + 1000000))

            .ToArray();

        for (var i = 0; i < items.Length; i++)
            Assert.Equal(items[i] + 1000000, filtered[i]);
    }

    [Fact]
    public void If_NullSource_ThrowsArgumentNullException()
    {
        int[]? items = null;
        Assert.Throws<System.ArgumentNullException>("source", () => items!.If(true, x => x));
    }

    [Fact]
    public void If_NullAction_ThrowsArgumentNullException()
    {
        var items = new int[] { 1, 2, 3, 4 };
        System.Func<System.Collections.Generic.IEnumerable<int>, System.Collections.Generic.IEnumerable<int>>? action = null;
        Assert.Throws<System.ArgumentNullException>("linqAction", () => items.If(true, action!));
    }

    [Fact]
    public void ElseIf_NullAction_ThrowsArgumentNullException()
    {
        var items = new int[] { 1, 2, 3, 4 };
        var conditional = items.If(false, x => x);
        System.Func<System.Collections.Generic.IEnumerable<int>, System.Collections.Generic.IEnumerable<int>>? action = null;
        Assert.Throws<System.ArgumentNullException>("linqAction", () => conditional.ElseIf(true, action!));
    }

    [Fact]
    public void Else_NullAction_ThrowsArgumentNullException()
    {
        var items = new int[] { 1, 2, 3, 4 };
        var conditional = items.If(false, x => x);
        System.Func<System.Collections.Generic.IEnumerable<int>, System.Collections.Generic.IEnumerable<int>>? action = null;
        Assert.Throws<System.ArgumentNullException>("linqAction", () => conditional.Else(action!));
    }

    [Fact]
    public void ConditionalIEnumerable_IEnumerableGetEnumerator()
    {
        var items = new int[] { 1, 2, 3, 4 };
        var conditional = items.If(true, x => x);

        System.Collections.IEnumerable enumerable = conditional;
        var enumerator = enumerable.GetEnumerator();

        var list = new System.Collections.Generic.List<int>();
        while (enumerator.MoveNext())
        {
            list.Add((int)enumerator.Current);
        }

        Assert.Equal(items.Length, list.Count);
        for (var i = 0; i < items.Length; i++)
            Assert.Equal(items[i], list[i]);
    }

}
