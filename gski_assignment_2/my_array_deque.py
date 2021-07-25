
class ArrayDeque():
    def __init__(self):
        self.capacity = 4
        self.arr = [None]*self.capacity
        self.front = 0
        self.size = 0

    def __str__(self):
        returnStr = ""
        if self.size > 0:
            index = self.front
            for i in range(self.size-1):
                if index == self.capacity:
                    returnStr += str(self.arr[i - (index-self.front)]) + " "
                
                else:
                    returnStr += str(self.arr[index]) + " "
                    index += 1
            
            back = (self.front + self.size -1) % self.capacity
            returnStr += str(self.arr[back])  
        return returnStr

    def pushFront(self, data):
        if self.size == self.capacity:
            self.__resize()

        front = (self.front -1) % self.capacity
        self.arr[front] = data
        self.front = front
        self.size += 1
    
    def pushBack(self, data):
        if self.size == self.capacity:
            self.__resize()

        back = (self.front + self.size) % self.capacity

        self.arr[back] = data
        self.size +=1

    def popFront(self):
        returnVal = self.arr[self.front]
        self.arr[self.front] = None

        self.front = (self.front + 1) % self.capacity
        
        if self.size > 0:
            self.size -= 1

        return returnVal

    def popBack(self):
        back = (self.front + self.size -1) % self.capacity
        returnVal = self.arr[back]
        
        self.arr[back] = None
        
        if self.size > 0:
            self.size -= 1

        return returnVal

    def getSize(self):
        return self.size
    
    def __resize(self):

        self.capacity *= 2
        new_arr = [0]*self.capacity

        index = self.front
        for i in range(self.size):
            # The index is used to keep track of where we began rearranging our old elements into the new array
            # Once the index == self.size we need to loop back to the 0 element in the old array. 
            # These calculations make sure that the new array is ordered once its resized. 
            if index == self.size:
                new_arr[i] = self.arr[i - (index-self.front)] 
            
            else:
                new_arr[i] = self.arr[index]
                index += 1                 
        self.front = 0
        self.arr = new_arr
