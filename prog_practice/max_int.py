# first we establish the values of num_int and previous_int
# then we prompt the user to input a number inside a loop that runs while the input is not a negative number
# then the program checks to see if the the latest input is higher than the previous and if so, saves it
# the loop stops one a negative number is input, and it prints out the max_int

num_int = 0
while num_int >= 0:
    previous_int = num_int
    num_int = int(input("Input a number: "))    # Do not change this line
    if num_int >= pervious_int:
        max_int = num_int
print("The maximum is", max_int)    # Do not change this line

