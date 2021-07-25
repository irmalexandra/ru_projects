# UIEmployees handles all employee related ui functions. It utilizes UIBaseFunctions
# For menu travelling. All sub menus are handled with dictionaries where the key would match
# the user input. And the value would be a function call to a different sub menu or function

class UIEmployees():
    RETURN_MENU_STR = "9. Return 0. Home"

    def __init__(self, LLAPI, modelAPI, UIBaseFunctions):
        self.__ll_api = LLAPI
        self.__modelAPI = modelAPI
        self.__ui_base_functions = UIBaseFunctions

    #All menu functions
    
    def get_employee_sub_menu(self):
        '''Handles all the configurations of employee sub menu'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.get_create_employee_sub_menu,
                    2: self.get_all_employees,
                    3: self.get_employee_search_menu,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        employee_menu = "1. Create 2. Get all 3. Search by"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(
            employee_menu, nav_dict)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_employee_search_menu(self):
        '''Handles all the configurations of employee search menu'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.get_all_employees_by_name,
                    2: self.get_all_employees_by_title,
                    3: self.get_employees_by_date_sub_menu,
                    4: self.get_pilots_by_airplane_type_sorted,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        employee_menu = "1. Name 2. Title 3. Availability 4. Airplane"
        return_value = self.__ui_base_functions.print_menu(
            employee_menu, nav_dict)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_employees_by_date_sub_menu(self):
        '''Handles all the configurations of employee by date menu'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.get_all_available_employees,
                    2: self.get_all_not_available_employees,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        employee_menu = "1. Available 2. Not available"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(
            employee_menu, nav_dict)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_create_employee_sub_menu(self):
        '''Handles all the configurations of create employee sub menu'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.create_pilot,
                    2: self.create_cabin_crew,
                    3: self.get_employee_search_menu,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        employee_menu = "1. Pilot 2. Cabin crew"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(
            employee_menu, nav_dict)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_selected_employee_menu(self, employee):
        '''Handles all the configurations of selected employee sub menu, Depending on if the
        employee is a Pilot or a Cabincrew a different menu is displayed'''
        # Dictionary to handle navigation in this sub menu
        if employee.get_title() == "Pilot":
            nav_dict = {1: self.get_edit_employee_menu,
                        2: self.get_work_schedule,
                        3: self.change_pilot_licence,
                        9: self.__ui_base_functions.back,
                        0: self.__ui_base_functions.home}
            employee_menu = "1. Edit employee 2. Work schedule 3. Change licence"
            # The return_value is used to keep track of how to return back one menu
            # or all the way to the home screen
            return_value = self.__ui_base_functions.print_menu(
                employee_menu, nav_dict, employee)
            return self.__ui_base_functions.check_return_value(return_value)
        else:
            # Dictionary to handle navigation in this sub menu
            nav_dict = {1: self.get_edit_employee_menu,
                        2: self.get_work_schedule,
                        9: self.__ui_base_functions.back,
                        0: self.__ui_base_functions.home}
            employee_menu = "1. Edit employee 2. Work schedule"
            # The return_value is used to keep track of how to return back one menu
            # or all the way to the home screen
            return_value = self.__ui_base_functions.print_menu(
                employee_menu, nav_dict,  employee)
            return self.__ui_base_functions.check_return_value(return_value)

    def get_edit_employee_menu(self, employee):
        '''Handles all the menu configuration for edit employee'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = employee.get_edit_dict()
        nav_dict[9] = self.__ui_base_functions.back
        edit_order_list = employee.get_edit_order_list()
        edit_menu = "1. Address 2. Home number 3. Mobile number 4. Title 5. Rank"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_edit_model_menu(
            edit_menu, nav_dict, employee, edit_order_list, self.__ll_api)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_select_from_employee_list_menu(self, employee_list):
        '''Handles all the menu configurations to select a employee from a list'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.__ui_base_functions.select_from_model_list,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        employee_menu = "1. Select employee"
        return_value = self.__ui_base_functions.print_menu(employee_menu, nav_dict, employee_list)
        # This check makes sure to display the select_employee_sub_menu 
        # only if the return_value is a employee
        if return_value != None and return_value != 0:
            return_value = self.get_selected_employee_menu(return_value)
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return self.__ui_base_functions.check_return_value(return_value)

    def get_select_from_airplane_type_list_menu(self, airplane_type_list):
        '''Handles all the menu configuration to select a airplane from a list'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.__ui_base_functions.select_from_model_list,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        employee_menu = "1. Select airplane type"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(employee_menu, nav_dict, airplane_type_list)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_select_from_pilots_list_menu(self, employee_list):
        '''Handles all the menu configuration to select a pilot from a list'''
        nav_dict = {1: self.__ui_base_functions.select_from_model_list,
                    2: self.get_pilots_filtered_by_airplane_type,
                    9: self.__ui_base_functions.back, 0: self.__ui_base_functions.home}
        employee_menu = "1. Select employee 2. Filter by airplane type"
        return_value = self.__ui_base_functions.print_menu(
            employee_menu, nav_dict, employee_list)
        # This check makes sure to display the select_employee_sub_menu 
        # only if the return_value is a employee
        if return_value != None and return_value != 0 and return_value != 9:
            return_value = self.get_selected_employee_menu(return_value)
        # This check makes sure you return correctly back one menu
        if return_value == None:
            return_value = 9
        return self.__ui_base_functions.check_return_value(return_value)

    # All list functions

    def get_all_employees(self):
        '''Gets all employees and calls UIBaseFunctions'''
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        employee_list = self.__ll_api.get_employee_list_by_name()
        return_value = self.__ui_base_functions.print_model_list(
            employee_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_employee_list_menu(employee_list)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_all_available_employees(self):
        '''Gets all available employees and calls UIBaseFunctions'''
        # Header flag is used by ModelAPI to fetch the correct header for the table
        header_flag = "date"
        # Sort is used by LLEmployees to return the correct list that was requested
        sort_flag = "not working"
        date = self.__ui_base_functions.get_user_date_input("date","DD-MM-YYYY")
        time = self.__ui_base_functions.get_user_date_input("time", "HH:MM")
        employee_list = self.__ll_api.filter_working(date, time, sort_flag)
        return_value = self.__ui_base_functions.print_model_list(employee_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_employee_list_menu(employee_list)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_all_not_available_employees(self):
        '''Gets all employees availability on a specific day and calls UIBaseFunctions'''
        # Header flag is used by ModelAPI to fetch the correct header for the table
        header_flag = "date"
        # Sort is used by LLEmployees to return the correct list that was requested
        sort_flag = "working"
        date = self.__ui_base_functions.get_user_date_input("date","DD-MM-YYYY")
        time = self.__ui_base_functions.get_user_date_input("time", "HH:MM")
        employee_list = self.__ll_api.filter_working(date, time, sort_flag)
        return_value = self.__ui_base_functions.print_model_list(employee_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_employee_list_menu(employee_list)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_all_employees_by_title(self):
        '''Gets all employees by title and calls UIBaseFunctions'''
        # Title is used to change the header_flag, there could easily be added a option 
        # to display pilots diffrently from cabin crew for example
        title = self.__ui_base_functions.get_user_input("title (Pilot or Cabincrew)")
        if title == "Pilot":
            header_flag = "aircraft"
        else:
            header_flag = "aircraft"
        employee_list = self.__ll_api.get_employee_list_by_title(title)
        return_value = self.__ui_base_functions.print_model_list(employee_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            # This checks the title to display a different sub menu for pilots 
            if title == "Pilot":
                return_value = self.get_select_from_pilots_list_menu(employee_list)
            else:
                return_value = self.get_select_from_employee_list_menu(employee_list)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_all_employees_by_name(self):
        '''Gets all employees found by searching for a name and calls UIBaseFunctions'''
        # Header flag is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        name = self.__ui_base_functions.get_user_input("name")
        found_employee_list = self.__ll_api.get_employee_list_filtered_by_name(
            name)
        return_value = self.__ui_base_functions.print_model_list(found_employee_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_employee_list_menu(return_value)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_pilots_by_airplane_type_sorted(self, employee_list = []):
        '''Gets all employees by airplane type and calls UIBaseFunctions'''
        # Header flag is used by ModelAPI to fetch the correct header for the table
        header_flag = "aircraft"
        employee_list = self.__ll_api.get_pilot_list_sorted_by_airplane_type()
        return_value = self.__ui_base_functions.print_model_list(employee_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_pilots_list_menu(employee_list)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_pilots_filtered_by_airplane_type(self, employee_list = []):
        '''Gets all pilots filtered by airplane type, prompts user to input Airplane name'''
        # Header flag is used by ModelAPI to fetch the correct header for the table
        header_flag = "aircraft"
        airplane = self.__ui_base_functions.get_user_input("airplane name")
        employee_list = self.__ll_api.get_pilot_list_filtered_by_airplane_type(airplane)
        return_value = self.__ui_base_functions.print_model_list(employee_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_pilots_list_menu(employee_list)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_work_schedule(self, employee):
        '''Gets all upcomming voyages for a selected employee by date'''
        date = self.__ui_base_functions.get_user_date_input("date","DD-MM-YYYY")
        employee_work_schedule = self.__ll_api.get_work_schedule_list(employee, date)
        # Header flag is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        return_value = self.__ui_base_functions.print_model_list(employee_work_schedule, self.__modelAPI, header_flag)
        return_value = self.__ui_base_functions.check_return_value(return_value)

    # Specific functions

    def create_pilot(self):
        '''Handles the create pilot process to write to DB'''
        new_emp = self.__modelAPI.get_model("Employee")
        new_emp.set_title("Pilot")
        self.change_pilot_licence(new_emp)
        # This line gets the creation process stored in the model
        # The order of how the model is populated
        # The Creation order is a list with keys that match the creation dict
        # The creation dict has keys that match the order list and every value is a
        # Set function on the model to populate it
        create_order_list, creation_dict = new_emp.get_creation_process()
        for attribute in create_order_list:
            while True:
                # Ask the user to input a value depending on where he is in the for loop
                # thats itterating over the order list
                new_attribute = self.__ui_base_functions.get_user_input(attribute)

                # This line tries to run the set function in the model
                # That corresponds to the attribute being itterated over
                # From the order list if the set function returns true the 
                # For loop continues else a error message is diplsayed
                if creation_dict[attribute](new_attribute):
                    break
                else:
                    print("Error, {} invalid!".format(attribute))
        # If the program is able to create and save a employee to the CSV
        # This returns true and the employee information is printed,
        # else a generic error message is displayed
        if self.__ll_api.create_employee(new_emp):
            self.__ui_base_functions.print_model(new_emp)
            self.__ui_base_functions.print_create_employee_results(new_emp)
        else:
            self.__ui_base_functions.print_generic_error_message()
    
    def create_cabin_crew(self):
        '''Handles the create cabin crew process and calls to write to DB'''
        new_emp = self.__modelAPI.get_model("Employee")
        new_emp.set_title("Cabincrew")
        # This line gets the creation process stored in the model
        # The order of how the model is populated
        # The Creation order is a list with keys that match the creation dict
        # The creation dict has keys that match the order list and every value is a
        # Set function on the model to populate it
        create_order_list, creation_dict = new_emp.get_creation_process()
        for attribute in create_order_list:
            while True:
                # Ask the user to input a value depending on where he is in the for loop
                # thats itterating over the order list
                new_attribute = self.__ui_base_functions.get_user_input(attribute)
                # This line tries to run the set function in the model
                # That corresponds to the attribute being itterated over
                # From the order list if the set function returns true the 
                # For loop continues else a error message is diplsayed
                if creation_dict[attribute](new_attribute):
                    break
                else:
                    print("Error, {} invalid!".format(attribute))
        # If the program is able to create and save a employee to the CSV
        # This returns true and the employee information is printed,
        # else a generic error message is displayed
        if self.__ll_api.create_employee(new_emp):
            self.__ui_base_functions.print_model(new_emp)
            self.__ui_base_functions.print_create_employee_results(new_emp)
        else:
            self.__ui_base_functions.print_generic_error_message()
   
    def change_pilot_licence(self, employee):
        '''Handles the change licence process for pilots and calls to write to DB'''
        # Header flag is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        airplane_type_list = self.__ll_api.get_all_licences(employee)
        return_value = self.__ui_base_functions.print_model_list(airplane_type_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_airplane_type_list_menu(return_value)
        # This check makes sure that the return_value is a AirplaneType
        if return_value != None and return_value != 0:
            # This check make sure that the airplaneID passes through the set function for the pilot licence
            if employee.set_licence(return_value.get_plane_type_id()):   
                return_value = self.__ui_base_functions.print_airplane_licence_results(return_value)
            # This check is only here since sometimes the function is used in the create pilot process
            # And writing down to the DB at that stage in the pilot create process is not neccessary
            if employee.get_name() != "":
                # This check is just to make sure that the pilot is written down into the DB
                # Else a generic error message is displayed
                if self.__ll_api.overwrite_all_models(employee):
                    return
                else:
                    self.__ui_base_functions.print_generic_error_message()
                    return
        # This is here to handle being able to return correctly
        if return_value == 9:
            return 
        return self.__ui_base_functions.check_return_value(return_value)

        
    
