from logic_layer.LLValidator import Validator
class Airplane():
    def __init__(self, insignia='', make='', model='', capacity=0):
        self.__models_validation = Validator()
        self.__insignia = insignia
        
        self.__make = make
        self.__model = model
        self.__capacity = capacity
        
        # A dictionary that is used in the validate model process
        self.__validation_dict = {self.get_insignia:self.set_insignia}

        self.__status = "Not in use"
        self.__current_destination = "N/A"
        self.__flight_number = "N/A"
        self.__date_available = "N/A"


    def __str__(self):
        return "Insignia: {:>2}\nMake: {:>2}\nModel: {:>2}\nMaximum seats: {:>2}".format(self.__insignia, self.__make, self.__model, self.__capacity)


    def raw_info(self):
        '''A function that returns a string in the format that the CSV document needs before writing'''
        return "NA" + self.__make + self.__model + "," + self.__insignia + "\n"


    def get_validation_dict(self):
        return self.__validation_dict


    def set_status(self, status):
        self.__status = status
    

    def get_status(self):
        return self.__status
    

    def set_current_destination(self, destination):
        self.__current_destination = destination
    

    def get_current_destination(self):
        return self.__current_destination


    def set_flight_number(self,flight_number):
        self.__flight_number = flight_number


    def get_flight_number(self):
        return self.__flight_number
    

    def set_date_available(self, date):
        self.__date_available = date
    

    def get_date_available(self):
        return self.__date_available
    

    def handle_key_value(self, key, value):
        '''A special function to handle the validate model function in the ModelAPI for validating the model'''
        return value(key())


    def get_insignia(self):
        return self.__insignia


    def set_insignia(self, new_insignia):
        if self.__models_validation.validate_airplane_insignia(new_insignia):
            self.__insignia = new_insignia
            return True
        else:
            return False


    def get_make(self):
        return self.__make


    def set_make(self, new_make):
        if self.__models_validation.validate_airplane_make(new_make):
            self.__make = new_make
            return True
        else:
            return False


    def get_model(self):
        return self.__model


    def set_model(self, new_model):
        if self.__models_validation.validate_airplane_model(new_model):
            self.__model = new_model
            return True
        else:
            return False


    def get_capacity(self):
        return self.__capacity


    def set_capacity(self, new_capacity):
        if self.__models_validation.validate_airplane_capacity(new_capacity):
            self.__capacity = new_capacity
            return True
        else:
            return False


    def change_date_time_format(self, date_string):
        if date_string != "N/A":
            date_string = date_string[:-3].replace("T", " ")
        return date_string


    def get_model_header_format(self, header_flag):
        '''The default header format for displaying airplanes in a list'''
        return "{:10}{:14}{:12}{:12}{:14}{:19}{:17}{:17}{:23}".format("Index: ",
                                                                    "Insignia:",
                                                                    "Make:",
                                                                    "Model:",
                                                                    "Capacity:",
                                                                    "Status:",
                                                                    "Destination:",
                                                                    "Flight number:",
                                                                    "Date/time available:")
 
    def get_model_list_info(self, header_flag):
        '''The default format for displaying a list of airplanes'''
        returnObject = "     {:14}{:12}{:12}{:14}{:19}{:17}{:17}{:23}|\n".format(
                                                                      self.get_insignia(),
                                                                      self.get_make(),
                                                                      self.get_model(),
                                                                      self.get_capacity(),
                                                                      self.get_status(),
                                                                      self.get_current_destination(),
                                                                      self.get_flight_number(),
                                                                      self.change_date_time_format(self.get_date_available()))
        return returnObject

