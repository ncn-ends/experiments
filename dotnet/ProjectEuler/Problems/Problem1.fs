module Problems.Problem1

let f = fun x ->
    let a = [ for i in 1..x-1 do if (i % 3 = 0 || i % 5 = 0) then i ]
    List.sum a