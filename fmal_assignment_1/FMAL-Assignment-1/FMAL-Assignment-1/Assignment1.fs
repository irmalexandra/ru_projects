// T-501-FMAL, Spring 2021, Assignment 1

(*
STUDENT NAMES HERE:
Ríkharður Friðgeirsson,
Loki Alexander Hopkins
*) 

module FMAL_Assignment_1.Assignment1

// Problem 1

// nf : int -> int

let rec nf (n:int) : int =
    match n with
    | n when n < 1 -> 1
    | n when n = 1 -> 2
    | _ -> 2 * nf(n-1) + 3 * nf(n-2)
    
printfn "nf test1 = %b" (nf 0 = 1)
printfn "nf test2 = %b" (nf 1 = 2)
printfn "nf test2 = %b" (nf 2 = 7)
printfn "nf test3 = %b" (nf 3 = 20)
printfn "nf test4 = %b" (nf 4 = 61)
    
    
//printf "%i" (nf(-1))


// Problem 2


// (i)

// lastTrue : (int -> bool) -> int -> int
  
let rec lastTrue f (n : int) : int =
    match n with
    | _ when n < 0 -> -1
    | _ when f (n-1) = true -> n-1
    | _ -> lastTrue f (n-1)
     
printfn "lastTrue test1 = %b" (lastTrue (fun x -> 10 < x && x < 40) 100 = 39)
printfn "lastTrue test2 = %b" (lastTrue (fun x -> 10 < x && x < 40) 30 = 29)
printfn "lastTrue test3 = %b" (lastTrue (fun x -> 10 < x && x < 40) 10 = -1)
printfn "lastTrue test3 = %b" (lastTrue (fun x -> true) (-3) = -1)

// (ii)

// lastEqual : 'a -> (int -> 'a) -> int -> int when 'a : equality
let lastEqual (x:'a) f (n : int) : int =
    lastTrue (fun n -> f n = x) n
    
printfn "lastEqual test1 = %b" (lastEqual 3 (fun x -> x / 10) 100 = 39)
printfn "lastEqual test2 = %b" (lastEqual 3 (fun x -> x / 10) 20 = -1)
printfn "lastEqual test3 = %b" (lastEqual 3 (fun x -> x % 10) 100 = 93)
printfn "lastEqual test4 = %b" (lastEqual 3 (fun x -> x % 10) 20 = 13)
    

// (iii)

// firstTrue : (int -> bool) -> int -> int

let firstTrue f (n : int) : int =
    let max_n = n;
    
    let rec recFirstTrue n =
        match n with
        | n when f (n) = true -> n
        | n when n = max_n -> -1
        | n -> recFirstTrue (n+1)
    if n < 0 then -1 else
    recFirstTrue 0
    
printfn "firstTrue test1 = %b" (firstTrue (fun x -> 10 < x && x < 40) 100 = 11)
printfn "firstTrue test2 = %b" (firstTrue (fun x -> 10 < x && x < 40) 30 = 11)
printfn "firstTrue test3 = %b" (firstTrue (fun x -> 10 < x && x < 40) 10 = -1)
printfn "firstTrue test4 = %b" (firstTrue (fun x -> true) (-3) = -1)


// (iv)

// If  lastTrue (fun x -> f x > f (x + 1)) 100  evaluates to -1,
// what can you say about  f?

(*
f x > f(x+1) is always false no matter what.
For example if f x = x+1 lets say x = 5 then f 5 > f(5+1) -> 6 > 7 -> false
But on the other had we could declare f as f x = if x % 2 = 0 then x + 100 else x
This would result in if x = 5 -> f 5 > f 6 -> 105 > 6 -> true
So the only conclusion from what we have gathered is that f x > f (x+1) is always false no matter what. 

*)

// How about if  lastTrue f 100 = firstTrue f 100  is  true?

(*
100 is the only value that f returns true. That's why both firstTrue and lastTrue return 100
*)


// Problem 3

// repeat_map : ('a -> 'a) -> 'a list -> 'a list
let repeat_map f data =
    let rec rec_repeat_map data =
        match data with
        | [] -> data
        | x :: data -> x :: rec_repeat_map(List.map(f) data)
    rec_repeat_map (List.map (f) data)

let repeat_map_1 f data =
    let rec rec_repeat_map_1 data =
        if List.length data = 0 then
            []
        else
            data.Head :: rec_repeat_map_1(List.map (f) data.Tail)
    rec_repeat_map_1 (List.map (f) data)
    
let rec repeat_map_2 f data =
    match List.map(f) data with
    | [] -> data
    | x :: data -> x :: (repeat_map_2 f data)


printfn "repeat_map   test 1: %b" (repeat_map (fun x -> x+1) [0..10] = [1;3;5;7;9;11;13;15;17;19;21])
printfn "repeat_map_1 test 1: %b" (repeat_map_1 (fun x -> x+1) [0..10] = [1;3;5;7;9;11;13;15;17;19;21])
printfn "repeat_map_2 test 1: %b" (repeat_map_2 (fun x -> x+1) [0..10] = [1;3;5;7;9;11;13;15;17;19;21])

printfn "repeat_map   test 2: %b" (repeat_map (fun x -> - x) [1..10] = [-1; 2; -3; 4; -5; 6; -7; 8; -9; 10])
printfn "repeat_map_1 test 2: %b" (repeat_map_1 (fun x -> - x) [1..10] = [-1; 2; -3; 4; -5; 6; -7; 8; -9; 10])
printfn "repeat_map_2 test 2: %b" (repeat_map_2 (fun x -> - x) [1..10] = [-1; 2; -3; 4; -5; 6; -7; 8; -9; 10])

printfn "repeat_map   test 3: %b" (repeat_map (fun x -> x + x) ["x"; "y"; "z"; "w"] = ["xx"; "yyyy"; "zzzzzzzz"; "wwwwwwwwwwwwwwww"])
printfn "repeat_map_1 test 3: %b" (repeat_map_1 (fun x -> x + x) ["x"; "y"; "z"; "w"] = ["xx"; "yyyy"; "zzzzzzzz"; "wwwwwwwwwwwwwwww"])
printfn "repeat_map_2 test 3: %b" (repeat_map_2 (fun x -> x + x) ["x"; "y"; "z"; "w"] = ["xx"; "yyyy"; "zzzzzzzz"; "wwwwwwwwwwwwwwww"])



// Problem 4

// (i)

// sum_some : int option list -> int
let rec sum_some data =
    match data with
    | [] -> 0
    | None :: data -> sum_some data
    | Some(x) :: data -> x + (sum_some data)
       

printfn "sum_some1 test 1: %b" ((sum_some []) = 0)
printfn "sum_some1 test 2: %b" ((sum_some [None; Some(2); None]) = 2)
printfn "sum_some1 test 3: %b" ((sum_some [None; Some(2); None; Some(4); Some(-1)]) = 5)
printfn "sum_some1 test 4: %b" ((sum_some [None; None; None]) = 0)
printfn "sum_some1 test 5: %b" ((sum_some [Some(1); Some(2); Some(3)]) = 6)


// (ii)  (uncomment the definition below when you've completed it)

let sum_some2 xs =
     List.fold(fun s o ->
        match o with
        | Some(o) -> s + o
        | None -> s) 0 xs

printfn "sum_some2 test 1: %b" ((sum_some2 []) = 0)
printfn "sum_some2 test 2: %b" ((sum_some2 [None; Some(2); None]) = 2)
printfn "sum_some2 test 3: %b" ((sum_some2 [None; Some(2); None; Some(4); Some(-1)]) = 5)
printfn "sum_some2 test 4: %b" ((sum_some2 [None; None; None]) = 0)
printfn "sum_some2 test 5: %b" ((sum_some2 [Some(1); Some(2); Some(3)]) = 6)
      


// (iii)  (uncomment the definition below when you've completed it)


let sum_some3 xs =
    let f o =
        match o with
        | Some(o) -> o
        | None -> 0
    List.fold (+) 0 (List.map f xs)

printfn "sum_some3 test 1: %b" ((sum_some3 []) = 0)
printfn "sum_some3 test 2: %b" ((sum_some3 [None; Some(2); None]) = 2)
printfn "sum_some3 test 3: %b" ((sum_some3 [None; Some(2); None; Some(4); Some(-1)]) = 5)
printfn "sum_some3 test 4: %b" ((sum_some3 [None; None; None]) = 0)
printfn "sum_some3 test 5: %b" ((sum_some3 [Some(1); Some(2); Some(3)]) = 6)

// Problem 5

type 'a nelist =
  | One of 'a
  | Cons of 'a * 'a nelist


// (i)

// ne_product : int nelist -> int
let rec ne_product (data : 'a nelist) : int =
    match data with
    | One x -> x
    | Cons (x, data) -> x * ne_product data
    
printfn "ne_product test 1: %b" (ne_product (One 2) = 2)
printfn "ne_product test 2: %b" (ne_product (Cons (3, One 2)) = 6)
printfn "ne_product test 3: %b" (ne_product (Cons (5, Cons (3, One 2))) = 30)
printfn "ne_product test 4: %b" (ne_product (Cons (6, Cons (5, Cons (3, One 2)))) = 180)


// (ii)

// ne_append : 'a nelist -> 'a nelist -> 'a nelist

let rec ne_append (data_1 : 'a nelist) (data_2 : 'a nelist) : 'a nelist =
    match data_1 with
    | One x -> Cons (x, data_2)
    | Cons (x, data_1) -> Cons (x, (ne_append data_1 data_2))
    
    
printfn "ne_append test 1: %b" (ne_append (Cons ("b", One "a")) (Cons ("x", One "y")) = Cons ("b",Cons ("a",Cons ("x",One "y"))))
printfn "ne_append test 2: %b" (ne_append (One "a") (Cons ("x", One "y")) = Cons ("a",Cons ("x",One "y")))
printfn "ne_append test 3: %b" (ne_append (Cons ("b", One "a")) (Cons ("x", One "y")) = Cons ("b",Cons ("a",Cons ("x",One "y"))))
printfn "ne_append test 4: %b" (ne_append (Cons ("c", Cons ("b", One "a"))) (Cons ("x", One "y")) = Cons ("c",Cons ("b",Cons ("a",Cons ("x",One "y")))))
printfn "ne_append test 5: %b" (ne_append (Cons (1, Cons (2, Cons (3, One 4)))) (One 6) = Cons (1,Cons (2,Cons (3,Cons (4,One 6)))))
    
// (iii)

// to_list : 'a nelist -> 'a list

let rec to_list (data : 'a nelist) : 'a list =
    match data with
    | One x -> [x]
    | Cons (x, data) -> x :: (to_list data)


printfn "to_list test 1: %b" (to_list (Cons (1, Cons (2, Cons (3, One 4)))) = [1; 2; 3; 4])
printfn "to_list test 2: %b" (to_list (One "x") = ["x"])
printfn "to_list test 3: %b" (to_list (Cons ("y", One "x")) = ["y"; "x"])

// (iv)

// ne_map : ('a -> 'b) -> 'a nelist -> 'b nelist

let rec ne_map f (data : 'a nelist) : 'b nelist =
    match data with
    | One x -> One (f x)
    | Cons (x, data) -> Cons (f x, ne_map f data)

printfn "ne_map test 1: %b" (ne_map (fun x -> x * 2) (Cons (1, Cons (2, Cons (3, One 4)))) = Cons (2,Cons (4,Cons (6,One 8))))
printfn "ne_map test 2: %b" (ne_map (fun x -> "a" + x) (Cons ("x", Cons ("y", One "z"))) = Cons ("ax",Cons ("ay",One "az")))
printfn "ne_map test 3: %b" (ne_map (fun x -> x + x) (Cons ("x", Cons ("y", Cons ("z", One "w")))) = Cons ("xx",Cons ("yy",Cons ("zz",One "ww"))))


// (v)

// to_pair : 'a nelist -> 'a * 'a list

let to_pair xs =
    match xs with 
    | One x -> (x, [])
    | Cons (x, xs) -> (x, to_list xs)

// from_pair : 'a * 'a list -> 'a nelist
let rec from_pair (pair : 'a * 'a list) : 'a nelist =
    match pair with 
    | (x, [])  -> One x
    | (x, y :: pair) -> Cons (x, from_pair (y, pair))


printfn "from_pair test 1: %b" (from_pair ("x", []) = One "x")
printfn "from_pair test 2: %b" (from_pair ("x", ["y"; "z"; "w"]) = Cons ("x",Cons ("y",Cons ("z",One "w"))))
printfn "from_pair test 3: %b" (from_pair (10, [1..5]) = Cons (10,Cons (1,Cons (2,Cons (3,Cons (4,One 5))))))
printfn "from_pair test 4: %b" (from_pair ([1], [[2..3]; [4..7]]) = Cons ([1],Cons ([2; 3],One [4; 5; 6; 7])))

// (vi)

// Is it possible to write a function  from_list : 'a list -> 'a nelist
// such that the expressions  to_list (from_list xs) = xs
// and  from_list (to_list ys) = ys  evaluate to  true?
// Explain why.

(*
If we look at from_pair and to_pair, we can see that a similar question would evaluate to true, therefore if from_list
is designed correctly, to_list would behave just the same regardless of whether you call it with 'a nelist or
(from_list xs)


*)


// Problem 6

type product_tree =
  { value: int
  ; children: product_tree list
  ; product: int option }
  
  
let t1  = { value = 2; children = []; product = None }
let t1' = { value = 2; children = []; product = Some 2 }
let t2 =
  { value = 3
  ; children =
    [ { value = 4; children = []; product = None }
    ; { value = 5; children = []; product = None }
    ]
  ; product = None }
let t2' =
  { value = 3
  ; children =
    [ { value = 4; children = []; product = Some 4 }
    ; { value = 5; children = []; product = Some 5 }
    ]
  ; product = Some 60 }
let t3 =
  { value = 6
  ; children =
    [ { value = 7
      ; children =
        [ { value = 8; children = []; product = None }
        ; { value = 9; children = []; product = None }
        ]
      ; product = None }
    ; { value = 10; children = []; product = None }
    ]
  ; product = None }
let t3' =
  { value = 6
  ; children =
    [ { value = 7
      ; children =
        [ { value = 8; children = []; product = Some 8 }
        ; { value = 9; children = []; product = None }
        ]
      ; product = None }
    ; { value = 10; children = []; product = None }
    ]
  ; product = Some 30240 }
let t3'' =
  { value = 6
  ; children =
    [ { value = 7
      ; children =
        [ { value = 8; children = []; product = Some 8 }
        ; { value = 9; children = []; product = Some 9 }
        ]
      ; product = Some 504 }
    ; { value = 10; children = []; product = None }
    ]
  ; product = None }
let t3''' =
  { value = 6
  ; children =
    [ { value = 7
      ; children =
        [ { value = 8; children = []; product = Some 8 }
        ; { value = 9; children = []; product = Some 9 }
        ]
      ; product = Some 504 }
    ; { value = 10; children = []; product = Some 10 }
    ]
  ; product = Some 30240 }
let t4 =
  { value = 6
  ; children =
    [ { value = 7; children = []; product = None }
    ; { value = 8; children = []; product = None }
    ; { value = 9; children = []; product = None }
    ; { value = 10; children = []; product = None }
    ]
  ; product = None }
let t4' =
  { value = 6
  ; children =
    [ { value = 7; children = []; product = Some 7 }
    ; { value = 8; children = []; product = None }
    ; { value = 9; children = []; product = Some 9 }
    ; { value = 10; children = []; product = None }
    ]
  ; product = None }
let t4'' =
  { value = 6
  ; children =
    [ { value = 7; children = []; product = Some 7 }
    ; { value = 8; children = []; product = Some 8 }
    ; { value = 9; children = []; product = Some 9 }
    ; { value = 10; children = []; product = Some 10 }
    ]
  ; product = Some 30240 }

// (i)

// are_same : product_tree -> product_tree -> bool
let rec are_same (tree_1 : product_tree) (tree_2 : product_tree) : bool =
    if tree_1.value = tree_2.value
    then
        List.forall2 (are_same) tree_1.children tree_2.children
    else
        false
    
printfn "are_same test 1: %b" (are_same t1 t1 = true)
printfn "are_same test 2: %b" (are_same t1 t1' = true)
printfn "are_same test 3: %b" (are_same t1 t2 = false)
printfn "are_same test 4: %b" (are_same t2 t2 = true)
printfn "are_same test 5: %b" (are_same t2 t2' = true)
printfn "are_same test 6: %b" (are_same t2 t3 = false)
printfn "are_same test 7: %b" (are_same t3 t3 = true)
printfn "are_same test 8: %b" (are_same t3 t3' = true)

// (ii)

// get_product : product_tree -> int

let rec get_product (tree: product_tree) : int =
    match tree.product with
    | _ when tree.children = [] && tree.product = None -> tree.value 
    | Some x -> x
    | None -> tree.value * (List.fold (fun s o -> s * get_product o) 1 tree.children)
    
printfn "get_product test t1:    %b" (get_product t1 = 2) 
printfn "get_product test t1':   %b" (get_product t1' = 2)
printfn "get_product test t2:    %b" (get_product t2 = 60)
printfn "get_product test t2':   %b" (get_product t2' = 60)
printfn "get_product test t3':   %b" (get_product t3' = 30240)
printfn "get_product test t3'':  %b" (get_product t3'' = 30240)
printfn "get_product test t3''': %b" (get_product t3''' = 30240)
printfn "get_product test t4:    %b" (get_product t4 = 30240)
printfn "get_product test t4':   %b" (get_product t4' = 30240)

let extra =
       { value = 3
        ;children = [{ value = 4
                   ;children = []
                   ;product = Some 4 };   
                   { value = 5
                    ;children = []
                    ;product = Some 5 }]
        ;product = Some 60 }
// (iii)

// fill_products : product_tree -> product_tree
let rec fill_products (tree: product_tree) : product_tree =
    { value = tree.value; children = (List.map (fill_products) tree.children); product = Some(get_product tree) }
    

printfn "fill_products test t1:   %b" (fill_products t1 = t1')
printfn "fill_products test t1':  %b" (fill_products t1' = t1')
printfn "fill_products test t2:   %b" (fill_products t2 = extra)
printfn "fill_products test t3:   %b" (fill_products t3 = t3''')
printfn "fill_products test t3':  %b" (fill_products t3' = t3''')
printfn "fill_products test t3'': %b" (fill_products t3'' = t3''')
printfn "fill_products test t4:   %b" (fill_products t4 = t4'')


