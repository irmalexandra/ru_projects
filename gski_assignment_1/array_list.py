class IndexOutOfBounds(Exception):
    pass

class NotFound(Exception):
    pass

class Empty(Exception):
    pass

class NotOrdered(Exception):
    pass

class ArrayList:
    def __init__(self, count = 0):
        self.__count = count
        
        if self.__count == 0:
            self.__capacity = 4
        else:
            self.__capacity = 2*count

        self.__arr = [0] * self.__capacity
        self.__is_ordered = False


    #Time complexity: O(n) - linear time in size of list
    def __str__(self):
        if self.__count == self.__capacity:
            self.resize()
        return_string = ""

        for i in range(self.__count):
            if self.__arr[i] != 0:
                if i != self.__count-1: 
                    return_string += str(self.__arr[i]) + ", "
                else:
                    return_string += str(self.__arr[i])

        return return_string

    def increase_count(self):

        for i in range(self.__count + 1):
            if self.__arr[i] == 0:
                return
        if self.__arr[self.__count] != 0:
            self.__count += 1


    #Time complexity: O(n) - linear time in size of list
    def prepend(self, value):
        if self.__count == self.__capacity:
            self.resize()

        for i in range(self.__count, 0, -1):
            self.__arr[i] = self.__arr[i-1]
        
        self.__arr[0] = value
        
        
        self.increase_count()

        self.if_ordered()


    #Time complexity: O(n) - linear time in size of list
    def insert(self, value, index):
        if 0 <= index <= self.__count:

            if self.__count == self.__capacity:
                self.resize()
            
            for i in range(self.__count, index, -1):
                self.__arr[i] = self.__arr[i-1]
            
            self.__arr[index] = value

            self.increase_count()
        
        else:
            raise IndexOutOfBounds()

        self.if_ordered()


    #Time complexity: O(1) - constant time
    def append(self, value):
        if self.__count+1 == self.__capacity:
            self.resize()

        if self.__arr[0] == 0:
            self.__arr[0] = value
        else:
            self.__arr[self.__count] = value

        self.increase_count()

        self.if_ordered()


    #Time complexity: O(1) - constant time
    def set_at(self, value, index):
        if 0 <= index < self.__count:
            self.__arr[index] = value

        else:
            raise IndexOutOfBounds()
        
        
        self.if_ordered()
        

    #Time complexity: O(1) - constant time
    def get_first(self):

        if self.__count != 0:
            return self.__arr[0]

        else:
            raise Empty()


    #Time complexity: O(1) - constant time
    def get_at(self, index):
        if (0 <= index < self.__count) and (self.__arr[index] != 0):
            return self.__arr[index]

        else:
            
            raise IndexOutOfBounds()


    #Time complexity: O(1) - constant time
    def get_last(self):
        if self.__count != 0:
            return self.__arr[self.__count-1]

        else:
            raise Empty()


    #Time complexity: O(n) - linear time in size of list
    def resize(self):
        self.__capacity *= 2
        new_arr = [0] * self.__capacity

        for i in range(self.__count):
            new_arr[i] = self.__arr[i]

        self.__arr = new_arr
        

    #Time complexity: O(n) - linear time in size of list
    def remove_at(self, index):
        if 0 <= index < self.__count:
            for i in range(index, self.__count-1):
                self.__arr[i] = self.__arr[i+1]

            self.__count -= 1     
        else:
            raise IndexOutOfBounds()

    #Time complexity: O(1) - constant time
    def clear(self):
        self.__count = 0

    
    def if_ordered(self):
        if self.__count > 1:
            for i in range(self.__count-1):
                try:    
                    if self.__arr[i] > self.__arr[i+1]:
                        self.__is_ordered = False
                        return self.__is_ordered
                except TypeError:
                    print(end="")
                    
        self.__is_ordered = True

        return self.__is_ordered
            

    #Time complexity: O(n) - linear time in size of list
    def insert_ordered(self, value):
        
        self.if_ordered()

        if self.__is_ordered:

            if self.__count == 0:
                self.__arr[0] = value
            
            else:
                for i in range(self.__count):
                    if self.__arr[i] >= value:
                        index = i
                        break
                    elif self.__arr[i] <= value:
                        index = i+1     

                if index == 0:
                    for i in range(self.__count, 0, -1):
                        self.__arr[i] = self.__arr[i-1]
                    self.__arr[0] = value
                else:
                    for i in range(self.__count-1, index-1, -1):
                        self.__arr[i+1] = self.__arr[i]
                    self.__arr[index] = value
            
            self.increase_count()

        else:
            raise NotOrdered()


    #Time complexity: O(n) - linear time in size of list
    #Time complexity: O(log n) - logarithmic time in size of list
    def find(self, value):
        if self.__is_ordered:
            
            index = self.binary_search(value, start = 0, end = self.__count-1)
            if type(index) == int:
                return index 

        else:

            index = self.unordered_search(value, self.__count)
            
        if type(index) == int:
            return index
        else:
            raise NotFound()


    def unordered_search(self, value, index):
        if index == -1:
            return

        if value == self.__arr[index]:
            return index     

        return self.unordered_search(value, index-1)


    def binary_search(self, value, start, end):
        mid = (start + end) // 2

        if value == self.__arr[0]:
            return 0
    
        if start+1 == end:
            return None # in case the upper calculations start comparing to the placeholder 0's

        if value == self.__arr[mid]:
            return mid
        
        elif value < self.__arr[mid]:
            return self.binary_search(value, start, end = mid)

        elif value > self.__arr[mid]:
            return self.binary_search(value, start = mid, end = end)


    #Time complexity: O(n) - linear time in size of list
    def remove_value(self, value):
        index = self.find(value)

        if type(index) == int:
            for i in range(index, self.__count-1):
                self.__arr[i] = self.__arr[i+1]

            self.__count -= 1     
        else:
            raise NotFound()


if __name__ == "__main__":
    pass
    # add your tests here or in a different file.
    # Do not add them outside this if statement
    # and make sure they are at this indent level
