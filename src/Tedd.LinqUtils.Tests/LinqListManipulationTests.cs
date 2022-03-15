using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using FluentAssertions;

using Xunit;

namespace Tedd.LinqUtils.Tests;
public class LinqListManipulationTests
{

    private void RemoveTestInt(int size, IEqualityComparer<ComparableObjectInt>? comparer)
    {
        var rnd = new Random();
        var objs = new ComparableObjectInt[size];
        var otherObjs = new ComparableObjectInt[size];
        for (int i = 0; i < size; i++)
            objs[i] = new(i);
        for (int i = 0; i < size; i++)
            if (rnd.NextBoolean(0.5))
                otherObjs[i] = new(i);
            else
                otherObjs[i] = new(-i);

        var result = objs
            .If(comparer == null, x => x.Remove(otherObjs))
            .If(comparer != null, x => x.Remove(otherObjs, comparer))
            .ToArray();
        var resultFacit = objs
            .ToHashSet().Except(otherObjs, comparer)
            .OrderBy(o => o.Value)
            .ToArray();
        for (var i = 0; i < result.Length; i++)
            result[i].Should().BeEquivalentTo(resultFacit[i]);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public void RemoveTest(int size)
    {
        RemoveTestInt(size, null);
    }

    [Fact]
    public void RemoveComparerTest()
    {
        RemoveTestInt(100, new ComparableObjectIntComparer());
    }
 
    [Fact]
    public void AppendTest()
    {
        var items = new int[] { 0, 1, 2, 3 };
        var appended = items.Append(4).ToArray();
        Assert.Equal(5, appended.Length);
        for (var i = 0; i < appended.Length; i++)
            Assert.Equal(i, appended[i]);
    }


    public void AppendMany(IEnumerable<int> items2)
    {
        var items1 = new int[] { 0, 1, 2, 3 };
        var appended = items1.Append(items2).ToArray();
        Assert.Equal(8, appended.Length);
        for (var i = 0; i < appended.Length; i++)
            Assert.Equal(i, appended[i]);
    }
    [Fact]
    public void AppendArrayTest() => AppendMany(new int[] { 4, 5, 6, 7 });
    [Fact]
    public void AppendListTest() => AppendMany(new List<int> { 4, 5, 6, 7 });
    [Fact]
    public void AppendIEnumerableTest() => AppendMany((new int[] { 4, 5, 6, 7 }).Select(s => s));
    public void PrependMany(IEnumerable<int> items2)
    {
        var items1 = new int[] { 4, 5, 6, 7 };
        var Prepended = items1.Prepend(items2).ToArray();
        Assert.Equal(8, Prepended.Length);
        for (var i = 0; i < Prepended.Length; i++)
            Assert.Equal(i, Prepended[i]);
    }
    [Fact]
    public void PrependArrayTest() => PrependMany(new int[] { 0, 1, 2, 3 });
    [Fact]
    public void PrependListTest() => PrependMany(new List<int> { 0, 1, 2, 3 });
    [Fact]
    public void PrependIEnumerableTest() => PrependMany((new int[] { 0, 1, 2, 3 }).Select(s => s));

    [Fact]
    public void AppendDistinctTest()
    {
        var items1 = new int[] { 4, 5, 6, 7 };
        var items2 = new int[] { 6, 7, 8, 9 };
        var result = items1.AppendDistinct(items2).ToArray();
        Assert.Equal(6, result.Length);
        //result.Should().EndWith(new int[] { 6, 7, 8, 9 });
        result.Should().Equal(new int[] { 4, 5, 6, 7, 8, 9 });
    }

    private void AppendDinstinctComparerArrayTest(int uniqueCount, IEnumerable<ComparableObjectInt> items1, IEnumerable<ComparableObjectInt> items2)
    {
        var comparer = new ComparableObjectIntComparer();


        var result = items1.AppendDistinct(items2, comparer).ToArray();
        Assert.Equal(uniqueCount, result.Length);
        var fasit = new ComparableObjectInt[] { new(4), new(5), new(6), new(7), new(8), new(9) };
        //result.Should().EndWith(new int[] { 6, 7, 8, 9 });
        for (var i = 0; i < result.Length; i++)
        {
            Assert.Equal(fasit[i], result[i], comparer);
        }
    }

    [Fact]
    public void AppendDistinctComparerTest()
    {
        var items1 = new ComparableObjectInt[] { new(4), new(5), new(6), new(7) };
        var items2 = new ComparableObjectInt[] { new(6), new(7), new(8), new(9) };

        AppendDinstinctComparerArrayTest(6, items1, items2);
        AppendDinstinctComparerArrayTest(6, items1.ToList(), items2);
        AppendDinstinctComparerArrayTest(6, items1, items2.ToList());
        AppendDinstinctComparerArrayTest(6, items1.Select(s => s), items2);
        AppendDinstinctComparerArrayTest(6, items1, items2.Select(s => s));
        AppendDinstinctComparerArrayTest(6, items1.Select(s => s), items2.Select(s => s));
    }

    private void ShuffleTestArray(IEnumerable<int> items, bool useCryptoGradeRandom)
    {
        var shuffledNormal = items.Shuffle(useCryptoGradeRandom: useCryptoGradeRandom).ToArray();
        var snCount = 0;
        for (var i = 0; i < shuffledNormal.Length; i++)
            if (shuffledNormal[i] == i)
                snCount++;
        snCount.Should().BeLessThan(10);

    }

    [Fact]
    public void ShuffleTest()
    {
        var size = 1000;
        var items = new int[size];
        for (var i = 0; i < size; i++)
            items[i] = i;
        ShuffleTestArray(items, useCryptoGradeRandom: true);
        ShuffleTestArray(items.ToList(), useCryptoGradeRandom:true);
        ShuffleTestArray(items.Select(s => s), useCryptoGradeRandom:true);
        ShuffleTestArray(items, useCryptoGradeRandom: false);
        ShuffleTestArray(items.ToList(), useCryptoGradeRandom:false);
        ShuffleTestArray(items.Select(s => s), useCryptoGradeRandom:false);
    }

    [Fact]
    public void DeltaListsTest()
    {
        //TODO
    }
}
