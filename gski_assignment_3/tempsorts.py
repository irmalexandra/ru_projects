def sort(self):

        self.rev_toggle = False
        tempNode = self.mergeSort(self.sentinel.get_next(self.rev_toggle))
        tempNode.set_prev(self.sentinel, self.rev_toggle)
        self.sentinel.set_next(tempNode, self.rev_toggle)

        print()

    def mergeSort(self, left):

        if left == None or left.get_next(self.rev_toggle) == None:
            return left

        right = self.split(left)

        left = self.mergeSort(left)
        right = self.mergeSort(right)

        val = self.merge(left, right)
        return val

    def split(self, head):

        runner = head
        middle = head

        while True:
            if runner.get_next(self.rev_toggle) == None:
                break
            if runner.get_next(self.rev_toggle).get_next(self.rev_toggle) == None:
                break
            if runner.get_next(self.rev_toggle).get_data() == None:
                break
            if runner.get_next(self.rev_toggle).get_next(self.rev_toggle).get_data() == None:
                break

            runner = runner.get_next(self.rev_toggle).get_next(self.rev_toggle)
            middle = middle.get_next(self.rev_toggle)

        temp = middle.get_next(self.rev_toggle)
        middle.set_next(None, self.rev_toggle)

        return temp

    def merge(self, first, second):

        if first == None:
            second.set_next(self.sentinel, self.rev_toggle)
            return second

        if second == None:
            first.set_next(self.sentinel, self.rev_toggle)
            return first

        if first.get_data() == None:  # edge case for the sentinel
            return second

        if second.get_data() == None:
            return first

        firstVal = first.get_data()
        secondVal = second.get_data()

        if firstVal < secondVal:
            first.set_next(self.merge(first.get_next(
                self.rev_toggle), second), self.rev_toggle)
            first.get_next(self.rev_toggle).set_prev(first, self.rev_toggle)
            first.set_prev(None, self.rev_toggle)

            if first.get_next(self.rev_toggle) == first:
                first.set_next(self.sentinel, self.rev_toggle)
                self.sentinel.set_prev(first, self.rev_toggle)
                
            return first

        else:
            second.set_next(self.merge(first, second.get_next(
                self.rev_toggle)), self.rev_toggle)
            second.get_next(self.rev_toggle).set_prev(second, self.rev_toggle)
            second.set_prev(None, self.rev_toggle)

            if second.get_next(self.rev_toggle) == second:
                second.set_next(self.sentinel, self.rev_toggle)
                self.sentinel.set_prev(second, self.rev_toggle)

            return second

    # def sort(self, selected_node=None):

    #     self.rev_toggle = False
    #     selected_node = self.sentinel.get_next(self.rev_toggle)
    #     while True:
    #         self.__swap(selected_node)

    #         selected_node = selected_node.get_next(self.rev_toggle)
    #         if selected_node.get_data() == None:
    #             self.current_position = 0
    #             self.current_node = self.sentinel.get_next(self.rev_toggle)
    #             return

    #     self.rev_toggle = False
    #     if selected_node == None:
    #         selected_node = self.sentinel.get_next(self.rev_toggle).get_next(self.rev_toggle)

    #     self.__swap(selected_node)

    #     if type(selected_node.get_next(self.rev_toggle).get_data()).__name__ != 'NoneType':
    #         self.sort(selected_node.get_next(self.rev_toggle))

    # def __swap(self, selected_node):
    #     if type(selected_node.get_prev(self.rev_toggle).get_data()).__name__ != 'NoneType':

    #         if selected_node.get_data() < selected_node.get_prev(self.rev_toggle).get_data():
    #             next_node = selected_node.get_next(self.rev_toggle)
    #             prev_node = selected_node.get_prev(self.rev_toggle)

    #             selected_node.get_next(self.rev_toggle).set_prev(prev_node, self.rev_toggle)
    #             selected_node.get_prev(self.rev_toggle).set_next(next_node, self.rev_toggle)
    #             selected_node.set_prev(prev_node.get_prev(self.rev_toggle), self.rev_toggle)
    #             prev_node.get_prev(self.rev_toggle).set_next(selected_node, self.rev_toggle)
    #             prev_node.set_prev(selected_node, self.rev_toggle)
    #             selected_node.set_next(prev_node, self.rev_toggle)

    #             self.__swap(selected_node)


    def sort(self):
        self.rev_toggle = False
        reset = True
        counter = 0
        
        while counter < self.size:
            if reset:
                current_node = self.sentinel.get_next(self.rev_toggle)
                next_node = current_node.get_next(self.rev_toggle)
            reset = True
            while next_node.get_data() != None:
                if current_node.get_data() > next_node.get_data():
                    self.swap(current_node, next_node)
                    next_node = current_node.get_next(self.rev_toggle)
                    counter = 0
                else:
                    current_node = current_node.get_next(self.rev_toggle)
                    next_node = current_node.get_next(self.rev_toggle)
                    reset = False
                    counter += 1


    # def swap(self, current_node, next_node):
    #     self.rev_toggle = True
    #     prev_node = current_node.get_prev(self.rev_toggle)

    #     current_node.set_next(next_node.get_next(self.rev_toggle), self.rev_toggle)
    #     current_node.set_prev(next_node, self.rev_toggle)

    #     next_node.set_next(current_node, self.rev_toggle)
    #     next_node.set_prev(prev_node, self.rev_toggle)

    #     prev_node.set_next(next_node, self.rev_toggle)
    #     current_node.get_next(self.rev_toggle).set_prev(current_node, self.rev_toggle)