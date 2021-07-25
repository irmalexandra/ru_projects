module Assignment_2.Program

// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.Linq.Expressions
open System.Runtime.Serialization
open System.Text.RegularExpressions

// Define a function to construct a message to print
// T-501-FMAL, Spring 2021, Assignment 2

(*
STUDENT NAMES HERE: ...
Loki Alexander Hopkins
Ríkharður Friðgeirsson

*)


(* Various type and function definitions, do not edit *)

type iexpr =
    | IVar of string
    | INumI of int
    | INumF of float
    | IPlus of iexpr * iexpr
    | ITimes of iexpr * iexpr
    | INeg of iexpr
    | IIfPositive of iexpr * iexpr * iexpr

type expr =
    | Var of string
    | NumI of int
    | NumF of float
    | Plus of expr * expr
    | Times of expr * expr
    | Neg of expr
    | IfPositive of expr * expr * expr
    | IntToFloat of expr
    | Match of expr * string * expr * string * expr

type value =
    | I of int
    | F of float

type envir = (string * value) list

type typ =
    | Int
    | Float

type tyenvir = (string * typ) list

let rec lookup (x : string) (env : (string * 'a) list) : 'a =
    match env with
    | []          -> failwith (x + " not found")
    | (y, v)::env -> if x = y then v else lookup x env

let paren b s = if b then  "(" + s + ")" else s

let iprettyprint (e : iexpr) : string =
    let rec iprettyprint' e acc =
        match e with
        | IVar x -> x
        | INumI i -> string i
        | INumF f -> sprintf "%A" f
        | IPlus  (e1, e2) ->
              paren (4 <= acc) (iprettyprint' e1 3 + " + " + iprettyprint' e2 4)
        | ITimes (e1, e2) ->
              paren (7 <= acc) (iprettyprint' e1 6 + " * " + iprettyprint' e2 7)
        | INeg e ->
              paren (10 <= acc) ("-" + iprettyprint' e 9)
        | IIfPositive (e, et, ef) ->
              paren (2 <= acc) ("if " + iprettyprint' e 3 + " > 0 then " + iprettyprint' et 2 + " else " + iprettyprint' ef 1)
    iprettyprint' e 0

let prettyprint (e : expr) : string =
    let rec prettyprint' e acc =
        match e with
        | Var x -> x
        | NumI i -> string i
        | Plus  (e1, e2) ->
             paren (4 <= acc) (prettyprint' e1 3 + " + " + prettyprint' e2 4)
        | Times (e1, e2) ->
             paren (7 <= acc) (prettyprint' e1 6 + " * " + prettyprint' e2 7)
        | Neg e ->
             paren (10 <= acc) ("-" + prettyprint' e 9)
        | IfPositive (e, et, ef) ->
             paren (2 <= acc) ("if " + prettyprint' e 3 + " > 0 then " + prettyprint' et 2 + " else " + prettyprint' ef 1)
        | NumF f -> sprintf "%A" f
        | IntToFloat e ->
             paren (10 <= acc) ("float " + prettyprint' e 10)
        | Match (e, xi, ei, xf, ef) ->
             paren (2 <= acc) ("match " + prettyprint' e 1 + " with"
               + " I " + xi + " -> " + prettyprint' ei 2
               + " | F " + xf + " -> " + prettyprint' ef 1)
    prettyprint' e 0

let plus_value (v1 : value, v2 : value) : value =
    match v1, v2 with
    | I x1, I x2 -> I (x1 + x2)
    | F x1, I x2 -> F (x1 + float x2)
    | I x1, F x2 -> F (float x1 + x2)
    | F x1, F x2 -> F (x1 + x2)

let times_value (v1 : value, v2 : value) : value =
    match v1, v2 with
    | I x1, I x2 -> I (x1 * x2)
    | F x1, I x2 -> F (x1 * float x2)
    | I x1, F x2 -> F (float x1 * x2)
    | F x1, F x2 -> F (x1 * x2)

let neg_value (v : value) : value =
    match v with
    | I x -> I (-x)
    | F x -> F (-x)

let is_positive_value (v : value) : bool =
    match v with
    | I x -> x > 0
    | F x -> x > 0.

type rinstr =
    | RLoad of int            // load from environment
    | RStore                  // move value from top of stack to
                              // 0th pos of the environment,
                              // shifting all others down
    | RErase                  // remove 0th value from environment,
                              // shifting all others up
    | RNum of int
    | RAdd
    | RSub
    | RMul
    | RPop
    | RDup
    | RSwap

type rcode = rinstr list
type stack = int list          // intermediate values
type renvir = int list         // values of numbered variables

let rec reval (inss : rcode) (stk : stack) (renv : renvir) =
    match inss, stk with
    | [], i :: _ -> i
    | [], []     -> failwith "reval: No result on stack!"
    | RLoad n :: inss,             stk ->
          reval inss (List.item n renv :: stk) renv
    | RStore  :: inss,        i :: stk -> reval inss stk (i :: renv)
    | RErase  :: inss,             stk -> reval inss stk (List.tail renv)
    | RNum i  :: inss,             stk -> reval inss (i :: stk) renv
    | RAdd    :: inss, i2 :: i1 :: stk -> reval inss ((i1+i2) :: stk) renv
    | RSub    :: inss, i2 :: i1 :: stk -> reval inss ((i1-i2) :: stk) renv
    | RMul    :: inss, i2 :: i1 :: stk -> reval inss ((i1*i2) :: stk) renv
    | RPop    :: inss,        i :: stk -> reval inss stk renv
    | RDup    :: inss,        i :: stk -> reval inss ( i ::  i :: stk) renv
    | RSwap   :: inss, i2 :: i1 :: stk -> reval inss (i1 :: i2 :: stk) renv
    | _ -> failwith "reval: too few operands on stack"


// Problem 1


let rec lookup_var (x : string) (env : (string * 'a) list) : 'a =
    match env with
    | []          -> I 0
    | (y, v)::env -> if x = y then v else lookup_var x env
let rec ieval (e : iexpr) (env : envir) : value =
    match e with
    | IVar x -> lookup_var x env                       // to modify
    | INumI i -> I i
    | INumF f -> F f
    | IPlus (e1, e2) -> plus_value (ieval e1 env, ieval e2 env)
    | ITimes (e1, e2) -> times_value (ieval e1 env, ieval e2 env)
    | INeg e -> neg_value (ieval e env)
    | IIfPositive (e, et, ef) ->
        if is_positive_value (ieval e env)
        then ieval et env
        else ieval ef env

printfn "----PROBLEM 1----"
printfn "%A" (ieval (IVar "x") [])
printfn "%A" (ieval (IVar "x") ["x", I 5])
printfn "%A" (ieval (IPlus (IVar "x", ITimes (IVar "y", IVar "z"))) ["x", F 1.1; "z", I 10])


// Problem 2

let rec eval (e : expr) (env : envir) : value =
    match e with
    | Var x -> lookup_var x env
    | NumI i -> I i
    | NumF f -> F f
    | Plus (e1, e2) ->                             // to complete
        match eval e1 env, eval e2 env with
        | I i1, I i2 -> I (i1 + i2)
        | F f1, F f2 -> F (f1 + f2)
        | _ -> failwith "wrong operand type"
    | Times (e1, e2) ->                            // to complete
        match eval e1 env, eval e2 env with
        | I i1, I i2 -> I (i1 * i2)
        | F f1, F f2 -> F (f1 * f2)
        | _ -> failwith "wrong operand type"
    | Neg e ->                                     // to complete
        match eval e env with
        | I i -> I (- i)
        | F f -> F (- f)
        | _ -> failwith "wrong operand type"
    | IntToFloat e ->
        match eval e env with
        | I i -> F (float i)
        | _ -> failwith "wrong operand type"
    | IfPositive (e, et, ef) ->
        if is_positive_value (eval e env)
        then eval et env
        else eval ef env
    | Match (e, xi, ei, xf, ef) ->
        match eval e env with
        | I n -> eval ei (env @ [xi, I n])
        | F n -> eval ef (env @ [xf, F n])
  

printfn "----PROBLEM 2----"     
printfn "Expected F 3.3, got %A" (eval (Plus (Var "x", Var "y")) ["x", F 1.1; "y", F 2.2])
printfn "Expected F 6.05, got %A" (eval (Times (Var "x", Plus (NumF 3.3, Var "y"))) ["x", F 1.1; "y", F 2.2])
printfn "Expected F 11.6, got %A" (eval (Plus (IntToFloat (Plus (NumI 2, NumI 3)), NumF 6.6)) [])
printfn "Expected F -5.6, got %A" (eval (Neg (Var "x")) ["x", F 5.6])
printfn "Expected F 1.1, got %A" (eval (IfPositive (Var "x", NumF 1.1, NumF 2.2)) ["x", I 1])
printfn "Expected F 2.2, got %A" (eval (IfPositive (Var "x", NumF 1.1, NumF 2.2)) ["x", I -1])
printfn "Expected F 1.1, got %A" (eval (IfPositive (Var "x", NumF 1.1, NumF 2.2)) ["x", F 1.0])
printfn "Expected F 2.2, got %A" (eval (IfPositive (Var "x", NumF 1.1, NumF 2.2)) ["x", F -1.0])
printfn "Expected I 12, got %A" (eval (Match (Var "x", "zi", Plus (Var "zi", NumI 2), "zf", Plus (Var "zf", NumF 3.))) ["x", I 10])
printfn "Expected F 13.0, got %A" (eval (Match (Var "x", "zi", Plus (Var "zi", NumI 2), "zf", Plus (Var "zf", NumF 3.))) ["x", F 10.])


// Problem 3

let to_float (v : value) : float =
    match v with
    | F v -> v
    | I v -> float(v)

printfn "----PROBLEM 3----"
printfn "Expected F 5.5, got %A" (to_float (F 5.5))
printfn "Expected F 5.0, got %A" (to_float (I 5))
printfn "Expected F -11.0, got %A" (to_float (I -11))
printfn "Expected F -11.0, got %A" (to_float (F -11.0))

// Problem 4

let extract_key (exp:expr) : string =
        match exp with
        | Var s -> s
        | _ -> ""

let to_float_expr (e : expr) : expr =
    Match(e, "d1", IntToFloat(Var "d1"), "d2", Var "d2")
    
let plus_expr (e1 : expr, e2 : expr) : expr =  
      let if_float (exp1:expr, exp2:expr): expr =
          Match(exp2, "d1", Plus(exp1, IntToFloat(exp2)), "d2", Plus(exp1, exp2))
      
      let if_int (exp1:expr, exp2:expr): expr =
          Match(exp2, "d1", Plus(exp1, exp2), "d2", Plus(IntToFloat(exp1), exp2))
      
      let exp1 = Match(e1, "d1", if_int(e1, e2), "d2", if_float(e1, e2))
      
      exp1

let times_expr (e1 : expr, e2 : expr) : expr =    
    let if_float (exp1:expr, exp2:expr): expr =
        Match(exp2, "d1", Times(exp1, IntToFloat(exp2)), "d2", Times(exp1, exp2))
      
    let if_int (exp1:expr, exp2:expr): expr =
        Match(exp2, "d1", Times(exp1, exp2), "d2", Times(IntToFloat(exp1), exp2))
      
    let exp1 = Match(e1, "d1", if_int(e1, e2), "d2", if_float(e1, e2))
      
    exp1

printfn "----PROBLEM 4----"
printfn "----TO_FLOAT----"
printfn "Expected F 4.0, got %A" (eval (to_float_expr (Var "x")) ["x", I 4])
printfn "Expected F 4.4, got %A" (eval (to_float_expr (Var "x")) ["x", F 4.4])
printfn "----PLUS----"
printfn "Expected I 13, got %A" (eval (plus_expr (Var "x", Var "y")) ["x", I 6; "y", I 7])
printfn "Expected F 13.1, got %A" (eval (plus_expr (Var "x", Var "y")) ["x", F 6.1; "y", I 7])
printfn "Expected F 13.2, got %A" (eval (plus_expr (Var "x", Var "y")) ["x", I 6; "y", F 7.2])
printfn "Expected F 13.3, got %A" (eval (plus_expr (Var "x", Var "y")) ["x", F 6.1; "y", F 7.2])
printfn "----TIMES----"
printfn "Expected I 42, got %A" (eval (times_expr (Var "x", Var "y")) ["x", I 6; "y", I 7])
printfn "Expected F 42.7, got %A" (eval (times_expr (Var "x", Var "y")) ["x", F 6.1; "y", I 7])
printfn "Expected F 43.2, got %A" (eval (times_expr (Var "x", Var "y")) ["x", I 6; "y", F 7.2])
printfn "Expected F 43.92, got %A" (eval (times_expr (Var "x", Var "y")) ["x", F 6.1; "y", F 7.2])


// Problem 5

let rec add_matches (e : iexpr) : expr =
    match e with
    | IVar elem -> Var elem
    | INumF elem -> NumF elem
    | INumI elem -> NumI elem
    | INeg elem -> Neg(add_matches(elem))
    | IPlus (elem1, elem2) -> plus_expr(add_matches(elem1), add_matches(elem2))
    | ITimes (elem1, elem2) -> times_expr(add_matches(elem1), add_matches(elem2))
    | IIfPositive (elem1, elem2, elem3) -> IfPositive (add_matches(elem1), add_matches(elem2), add_matches(elem3))
    

printfn "----PROBLEM 5----"
printfn "Expected I 5, got %A" (eval (add_matches (IVar "x")) ["x", I 5])
printfn "Expected F 5.5, got %A" (eval (add_matches (IVar "x")) ["x", F 5.5])
printfn "Expected F -16.5, got %A" (eval (add_matches (INeg (ITimes (INumI 3, INumF 5.5)))) [])
printfn "Expected F 10.4, got %A" (eval (add_matches (IIfPositive (IVar "x", INumI 1, IPlus (IVar "y", INumF 4.4)))) ["x", I -2; "y", I 6])
printfn "Expected I 20, got %A" (eval (add_matches (IIfPositive (INeg (IVar "x"), IPlus (IVar "y", INumI 5), ITimes (IVar "y", INumI 5)))) ["x", F 2.2; "y", I 4])


// Problem 6

let rec infer (e : expr) (tyenv : tyenvir) : typ =
    match e with
    | Var x -> lookup x tyenv
    | NumI _ -> Int
    | NumF _ -> Float
    | Plus(x, y) ->
        match infer x tyenv, infer y tyenv with
        | Int, Int -> Int
        | Float, Float -> Float
        | _ -> failwith "wrong operand type"
    | Times(x, y) ->
        match infer x tyenv, infer y tyenv with
        | Int, Int -> Int
        | Float, Float -> Float
        | _ -> failwith "wrong operand type"
    | Neg(x) ->
        match infer x tyenv with
        | Int -> Int
        | Float -> Float
    | IntToFloat(x) ->
        match infer x tyenv with
        | Int -> Float
        | _ -> failwith "wrong operand type"
    |IfPositive(x, y, z) ->
        match infer x tyenv, infer y tyenv, infer z tyenv with
        | Int, Int, Int -> Int
        | Float, Float, Float -> Float
        | Float, Int, Int -> Int
        | Int, Float, Float -> Float
        | _ -> failwith "wrong operand type"
    |Match(e, xi, ei, xf, ef) ->
        match infer e tyenv with
        | Int -> infer ei (tyenv @ [xi, Int])
        | Float -> infer ef (tyenv @ [xf, Float])


printfn "----PROBLEM 6----"
printfn "Expected Int, got %A" (infer (Plus (Var "x", Var "y")) ["x", Int; "y", Int])
printfn "Expected Float, got %A" (infer (Plus (Var "x", Var "y")) ["x", Float; "y", Float])
printfn "Expected Float, got %A" (infer (Times (Var "x", Var "y")) ["x", Float; "y", Float])
//printfn "Expected System.Exception: wrong operand type, got %A" (infer (Plus (Var "x", Var "y")) ["x", Int; "y", Float])
//printfn "Expected System.Exception: wrong operand type, got %A" (infer (Times (Var "x", Var "y")) ["x", Float; "y", Int])
printfn "Expected Int, got %A" (infer (IfPositive (Var "x", Var "y", Var "z")) ["x", Int; "y", Int; "z", Int])
printfn "Expected Float, got %A" (infer (IfPositive (Var "x", Var "y", Var "z")) ["x", Int; "y", Float; "z", Float])
printfn "Expected Int, got %A" (infer (IfPositive (Var "x", Var "y", Var "z")) ["x", Float; "y", Int; "z", Int])
printfn "Expected Float, got %A" (infer (IfPositive (Var "x", Var "y", Var "z")) ["x", Float; "y", Float; "z", Float])
//printfn "Expected System.Exception: branches of different types, got %A" (infer (IfPositive (Var "x", Var "y", Var "z")) ["x", Int; "y", Int; "z", Float])
//printfn "Expected System.Exception: branches of different types, got %A" (infer (IfPositive (Var "x", Var "y", Var "z")) ["x", Float; "y", Float; "z", Int])
printfn "Expected Int, got %A" (infer (Match (Var "x", "xi", Var "xi", "xf", NumI 1)) ["x", Int])
printfn "Expected Int, got %A" (infer (Match (Var "x", "xi", Var "xi", "xf", NumI 1)) ["x", Float])
printfn "Expected Float, got %A" (infer (Match (Var "x", "xi", NumF 1.1, "xf", Var "xf")) ["x", Int])
printfn "Expected Float, got %A" (infer (Match (Var "x", "xi", NumF 1.1, "xf", Var "xf")) ["x", Float])
printfn "Expected Int, got %A" (infer (Match (Neg (Var "x"), "xi", Plus (NumI 4, Var "xi"), "xf", IfPositive(Var "xf", NumI 5, Times (NumI 4, NumI 6)))) ["x", Int])
printfn "Expected Float, got %A" (infer (Plus (NumF 3.4, IntToFloat (NumI 4))) [])
//printfn "Expected System.Exception: wrong operand type, got %A" (infer (Plus (NumF 3.4, IntToFloat (NumF 4.4))) [])
//printfn "Expected System.Exception: wrong operand type, got %A" (infer (Plus (NumI 3, IntToFloat (NumI 4))) [])

// Problem 7

let rec add_casts (e : iexpr) (tyenv : tyenvir) : expr =
    match e with
    | IVar x -> Var x
    | INumI x -> NumI x
    | INumF x -> NumF x
    | IPlus (exp1, exp2) ->
        match infer(add_casts exp1 tyenv) tyenv, infer(add_casts exp2 tyenv) tyenv with
        | Int, Float -> Plus(IntToFloat(add_casts(exp1) tyenv), add_casts(exp2) tyenv)
        | Float, Int -> Plus(add_casts(exp1) tyenv, IntToFloat(add_casts(exp2) tyenv))
        | _ -> Plus(add_casts(exp1) tyenv, add_casts(exp2) tyenv)
    | ITimes (exp1, exp2) ->
        match infer(add_casts exp1 tyenv) tyenv, infer(add_casts exp2 tyenv) tyenv with
        | Int, Float -> Times(IntToFloat(add_casts(exp1) tyenv), add_casts(exp2) tyenv)
        | Float, Int -> Times(add_casts(exp1) tyenv, IntToFloat(add_casts(exp2) tyenv))
        | _ -> Times(add_casts(exp1) tyenv, add_casts(exp2) tyenv)
    | INeg exp -> Neg(add_casts exp tyenv)
    | IIfPositive (exp1, exp2, exp3) -> IfPositive((add_casts(exp1) tyenv, add_casts(exp2) tyenv, add_casts(exp3) tyenv))
        

printfn "----PROBLEM 7----"
printfn "Expected I 5, got %A" (eval (add_casts (IVar "x") ["x", Int]) ["x", I 5])
printfn "Expected F 5.5, got %A" (eval (add_casts (IVar "x") ["x", Float]) ["x", F 5.5])
printfn "Expected F -16.5, got %A" (eval (add_casts (INeg (ITimes (INumI 3, INumF 5.5))) []) [])
printfn "Expected F 10.4 got %A" (eval (add_casts (IIfPositive (IVar "x", INumI 1, IPlus (IVar "y", INumF 4.4))) ["x", Int; "y", Int]) ["x", I -2; "y", I 6])
printfn "Expected I 20 got %A" (eval (add_casts (IIfPositive (INeg (IVar "x"), IPlus (IVar "y", INumI 5), ITimes (IVar "y", INumI 5))) ["x", Float; "y", Int]) ["x", F 2.2; "y", I 4])

// Problem 8
    // As add_matches and add_casts are designed to output the same result, then they should never return a different result
    // given the same input. This applies to both eval and infer. However, the behaviour inside the functions can vary,
    // namely due to how Match interprets the inputs.

// Problem 9

let rec rlower (inss : rcode) : rcode =
    match inss with
    | [] -> inss
    | RDup::inss -> RStore :: RLoad 0 :: RLoad 0 :: RErase :: rlower inss
    | RPop::inss -> RStore :: RErase :: rlower inss
    | RSwap::inss -> RStore :: RStore :: RLoad 1 :: RLoad 0 :: rlower inss
    | other::inss -> other :: rlower inss


printfn "----PROBLEM 9----"
printfn "Expected 4 got %A" (reval (rlower [RDup; RAdd; RAdd]) [1;2] [])
printfn "Expected 5 got %A" (reval (rlower [RPop; RAdd]) [1;2;3] [])
printfn "Expected -1 got %A" (reval (rlower [RSwap; RSub]) [1;2] [])
printfn "Expected 16 got %A" (reval (rlower [RLoad 1; RDup; RAdd; RDup; RLoad 2; RSwap; RAdd; RLoad 0; RPop]) [] [4;5;6])
