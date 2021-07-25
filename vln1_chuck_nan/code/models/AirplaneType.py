from logic_layer.LLValidator import Validator


class AirplaneType():
    def __init__(self, plane_type_id="", make="", model="", capacity=""):
        self.__model_validation = Validator()

        self.__plane_type_id = plane_type_id
        self.__make = make
        self.__model = model
        self.__capacity = capacity

        # Dictionary keeping all the different header formats for the destination model
        self.__header_format_dict = {"default": self.get_model_header_default_format}

        # Dictionary keeping all the different list formats for the destination model
        self.__list_info_dict = {"default": self.get_model_list_default_info}


    def get_model_header_format(self, header_flag):
        '''Takes a header flag as an argument and uses the dictionary to return the correct format back'''
        return self.__header_format_dict[header_flag]()


    def get_model_list_info(self, header_flag):
        '''Takes a header flag as an argument and uses the dictionary to return the correct format back'''
        return self.__list_info_dict[header_flag]()


    def get_model_header_default_format(self):
        '''Default format for displaying the destination model header'''
        return "{:10}{:20}{:17}{:20}{:71}".format("Index:",
                                                  "Airplane type:",
                                                  "Make:",
                                                  "Model:",
                                                  "Capacity:")


    def get_model_list_default_info(self):
        '''Default format for displaying the destination model in a list'''
        returnObject = ("     {:20}{:17}{:20}{:71}|\n".format(self.get_plane_type_id(),
                                                              self.get_make(),
                                                              self.get_model(),
                                                              self.get_capacity()))
        return returnObject


    def raw_info(self):
        '''A function that returns a string in the format that the CSV document needs before writing'''
        return str(self.__plane_type_id) + "," + str(self.__make) + "," + str(self.__model) + "," + str(self.__capacity)


    def __str__(self):
        return "Airplane type {:>2} \nMake: {:>2} \nModel: {:>2} \nCapacity: {:>2}".format(self.__plane_type_id, self.__make, self.__model, self.__capacity)


    def get_plane_type_id(self):
        return self.__plane_type_id


    def set_plane_type_id(self, plane_type_id):
        if self.__model_validation.validate_airplane_typeid(plane_type_id):
            self.__plane_type_id = plane_type_id
            return True
        else:
            return False


    def get_make(self):
        return self.__make


    def set_make(self, make):
        if self.__model_validation.validate_airplane_make(make):
            self.__make = make
            return True
        else:
            return False


    def get_model(self):
        return self.__model


    def set_model(self, model):
        if self.__model_validation.validate_airplane_model(model):
            self.__model = model
            return True
        else:
            return False


    def get_capacity(self):
        return self.__capacity


    def set_capacity(self, capacity):
        if self.__model_validation.validate_airplane_capacity(str(capacity)):
            self.__capacity = capacity
            return True
        else:
            return False
