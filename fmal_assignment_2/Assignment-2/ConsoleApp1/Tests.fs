// T-501-FMAL, Spring 2021, Assignment 1

// Test cases for Problem 1

// > ieval (IVar "x") [];;
// val it : value = I 0
// > ieval (IVar "x") ["x", I 5];;
// val it : value = I 5
// > ieval (IPlus (IVar "x", ITimes (IVar "y", IVar "z"))) ["x", F 1.1; "z", I 10];
// val it : value = F 1.1


// Test cases for Problem 2

// > eval (Plus (Var "x", Var "y")) ["x", F 1.1; "y", F 2.2];;
// val it : value = F 3.3
// > eval (Times (Var "x", Plus (NumF 3.3, Var "y"))) ["x", F 1.1; "y", F 2.2];;
// val it : value = F 6.05
// > eval (Plus (IntToFloat (Plus (NumI 2, NumI 3)), NumF 6.6)) [];;
// val it : value = F 11.6
// > eval (Plus (NumI 1, NumF 2.0)) [];;
// System.Exception: wrong operand type
// > eval (Times (NumF 1.0, NumI 2)) [];;
// System.Exception: wrong operand type
// > eval (Neg (Var "x")) ["x", F 5.6];;
// val it : value = F -5.6

// > eval (IfPositive (Var "x", NumF 1.1, NumF 2.2)) ["x", I 1];;
// val it : value = F 1.1
// > eval (IfPositive (Var "x", NumF 1.1, NumF 2.2)) ["x", I -1];;
// val it : value = F 2.2
// > eval (IfPositive (Var "x", NumF 1.1, NumF 2.2)) ["x", F 1.0];;
// val it : value = F 1.1
// > eval (IfPositive (Var "x", NumF 1.1, NumF 2.2)) ["x", F -1.0];;
// eval it : value = F 2.2

// > eval (Match (Var "x", "zi", Plus (Var "zi", NumI 2), "zf", Plus (Var "zf", NumF 3.))) ["x", I 10];;
// val it : value = I 12
// > eval (Match (Var "x", "zi", Plus (Var "zi", NumI 2), "zf", Plus (Var "zf", NumF 3.))) ["x", F 10.];;
// val it : value = F 13.0


// Test cases for Problem 3

// > to_float (F 5.5);;
// val it : float = 5.5
// > to_float (I 5);;
// val it : float = 5.0
// > to_float (I -11);;
// val it : float = -11.0
// > to_float (F -11.0);;
// val it : float = -11.0


// Test cases for Problem 4

// > eval (to_float_expr (Var "x")) ["x", I 4];;
// val it : value = F 4.0
// > eval (to_float_expr (Var "x")) ["x", F 4.4];;
// val it : value = F 4.4

// > eval (plus_expr (Var "x", Var "y")) ["x", I 6; "y", I 7];;
// val it : value = I 13
// > eval (plus_expr (Var "x", Var "y")) ["x", F 6.1; "y", I 7];;
// val it : value = F 13.1
// > eval (plus_expr (Var "x", Var "y")) ["x", I 6; "y", F 7.2];;
// val it : value = F 13.2
// > eval (plus_expr (Var "x", Var "y")) ["x", F 6.1; "y", F 7.2];;
// val it : value = F 13.3

// > eval (times_expr (Var "x", Var "y")) ["x", I 6; "y", I 7];;
// val it : value = I 42
// > eval (times_expr (Var "x", Var "y")) ["x", F 6.1; "y", I 7];;
// val it : value = F 42.7
// > eval (times_expr (Var "x", Var "y")) ["x", I 6; "y", F 7.2];;
// val it : value = F 43.2
// > eval (times_expr (Var "x", Var "y")) ["x", F 6.1; "y", F 7.2];;
// val it : value = F 43.92


// Test cases for Problem 5

// > eval (add_matches (IVar "x")) ["x", I 5];;
// val it : value = I 5
// > eval (add_matches (IVar "x")) ["x", F 5.5];;
// val it : value = F 5.5
// > eval (add_matches (INeg (ITimes (INumI 3, INumF 5.5)))) [];;
// val it : value = F -16.5
// > eval (add_matches (IIfPositive (IVar "x", INumI 1, IPlus (IVar "y", INumF 4.4)))) ["x", I -2; "y", I 6];;
// val it : value = F 10.4
// > eval (add_matches (IIfPositive (INeg (IVar "x"), IPlus (IVar "y", INumI 5), ITimes (IVar "y", INumI 5)))) ["x", F 2.2; "y", I 4];;
// val it : value = I 20


// Test cases for Problem 6

// > infer (Plus (Var "x", Var "y")) ["x", Int; "y", Int];;
// val it : typ = Int
// > infer (Plus (Var "x", Var "y")) ["x", Float; "y", Float];;
// val it : typ = Float
// > infer (Times (Var "x", Var "y")) ["x", Float; "y", Float];;
// val it : typ = Float
// > infer (Plus (Var "x", Var "y")) ["x", Int; "y", Float];;
// System.Exception: wrong operand type
// > infer (Times (Var "x", Var "y")) ["x", Float; "y", Int];;
// System.Exception: wrong operand type

// > infer (IfPositive (Var "x", Var "y", Var "z")) ["x", Int; "y", Int; "z", Int];;
// val it : typ = Int
// > infer (IfPositive (Var "x", Var "y", Var "z")) ["x", Int; "y", Float; "z", Float];;
// val it : typ = Float
// > infer (IfPositive (Var "x", Var "y", Var "z")) ["x", Float; "y", Int; "z", Int];;
// val it : typ = Int
// > infer (IfPositive (Var "x", Var "y", Var "z")) ["x", Float; "y", Float; "z", Float];;
// val it : typ = Float
// > infer (IfPositive (Var "x", Var "y", Var "z")) ["x", Int; "y", Int; "z", Float];;
// System.Exception: branches of different types
// > infer (IfPositive (Var "x", Var "y", Var "z")) ["x", Float; "y", Float; "z", Int];;
// System.Exception: branches of different types

// > infer (Match (Var "x", "xi", Var "xi", "xf", NumI 1)) ["x", Int];;
// val it : typ = Int
// > infer (Match (Var "x", "xi", Var "xi", "xf", NumI 1)) ["x", Float];;
// val it : typ = Int
// > infer (Match (Var "x", "xi", NumF 1.1, "xf", Var "xf")) ["x", Int];;
// val it : typ = Float
// > infer (Match (Var "x", "xi", NumF 1.1, "xf", Var "xf")) ["x", Float];;
// val it : typ = Float
// > infer (Match (Neg (Var "x"), "xi", Plus (NumI 4, Var "xi"), "xf", IfPositive(Var "xf", NumI 5, Times (NumI 4, NumI 6)))) ["x", Int];;
// val it : typ = Int

// > infer (Plus (NumF 3.4, IntToFloat (NumI 4))) [];;
// val it : typ = Float
// > infer (Plus (NumF 3.4, IntToFloat (NumF 4.4))) [];;
// System.Exception: wrong operand type
// > infer (Plus (NumI 3, IntToFloat (NumI 4))) [];;
// System.Exception: wrong operand type

// Test cases for Problem 7

// > eval (add_casts (IVar "x") ["x", Int]) ["x", I 5];;
// val it : value = I 5
// > eval (add_casts (IVar "x") ["x", Float]) ["x", F 5.5];;
// val it : value = F 5.5
// > eval (add_casts (INeg (ITimes (INumI 3, INumF 5.5))) []) [];;
// val it : value = F -16.5
// > eval (add_casts (IIfPositive (IVar "x", INumI 1, IPlus (IVar "y", INumF 4.4))) ["x", Int; "y", Int]) ["x", I -2; "y", I 6];;
// val it : value = F 10.4
// > eval (add_casts (IIfPositive (INeg (IVar "x"), IPlus (IVar "y", INumI 5), ITimes (IVar "y", INumI 5))) ["x", Float; "y", Int]) ["x", F 2.2; "y", I 4];;
// val it : value = I 20


// Test cases for Problem 9

// > reval (rlower [RDup; RAdd; RAdd]) [1;2] [];;
// val it : int = 4
// > reval (rlower [RPop; RAdd]) [1;2;3] [];;
// val it : int = 5
// > reval (rlower [RSwap; RSub]) [1;2] [];;
// val it : int = -1
// > reval (rlower [RLoad 1; RDup; RAdd; RDup; RLoad 2; RSwap; RAdd; RLoad 0; RPop]) [] [4;5;6];;
// val it : int = 16

