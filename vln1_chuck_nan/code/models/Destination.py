from logic_layer.LLValidator import Validator

class Destination():
    def __init__(self, destination_id = 0, country = '', airport = '', flight_time = '', 
                 distance = '', contact_name = '', contact_number = ''):
        self.__models_validation = Validator()
        self.__country = country
        self.__airport = airport
        self.__flight_time = flight_time
        self.__distance = distance
        self.__contact_name = contact_name
        self.__contact_number = contact_number
        self.__destination_id = destination_id

        # Dictionary keeping all the different header formats for the destination model
        self.__header_format_dict = {"default": self.get_model_header_default_format}

        # Dictionary keeping all the different list formats for the destination model
        self.__list_info_dict = {"default": self.get_model_list_default_info}
        
        # Dictionary that is used in the validate model process 
        self.__validation_dict = {self.get_country: self.set_country, 
                                         self.get_airport: self.set_airport,
                                         self.get_flight_time: self.set_flight_time,
                                         self.get_distance: self.set_distance,
                                         self.get_contact_name: self.set_contact_name,
                                         self.get_contact_number: self.set_contact_number}

        # List keeping the order for the creation process of the destination model
        self.__create_order_list = [
            "country", "airport", "flight time (hours)", "distance (kilometers)", "contact name (first and last)", "contact number"]
        
        # A dictionary where the keys match the order list in order to call the correct set function
        self.__creation_dict = {"country": self.set_country,
                                "airport": self.set_airport,
                                "flight time (hours)": self.set_flight_time,
                                "distance (kilometers)": self.set_distance,
                                "contact name (first and last)": self.set_contact_name,
                                "contact number": self.set_contact_number 
        }

    def __str__(self):
        return "Country: {:>2}\nAirport: {:>2}\nFlight time (hours): {:>2}\nDistance (kilometers): {:>2}\nContact name: {:>2}\nContact number: {:>2}".format(self.__country, self.__airport, self.__flight_time, self.__distance, self.__contact_name, self.__contact_number)


    def get_creation_process(self):
        return self.__create_order_list, self.__creation_dict
    

    def get_model_header_format(self, header_flag):
        '''Takes a header flag as an argument and uses the dictionary to return the correct format back'''
        return self.__header_format_dict[header_flag]()


    def get_model_list_info(self, header_flag):
        '''Takes a header flag as an argument and uses the dictionary to return the correct format back'''
        return self.__list_info_dict[header_flag]()
    
    
    def get_model_header_default_format(self):
        '''Default format for displaying the destination model header'''
        return "{:10}{:20}{:17}{:20}{:15}{:22}{:34}".format("Index:",
                                                          "Country:",
                                                          "Airport:",
                                                          "Flight time:",
                                                          "Distance:",
                                                          "Contact name:",
                                                          "Contact number:")
        

    def get_model_list_default_info(self):
        '''Default format for displaying the destination model in a list'''
        returnObject = ("     {:20}{:17}{:20}{:15}{:22}{:34}|\n".format(
                                                                      self.get_country(),
                                                                      self.get_airport(),
                                                                      self.get_flight_time(),
                                                                      self.get_distance(),
                                                                      self.get_contact_name(),
                                                                      self.get_contact_number()))
        return returnObject


    def raw_info(self):
        '''A function that returns a string in the format that the CSV document needs before writing'''
        return self.__country + "," + self.__airport + "," + self.__flight_time + "," + self.__distance + \
            "," + self.__contact_name + "," + self.__contact_number + "," + self.__destination_id + "\n"


    def get_validation_dict(self):
        return self.__validation_dict


    def get_create_order_list(self):
        return self.__create_order_list


    def handle_key_value(self, key, value):
        '''A special function to handle the validate model function in the ModelAPI for validating the model'''
        return value(key())    


    def get_country(self):
        '''test '''
        return self.__country


    def set_country(self, new_country):
        if self.__models_validation.validate_country(new_country):
            self.__country = new_country
            return True
        return False


    def get_airport(self):
        return self.__airport


    def set_airport(self, new_airport):
        if self.__models_validation.validate_airport(new_airport):
            self.__airport = new_airport
            return True
        return False


    def get_flight_time(self):
        return self.__flight_time


    def set_flight_time(self, new_flight_time):
        if self.__models_validation.validate_flight_time(new_flight_time):
            self.__flight_time = new_flight_time
            return True
        else:
            return False


    def get_distance(self):
        return self.__distance


    def set_distance(self, new_distance):
        if self.__models_validation.validate_distance(new_distance):
            self.__distance = new_distance
            return True
        else:
            return False


    def get_contact_name(self):
        return self.__contact_name


    def set_contact_name(self, new_contact_name):
        if self.__models_validation.validate_contact_name(new_contact_name):
            self.__contact_name = new_contact_name
            return True
        else:
            return False


    def get_contact_number(self):
        return self.__contact_number


    def set_contact_number(self, new_contact_number):
        if self.__models_validation.validate_contact_number(new_contact_number):
            self.__contact_number = new_contact_number
            return True
        else:
            return False


    def get_destination_id(self):
        return self.__destination_id


    def set_destination_id(self, new_id):
        if self.__models_validation.validate_id(new_id):
            self.__destination_id = new_id
            return True
        return False
