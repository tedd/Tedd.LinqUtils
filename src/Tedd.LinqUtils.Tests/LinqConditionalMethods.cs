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

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GetEnumeratorTest(bool condition)
    {
        var items = new int[] { 1, 2, 3, 4 };
        System.Collections.IEnumerable conditional = items.If(condition, x => x.Select(s => s + 1));

        var enumerator = conditional.GetEnumerator();
        Assert.NotNull(enumerator);

        var count = 0;
        while (enumerator.MoveNext())
        {
            var value = (int)enumerator.Current;
            Assert.Equal(items[count] + (condition ? 1 : 0), value);
            count++;
        }
        Assert.Equal(4, count);
    }
}
