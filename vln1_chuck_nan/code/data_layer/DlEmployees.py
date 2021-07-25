import os
from os import path
class DLEmployees():
    SSN = 0
    NAME = 1
    ADDRESS = 2
    HOME_NUMBER = 3
    MOBILE_NUBER = 4
    EMAIL = 5
    TITLE = 6
    RANK = 7
    LICENSE = 8
    CSV_ROWS = 9


    def __init__(self, modelAPI):
        self.__modelAPI = modelAPI


    def pull_all_employees(self):
        '''Opens a csv and returns a list of all employees (ssn, name, address, home number, mobile number, e-mail, rank, title)'''
        if path.exists('./repo/employees.csv') and path.exists('./repo/employees_temp.csv'):
            filestream = open("./repo/employees.csv", "r") # Makes sure that if something happens in the overwriting process
            os.remove("./repo/employees_temp.csv")         # it will be covered
        elif  path.exists('./repo/employees.csv') and path.exists('./repo/employees_temp.csv') == False:
            filestream = open("./repo/employees.csv", "r")
        elif path.exists('./repo/employees.csv') == False and path.exists('./repo/employees_temp.csv'):
            filestream = open("./repo/employees_temp.csv", "r")
        else:
            print("employee data files not found")
            return

        all_crew_list = [] # The list that will be returned once it has been filled with instances
        for line in filestream:
            line_list = line.strip().split(",") # Get the columns into a list to work with
            if len(line_list) == self.CSV_ROWS:
                check_list = [] # This list holds the output from the validator
                new_emp = self.__modelAPI.get_model('Employee')
                
                check_list.append(new_emp.set_ssn(line_list[DLEmployees.SSN]))
                check_list.append(new_emp.set_name(line_list[DLEmployees.NAME])) # Setting all the nessecariy info into the model instance
                check_list.append(new_emp.set_address(line_list[DLEmployees.ADDRESS]))
                check_list.append(new_emp.set_home_num(line_list[DLEmployees.HOME_NUMBER]))
                check_list.append(new_emp.set_mobile_num(line_list[DLEmployees.MOBILE_NUBER]))
                check_list.append(new_emp.set_email(line_list[DLEmployees.EMAIL]))
                check_list.append(new_emp.set_rank(line_list[DLEmployees.RANK]))
                check_list.append(new_emp.set_title(line_list[DLEmployees.TITLE]))
                check_list.append(new_emp.set_licence(line_list[DLEmployees.LICENSE]))
                if False not in check_list: # If the validator returned a false bool anywhere, the instance is not appended and thus not 
                    all_crew_list.append(new_emp) # sent down to the other layers, this excludes the header and "corrupt" lines
        filestream.close()

        return all_crew_list


    def overwrite_all_employees(self, emp_list):
        # employee_file.write(new_emp_str)
        HEADER = "ssn,name,address,homenumber,mobilenumber,email,role,rank,licence\n"
        filestream2 = open("./repo/employees_temp.csv", "a")
        filestream2.write(HEADER) # Writes the first line of the temp as the header
        for emp_info in emp_list: 
            filestream2.write(emp_info.raw_info()) # "Appends" the raw info lines into the temp, raw info being csv friendly strings
        filestream2.close()
        os.remove("./repo/employees.csv")
        os.rename("./repo/employees_temp.csv", "./repo/employees.csv")
        return True
        
    def append_employee(self,employee):
        '''Adds a new employee to the employee string'''
        employee_stream = open('./repo/employees.csv', 'a')
        emp_str = employee.raw_info() # Gets the "raw info" from the instance, which is a csv friendly string
        employee_stream.write(emp_str)
        employee_stream.close()
        return True      
