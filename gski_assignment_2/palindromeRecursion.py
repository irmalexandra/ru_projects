from my_linked_list import LinkedList
EVENLENGTH = 4
ODDLENGTH = 3

def generatePalindrome(typeStr = ""):
    if typeStr == "odd":
        palindromeOdd = LinkedList()
        for i in range(ODDLENGTH):
            if i % 2 == 0:
                palindromeOdd.pushFront(0)
            else:
                palindromeOdd.pushFront(1)
        return palindromeOdd
    else:
        palindromeEven = LinkedList()
        for i in range(EVENLENGTH):
            if i < EVENLENGTH / 2:
                if i == 0:
                    palindromeEven.pushFront(1)
                elif i % 2 == 0:
                    palindromeEven.pushFront(1)
                else:
                    palindromeEven.pushFront(0)
            else:
                if i % 2 == 1:
                    palindromeEven.pushFront(1)
                else:
                    palindromeEven.pushFront(0)
        return palindromeEven

def palindrome(head, originalNode = None, counter = 1, middle = 0):
    
    if counter == 1: # Need to remember the original node that was sent in, special case
        originalNode = head

    if head.getNextNode() == None: # Base case, end of the linked list.
        middle = counter // 2
        
        if originalNode.data == head.data: # Check if the first and last nodes are the same
            palindromeCheck = True
            originalNode = originalNode.getNextNode() # Advance the original node by 1
            return middle, palindromeCheck, originalNode
        
        palindromeCheck = False
        return middle, palindromeCheck, originalNode

    else:
        middle, palindromeCheck, originalNode = palindrome(head.getNextNode(), originalNode, counter+1) # Recursion call advancing the node forward by one.
        
        if middle >= counter: # We only need to check each node until we cover half of the list.
            if counter == 1: # Edge case to return correctly out of the original function call.
                return palindromeCheck
            else:
                return middle, palindromeCheck, originalNode

        if palindromeCheck: # Check to see the previous recursions result 
            if originalNode.data == head.data:
                originalNode = originalNode.getNextNode() # Advance the original node by 1
                return middle, palindromeCheck, originalNode
            else:
                palindromeCheck = False
                return middle, palindromeCheck, originalNode
        else:
            return middle, palindromeCheck, originalNode
        
        
        
    


def main():

    evenPalindrome = generatePalindrome("even") # Generate even length Palindrome
    oddPalindrome = generatePalindrome("odd") # Generate odd length Palindrome

    evenNotPalindrome = LinkedList()
    oddNotPalindrome = LinkedList()
    for i in range(EVENLENGTH):
        evenNotPalindrome.pushFront(i)

    for i in range(ODDLENGTH):
        oddNotPalindrome.pushFront(i)



    testPalindrome = LinkedList()

    testPalindrome.pushFront("a")
    testPalindrome.pushFront("b")
    testPalindrome.pushFront("a")
    testPalindrome.pushFront("a")
    testPalindrome.pushFront("b")
    testPalindrome.pushFront("b")
    testPalindrome.pushFront("a")
    testPalindrome.pushFront("a")
    testPalindrome.pushFront("b")
    testPalindrome.pushFront("a")

    #print(evenPalindrome)
    print(palindrome(evenPalindrome.getNode()))
    
    #print(oddPalindrome)    
    print(palindrome(oddPalindrome.getNode()))

    #print(evenNotPalindrome)    
    print(palindrome(evenNotPalindrome.getNode()))

    #print(oddNotPalindrome)    
    print(palindrome(oddNotPalindrome.getNode()))

    print(palindrome(testPalindrome.getNode()))


main()