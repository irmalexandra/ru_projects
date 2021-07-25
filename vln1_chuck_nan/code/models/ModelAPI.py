from models.Airplane import Airplane
from models.Employee import Employee
from models.Voyage import Voyage
from models.Destination import Destination
from models.AirplaneType import AirplaneType


class ModelAPI():
    def __init__(self):
        self.model_dict = {"Airplane": Airplane,\
                      "Employee": Employee,\
                      "Voyage": Voyage,\
                      "Destination": Destination,
                      "AirplaneType": AirplaneType}


    def get_model(self, model_name):
        model_obj = self.model_dict[model_name]
        return model_obj()


    def get_model_header_format(self, model, header_flag):
        return "|{}|".format(model.get_model_header_format(header_flag))


    def get_model_list_info(self, model_list, header_flag):
        returnObject = ""
        for index, model in enumerate(model_list):
            returnObject += "| {:>2d}. {}".format(index+1, model.get_model_list_info(header_flag))
        return returnObject


    def validate_model(self, model):
        '''Gets a object instance from the logic layer and returns a tuple'''
        validation_dict = model.get_validation_dict()

        for key, value in validation_dict.items():
            check = model.handle_key_value(key, value)
            if not check:
                return False    
        return True       

 
