using System.Diagnostics;
using System.Globalization;
using Subjects.Structures;
using Subjects.Structures.Trees;
using Utils;

namespace Subjects.AoC._2022._7;



public static class Day7Solution
{
    private static string _input = AOCInput.Import().Trim();

//     private static string _input = """
// $ cd /
// $ ls
// dir a
// 14848514 b.txt
// 8504156 c.dat
// dir d
// $ cd a
// $ ls
// dir e
// 29116 f
// 2557 g
// 62596 h.lst
// $ cd e
// $ ls
// 584 i
// $ cd ..
// $ cd ..
// $ cd d
// $ ls
// 4060174 j
// 8033020 d.log
// 5626152 d.ext
// 7214296 k
// """;

    private static int GetDirectorySize(TreeNode<(string, int)> node)
    {
        var (name, size) = node.Value;
        if (size > -1) return size;
        return node.Children.Sum(child => GetDirectorySize(child));
    }

    private static List<int> _list = new List<int>();

    private static int SearchForDirectories(TreeNode<(string, int)> node)
    {
        var (name, size) = node.Value;
        var total = 0;
        if (size != -1) return total;
        var dirSize = GetDirectorySize(node);
        _list.Add(dirSize);
        if (dirSize <= 100000) total += dirSize;
        foreach (var child in node.Children)
        {
            var (childFileName, childFileSize) = child.Value;
            if (childFileSize == -1) total += SearchForDirectories(child);
        }

        return total;
    }

    // private static List<int> GetAllDirectorySizes(TreeNode<(string, int)> node)
    // {
    //     var list = new List<int> {GetDirectorySize(node)};
    //     // Debugger.Break();
    //     foreach (var child in node.Children)
    //     {
    //         var (childFileName, childFileSize) = child.Value;
    //         if (childFileSize > -1) continue;
    //
    //         var sizes = GetAllDirectorySizes(child);
    //         list.AddRange(sizes);
    //     }
    //
    //     return list;
    // }

    private static Tree<(string, int)> ConvertInputToTree()
    {

        var root = new TreeNode<(string, int)>(("/", -1));
        var tree = new Tree<(string, int)>(root);

        var lines = _input.Split("\n");
        var cwd = root; // default to root, but will change
        for (int i = 1; i < lines.Length; i++)
        {
            var s = lines[i];

            if (s.Contains("$ ls")) continue;
            if (s.Contains("dir"))
            {
                var dirName = s.Split("dir ")[1];
                cwd.AddChild(new TreeNode<(string, int)>((dirName, -1)));
                continue;
            }
            if (s.Contains("$ cd"))
            {
                var dirToGoTo = s.Split("cd ")[1];
                if (dirToGoTo is "..")
                {
                    cwd = cwd.Parent;
                    continue;
                }
                var dirAsChild = cwd.GetChildByValue((dirToGoTo, -1));
                cwd = dirAsChild ?? throw new Exception("Child not found as expected directory name..");
                continue;
            }

            if (s.Split(" ") is not [var fileSize, var fileName]) throw new Exception("bad");

            cwd.AddChild(new TreeNode<(string, int)>((fileName, int.Parse(fileSize))));
        }

        return tree;
    }

    // public static int DoPart1()
    // {
    //     var tree = ConvertInputToTree();
    //
    //     return SearchForDirectories(tree.Root);
    // }

    public static int DoPart2()
    {
        var tree = ConvertInputToTree();
        const int total = 70_000_000;
        const int max = 30_000_000;
        var currentSize = GetDirectorySize(tree.Root);
        var currentUnused = total - currentSize;
        var minSizeToDelete = max - currentUnused;
        // var sizes = GetAllDirectorySizes(tree.Root).OrderByDescending(x => x);
        SearchForDirectories(tree.Root);
        var asd = _list.OrderBy(x => x).ToList();
        var ans = asd.FirstOrDefault(x => x >= minSizeToDelete);

        // 20646114 = too high
        Debugger.Break();

        // return sizes.LastOrDefault(x => x > minSizeToDelete);
        return 0;
    }

    public static void Output()
    {
        Console.Write("Part 1: ");
        // Console.WriteLine(DoPart1());

        Console.Write("Part 2: ");
        Console.WriteLine(DoPart2());
    }
}