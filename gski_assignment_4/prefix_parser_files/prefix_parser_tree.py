
import sys
from enum import Enum


class DivisionByZero(Exception):
    pass


class UnknownInTree(Exception):
    pass


class OutputFormat(Enum):
    PREFIX = 0
    INFIX = 1
    POSTFIX = 2


class Tokenizer:
    def __init__(self):
        self.position = 0

    def get_next_token(self, statement):
        i = self.position
        while i < len(statement) and statement[i] != " ":
            i += 1
        ret_val = statement[self.position:i]
        self.position = i + 1
        return ret_val


class PrefixParseTree:

    MATH_OPERATORS = ["+", "-", "*", "/"]

    class Node:
        def __init__(self, data=None, left=None, right=None):
            self.data = data
            self.left = left
            self.right = right

    def __init__(self):
        self.tokenizer = Tokenizer()
        self.root = None
        self.format = 0
        self.OPERATION_DICT = {
            "+": self.__add,
            "-": self.__subtract,
            "/": self.__divide,
            "*": self.__multiply
        }
        self.original_statement = ""
        self.negX = False # Boolean used for when x is preceeded by - in solve

    def __add(self, left, right):
        return left+right

    def __subtract(self, left, right):
        return left-right

    def __divide(self, left, right):
        return left / right

    def __multiply(self, left, right):
        return left*right

    def __str__(self):
        if self.format == 0:
            return self.prefix()
        elif self.format == 1:
            return self.infix()
        elif self.format == 2:
            return self.postfix()

    def prefix(self):
        return self.__prefix_recur(self.root)

    def infix(self):
        return self.__infix_recur(self.root)

    def postfix(self):
        return self.__postfix_recur(self.root)

    def __prefix_recur(self, node):
        ret_str = ""
        if node.left == None:
            return str(node.data)
        ret_str += node.data + " "
        ret_str += self.__prefix_recur(node.left) + " "
        ret_str += self.__prefix_recur(node.right)
        return ret_str

    def __infix_recur(self, node):
        ret_str = ""
        if node.left == None:
            return str(node.data)

        ret_str += "(" + self.__infix_recur(node.left)
        ret_str += " " + node.data + " "
        ret_str += self.__infix_recur(node.right) + ")"
        return ret_str

    def __postfix_recur(self, node):
        ret_str = ""
        if node.left == None:
            return str(node.data)
        ret_str += self.__postfix_recur(node.left) + " "
        ret_str += self.__postfix_recur(node.right) + " "
        ret_str += node.data
        return ret_str
    
    def set_format(self, out_format):
        self.format = out_format.value

    def load_statement_string(self, statement):
        self.original_statement = statement
        self.root = self.__load_statement_string_recur(statement)

    def __load_statement_string_recur(self, statement):
        token = self.tokenizer.get_next_token(statement)

        if self.tokenizer.position == len(statement):
            return self.Node(token)
        else:
            if token not in self.MATH_OPERATORS:
                if token.isalpha():
                    return self.Node(token)
                else:
                    return self.Node(int(token))
            left = self.__load_statement_string_recur(statement)
            right = self.__load_statement_string_recur(statement)

            node = self.Node(token, left, right)
            if node.right.data == "x" and node.data == "-":
                self.negX = True
            return node

    def root_value(self):
        return self.__root_value_recur(self.root)

    def __root_value_recur(self, node):
        if str(node.data).isalpha():
            raise UnknownInTree()
        if node.data not in self.MATH_OPERATORS:
            return node.data
        if node.left != None:
            left = self.__root_value_recur(node.left)
        if node.right != None:
            right = self.__root_value_recur(node.right)
        if node.data in self.MATH_OPERATORS:
            if right == 0 and node.data == "/":
                raise DivisionByZero()

            result = self.OPERATION_DICT[node.data](left, right)
            return result

    def simplify_tree(self):
        self.__simplify_tree_recur(self.root)

    def __simplify_tree_recur(self, node):
        if node.data not in self.MATH_OPERATORS:
            return node.data
        if node.left != None:
            left = self.__simplify_tree_recur(node.left)
        if node.right != None:
            right = self.__simplify_tree_recur(node.right)
        if node.data in self.MATH_OPERATORS \
                and (left not in self.MATH_OPERATORS and right not in self.MATH_OPERATORS):

            if (type(left).__name__ == "int" or type(left).__name__ == "float") \
                    and (type(right).__name__ == "int" or type(right).__name__ == "float")\
                    and not (right == 0 and node.data == "/"):

                node.data = self.OPERATION_DICT[node.data](left, right)
                node.left = None
                node.right = None
        return node.data

    def solve_tree(self, root_value):
        multiplier = 1
        if self.negX == False:
            multiplier = -1
        new_statement = self.original_statement.replace("x", str(root_value * multiplier))
        new_tree = PrefixParseTree()
        new_tree.load_statement_string(new_statement)
        x = int(new_tree.root_value())
        return x * multiplier

# This is a tester function to test that
# the output and/or error message from the
# prefix_tree operations are correct.

new_tree = PrefixParseTree()
new_tree.load_statement_string("/ 4 x")
print(new_tree.solve_tree(2))


def test_prefix_parser(str_statement, solve=False, root_value=0):

    if solve == True:
        prefix_tree = PrefixParseTree()
        prefix_tree.load_statement_string(str_statement)
        print("PREFIX: " + str(prefix_tree))
        print("The value of x if the root_value is " + str(root_value) +
              " is: " + str(prefix_tree.solve_tree(root_value)))
    else:
        prefix_tree = PrefixParseTree()
        prefix_tree.load_statement_string(str_statement)
        print("PREFIX: " + str(prefix_tree))
        prefix_tree.set_format(OutputFormat.INFIX)
        print("INFIX: " + str(prefix_tree))
        prefix_tree.set_format(OutputFormat.POSTFIX)
        print("POSTFIX: " + str(prefix_tree))

        str_print = "The value of the tree is: "
        try:
            str_print += str(prefix_tree.root_value())
        except DivisionByZero:
            str_print += str("A division by zero occurred")
        except UnknownInTree:
            str_print += str("There is an unknown value in the tree")
        print(str_print)

        print("SIMPLIFIED:")
        prefix_tree.simplify_tree()
        prefix_tree.set_format(OutputFormat.PREFIX)
        print("PREFIX: " + str(prefix_tree))
        prefix_tree.set_format(OutputFormat.INFIX)
        print("INFIX: " + str(prefix_tree))
        prefix_tree.set_format(OutputFormat.POSTFIX)
        print("POSTFIX: " + str(prefix_tree))

    print("\n\n")


if __name__ == "__main__":
    org_out = sys.stdout
    fout = open(sys.path[0] + "/parse_out.txt", "w+")
    sys.stdout = fout
    f = open(sys.path[0] + "/prefix_statements.txt", "r")
    previous_line = None
    for line in f:
        some_split = line.split()
        if some_split[0] == "solve":
            test_prefix_parser(previous_line.strip(), True, int(some_split[1]))
        test_prefix_parser(line.strip())
        previous_line = line
    f.close()
    sys.stdout = org_out
    fout.close()
