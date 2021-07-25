# UIAirplanes handles all airplanes related ui functions. It utilizes UIBaseFunctions
# For menu travelling. All sub menus are handled with dictionaries where the key would match
# the user input. And the value would be a function call to a different sub menu or function

class UIAirplanes():
    RETURN_MENU_STR = "9. Return 0. Home"


    def __init__(self, LLAPI, modelAPI, UIBaseFunctions):
        self.__ll_api = LLAPI
        self.__modelAPI = modelAPI
        self.__ui_base_functions = UIBaseFunctions

    # All menu functions
    
    def get_airplanes_sub_menu(self):
        '''Handles all the configurations of airplanes sub menu'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1:self.create_airplane,
                    2:self.get_all_airplanes,
                    3:self.get_airplane_search_menu,
                    9:self.__ui_base_functions.back,
                    0:self.__ui_base_functions.home}
        airplane_menu = "1. Create 2. Get all 3. Search by"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(airplane_menu,nav_dict)
        return self.__ui_base_functions.check_return_value(return_value)
    
    def get_airplane_search_menu(self):
        '''Handles all the configurations of airplanes search menu'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.get_all_airplanes_by_date,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        airplane_menu = "1. Date"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(airplane_menu,nav_dict)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_select_from_airplane_type_list_menu(self, airplane_type_list):
        '''Handles all the configuration to select a airplane type from a list'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.__ui_base_functions.select_from_model_list,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        employee_menu = "1. Select airplane type"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(employee_menu, nav_dict, airplane_type_list)
        return self.__ui_base_functions.check_return_value(return_value)
        
    def get_select_from_airplane_list_menu(self,airplanes_list):
        '''Handles all the configuration to select a airplane from a list'''
        # Dictionary to handle navigation in this sub menu
        nav_dict = {1: self.__ui_base_functions.select_from_model_list,
                    9: self.__ui_base_functions.back,
                    0: self.__ui_base_functions.home}
        airplane_menu = "1. Select airplane"
        # The return_value is used to keep track of how to return back one menu
        # or all the way to the home screen
        return_value = self.__ui_base_functions.print_menu(airplane_menu,nav_dict,airplanes_list)
        return self.__ui_base_functions.check_return_value(return_value)
    
    def get_all_airplanes(self):
        '''Gets all airplanes and calls UIBaseFunctions'''
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        airplanes_list = self.__ll_api.get_all_airplane_list()
        return_value = self.__ui_base_functions.print_model_list(airplanes_list,self.__modelAPI,header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_airplane_list_menu(airplanes_list)
        return self.__ui_base_functions.check_return_value(return_value)

    def get_all_airplanes_by_date(self):
        '''Gets all airplanes by date and calls UIBaseFunctions'''
        # Header is used by ModelAPI to fetch the correct header for the table
        header_flag = "default"
        new_date = self.__ui_base_functions.get_user_date_input("date", "DD-MM-YYYY")
        new_time = self.__ui_base_functions.get_user_date_input("time", "HH:MM")
        
        airplane_list = self.__ll_api.get_all_airplane_list_by_period(new_date, new_time)
        return_value = self.__ui_base_functions.print_model_list(
            airplane_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_airplane_list_menu(airplane_list)
        return self.__ui_base_functions.check_return_value(return_value)
    
    
    def create_airplane(self):
        '''Handles the create destination process and calls to write to DB'''
        new_airplane = self.__modelAPI.get_model("Airplane")
        airplane_type_list = self.__ll_api.get_airplane_type_list()
        existing_airplane_types_list = self.__ll_api.get_airplane_type_list()
        header_flag = "default"
        return_value = self.__ui_base_functions.print_model_list(airplane_type_list, self.__modelAPI, header_flag)
        # This check makes sure that if the list is empty to not display the select from list sub menu
        if type(return_value).__name__ == "list":
            return_value = self.get_select_from_airplane_type_list_menu(return_value)
            # This check makes sure that if the list is empty to not display the select from list sub menu
            if return_value != None and return_value != 0:
                make = return_value.get_make()
                model = return_value.get_model()
                airplane_type_id = return_value.get_plane_type_id()
                new_airplane.set_make(make)
                new_airplane.set_model(model)
                while True:
                    insignia = self.__ui_base_functions.get_user_input("Insignia (must be 3 letters): TF-")
                    # This check makes sure the insigna is valid
                    check = new_airplane.set_insignia("TF-" +insignia)
                    if check:
                        # This check makes sure the airplane is created successfully and store in DB
                        # Either prints out that the airplane already exits, is created successfully or a 
                        # Generic error message if something else goes wrong
                        if self.__ll_api.create_airplane(new_airplane, existing_airplane_types_list,insignia):
                            print("\nAirplane created!\n{}".format(new_airplane))
                            return
                        else:
                            print("\n{}\nAlready exists!".format(new_airplane.get_insignia()))
                    else:
                        print("\nInvalid insignia {}\n".format(insignia))
        return self.__ui_base_functions.check_return_value(return_value)

    
    
    
        