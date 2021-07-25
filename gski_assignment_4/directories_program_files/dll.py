class Node():
    def __init__(self):
        self.prev = None
        self.data = None
        self.next = None

class DLL:
    def __init__(self):
        self.head = Node()
        self.tail = Node()

        self.head.next = self.tail
        self.tail.prev = self.head

        self.size = 0

    def __len__(self):
        return self.size

    def __str__(self):
        return_str = ''

        if self.size != 0:
            node = self.head.next
            while node.next != None:
                return_str +=  str(node.data.name) + "\n"
                node = node.next

        return return_str

    def insert(self, value):
        self.size += 1
        current_node = self.head.next

        new_node = Node()
        new_node.data = value

        new_node.next = current_node
        new_node.prev = current_node.prev

        current_node.prev.next = new_node
        current_node.prev = new_node

        current_node = new_node

    def remove(self, node):
        next_node = node.next
        prev_node = node.prev

        next_node.prev = prev_node
        prev_node.next = next_node
        self.size -= 1


    def search(self, value, selected_node = None):
        if selected_node == None:
            selected_node = self.head.next

        if selected_node.data != None:
            if selected_node.data.name == value:
                return selected_node
            else:
                return self.search(value, selected_node.next)
        else:
            return None

    def sort(self):
        if self.size == 0:
            return

        currentNode = self.head.next.next
        for i in range(self.size):
            nextNode = currentNode.next
            for j in range(self.size):
                prevNode = currentNode.prev
                if prevNode.data != None and currentNode.data != None:
                    if prevNode.data > currentNode.data:
                        self.__swap(currentNode, prevNode)
                    else:
                        break
                else:
                    break
            currentNode = nextNode


    def __swap(self, currentNode, prevNode):

        nextNode = currentNode.next

        currentNode.prev = prevNode.prev
        currentNode.next = prevNode

        nextNode.prev = prevNode
        prevNode.next = nextNode

        prevNode.prev.next = currentNode
        prevNode.prev = currentNode




