# UIDestinations handles all destination related ui functions. It utilizes UIBaseFunctions
# For menu travelling. All sub menus are handled with dictionaries where the key would match
# the user input. And the value would be a function call to a different sub menu or function

class UIDestinations():
    RETURN_MENU_STR = "9. Return 0. Home"

    def __init__(self, LLAPI, modelAPI, UIBaseFunctions):
        self.__ll_api = LLAPI
        self.__modelAPI = modelAPI
        self.__ui_base_functions = UIBaseFunctions

    # All menu functions

    def get_destination_sub_menu(self):
        '''Handles all the configurations of destination sub menu '''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.create_destination,
                    2: self.get_all_destinations,
                    3: self.get_destination_search_menu,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        destination_menu = "1. Create 2. Get all 3. Search by"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(
            destination_menu, nav_dict)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_destination_search_menu(self):
        '''Handles all the configurations of destination search menu'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.get_all_destinations_by_country,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        destination_menu = "1. Country name"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(
            destination_menu, nav_dict)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_selected_destination_menu(self, destination):
        '''Handles all the configuration for a selected destination'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.change_contact_name,
                    2: self.change_contact_number,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        destination_menu = "1. Change contact name 2. Change phone number"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(
            destination_menu, nav_dict, destination)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_select_from_destination_list_menu(self, destination_list):
        '''Handles all the configuration to select a destination from a list'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.__ui_base_functions.select_from_model_list,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        destination_menu = "1. Select destination"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(destination_menu, nav_dict, destination_list)
        if return_value != None and return_value != 0:
            return_value = self.get_selected_destination_menu(return_value)
        return self.__ui_base_functions.check_return_value(return_value)

    #All list functions

    def get_all_destinations(self):
        '''Gets all destinations and calls UIBaseFunctions'''
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        destination_list = self.__ll_api.get_all_destination_list()
        return_value = self.__ui_base_functions.print_model_list(
            destination_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_destination_list_menu(destination_list)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_all_destinations_by_country(self):
        '''Gets all destinations by country'''
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        country = self.__ui_base_functions.get_user_input("country")
        found_destination_list = self.__ll_api.get_destination_list_by_country(
            country)
        return_value = self.__ui_base_functions.print_model_list(found_destination_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_destination_list_menu(return_value)
        return self.__ui_base_functions.check_return_value(return_value)
 
    # Specific functions
    
    def create_destination(self):
        '''Handles the create destination process and calls to write to DB'''
        new_destination = self.__modelAPI.get_model("Destination")
        # This line gets the creation process stored in the model
        # The order of how the model is populated
        # The Creation order is a list with keys that match the creation dict
        # The creation dict has keys that match the order list and every value is a
        # Set function on the model to populate it
        create_order_list, creation_dict = new_destination.get_creation_process()
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
        if self.__ll_api.create_destination(new_destination):
            self.__ui_base_functions.print_model(new_destination)
            self.__ui_base_functions.print_create_destination_results(new_destination)
        else:
            self.__ui_base_functions.print_generic_error_message()
    
    def change_contact_name(self, destination):
        '''Handles the process of changing the contact name for a selected destination and calls to write to DB'''
        new_name = self.__ui_base_functions.get_user_input("new contact name (first and last)")
        # Check to see if the new name is valid
        if destination.set_contact_name(new_name):
            # Check to see if the program is able to write to DB
            # Displays a result message or error message depending on the result
            if self.__ll_api.overwrite_all_models(destination):
                self.__ui_base_functions.print_edit_destination_contact_results(destination)
                self.__ui_base_functions.print_model(destination)
        
            else:
                self.__ui_base_functions.print_generic_error_message()
        else:
            print("Error, {} invalid!".format(new_name))

    def change_contact_number(self, destination):
        '''Handles the process of changing the contact number for the selected destinaiton and calls to write to DB'''
        new_number = self.__ui_base_functions.get_user_input("new phone number")
        # Check to see if the new number is valid
        if destination.set_contact_number(new_number):
            # Check to see if the program is able to write to DB
            # Displays a result message or error message depending on the result
            if self.__ll_api.overwrite_all_models(destination):
                self.__ui_base_functions.print_edit_destination_number_results(destination)
                self.__ui_base_functions.print_model(destination)
            
            else:
                self.__ui_base_functions.print_generic_error_message()
        else:
            print("Error, {} invalid!".format(new_number))
    
    
        
