import random

class MyHashableKey:

    def __init__(self, int_value, string_value):
        self.int_value = int_value
        self.string_value = string_value
        self.prime_numbers = [2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83,
                              89, 97]
        self.prime_number = 0
        self.multipliers = [10000000000000, -30000000000000]
        self.multiplier = 0

    def __eq__(self, other):
        if hash(self) == hash(other):
            return True
        return False

    def __hash__(self):
        if self.multiplier == 0:
            self.multiplier = random.choice(self.multipliers)
        if self.prime_number == 0:
            self.prime_number = random.choice(self.prime_numbers)
        return_value = self.int_value
        string_concat = ""
        for char in self.string_value:
            string_concat += str(ord(char))
        return_value += int(string_concat)
        for char in self.string_value:
            return_value ^= (ord(char) + self.int_value)
        return return_value * self.multiplier * self.prime_number
