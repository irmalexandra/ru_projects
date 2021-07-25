from datetime import datetime
# UIBaseFunctions handles almost all user interaction, and displays.
# Other UI classes call UIBasefunctions to print out lists, menus, edit menus
# And hnadle user input
# It also has all the primary navigation functions for going back, and home and exiting the program
  
class UIBaseFunctions():
    UI_DIVIDER_INT = 140
    DEVIATION_INT = 2
    LINE_LEN = 88
    WALL = "|"
    T_LEN = 26
    TRUE_LEN = LINE_LEN + 2 * T_LEN
    DISTINGUISHER = "{}\n{}\n\n".format("_"*TRUE_LEN, "_"*TRUE_LEN)


    def print_nan_airlines(self):
        print(self.DISTINGUISHER)
        print(self.T_LEN*" " + "███╗   ██╗ █████╗ ███╗   ██╗     █████╗ ██╗██████╗ ██╗     ██╗███╗   ██╗███████╗███████╗")
        print(self.T_LEN*" " + "████╗  ██║██╔══██╗████╗  ██║    ██╔══██╗██║██╔══██╗██║     ██║████╗  ██║██╔════╝██╔════╝")
        print(self.T_LEN*" " + "██╔██╗ ██║███████║██╔██╗ ██║    ███████║██║██████╔╝██║     ██║██╔██╗ ██║█████╗  ███████╗")
        print(self.T_LEN*" " + "██║╚██╗██║██╔══██║██║╚██╗██║    ██╔══██║██║██╔══██╗██║     ██║██║╚██╗██║██╔══╝  ╚════██║")
        print(self.T_LEN*" " + "██║ ╚████║██║  ██║██║ ╚████║    ██║  ██║██║██║  ██║███████╗██║██║ ╚████║███████╗███████║")
        print(self.T_LEN*" " + "╚═╝  ╚═══╝╚═╝  ╚═╝╚═╝  ╚═══╝    ╚═╝  ╚═╝╚═╝╚═╝  ╚═╝╚══════╝╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝")
        print(self.DISTINGUISHER, end="")


    def __init__(self):
        pass

    # Special functions
    
    def back(self, optional = None):
        ''' Handles the back function in the program '''
        return 9


    def home(self, optional = None):
        ''' Handles the home function in the program '''
        return 0


    def exit_program(self):
        ''' Exits the program '''
        exit()
    

    def check_return_value(self, return_value):
        ''' Handles most the logic for returning correctly through all the sub menus if back or home is pressed '''
        if return_value == 0:
            return 0
        if return_value == 9:
            return 9
        return return_value
    
    # User input functions

    def get_user_int_input(self, key_word):
        ''' Prompts user for input, returns int input if possible else asks user for input again '''
        while True:
            try:
                return_value = int(input("Enter {}: ".format(key_word)))
                return return_value
            except ValueError:
                print("Invalid value for {}".format(key_word))


    def get_user_input(self, key_word):
        ''' Prompts user for input returns the input '''
        return input("Enter {}: ".format(key_word))


    def get_user_selection(self, collection, key_word = "selection"):
        ''' Prompts user to select a menu option (dictionary) or to select a object in a list(index)
         returns the value from the dictionary or the object from the list ''' 
        while True:
            try:
                selection = int(input("Enter {}: ".format(key_word)))
                if type(collection).__name__ == "dict":
                    if selection in collection.keys():
                        return selection
                    else:
                        print("Invalid selection")
                elif type(collection).__name__ == "list":
                    if len(collection) >= selection:
                        return selection
                    else:
                        print("Invalid index")
                else:
                    print("Invalid {}".format(key_word))
            except ValueError:
                print("Invalid input")


    def get_user_date_input(self,date_or_time, date_or_time_format):
        ''' Prompts user for a date input on a specific format, and returns the date if its in a correct format '''
        while True:
            
            new_date_or_time = input("Enter {} ({}): ".format(date_or_time, date_or_time_format))
            try:
                if date_or_time_format == "DD-MM-YYYY":
                    datetime.strptime(new_date_or_time,'%d-%m-%Y')
                    return new_date_or_time

                if date_or_time_format == "HH:MM":
                    datetime.strptime(new_date_or_time,'%H:%M')
                    new_date_or_time += ":00"
                    return new_date_or_time
            except:
                print("Invalid date format for ({})!".format(date_or_time_format))
    
    # Menu Functions
    
    def print_menu(self, menu_str, nav_dict, model_list = None ,return_menu_str="9. Return 0. Home"):
        ''' Prints all the different sub menus in the system, it takes a 
        menu_str to display, and a nav_dict to match
        all the options to a function call, a model list is optional in 
        special cases when a select_from_model_list or 
        select_from_crew_list is a possible option
        '''
        while True:
            print("-" * self.UI_DIVIDER_INT)
            print("|{}{}{}|".format(menu_str, " "*(self.UI_DIVIDER_INT - len(menu_str) - len(return_menu_str) - self.DEVIATION_INT), return_menu_str))
            print("-" * self.UI_DIVIDER_INT)
            return_value = self.get_user_selection(nav_dict)
            # Checks to see if the model list is none, to make sure 
            # a arguement is not needed for the function call ahead
            if model_list == None:
                return_value = nav_dict[return_value]()
                return_value = self.check_return_value(return_value)
            # Checks to see if the model list is set, this would mean
            # that the function call about to be called in the nav dict
            # requires the model list as an arguement
            elif model_list != None:
                return_value = nav_dict[return_value](model_list)
                return_value = self.check_return_value(return_value)
            # All the return and home logic from different menus
            # Return 0 needs to return 0 again to go home all the way up to call chain
            # Return 9 is the back function so it needs to return None in order for it to stop
            # in the previous menu
            if return_value == 0:
                return 0
            if return_value == 9:
                return 
            if return_value != None:
                return return_value


    def print_edit_model_menu(self,menu_str, nav_dict, model, edit_order_list, llapi, return_menu_str="9. Save"):
        ''' Handles the edit process for different model classes in the system, takes in a 
        menu_str, nav_dict, model, edit_order_list, llapi and a optional return menu str 
        '''

        # This function mainly handles all the user interaction in regard to the edit process
        # of the model that it takes in. At the end of the function it calls the LLAPI in order to
        # store the changes in the DB
        while True:
            self.print_model(model)
            print("-" * self.UI_DIVIDER_INT)
            print("|{}{}{}|".format(menu_str, " "*(self.UI_DIVIDER_INT - len(menu_str) -
                                                   len(return_menu_str) - self.DEVIATION_INT), return_menu_str))
            print("-" * self.UI_DIVIDER_INT)
            try:
                return_value = self.get_user_selection(nav_dict)
                # Check for back and return home logic
                if return_value != 9 and return_value != 0:
                    # The edit order list matches the menu being displayed.
                    # So if the user selects a attribute to edit the value_to_edit
                    # would become the attribute he wanted to edit.
                    value_to_edit = edit_order_list[return_value-1] # -1 for human readability
                    while True:
                        new_value = self.get_user_input(value_to_edit)
                        # The nav dict here is dictionary where the key matches the user selection to a 
                        # value where the value would be a set function in the model for the selected attribute
                        # to edit
                        check = nav_dict[return_value](new_value)
                        # Check to see if the set function returned true
                        if check == True:
                            return_value = check
                            break
                        else:
                            print("Invalid {}".format(edit_order_list[return_value-1]))# for -1 human readability
                        
                else:
                    # Check to see if the DL was able to write the changes successfully
                    if llapi.overwrite_all_models(model): 
                        print(type(model).__name__+" edited successfully")
                        return
                    else:
                        self.print_generic_error_message()
                        return
            except IndexError:
                print("Invalid index")
    
    # Print Model functions
    
    def print_model_list(self, model_list, modelAPI, header_flag):
        ''' Prints a string return from the ModelAPI depending on the header flag for the model '''
        # Check to see if the list is empty
        if len(model_list) > 0:
            print("-" * self.UI_DIVIDER_INT)
            print(modelAPI.get_model_header_format(model_list[0], header_flag))
            print(modelAPI.get_model_list_info(model_list, header_flag))
            return self.check_return_value(model_list)
        else:
            print("No search results")
            return 


    def print_model(self, model):
        ''' Prints the __str__ function for the model '''
        print("-" * self.UI_DIVIDER_INT)
        print(model)
        print("-" * self.UI_DIVIDER_INT)
        return self.check_return_value(model)

    # Select from list functions
    
    def select_from_model_list(self, model_list):
        ''' Handles the user input for selecting a item from a list '''
        return_value = self.get_user_selection(model_list, "index")
        return_value = self.print_model(model_list[return_value-1]) # -1 for human readability
        return self.check_return_value(return_value)


    def select_from_crew_list(self, crew_list):
        ''' Handles the user input for selecting a item from the crew list '''
        return_value = self.get_user_selection(crew_list, "index")
        return_value = crew_list[return_value-1] # -1 for human readability
        return self.check_return_value(return_value)

    # All result functions for different operations in the system

    def print_add_crew_results(self, employee):
        print("-" * self.UI_DIVIDER_INT)
        print("Name: {}\nRank: {} \nAdded to voyage successfully!".format(employee.get_name(), employee.get_rank()))


    def print_airplane_added_results(self, airplane):
        print("Insignia {} added to voyage successfully!".format(airplane.get_insignia()))


    def print_airplane_licence_results(self, airplane):
        print("Airplane type {} selected successfully!".format(airplane.get_plane_type_id()))
        return airplane


    def print_create_voyage_results(self, destination, date, time):
        print("Voyage to {} departing on {} at {} created successfully!".format(destination.get_airport(), date, time))


    def print_create_employee_results(self, employee):
        print("Employee {} created successfully!".format(employee.get_name()))


    def print_create_destination_results(self, destination):
        print("Destination {}, {} created successfully!".format(destination.get_airport(), destination.get_country()))


    def print_edit_destination_number_results(self, destination):
        print("Destination contact info updated successfully! New contact number {}".format(destination.get_contact_number()))


    def print_edit_destination_contact_results(self, destination):
        print("Destination contact info updated successfully! New contact name {}".format(destination.get_contact_name()))


    def print_generic_error_message(self):
        print("Something went wrong... ")
