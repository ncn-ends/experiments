module ProjectEuler.Problems.Problem2

open NUnit.Framework

let rec fib = fun x ->
    match x with
    | 0 -> 0
    | 1 -> 1
    | 2 -> 2
    | _ -> fib(x - 1) + fib(x - 2)

[<TestCase(0, 0)>]
[<TestCase(1, 1)>]
[<TestCase(2, 2)>]
[<TestCase(3, 3)>]
[<TestCase(5, 8)>]
[<TestCase(9, 55)>]
let TestFib input expected = Assert.AreEqual(expected, fib input)

let solve max =
    let rec fib a b list =
        if b >= max then
            list
        else
            let nextList = if b % 2 = 0 then b :: list else list
            fib b (a + b) nextList

    let fibsWithEvens = fib 0 1 []
    List.sum fibsWithEvens

[<Test>]
let TestProblemSolution () =
    Assert.AreEqual(44, solve 100)
    Assert.AreEqual(4613732, solve 4_000_000)