module Slide01_SampleForIl

let square x = x * x

let sumOfSquares n =
   [1..n]
   |> List.map square
   |> List.sum

printfn "%d" (sumOfSquares 100)