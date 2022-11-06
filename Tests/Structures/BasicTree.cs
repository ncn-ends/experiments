using System;
using System.Collections.Generic;
using FluentAssertions;
using Subjects.LeetCode;
using Subjects.Structures;
using Xunit;

namespace Tests.LeetCode;

public class BasicTreeTests
{

    // public static IEnumerable<object[]> Data()
    // {
    //     yield return new object[]
    //     {
    //         new List<char> {'h', 'e', 'l', 'l', 'o'},
    //         new List<char> {'o', 'l', 'l', 'e', 'h'}
    //     };
    //     yield return new object[]
    //     {
    //         new List<char> {'H', 'a', 'n', 'n', 'a', 'h'},
    //         new List<char> {'h', 'a', 'n', 'n', 'a', 'H'}
    //     };
    // }

    // [MemberData(nameof(Data))]
    [Fact]
    public void BasicTreeOfInts()
    {
        var root = new TreeNode<int>(100);
        root.Value.Should().Be(100);
        var layer1 = new List<TreeNode<int>>
        {
            new(50),
            new(1),
            new(150)
        };
        root.AddChildren(layer1);
        
        root.Children[0].Value.Should().Be(50);
        root.Children[1].Value.Should().Be(1);
        root.Children[2].Value.Should().Be(150);
        
        var tree = new BasicTree<int>(root);
        tree.AddChildByValue(50, new TreeNode<int>(12));
        tree.FindNodeByValue(50)?.Children[0].Value.Should().Be(12);
        
        tree.AddChildByValue(12, new TreeNode<int>(12));
        tree.AddChildByValue(12, new TreeNode<int>(21));
        
        tree.AddChildByValue(21, new TreeNode<int>(6));
        
        tree.AddChildByValue(1, new TreeNode<int>(70));
        tree.AddChildByValue(1, new TreeNode<int>(61));
        
        tree.AddChildByValue(150, new TreeNode<int>(30));
        tree.AddChildByValue(150, new TreeNode<int>(5));
        tree.AddChildByValue(150, new TreeNode<int>(11));
        
        tree.AddChildByValue(30, new TreeNode<int>(96));
        tree.AddChildByValue(30, new TreeNode<int>(9));
    }

    public class Person
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; } = "UNSET NAME";
        public string Title { get; set; } = "UNSET TITLE";
    }
    
    [Fact]
    public void BasicTreeOfPersons()
    {
        var tree = new BasicTree<Person>(new TreeNode<Person>(new Person
        {
            Id = 1,
            Name = "Marcin Jamro",
            Title = "CEO"
        }));
        
        tree.AddChildByValue(p => p.Id == 1, new TreeNode<Person>(new Person
        {
            Id = 2,
            Name = "John Smith",
            Title = "Head of Development"
        }));
        
        tree.Root.Children.Count.Should().Be(1);
        
    }
}