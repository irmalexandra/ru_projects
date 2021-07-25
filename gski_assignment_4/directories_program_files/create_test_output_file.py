import sys    
    


import directories_program as directories

def run_directories_test():
    
    orig_stdin = sys.stdin
    fin = open(sys.path[0] + "/commands.txt")
    sys.stdin = fin
    try:
        directories.run_directories_program()
    except:
        print("Exiting directory program in a BAD way")
    sys.stdin = orig_stdin


if __name__ == "__main__":
    orig_stdout = sys.stdout
    fout = open('out.txt', 'w+')
    sys.stdout = fout
    run_directories_test()
    sys.stdout = orig_stdout
    fout.close()