module ProjectEuler.Problem1

open NUnit.Framework

let f = fun x ->
    [ for i in 1..x-1 do if (i % 3 = 0 || i % 5 = 0) then i ]
    |> List.sum

[<SetUp>]
let Setup () =
    ()

[<Test>]
let Test1 () =
    Assert.AreEqual(f 10, 23)

[<Test>]
let Test2 () =
    Assert.AreEqual(f 1000, 233168)