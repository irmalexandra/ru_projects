from my_array_deque import ArrayDeque
from my_linked_list import LinkedList

class Stack:
    
    def __init__(self, typeStr = ""):
        if typeStr == "array":
            self.stack = ArrayDeque()
        elif typeStr == "linked":
            self.stack = LinkedList()

    def push(self, data):
        self.stack.pushFront(data)

    def pop(self):
        return self.stack.popFront()

    def getSize(self):
        return self.stack.getSize()

