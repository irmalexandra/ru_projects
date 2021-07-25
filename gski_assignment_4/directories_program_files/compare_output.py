
f1 = open("out.txt")
f2 = open("expected_out.txt")
f3 = open("out_diff.txt", "w+")

line_number = 1
for line1, line2 in zip(f1, f2):
    if line1.strip() != line2.strip():
        print("Difference in line " + str(line_number))
        f3.write("Difference in line " + str(line_number) + "\n")
    line_number += 1

f1.close()
f2.close()
f3.close()