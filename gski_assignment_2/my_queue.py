from my_array_deque import ArrayDeque
from my_linked_list import LinkedList

class Queue:
    
    def __init__(self, typeStr = ""):
        if typeStr == "array":
            self.queue = ArrayDeque()
        elif typeStr == "linked":
            self.queue = LinkedList()

    def add(self, data):
        self.queue.pushBack(data)

    def remove(self):
        return self.queue.popFront()

    def getSize(self):
        return self.queue.getSize()

