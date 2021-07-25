my_int = int(input('Give me an int >= 0: '))

# bin_str starts as an empty string
bin_str = ''
div_int = my_int

# for loop runs until we reach -1 because otherwise an input of 0 would yield no results
for i in range(my_int,-1,-1):
    temp_bin_str = str(div_int % 2)
    bin_str = bin_str + temp_bin_str
    div_int = div_int // 2

    if div_int == 0:
        bin_str = bin_str[::-1]
        print("The binary of", my_int, "is", bin_str)
        break
