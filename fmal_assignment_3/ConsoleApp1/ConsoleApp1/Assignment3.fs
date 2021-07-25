// T-501-FMAL, Spring 2021, Assignment 3

(*
STUDENT NAMES HERE: Ríkharður Friðgeirsson, Loki Alexander Hopkins


*)

module Assignment3

// Problem 1

// When we are working with the static scope rule the result of this expression is 31.
// This is because in the last line of the code, y has gotten the value 13, it got this value from assigned the function
// f g where f g ended up being (2 + 4) + (3 + 4). This 3 in the 3 + 4 is from the value y had in the outer scope.
// When the last function is called, which ends up being (2 + 13) + (3 + 13), its 3 + 13 because when f g was defined,
// the value of y is 3.

// When we are working with the dynamic scope rule the result of this expression is 41.
// The function works mostly the same until the very end when the last function is called, the result is
// (2 + 13) + (13 + 13) because instead of g y using the value of y in its scope (which is 3) it uses the dynamically
// up to date value of 13.

// Problem 2

let list_fun f x xs =
    if List.map(f) xs = xs then f x else x

let option_fun f x xOp =
    match xOp with
    | Some(x) when f x = x -> x
    | _ -> x

// Problem 3

(*
ANSWER 3(i) HERE:
    // this expression is unifiable.
    // 'a > int list
    // int list -> int list and int list -> int list
*)

(*
ANSWER 3(ii) HERE:
    // this expression is unifiable.
    // 'b > int list
    // 'a -> int list and 'a -> int list
*)

(*
ANSWER 3(iii) HERE:
    // this expression is unifiable.
    // 'a > (int -> int)
    // (int -> int) -> (int -> int) and (int -> int) -> (int -> int)
*)

(*
ANSWER 3(iv) HERE:
    // this expression is unifiable.
    // 'b > 'a list
    // 'a list -> 'a list and 'a list -> 'a list
*)

(*
ANSWER 3(v) HERE:
    // this expression is not unifiable
    // 'a can not unify with 'a list because it would create an infinite type.
*)

(*
ANSWER 3(vi) HERE:
    // this expression is unifiable
    // 'd > ('a -> 'b)
    // 'c > 'e list
    // ('a -> 'b) -> 'e list and ('a -> 'b) -> 'e list
*)

(* Various type and function definitions, do not edit *)

type expr =
    | NumF of float
    | Vect of float list
    | Plus of expr * expr
    | Average of expr
    | Scale of expr * expr
    | IfPositive of expr * expr * expr
    | Var of string
    | Call of expr * expr
    | LetFun of string * string * expr * expr
        // Non-recursive let
    | LetFunNoGeneralize of string * string * expr * expr
type value =
    | N of float
    | V of float list
    | F of string * expr * envir
and envir = (string * value) list
type lvarkind =
    | NoLink of string
    | LinkTo of length
and lvar = (lvarkind * int) ref
and length =
    | LVar of lvar
    | LNum of int
type typ =
    | Float
    | Vector of length
    | Fun of length * typ  // Fun (l, t) is the type Vector(l) -> t
type typescheme =
    | TypeScheme of lvar list * typ
type tyenvir = (string * typescheme) list


let rec lookup (x : string) (env : (string * 'a) list) : 'a =
    match env with
    | []          -> failwith (x + " not found")
    | (y, v)::env -> if x = y then v else lookup x env

let setVarKind (tv : lvar) (kind : lvarkind) : unit =
    let _, lvl = !tv
    tv := kind, lvl

let setVarLevel (tv : lvar) (lvl : int) : unit =
    let kind, _ = !tv
    tv := kind, lvl

let rec normLength (l : length) : length =
    match l with
    | LVar lv ->
        match !lv with
        | LinkTo l', _ -> let ln = normLength l'
                          setVarKind lv (LinkTo ln); ln
        | _ -> l
    |  _ -> l

let rec union xs ys =
    match xs with
    | []    -> ys
    | x::xs -> if List.contains x ys then union xs ys
               else x :: union xs ys

let freeLengthVarsL (l : length) : lvar list =
    match normLength l with
    | LVar lv -> [lv]
    | LNum _ -> []
let rec freeLengthVars (t : typ) : lvar list =
    match t with
    | Float -> []
    | Vector l -> freeLengthVarsL l
    | Fun (l, t) ->
        union (freeLengthVarsL l) (freeLengthVars t)


let pruneLevel (maxLevel : int) (tvs : lvar list) : unit =
    let reducelevel tv =
        let _, lvl = !tv
        setVarLevel tv (min lvl maxLevel)
    List.iter reducelevel tvs

let rec linkVarToLength (lv : lvar) (l : length) : unit =
   let _, lvl = !lv
   let lvs = freeLengthVarsL l
   pruneLevel lvl lvs;
   setVarKind lv (LinkTo l)

let showLength (l : length) : string =
    match normLength l with
    | LVar lv ->
        match !lv with
        | NoLink name, _ -> name
        | _               -> failwith "we should not have ended up here"
    | LNum x -> string x
let rec showType (t : typ) : string =
    match t with
    | Float -> "Float"
    | Vector v -> sprintf "Vector(%s)" (showLength v)
    | Fun (l, t) -> sprintf "Vector(%s) -> %s" (showLength l) (showType t)

let varno : int ref = ref 0
let newLengthVar (lvl : int) : lvar =
    let alphabet = ['m'..'z'] @ ['a'..'l']
    let alphabet_length = List.length alphabet
    let rec mkname i res =
            if i < alphabet_length then alphabet.[i] :: res
            else mkname (i/alphabet_length-1) (alphabet.[i%alphabet_length] :: res)
    let intToName i = new System.String(Array.ofList('\'' :: mkname i []))
    varno := !varno + 1;
    ref (NoLink (intToName (!varno)), lvl)

let rec generalize (lvl : int) (t : typ) : typescheme =
    let notfreeincontext tv =
        let _, linkLvl = !tv
        linkLvl > lvl
    let lvs = List.filter notfreeincontext (freeLengthVars t)    
    TypeScheme (lvs, t)

let rec copyLength (subst : (lvar * length) list) (l : length) : length =
    match l with
    | LNum _ -> l
    | LVar lv ->
        let rec loop subst' =
            match subst' with
            | (lv', l') :: subst' -> if lv = lv' then l' else loop subst'
            | [] -> match !lv with
                    | NoLink _ , _ -> l
                    | LinkTo l', _ -> copyLength subst l'
        loop subst

let rec copyType (subst : (lvar * length) list) (t : typ) : typ =
    match t with
    | Float -> Float
    | Vector l -> Vector (copyLength subst l)
    | Fun (l,t) -> Fun (copyLength subst l, copyType subst t)

let specialize (lvl : int) (TypeScheme (lvs, t)) : typ =
    let bindfresh lv = (lv, LVar (newLengthVar lvl))
    match lvs with
    | [] -> t
    | _  -> let subst = List.map bindfresh lvs
            copyType subst t

let ensureFloat (t : typ) : unit =
    match t with
    | Float -> ()
    | _ -> failwith "expected a float"
let ensureVector (t : typ) : unit =
    match t with
    | Vector _ -> ()
    | _ -> failwith "expected a vector"
let ensureFloatOrVector (t : typ) : unit =
    match t with
    | Float
    | Vector _ -> ()
    | _ -> failwith "expected a float or a vector"

let unifyLength (l1 : length) (l2 : length) : unit =
    let l1' = normLength l1
    let l2' = normLength l2
    match l1', l2' with
    | LNum x, LNum y ->
        if x <> y then failwith (sprintf "lengths %i and %i differ" x y)
    | LVar lv1, LVar lv2 ->
        let _, lv1level = !lv1
        let _, lv2level = !lv2
        if lv1 = lv2                then ()
        else if lv1level < lv2level then linkVarToLength lv1 l2'
                                    else linkVarToLength lv2 l1'
    | LVar lv1, _ -> linkVarToLength lv1 l2'
    | _, LVar lv2 -> linkVarToLength lv2 l1'

(* Evaluation function *)
let rec eval (e : expr) (env : envir) : value =
    match e with
    | NumF x -> N x
    | Vect v -> V v
    | Plus (e1, e2) ->
        match eval e1 env, eval e2 env with
        | N x1, N x2 -> N (x1 + x2)
        | V v1, V v2 -> V (List.map2 (+) v1 v2)
        | _ -> failwith "wrong operand type"
    | Average e ->
        match eval e env with
        | V v -> N (List.average v)
        | _ -> failwith "wrong operand type"
    | Scale (e1, e2) ->
        match eval e1 env, eval e2 env with
        | N x, V v -> V (List.map ((*) x) v)
        | _ -> failwith "wrong operand type"
    | IfPositive (e, et, ef) ->
        let guard =
            match eval e env with
            | N f -> f > 0.0
            | _ -> failwith "wrong operand type"
        eval (if guard then et else ef) env
    | Var x  ->  lookup x env
    | Call (f, earg) ->
        match eval f env with
        | F (x, ebody, env0) as clo ->
            let v = eval earg env
            eval ebody ((x, v) :: env0)
        | _   -> failwith "variable called not a function"
    | LetFun (f, xs, erhs, ebody)
    | LetFunNoGeneralize (f, xs, erhs, ebody) ->
        let env' = (f, F (xs, erhs, env)) :: env
        eval ebody env'

// Problem 4

let rec unify (t1 : typ) (t2 : typ) : unit =
    match t1, t2 with
    | Float, Float -> ()
    | Vector(l1), Vector(l2) -> unifyLength l1 l2
    | Fun(l1, t1), Fun(l2, t2) -> if unifyLength l1 l2 = () then unify t1 t2
    | _ -> failwith ("cannot unify" + showType t1 + " and " + showType t2)

let n = ref (NoLink "'n", 0)
let o = ref (NoLink "'o", 0)
let unifyTest t1 t2 =
  n := (NoLink "'n", 0);
  o := (NoLink "'o", 0);
  unify t1 t2;
  showType t1
  

// Problem 5

let rec infer (e : expr) (lvl : int) (env : tyenvir) : typ =
    match e with
    | NumF _ -> Float
    | Vect v ->
        let len = List.length v
        if len = 0 then failwith "empty vectors not allowed"
        Vector (LNum len)
    | Plus (e1, e2) ->
        let t1 = infer e1 lvl env
        let t2 = infer e2 lvl env
        unify t1 t2
        t1               
    | Average e ->
        let t = infer e lvl env
        match ensureVector t with
        | ()  -> Float
    | Scale (e1, e2) ->
        let t1 = infer e1 lvl env
        let t2 = infer e2 lvl env
        ensureFloat t1
        ensureVector t2
        t2
    | IfPositive (e, e1, e2) ->
        let t = infer e lvl env
        ensureFloat t;
        let t1 = infer e1 lvl env
        let t2 = infer e2 lvl env
        unify t1 t2;
        t1
    | Var x  -> specialize lvl (lookup x env)
    | Call (f, earg) ->
        let tf = infer f lvl env
        let targ = infer earg lvl env
        let arg_length =
            match targ with
            | Vector l -> l
            | _ -> failwith "argument of function not a vector"
        let tr =
            match tf with
            | Fun (_, t) -> t
            | _ -> failwith "expression called not a function"
        unify tf (Fun (arg_length, tr)); tr
    | LetFun (f, x, erhs, ebody) ->
        let lvl' = lvl + 1
        let arg_length = LVar (newLengthVar lvl')
        let env' = (x, TypeScheme ([], Vector arg_length)) :: env
        let tr = infer erhs lvl' env'
        let tf = Fun (arg_length, tr)
        let env'' = (f, generalize lvl tf) :: env
        infer ebody lvl env''
    | LetFunNoGeneralize (f, x, erhs, ebody) ->
        let arg_length = LVar (newLengthVar lvl)
        let env' = (x, TypeScheme ([], Vector arg_length)) :: env
        let tr = infer erhs lvl env'
        let tf = Fun (arg_length, tr)
        let env'' = (f, TypeScheme ([], tf)) :: env
        infer ebody lvl env''

let inferTop e =
    varno := 0;
    showType (infer e 0 [])


// Problem 6
// Complete the following declaration, and uncomment it

let no_generalize : expr =
    LetFun ("f", "x", Average(Var "x"), Plus(Call(Var "f", Vect [32.;42.]), Call(Var "f", Vect [43.])))
   