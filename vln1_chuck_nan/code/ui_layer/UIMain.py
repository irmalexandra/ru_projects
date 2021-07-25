import os
clear = lambda: os.system('cls') #on Windows System
from logic_layer.LLAPI import LLAPI
from models.ModelAPI import ModelAPI
from ui_layer.UIBaseFunctions import UIBaseFunctions
from ui_layer.UIEmployees import UIEmployees
from ui_layer.UIVoyages import UIVoyages
from ui_layer.UIDestinations import UIDestinations
from ui_layer.UIAirplanes import UIAirplanes
import string

# UIMain is primarly used to initiate all the other classes in the system. 
# He also has the configuration for the main menu display
class UIMain():
    RETURN_MENU_STR = "0. Exit"

    def __init__(self):
        self.__LLAPI = LLAPI()
        self.__ui_base_functions = UIBaseFunctions()
        self.__modelAPI = ModelAPI()
        self.__ui_employees = UIEmployees(
                                        self.__LLAPI,
                                        self.__modelAPI, 
                                        self.__ui_base_functions)
        self.__ui_voyages = UIVoyages(
                                        self.__LLAPI, 
                                        self.__modelAPI, 
                                        self.__ui_base_functions)
        self.__ui_destinations = UIDestinations(
                                        self.__LLAPI, 
                                        self.__modelAPI, 
                                        self.__ui_base_functions)
        self.__ui_airplanes = UIAirplanes(
                                        self.__LLAPI, 
                                        self.__modelAPI, 
                                        self.__ui_base_functions)
    # This function calls on the ui base function print menu to show the main menu
    def display_main_menu(self):
        while True:
            clear()
            # The dictionary holds function calls to all the different sub menus and base functions
            nav_dict = {1: self.__ui_employees.get_employee_sub_menu,
                        2: self.__ui_voyages.get_voyage_sub_menu,
                        3: self.__ui_destinations.get_destination_sub_menu,
                        4: self.__ui_airplanes.get_airplanes_sub_menu,
                        0: self.__ui_base_functions.exit_program}
            self.__ui_base_functions.print_nan_airlines()
            main_menu = "1. Employees 2. Voyages 3. Destinations 4. Airplanes"
            return_value = self.__ui_base_functions.print_menu(main_menu, nav_dict, None, self.RETURN_MENU_STR)

   