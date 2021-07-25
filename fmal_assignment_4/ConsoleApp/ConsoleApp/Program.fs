// T-501-FMAL, Spring 2021, Assignment 4

(*
STUDENT NAMES HERE:
Loki Alexander Hopkins
Ríkharður Friðgeirsson


*)

module Assignment4

// Problem 1

(*
ANSWER 1 HERE:
    (i) g(1, 2) prints 2, h(1, 2) prints 2

   (ii) g(1, 0) prints 0, h(1, 0) prints 0

  (iii) g(0, 0) prints 0, h(0, 0) prints 50
        A function call to g will always just return the value of y because the return value of the function f is not caught.
        However, for h(0, 0) the return address from the f function (which is the y value in h) is assigned to the pointer p, which now is the same as
        the reference address of y. So when f is called for a second time, the value of the address p points to is updated to 50, which
        also happens to be the value of the y reference since they point to the same place. So h(0,0) prints out the direct value y, which is 50.
*)


// Abstract syntax
type expr =
    | Access of access            // a
    | Addr of access              // &a
    | Num of int                  // n
    | Op of string * expr * expr  // e1 op e2
and access =
    | AccVar of string            // x
    | AccDeref of expr            // *p
and stmt =
    | Alloc of access * expr        // p = alloc e
    | Print of expr               // print e
    | Assign of access * expr       // p = e
    | TestAndSet of expr * expr   // test_and_set(p, q)
    | Call of string * expr list  // f(e1, ..., en)
    | Block of stmt list          // { stmt1; ...; stmtN }
    | If of expr * stmt * stmt    // if (e) e1 else e2
    | While of expr * stmt        // while (e) stmt
and fundec =
    string *                      // function name
    string list *                 // argument names
    string list *                 // local variable names
    stmt                          // function body
and program =
    | Prog of fundec list

// Examples of concrete and abstract syntax

// void main () {
//   var p;
//   p = alloc(2);
//   *(p + 1) = 11;
//   print(*(p + 1));
// }
let ex =
  ("main", [], ["p"], Block [
    Alloc (AccVar "p", Num 2);
    Assign (AccDeref (Op ("+", Access (AccVar "p"), Num 1)), Num 11);
    Print (Access (AccDeref (Op ("+", Access (AccVar "p"), Num 1))))
  ])

// void make_range(dest_p, lower, upper) {
//   var i;
//   *dest_p = alloc((upper - lower) + 1);
//   while (lower <= upper) {
//     *((*dest_p) + i) = lower;
//     i = i + 1;
//     lower = lower + 1;
//   }
// }
let make_range =
  ("make_range", ["dest_p"; "lower"; "upper"], ["i"], Block [
    Alloc (AccDeref (Access (AccVar "dest_p")), Op ("+", Op ("-", Access (AccVar "upper"), Access (AccVar "lower")), Num 1));
    While (Op ("<=", Access (AccVar "lower"),  Access (AccVar "upper")), Block [
      Assign (AccDeref (Op ("+", Access (AccDeref (Access (AccVar "dest_p"))), Access (AccVar "i"))), Access (AccVar "lower"));
      Assign (AccVar "i", Op ("+", Access (AccVar "i"), Num 1));
      Assign (AccVar "lower", Op ("+", Access (AccVar "lower"), Num 1))
    ])
  ])



// Problem 2

// void print_array(a, length) {
//   var i;
//   while (i < length) {
//     print(*(a + i));
//     i = i + 1;
//   }
// }
let print_array =
  ("print_array", ["a"; "length"], ["i"], Block [
   While (Op ("<", Access (AccVar "i"), Access (AccVar "length")), Block [
     Print (Access (AccDeref(Op("+", Access (AccVar "a"), Access (AccVar "i")))))
     Assign (AccVar "i", Op ("+", Access (AccVar "i"), Num 1))
   ]) 
  ])

// void memcpy(dest, src, length) {
//   while (length) {
//     *dest = *src;
//     dest = dest + 1;
//     src = src + 1;
//     length = length - 1;
//   }
// }
let memcpy =
  ("memcpy", ["dest"; "src"; "length"], [], Block [
    While (Access (AccVar "length"), Block [
      Assign (AccDeref(Access(AccVar "dest")), Access(AccDeref(Access(AccVar "src"))))
      Assign (AccVar "dest", Op ("+", Access (AccVar "dest"), Num 1))
      Assign (AccVar "src", Op ("+", Access (AccVar "src"), Num 1))
      Assign (AccVar "length", Op ("-", Access (AccVar "length"), Num 1))
    ])
  ])

// void make_copy(dest_p, src, length) {
//   *dest_p = alloc(length);
//   memcpy(*dest_p, src, length);
// }
let make_copy =
  ("make_copy", ["dest_p"; "src"; "length"], [], Block [
    Alloc (AccDeref(Access(AccVar "dest_p")), Access(AccVar "length"))
    Call ("memcpy", [Access(AccDeref(Access(AccVar "dest_p"))); Access(AccVar "src"); Access(AccVar "length")])
  ])


// Problem 3


// (i)
// void array_to_list(dest_p, a, length) {
//   var cur;
//   *dest_p = 0;
//   while (length) {
//      length = length - 1;
//      cur = alloc(2)
//      *cur = *(a + length)
//      *(cur + 1) = *dest_p
//      *dest_p = cur
// }
let array_to_list =
  ("array_to_list", ["dest_p"; "a"; "length"], ["cur"], Block [
    Assign (AccDeref (Access (AccVar "dest_p")), Num 0);
    While (Access (AccVar "length"), Block [
      Assign (AccVar "length", Op ("-", Access (AccVar "length"), Num 1));
      Alloc (AccVar "cur", Num 2);
      Assign (AccDeref (Access (AccVar "cur")), Access (AccDeref (Op ("+", Access (AccVar "a"), Access (AccVar "length")))));
      Assign (AccDeref (Op ("+", Access (AccVar "cur"), Num 1)), Access (AccDeref (Access (AccVar "dest_p"))));
      Assign (AccDeref (Access (AccVar "dest_p")), Access (AccVar "cur"))
    ])
  ])

// (ii)

// void print_list(list) {
//   while (list) {
//      print *list
//      list = *(list + 1)
// }

//let print_list =
//  ("print_list", ["l"], ["cur"], Block [
//    Assign (AccVar "cur", Access(AccVar "l"))
//    While (Op ("!=", Access (AccVar "cur"), Num 0), Block [
//      Print (Access(AccDeref(Access(AccVar "cur"))))
//      Assign (AccVar "cur", Access(AccDeref(Op("+", Access(AccVar "cur"), Num 1))))
//    ])
//  ])
//  
let print_list =
  ("print_list", ["l"], [], Block [
    While (Access (AccVar "l"), Block [
      Print (Access(AccDeref(Access(AccVar "l"))))
      Assign (AccVar "l", Access(AccDeref(Op("+", Access(AccVar "l"), Num 1))))
    ])
  ])

// Various definitions used in the interpreter

type 'data envir = (string * 'data) list
let rec lookup env x =
    match env with
    | []          -> failwith (x + " not found")
    | (y, v)::env -> if x = y then v else lookup env x

type locEnv = int envir * int
type funEnv = (string list * string list * stmt) envir

type address = int
type store = Map<address,int> * int
let emptyStore = (Map.empty<address,int>, 1)
let setSto ((map, nextloc) : store) addr value = map.Add (addr, value), nextloc
let getSto ((map, nextloc) : store) addr = map.Item addr
let allocSto ((map, nextloc) : store) length =
  let r = nextloc
  let nextloc' = r + length
  let map' = List.fold (fun (m : Map<address,int>) addr -> m.Add (addr, 0)) map [r..(nextloc' - 1)]
  let sto' = map', nextloc'
  sto', r

let bindVar x v (env, nextloc) sto : locEnv * store =
    let env' = (x, nextloc) :: env
    ((env', nextloc - 1), setSto sto nextloc v)
let rec bindVars xs vs locEnv sto : locEnv * store =
    match xs, vs with
    | [], []       -> locEnv, sto
    | x::xs, v::vs ->
        let locEnv', sto' = bindVar x v locEnv sto
        bindVars xs vs locEnv' sto'
    | _ -> failwith "parameter/argument mismatch"

let initFunEnv (fundecs : fundec list) : funEnv =
    let rec addv decs funEnv =
        match decs with
        | [] -> funEnv
        | (f, parameters, locals, body) :: decr ->
            addv decr ((f, (parameters, locals, body)) :: funEnv)
    addv fundecs []



// Interpreter
let rec eval (e : expr) (locEnv : locEnv) (funEnv : funEnv) (sto : store) : int =
    match e with
    | Access acc      -> getSto sto (access acc locEnv funEnv sto)
    | Num i           -> i
    | Addr acc        -> access acc locEnv funEnv sto
    | Op (op, e1, e2) ->
        let i1 = eval e1 locEnv funEnv sto
        let i2 = eval e2 locEnv funEnv sto
        match op with
        | "*"  -> i1 * i2
        | "+"  -> i1 + i2
        | "-"  -> i1 - i2
        | "/"  -> i1 / i2
        | "%"  -> i1 % i2
        | "==" -> if i1 =  i2 then 1 else 0
        | "!=" -> if i1 <> i2 then 1 else 0
        | "<"  -> if i1 <  i2 then 1 else 0
        | "<=" -> if i1 <= i2 then 1 else 0
        | ">=" -> if i1 >= i2 then 1 else 0
        | ">"  -> if i1 >  i2 then 1 else 0
        | _    -> failwith ("unknown primitive " + op)
and access acc locEnv funEnv (sto : store) : int =
    match acc with
    | AccVar x   -> lookup (fst locEnv) x
    | AccDeref e -> eval e locEnv funEnv sto
and evals es locEnv funEnv (sto : store) : int list =
    List.map (fun e -> eval e locEnv funEnv sto) es
and callfun f es locEnv (funEnv : funEnv) (sto : store) : store =
    let _, nextloc = locEnv
    let paramNames, localNames, fBody = lookup funEnv f
    let arguments = evals es locEnv funEnv sto
    // local variables are initialized to 0
    let localValues = List.map (fun _ -> 0) localNames
    let fBodyEnv, sto' = bindVars (paramNames @ localNames) (arguments @ localValues) ([], nextloc) sto
    exec fBody fBodyEnv funEnv sto'





// Problem 4

and exec stm (locEnv : locEnv) (funEnv : funEnv) (sto : store) : store =
    match stm with
    | Print e ->
        let res = eval e locEnv funEnv sto
        printf "%d " res;
        sto
    | Call (f, es) -> callfun f es locEnv funEnv sto
    | Assign (acc, e) ->
        let loc = access acc locEnv funEnv sto
        let res = eval e locEnv funEnv sto
        setSto sto loc res
    | TestAndSet (p, q) ->
        failwith "not implemented"
    | Alloc (acc, e) ->
        let loc = access acc locEnv funEnv sto
        let n = eval e locEnv funEnv sto
        let sto', res = allocSto sto n
        setSto sto' loc res
    | Block stms ->
        List.fold (fun sto' s -> exec s locEnv funEnv sto') sto stms
    | If (e, stm1, stm2) ->
        let v = eval e locEnv funEnv sto
        if v <> 0 then exec stm1 locEnv funEnv sto
                  else exec stm2 locEnv funEnv sto
    | While (e, body) ->
        let rec loop sto =
            let v = eval e locEnv funEnv sto
            if v <> 0 then loop (exec body locEnv funEnv sto)
                    else sto
        loop sto

// Run a complete program
let run (Prog topdecs) vs =
    let funEnv = initFunEnv topdecs
    let locEnv = ([], System.Int32.MaxValue)
    let sto = emptyStore
    callfun "main" [] locEnv funEnv sto






// Problem 5

(* ANSWER 5 HERE

    (i) The case when 10 is printed, p and q have been allocated memory linearly through the alloc methods being called.
    So p would get memory allocation first and then q. So if we take q and go one "slot" backwards (*(q-1)) we would end up in the
    address that has been allocated for p. And then 10 is assigned to that address by dereferencing it. So when we dereference p in the print statement
    we are looking into the place in the memory that 10 was just signed to or (q-1). This is why 10 is printed when we call print (*p).
    
   (ii) First we declare variables a,b,c and d. Then 1234 is assigned to a. This means that 1234 is stored at the memory address of a. Then inside the f function
   we again declare a variable i. And access its memory address through the reference operator &. The while condition is taking the address of i with &i and we add i to it.
   And then dereference it with the * operator and check if the value returned from this address equals to 1234 and break if that is true. Otherwise inside the loop we
   increment i by 1 so that in the next iteration of the while loop we are looking "1" further in the memory to find 1234. And finally once we find 1234 the loop breaks
   and we have the address that 1234 was stored in. Or in other words we have the address of a from the main function. Then we assign 0 to that address and return from f.
   After we return we print a. And as we noted before 0 was assigned to a through the f function. That is why 0 is printed at the end of the main function.
   
*)

// void main() {
//   var p, q;
//   p = alloc(1);
//   q = alloc(1);
//   *(q - 1) = 10;
//   print(*p);
// }
let prog5i =
  Prog (
    [ ("main", [], ["p"; "q"], Block [
        Alloc (AccVar "p", Num 1);
        Alloc (AccVar "q", Num 1);
        Assign (AccDeref (Op ("-", Access (AccVar "q"), Num 1)), Num 10);
        Print (Access (AccDeref (Access (AccVar "p"))))
      ])
    ])

// void f() {
//   var i;
//   while (*(&i + i) != 1234) {
//     i = i + 1;
//   }
//   *(&i + i) = 0;
// }
// void main() {
//   var a, b, c, d;
//   a = 1234;
//   f();
//   print a;
// }
let prog5ii =
  Prog (
    [ ("f", [], ["i"], Block [
        While (Op ("!=", Access (AccDeref (Op ("+", Addr (AccVar "i"), Access (AccVar "i")))), Num 1234), Block [
          Assign (AccVar "i", Op ("+", Access (AccVar "i"), Num 1))
        ]);
        Assign (AccDeref (Op ("+", Addr (AccVar "i"), Access (AccVar "i"))), Num 0)
      ])
    ; ("main", [], ["a"; "b"; "c"; "d"], Block [
        Assign (AccVar "a", Num 1234);
        Call ("f", []);
        Print (Access (AccVar "a"))
      ])
    ])





// Test cases for Problem 2

// void main() {
//   int p;
//   make_range(&p, 5, 50);
//   print_array(p, 10);
// }
let print_array_test =
  ("main", [], ["p"], Block [
    Call ("make_range", [Addr (AccVar "p"); Num 5; Num 50]);
    Call ("print_array", [Access (AccVar "p"); Num 10])
  ]);;

// void main() {
//   int p;
//   make_range(&p, 5, 10);
//   print_array(p, 6);
// }
let print_array_test2 =
  ("main", [], ["p"], Block [
    Call ("make_range", [Addr (AccVar "p"); Num 5; Num 10]);
    Call ("print_array", [Access (AccVar "p"); Num 6])
  ]);;

// void main() {
//   int p, q;
//   make_range(&p, 5, 50);
//   make_range(&q, 100, 150);
//   memcpy(p, q, 5);
//   print_array(p, 10);
// }
let memcpy_test =
  ("main", [], ["p"; "q"], Block [
    Call ("make_range", [Addr (AccVar "p"); Num 5; Num 50]);
    Call ("make_range", [Addr (AccVar "q"); Num 100; Num 150]);
    Call ("memcpy", [Access (AccVar "p"); Access (AccVar "q"); Num 5]);
    Call ("print_array", [Access (AccVar "p"); Num 10])
  ]);;

// void main() {
//   int p, q;
//   make_range(&p, 5, 50);
//   make_copy(&q, p, 10);
//   print_array(q, 10);
// }
let make_copy_test =
  ("main", [], ["p"; "q"], Block [
    Call ("make_range", [Addr (AccVar "p"); Num 5; Num 50]);
    Call ("make_copy", [Addr (AccVar "q"); Access (AccVar "p"); Num 10]);
    Call ("print_array", [Access (AccVar "q"); Num 10])
  ]);;

//run (Prog [make_range; print_array; print_array_test]) [] |> ignore
//// 5 6 7 8 9 10 11 12 13 14 val it : unit = ()
//printfn "";
//run (Prog [make_range; print_array; print_array_test2]) [] |> ignore
//// 5 6 7 8 9 10 val it : unit = ()
//printfn "";
//run (Prog [make_range; memcpy; print_array; memcpy_test]) [] |> ignore
//// 100 101 102 103 104 10 11 12 13 14 val it : unit = ()
//printfn "";
//run (Prog [make_range; memcpy; make_copy; print_array; make_copy_test]) [] |> ignore
//// 5 6 7 8 9 10 11 12 13 14 val it : unit = ()


// Test cases for Problem 3

// void main() {
//   var a, b;
//   b = alloc(2);
//   *b = 10;
//   a = alloc(2);
//   *a = 11;
//   *(a + 1) = b;
//   print_list(a);
// }
let print_list_test =
  ("main", [], ["a"; "b"], Block [
    Alloc (AccVar "b", Num 2);
    Assign (AccDeref (Access (AccVar "b")), Num 10);
    Alloc (AccVar "a", Num 2);
    Assign (AccDeref (Access (AccVar "a")), Num 11);
    Assign (AccDeref (Op ("+", Access (AccVar "a"), Num 1)), Access (AccVar "b"));
    Call ("print_list", [Access (AccVar "a")]);
  ])

// void main() {
//   var a, b;
//   make_range(&a, 100, 105);
//   array_to_list(&b, a, 6);
//   print_list(b);
// }
let print_list_test2 =
  ("main", [], ["a"; "b"], Block [
    Call ("make_range", [Addr (AccVar "a"); Num 100; Num 105]);
    Call ("array_to_list", [Addr (AccVar "b"); Access (AccVar "a"); Num 6]);
    Call ("print_list", [Access (AccVar "b")]);
  ])
  
//run (Prog [make_range; array_to_list; print_list; print_list_test]) [] |> ignore
//// 11 10 val it : unit = ()
//printfn ""
//run (Prog [make_range; array_to_list; print_list; print_list_test2]) [] |> ignore
// 100 101 102 103 104 105 val it : unit = ()


// Test cases for Problem 4

// void main() {
//   int a, b;
//   a = 5;
//   test_and_set(&b, &a);
//   print a;
//   print b;
// }
let test_and_set_test =
  ("main", [], ["a"; "b"], Block [
    Assign (AccVar "a", Num 5);
    TestAndSet (Addr (AccVar "b"), Addr (AccVar "a"));
    Print (Access (AccVar "a"));
    Print (Access (AccVar "b"))
  ]);;

// void main() {
//   int p;
//   p = alloc(2);
//   *p = 3;
//   *(p + 1) = 4;
//   test_and_set(p, p + 1);
//   print(*p);
//   print(*(p + 1));
// }
let test_and_set_test2 =
  ("main", [], ["p"], Block [
    Alloc (AccVar "p", Num 2);
    Assign (AccDeref (Access (AccVar "p")), Num 3);
    Assign (AccDeref (Op ("+", Access (AccVar "p"), Num 1)), Num 4);
    TestAndSet (Access (AccVar "p"), Op ("+", Access (AccVar "p"), Num 1));
    Print (Access (AccDeref (Access (AccVar "p"))));
    Print (Access (AccDeref (Op ("+", Access (AccVar "p"), Num 1))))
  ]);;

// void main() {
//   var a, b, i;
//   make_range(&a, 55, 65);
//   make_range(&b, 105, 115);
//   while (i <= 10) {
//     test_and_set(a + i, b + i);
//     i = i + 1;
//   }
//   print_array(a, 21);
//   print_array(b, 21);
// }
let test_and_set_test3 =
  ("main", [], ["a"; "b"; "i"], Block [
    Call ("make_range", [Addr (AccVar "a"); Num 55; Num 65]);
    Call ("make_range", [Addr (AccVar "b"); Num 105; Num 115]);
    While (Op ("<=", Access (AccVar "i"), Num 10), Block [
      TestAndSet(
        Op ("+", Access (AccVar "a"), Access (AccVar "i")),
        Op ("+", Access (AccVar "b"), Access (AccVar "i")));
      Assign (AccVar "i", Op ("+", Access (AccVar ("i")), Num 1))
    ]);
    Call ("print_array", [Access (AccVar "a"); Num 11]);
    Call ("print_array", [Access (AccVar "b"); Num 11])
  ]);;

// void main() {
//   var a, i;
//   make_range(&a, 55, 65);
//   while (i <= 9) {
//     test_and_set(a + i, a + (i + 1));
//     i = i + 2;
//   }
//   print_array(a, 11);
// }
let test_and_set_test4 =
  ("main", [], ["a"; "i"], Block [
    Call ("make_range", [Addr (AccVar "a"); Num 55; Num 65]);
    While (Op ("<=", Access (AccVar "i"), Num 9), Block [
      TestAndSet(
        Op ("+", Access (AccVar "a"), Access (AccVar "i")),
        Op ("+", Access (AccVar "a"), Op ("+", Access (AccVar "i"), Num 1)));
      Assign (AccVar "i", Op ("+", Access (AccVar ("i")), Num 2))
    ]);
    Call ("print_array", [Access (AccVar "a"); Num 11])
  ]);;

// void main() {
//   var a, i;
//   make_range(&a, 55, 65);
//   while (i <= 9) {
//     test_and_set(a + i, a + (i + 1));
//     i = i + 1;
//   }
//   print_array(a, 11);
// }
let test_and_set_test5 =
  ("main", [], ["a"; "i"], Block [
    Call ("make_range", [Addr (AccVar "a"); Num 55; Num 65]);
    While (Op ("<=", Access (AccVar "i"), Num 9), Block [
      TestAndSet(
        Op ("+", Access (AccVar "a"), Access (AccVar "i")),
        Op ("+", Access (AccVar "a"), Op ("+", Access (AccVar "i"), Num 1)));
      Assign (AccVar "i", Op ("+", Access (AccVar ("i")), Num 1))
    ]);
    Call ("print_array", [Access (AccVar "a"); Num 11])
  ]);;

run (Prog [test_and_set_test]) [] |> ignore;;
// 1 5 val it : unit = ()
// > run (Prog [test_and_set_test2]) [] |> ignore;;
// 4 1 val it : unit = ()
// > run (Prog [make_range; print_array; test_and_set_test3]) [] |> ignore;;
// 105 106 107 108 109 110 111 112 113 114 115 1 1 1 1 1 1 1 1 1 1 1 val it : unit = ()
// > run (Prog [make_range; print_array; test_and_set_test4]) [] |> ignore;;
// 56 1 58 1 60 1 62 1 64 1 65 val it : unit = ()
// > run (Prog [make_range; print_array; test_and_set_test5]) [] |> ignore;;
// 56 57 58 59 60 61 62 63 64 65 1 val it : unit = ()

