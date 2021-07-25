class Node():
    def __init__(self):
        self.prev_node = None
        self.data = None
        self.next_node = None

    def get_prev(self, reverse_toggle):
        if reverse_toggle == False:
            return self.prev_node
        else:
            return self.next_node

    def set_prev(self, node, reverse_toggle):
        if reverse_toggle == False:
            self.prev_node = node
        else:
            self.next_node = node

    def get_next(self, reverse_toggle):
        if reverse_toggle == False:
            return self.next_node
        else:
            return self.prev_node

    def set_next(self, node, reverse_toggle):
        if reverse_toggle == False:
            self.next_node = node
        else:
            self.prev_node = node

    def get_data(self):
        return self.data

    def set_data(self, data):
        self.data = data


class DLL:
    def __init__(self):
        self.head = Node()
        self.tail = Node()

        self.rev_toggle = False

        self.head.set_next(self.tail, self.rev_toggle)
        self.tail.set_prev(self.head, self.rev_toggle)

        self.current_position = 0
        self.current_node = self.tail
        self.size = 0

    def __len__(self):
        return self.size

    def __str__(self):
        return_str = ''

        if self.size != 0:
            node = self.head.get_next(self.rev_toggle)
            while node.get_data() != None:
                return_str += str(node.get_data()) + " "
                node = node.get_next(self.rev_toggle)

        return return_str

    def insert(self, value):
        self.size += 1
        if self.current_position == -1:  # to make sure the current position is changed to the end of the list
            self.current_position = self.size-1
            self.current_node = self.tail

        new_node = Node()
        new_node.set_data(value)

        new_node.set_next(self.current_node, self.rev_toggle)
        new_node.set_prev(self.current_node.get_prev(
            self.rev_toggle), self.rev_toggle)

        self.current_node.get_prev(self.rev_toggle).set_next(
            new_node, self.rev_toggle)
        self.current_node.set_prev(new_node, self.rev_toggle)

        self.current_node = new_node

    def remove(self, current_node=None, is_remove_all=False):
        if current_node == None: # for when remove_all is not in use
            current_node = self.current_node
            if self.current_node.get_data() == None:
                return

        next_node = current_node.get_next(self.rev_toggle)
        prev_node = current_node.get_prev(self.rev_toggle)

        next_node.set_prev(prev_node, self.rev_toggle)
        prev_node.set_next(next_node, self.rev_toggle)

        if is_remove_all == False:
            self.current_node = next_node
            if self.current_node == self.tail:  # edge case when the last node is removed
                self.current_position = -1

        self.size -= 1

    def remove_all(self, value, selected_node=None, selected_position=0):
        current_removed = False
        if selected_node == None:
            selected_node = self.head.get_next(self.rev_toggle)

        if selected_node.get_data() != None:
            self.remove_all(value, selected_node.get_next(
                self.rev_toggle), selected_position+1)
            if selected_node.get_data() == value:
                if selected_node == self.current_node:
                    current_removed = True

                self.remove(selected_node, True)
                if selected_position < self.current_position:
                    self.current_position -= 1
        if current_removed:
            self.current_node = self.head.get_next(self.rev_toggle)
            self.current_position = 0

    def sort(self):
        if self.size == 0:
            return
        if self.rev_toggle:
            tail = self.tail
            head = self.head
            self.head = tail
            self.tail = head
            self.rev_toggle = False
        currentNode = self.head.get_next(
            self.rev_toggle).get_next(self.rev_toggle)
        for i in range(self.size):
            nextNode = currentNode.get_next(self.rev_toggle)
            for j in range(self.size):
                prevNode = currentNode.get_prev(self.rev_toggle)
                if prevNode.get_data() != None and currentNode.get_data() != None:
                    if prevNode.get_data() > currentNode.get_data():
                        self.swap(currentNode, prevNode)
                    else:
                        break
                else:
                    break
            currentNode = nextNode

        self.current_node = self.head.get_next(self.rev_toggle)
        self.current_position = 0

    def swap(self, currentNode, prevNode):

        nextNode = currentNode.get_next(self.rev_toggle)

        currentNode.set_prev(prevNode.get_prev(
            self.rev_toggle), self.rev_toggle)
        currentNode.set_next(prevNode, self.rev_toggle)

        nextNode.set_prev(prevNode, self.rev_toggle)
        prevNode.set_next(nextNode, self.rev_toggle)

        prevNode.get_prev(self.rev_toggle).set_next(
            currentNode, self.rev_toggle)
        prevNode.set_prev(currentNode, self.rev_toggle)

    def get_value(self):
        return self.current_node.get_data()

    def move_to_next(self):
        if self.current_position == self.size:
            self.current_position = -1
            self.current_node = self.tail
            
        if self.current_position != -1:
            if self.current_position + 1 == self.size:
                self.current_position = -1
                self.current_node = self.tail
            else:
                self.current_position += 1
                self.current_node = self.current_node.get_next(self.rev_toggle)

    def move_to_prev(self):
        if self.current_position == -1:
            self.current_position = self.size-1
            self.current_node = self.tail.get_prev(self.rev_toggle)

        elif self.current_position - 1 != -1:
            self.current_position -= 1
            self.current_node = self.current_node.get_prev(self.rev_toggle)

    def move_to_pos(self, position):
        if 0 <= position <= self.size-1:
            self.current_node = self.head.get_next(self.rev_toggle)
            for i in range(position):
                self.current_node = self.current_node.get_next(self.rev_toggle)
            self.current_position = position

        elif position == self.size:
            self.current_position = -1
            self.current_node = self.tail

    def reverse(self):
        if self.size == 0:
            return
        tail = self.tail
        head = self.head
        self.head = tail
        self.tail = head
        self.rev_toggle = not self.rev_toggle
        self.current_position = 0
        self.current_node = self.head.get_next(self.rev_toggle)