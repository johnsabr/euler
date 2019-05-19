(* Aaron Johnson
   2017-10-25 *)

namespace Problems

/// <title>Multiples of 3 and 5</title>
/// <summary>
/// If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6
/// and 9. The sum of these multiples is 23. Find the sum of all the multiples of 3 or 5
/// below 1000.
/// </summary>
type Problem01 () =
    member this.Solve () =
        seq { for x in 1 .. 999 do
              if x % 3 = 0 || x % 5 = 0
              then yield x }
              |> Seq.sum
              |> sprintf "%i"

    interface IProblem with member this.Solve () = this.Solve ()

/// <title>Even Fibonacci Numbers</title>
/// <summary>
/// Each new term in the Fibonacci sequence is generated by adding
/// the previous two terms. By starting with 1 and 2, the first 10 terms will be:
///
/// 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...
///
/// By considering the terms in the Fibonacci sequence whose values do not exceed
/// four million, find the sum of the even-valued terms.
/// </summary>
type Problem02 () =
    member this.Solve () =
        Sequences.Fibonacci ()
        |> Seq.filter (fun x -> x % 2 = 0)
        |> Seq.takeWhile (fun x -> x < 4000000)
        |> Seq.sum
        |> sprintf "%i"

    interface IProblem with member this.Solve () = this.Solve ()

/// <title>Largest prime factor</title>
/// <summary>
/// The prime factors of 13195 are 5, 7, 13 and 29.
/// What is the largest prime factor of the number 600851475143 ?
/// </summary>
type Problem03 () =
    member this.Solve () =
        Sequences.PrimeFactors 600851475143L
        |> Seq.max
        |> sprintf "%i"

    interface IProblem with member this.Solve () = this.Solve ()

/// <title>Largest palindrome product</title>
/// <summary>
/// A palindromic number reads the same both ways. The largest palindrome
/// made from the product of two 2-digit numbers is 9009 = 91 × 99.
/// Find the largest palindrome made from the product of two 3-digit numbers.
/// </summary>
type Problem04 () =
    let isPalindrome (s : string) =
        let half = s.Length / 2
        in Seq.forall2 (=)
            (s |> Seq.take half)
            (s |> Seq.rev |> Seq.take half)

    member this.Solve () =
        query { for i in [100..999] do
                for j in [101..999] do
                let m = i * j
                where (isPalindrome (sprintf "%i" m))
                select m }
            |> Seq.max
            |> sprintf "%i"

    interface IProblem with member this.Solve () = this.Solve ()

/// <title>Smallest multiple</title>
/// <summary>
/// 2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
/// What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
/// </summary>
/// <remarks>
/// This problem builds off of problem 3, Largest prime factor, which provided a method to list the
/// prime factors of a number.
/// </remarks>
type Problem05 () =

    let countCommonFactors n =
        Sequences.PrimeFactors n
        |> Seq.countBy id
        |> Map.ofSeq

    let unionMaps (m1: Map<'a,'b>) (m2: Map<'a,'b>) =
        let folder acc newKey newValue =
            match Map.tryFind newKey acc with
            | Some current when current > newValue -> acc
            | _ -> Map.add newKey newValue acc
        in Map.fold folder m1 m2

    let unionManyMaps ms = Seq.reduce unionMaps ms

    member this.Solve () =
        [1L..20L]
        |> List.map countCommonFactors
        |> unionManyMaps
        |> Map.fold (fun acc prime count -> acc * (pown prime count)) 1L
        |> sprintf "%i"

    interface IProblem with member this.Solve () = this.Solve ()

/// <title>Sum square difference</title>
/// <summary>
/// The sum of the squares of the first ten natural numbers is,
/// 12 + 22 + ... + 102 = 385
/// The square of the sum of the first ten natural numbers is,
/// (1 + 2 + ... + 10)2 = 552 = 3025
/// Hence the difference between the sum of the squares of the first ten natural numbers
/// and the square of the sum is 3025 − 385 = 2640.
///
/// Find the difference between the sum of the squares of the first one hundred natural
/// numbers and the square of the sum.
/// </summary>
type Problem06 () =

    member this.Solve () =
        abs (
            ([1..100] |> List.sumBy (fun x -> x * x))
            - (let v = [1..100] |> List.sum in v * v)
        ) |> sprintf "%i"

    interface IProblem with member this.Solve () = this.Solve ()

// <summary>
/// By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
/// What is the 10001st prime number?
/// </summary>
type Problem07 () =

    member this.Solve () = Seq.head (Seq.skip 10000 (Sequences.Primes ())) |> sprintf "%i"

    interface IProblem with member this.Solve () = this.Solve ()

/// <title>Largest product in a series</title>
/// <summary>
/// The four adjacent digits in the 1000-digit number that have the greatest product are 9 × 9 × 8 × 9 = 5832.
///
/// 73167176531330624919225119674426574742355349194934
/// 96983520312774506326239578318016984801869478851843
/// 85861560789112949495459501737958331952853208805511
/// 12540698747158523863050715693290963295227443043557
/// 66896648950445244523161731856403098711121722383113
/// 62229893423380308135336276614282806444486645238749
/// 30358907296290491560440772390713810515859307960866
/// 70172427121883998797908792274921901699720888093776
/// 65727333001053367881220235421809751254540594752243
/// 52584907711670556013604839586446706324415722155397
/// 53697817977846174064955149290862569321978468622482
/// 83972241375657056057490261407972968652414535100474
/// 82166370484403199890008895243450658541227588666881
/// 16427171479924442928230863465674813919123162824586
/// 17866458359124566529476545682848912883142607690042
/// 24219022671055626321111109370544217506941658960408
/// 07198403850962455444362981230987879927244284909188
/// 84580156166097919133875499200524063689912560717606
/// 05886116467109405077541002256983155200055935729725
/// 71636269561882670428252483600823257530420752963450
///
/// Find the thirteen adjacent digits in the 1000-digit number that have the greatest product. What is the value of this product?
/// </summary>
type Problem08 () =

    let digits = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450"
                 |> Seq.map (System.Char.GetNumericValue >> int64)
                 |> Seq.toList

    let product13 (digits:List<'a>) start = digits.[start..(start+12)] |> List.reduce (*)

    let maxProduct =
        let d = digits
        [0..((List.length d) - 14)]
        |> List.map (fun i -> product13 d i)
        |> List.max

    member this.Solve () = sprintf "%i" maxProduct

    interface IProblem with member this.Solve () = this.Solve ()

/// <title>Special Pythagorean triplet</title>
/// <summary>
/// A Pythagorean triplet is a set of three natural numbers, a LT b LT c, for which,
/// a^2 + b^2 = c^2
///
/// For example, 32 + 42 = 9 + 16 = 25 = 52.
///
/// There exists exactly one Pythagorean triplet for which a + b + c = 1000.
/// Find the product a*b*c.
/// </summary>
type Problem09 () =

    let pythaAddsTo1000 =
        let triplets = Sequences.PythagoreanTriplets ()
        let found = Seq.tryFind (fun (a, b, c) -> a + b + c = 1000) triplets
        in match found with
           | Some (a, b, c) -> a * b * c
           | _ -> 0

    member this.Solve () = pythaAddsTo1000 |> sprintf "%i"

    interface IProblem with member this.Solve () = this.Solve ()

// <summary>
/// The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
/// Find the sum of all the primes below two million.
/// </summary>
type Problem10 () =
    member this.Solve () =
        (Sequences.Primes ()) |> Seq.takeWhile (fun x -> x <= 2000000L)
        |> Seq.sum |> sprintf "%i"

    interface IProblem with member this.Solve () = this.Solve ()