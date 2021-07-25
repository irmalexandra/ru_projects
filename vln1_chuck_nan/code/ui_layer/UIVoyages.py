# UIVoyages handles all voyage related ui functions. It utilizes UIBaseFunctions
# For menu travelling. All sub menus are handled with dictionaries where the key would match
# the user input. And the value would be a function call to a different sub menu or function

class UIVoyages():
    RETURN_MENU_STR = "9. Return 0. Home"

    def __init__(self, LLAPI, modelAPI, UIBaseFunctions):
        self.__ll_api = LLAPI
        self.__modelAPI = modelAPI
        self.__ui_base_functions = UIBaseFunctions

    # All menu functions
    
    def get_voyage_sub_menu(self):
        '''Handles all the configurations of Voyage sub menu'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.create_voyage,
                    2: self.get_all_voyages,
                    3: self.get_voyage_search_menu,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        voyage_menu = "1. Create 2. Get all 3. Search by"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(
            voyage_menu, nav_dict)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_voyage_search_menu(self):
        '''Handles all the configurations of voyage search menu'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.get_all_voyages_by_airport,
                    2: self.get_all_voyages_by_date,
                    3: self.get_all_empty_voyages,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        voyage_menu = "1. Airport 2. Period 3. Unstaffed voyages"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(
            voyage_menu, nav_dict)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_select_from_voyage_list_menu(self, voyage_list):
        '''Handles all the menu configurations to select a voyage from a list menu'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.__ui_base_functions.select_from_model_list,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        voyage_menu = "1. Select voyage"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(voyage_menu, nav_dict, voyage_list)
        if return_value != None and return_value != 0:
            # This check is to make sure to display a correct sub menu if the airplane is missing
            if return_value.get_airplane_insignia() == ".":
                return_value = self.get_selected_voyage_no_airplane_menu(return_value)
            # This check is to make sure to display a correct if a crew member is missing
            elif return_value.get_staffed() == "Not staffed":
                return_value = self.get_selected_voyage_empty_menu(return_value)
            else:
                return_value = self.get_selected_voyage_menu(return_value)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_select_from_airplane_list_menu(self, airplane_list, voyage):
        '''Handles all the menu configuration to select a airplane from a list menu'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.__ui_base_functions.select_from_model_list,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        voyage_menu = "1. Select airplane"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(voyage_menu, nav_dict, airplane_list)
        if return_value != None and return_value != 0:
            # This check makes sure that the airplane is added successfully to the voyage
            if self.__ll_api.add_airplane_to_voyage(voyage, return_value):
                self.__ui_base_functions.print_airplane_added_results(return_value)
            else:
                self.__ui_base_functions.print_generic_error_message()
        return self.__ui_base_functions.check_return_value(return_value)

    def get_selected_voyage_no_airplane_menu(self, voyage):
        '''Handles all the configuration for a selected voyage menu without a airplane menu'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.duplicate_voyage,
                    2: self.repeat_voyage,
                    3: self.get_all_airplanes,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        voyage_menu = "1. Duplicate 2. Repeat 3. Add Airplane"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(
            voyage_menu, nav_dict, voyage)
        if return_value != None and return_value != 0:
            # This checks makes sure a airplane has been added to the voyage
            # Before updating the pointer to the new instance that was pulled
            # After the airplane was added
            if voyage.get_airplane_insignia() != ".":
                voyage = self.__ll_api.update_voyage_pointer(voyage)
                return_value = self.get_selected_voyage_empty_menu(voyage)
        return self.__ui_base_functions.check_return_value(return_value)
    
    def get_selected_voyage_menu(self, voyage):
        '''Handles all the configuration for a selected voyage with a airplane and staffed menu'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.duplicate_voyage,
                    2: self.repeat_voyage,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        voyage_menu = "1. Duplicate 2. Repeat"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(
            voyage_menu, nav_dict, voyage)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_selected_voyage_empty_menu(self, voyage):
        '''Handles all the configuration for a selected voyage that is unstaffed'''
        nav_dict = {1: self.duplicate_voyage,
                    2: self.repeat_voyage,
                    3: self.get_add_crew_voyage_menu,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        voyage_menu = "1. Duplicate 2. Repeat 3. Add Crew"
        return_value = self.__ui_base_functions.print_menu(
            voyage_menu, nav_dict, voyage)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_add_crew_voyage_menu(self, voyage):
        '''Handles all the configuration to add crew members to a selected voyage menu'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.get_all_captains_by_airplane_and_availability,
                    2: self.get_all_copilots_by_airplane_and_availability,
                    3: self.get_all_fsm_by_availability,
                    4: self.get_all_flight_attendants_by_availability,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        edit_menu = "Add: 1. Captain 2. Co-Pilot 3. Flight Service Manager 4. Flight Attendant"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(
            edit_menu, nav_dict, voyage)
        if return_value != None and return_value != 0:
            voyage = self.__ll_api.update_voyage_pointer(voyage)
            return_value = self.get_select_from_add_crew_list_menu(return_value, voyage)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_select_from_add_crew_list_menu(self, crew_list, voyage):
        '''Handles all the configuration to select a crew member from a list'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.__ui_base_functions.select_from_crew_list,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        voyage_menu = "1. Select crew member"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(voyage_menu, nav_dict, crew_list)
        if return_value != None and return_value != 0:
            voyage = self.__ll_api.update_voyage_pointer(voyage)
            # This checks makes sure a crew member has been added to the voyage
            # Before updating the pointer to the new instance that was pulled
            # After the crew member was added
            if self.__ll_api.add_employee_to_voyage(voyage, return_value):
                self.__ui_base_functions.print_add_crew_results(return_value)
            else:
                self.__ui_base_functions.print_generic_error_message()
        
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
        return self.__ui_base_functions.check_return_value(return_value)

    # All list functions

    def get_all_airplanes(self, voyage):
        '''Gets a list of all airplanes and calls UIBaseFunctions'''
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        airplane_list = self.__ll_api.get_all_available_airplane_list(voyage)
        return_value = self.__ui_base_functions.print_model_list(
            airplane_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_airplane_list_menu(airplane_list, voyage)
        return self.__ui_base_functions.check_return_value(return_value)
    
    def get_all_voyages(self):
        '''Gets a list of all voyages and calls UIBaseFunctions'''
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        voyage_list = self.__ll_api.get_all_voyage_list()
        return_value = self.__ui_base_functions.print_model_list(
            voyage_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_voyage_list_menu(voyage_list)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_all_voyages_by_airport(self):
        '''Gets a list of voyages by airport name and calls UIBaseFunctions'''
        airport = self.__ui_base_functions.get_user_input("airport")
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        voyage_list = self.__ll_api.get_all_voyage_list_by_airport(airport)
        return_value = self.__ui_base_functions.print_model_list(
            voyage_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_voyage_list_menu(voyage_list)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_all_voyages_by_date(self):
        '''Gets a list of voyages by date and calls UIBaseFunctions'''
        start_date = self.__ui_base_functions.get_user_date_input("start date", "DD-MM-YYYY")
        end_date = self.__ui_base_functions.get_user_date_input("end date", "DD-MM-YYYY")
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        voyage_list = self.__ll_api.get_all_voyage_list_by_period_list(start_date, end_date)
        return_value = self.__ui_base_functions.print_model_list(
            voyage_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_voyage_list_menu(voyage_list)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_all_empty_voyages(self):
        '''Gets a list of all empty voyages and calls UIBaseFunctions'''
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        voyage_list = self.__ll_api.get_all_empty_voyage_list()
        return_value = self.__ui_base_functions.print_model_list(
            voyage_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_voyage_list_menu(voyage_list)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_all_captains_by_airplane_and_availability(self, voyage):
        '''Gets a list of all captains available for the airplane in the voyage and calls UIBaseFunctions'''
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "aircraft"
        rank = "Captain"
        crew_list = self.__ll_api.get_filtered_employee_list_for_voyage(rank,voyage)
        return_value = self.__ui_base_functions.print_model_list(
            crew_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_add_crew_list_menu(crew_list, voyage)
        return self.__ui_base_functions.check_return_value(return_value)
    
    def get_all_copilots_by_airplane_and_availability(self, voyage):
        '''Gets a list of all copilots available for the airplane in the voyage and calls UIBaseFunctions'''
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "aircraft"
        rank = "Copilot"
        crew_list = self.__ll_api.get_filtered_employee_list_for_voyage(rank,voyage)
        return_value = self.__ui_base_functions.print_model_list(
            crew_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_add_crew_list_menu(crew_list, voyage)
        return self.__ui_base_functions.check_return_value(return_value)
    
    def get_all_fsm_by_availability(self, voyage):
        '''Gets a list of all Flight Service Managers available for the voyage and calls UIBaseFunctions'''
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        rank = "Flight Service Manager"
        crew_list = self.__ll_api.get_filtered_employee_list_for_voyage(rank,voyage)
        return_value = self.__ui_base_functions.print_model_list(
            crew_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_add_crew_list_menu(crew_list, voyage)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_all_flight_attendants_by_availability(self, voyage):
        '''Gets all Flight Attendants available for the voyage and calls UIBaseFunctions'''
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        rank = "Flight Attendant"
        crew_list = self.__ll_api.get_filtered_employee_list_for_voyage(rank,voyage)
        return_value = self.__ui_base_functions.print_model_list(
            crew_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_add_crew_list_menu(crew_list, voyage)
        return self.__ui_base_functions.check_return_value(return_value)

        
    
    # All Special functions

    def create_voyage(self):
        '''Handles the creation process for voyages and calls to write to DB'''
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        destination_list = self.__ll_api.get_all_destination_list()
        return_value = self.__ui_base_functions.print_model_list(destination_list, self.__modelAPI, header_flag)
         # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_destination_list_menu(return_value)
        # This check make sure that a destination is selected for the voyage before the date and time is entered
        if return_value != None and return_value != 0:
            new_date = self.__ui_base_functions.get_user_date_input("new date", "DD-MM-YYYY")
            new_time = self.__ui_base_functions.get_user_date_input("new time", "HH:MM")
            # This check makes sure that the voyage is created and written to the DB
            # Then prints a message if its created successfully or a generic error message
            if self.__ll_api.create_voyage(return_value, new_date, new_time):
                self.__ui_base_functions.print_create_voyage_results(return_value, new_date, new_time)
            else:
                self.__ui_base_functions.print_generic_error_message()         

    def duplicate_voyage(self, voyage):
        '''Handles the duplication process of a selected voyage and calls to write to DB'''
        new_date = self.__ui_base_functions.get_user_date_input("new date", "DD-MM-YYYY")
        new_time = self.__ui_base_functions.get_user_date_input("new time", "HH:MM")
        
        return_value = self.__ll_api.duplicate_voyage(voyage, new_date, new_time)
        # Check to make sure the duplication process was successfull
        if return_value == True:
            print("Voyage duplication successful!")
        else:
            print("Flight times not available")
        
    
    def repeat_voyage(self, voyage):
        '''Handles the repeat voyage process of a selected voyage and calls to write to DB'''
        interval = self.__ui_base_functions.get_user_int_input("repeat inverval (in days)")
        end_date = self.__ui_base_functions.get_user_date_input("end date", "DD-MM-YYYY")
        
        return_value = self.__ll_api.repeat_voyage(voyage, interval, end_date)
        # check to make sure the repeat process was successfull
        if return_value == True:
            print("Creation of reccuring voyage successful!")
            return self.__ui_base_functions.check_return_value(9)
        else:
            print("Flight times not available")
            return self.__ui_base_functions.check_return_value(9)
        
