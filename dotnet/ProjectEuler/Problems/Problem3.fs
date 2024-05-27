module ProjectEuler.Problems.Problem3

open NUnit.Framework

// iterate from 1 to the number, let i be the iterator
// if the number is divisible by i without any remainders, add it to the prime factors
// call this function again, this time adding calling it with the number divided as the input

// let GetPrimeFactors = n ->
//     []

[<Test>]
let Test1() =
    Assert.True(true)