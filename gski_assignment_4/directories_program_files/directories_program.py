from dll import DLL

class TreeNode:
    def __init__(self, name = ""):
        self.name = name
        self.parent = None
        self.directories = DLL()

    def __gt__(self, node):
        currentName = self.name
        prevName = node.name
        if prevName < currentName:
            return True
        else:
            return False


class DirectoryTree:
    def __init__(self, node = TreeNode("root")):
        self.root = node
        self.current = self.root
        
    def mkdir(self, name: str):
        newDir = TreeNode(name)
        newDir.parent = self.current
        if self.current.directories.search(name) == None:
            self.current.directories.insert(newDir)
            return True
        else:
            return False
            
    def cd(self, name):
        if name == "..":
            if self.current.parent == None:
                return False
            self.current = self.current.parent
        elif cdNode := self.current.directories.search(name):
            self.current = cdNode.data
            return True
        else:
            return False

    def ls(self):
        self.current.directories.sort()
        print(self.current.directories, end="")
        
    def rm(self, name):
        if returnNode := self.current.directories.search(name):
            self.current.directories.remove(returnNode)
            return True
        else:
            return False






# '''
# Note that all the "if False" and "if True" are simply there to
# give you the correct success and error message formats.
# You can use if sentences or try catch or any other
# means of programming you control flow.
# You can make an encapsulting class for everything and start with that,
# rather than starting with the single TreeNode("root").
# Just make sure the input and output of the program is exactly as
# specified and fits with the  expected_out.txt when the tester
# program is run with the original commands.txt.
# Then feel free to make your own, more extensive tests.
# '''

def run_commands_on_tree(tree):

    print("  current directory: " + tree.current.name)
    while True:
        user_input = input()
        command = user_input.split()
        func_call = command[0]
        if func_call == "ls":
            command.append("")
        dir_name = command[1]
        
        if func_call == "mkdir":
            
            print("  Making subdirectory " + dir_name)
            if tree.mkdir(dir_name) == False:
                print("  Subdirectory with same name already in directory")

        elif func_call == "ls":
            print("  Listing the contents of current directory,  " + str(tree.current.name)) # Add the name of the directory here
            tree.ls()

        elif func_call == "cd":
            print("  switching to directory " + dir_name)
                # dir_name is the name of the subdirectory that should now become the current directory

            if dir_name == "..":
                if tree.cd(dir_name) == False:
                    print("Exiting directory program")
                    return 
            else:
                if tree.cd(dir_name) == False:
                    print("  No folder with that name exists")
            print("  current directory: " + str(tree.current.name)) # Add the name of the current directory here

        elif func_call == "rm":
            print("  removing directory " + dir_name)
                # dir_name is the name of the subdirectory that should now become the current directory
            if tree.rm(dir_name):
                print("  directory successfully removed!")
            else:
                print("  No folder with that name exists")
        else:
            print("  command not recognized")



def run_directories_program():
    run_commands_on_tree(DirectoryTree())

if __name__ == "__main__":
    run_directories_program()
    
