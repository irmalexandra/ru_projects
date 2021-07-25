
def modulus(a, b):  # ONLY NEEDS TO WORK FOR POSITIVE INTEGERS
    if b == 0:
        return "undefined"

    if a < b:
        return a

    return modulus(a - b,b)


def how_many(lis1, lis2, total_counter = 0):
    if len(lis1) >= 1:
        total_counter += compare_elem_to_list(lis1[0], lis2)
        
        total_counter = how_many(lis1[1:], lis2, total_counter)
    return total_counter

def compare_elem_to_list(elem, lis, base_counter = 0):
    if len(lis) >= 1:
        if elem == lis[0]:
            base_counter += 1
        base_counter = compare_elem_to_list(elem, lis[1:], base_counter)
    return base_counter
    


# FEEL FREE TO EDIT THE TESTS AND MAKE THEM BETTER
# REMEMBER EDGE CASES!

def test_modulus(num1, num2):
    print("The modulus of " + str(num1) + " and " + str(num2) + " is " + str(modulus(num1, num2)))

def test_how_many(lis1, lis2):
    print(str(how_many(lis1, lis2)) + " of the items in " + str(lis1) + " are also in " + str(lis2))

def run_recursion_program():

    print("\nTESTING MODULUS:\n")

    test_modulus(8, 3)
    test_modulus(9, 3)
    test_modulus(10, 3)
    test_modulus(11, 3)
    test_modulus(8, 2)
    test_modulus(0, 7)
    test_modulus(15, 5)
    test_modulus(128, 16)
    test_modulus(128, 15)

    #edge cases

    test_modulus(8, 0)
    test_modulus(8, 8)

    print("\nTESTING HOW MANY:\n")

    test_how_many(['a', 'f', 'd', 't'], ['a', 'b', 'c', 'd', 'e'])
    test_how_many(['a', 'b', 'f', 'g', 'a', 't', 'c'], ['a', 'b', 'c', 'd', 'e'])
    test_how_many(['f', 'g', 't'], ['a', 'b', 'c', 'd', 'e'])
    



if __name__ == "__main__":
    run_recursion_program()