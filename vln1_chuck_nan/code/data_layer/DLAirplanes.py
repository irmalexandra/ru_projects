import os
from os import path

class DLAirplanes():
    PLANE_TYPE_ID = 0
    PLANE_INSIGNIA = 1
    AIRPLANE_DICT_PLANE_TYPE = 0
    AIRPLANE_DICT_MODEL = 1
    AIRPLANE_DICT_CAPACITY = 2
    PLANE_TYPE_ID = 0
    PLANE_TYPE_MAKE = 1
    PLANE_TYPE_MODEL = 2
    PLANE_TYPE_CAPACITY = 3
    CSV_ROWS = 2


    def __init__(self, modelAPI):
        self.__modelAPI = modelAPI


    def pull_all_airplanes(self):
        '''Opens csv files and returns a list of all the airplanes (type ID, insignia, type)'''
        airplane_stream = open("./repo/Airplane.csv", "r")
        type_stream = open("./repo/AirplaneType.csv", "r")
        all_airplanes_list = [] # The list that will be returned once it has been filled with instances
        type_stream_list = [line.strip().split(",") for line in type_stream] # Fills type_stream_list with each line as a value from airplane types
        type_dict = dict() # A dict that holds all the existing supported aircraft types as keys, and their info as values

        for airplane_info in type_stream_list:
            type_dict[airplane_info[DLAirplanes.PLANE_TYPE_ID]] = airplane_info[1:] # Key = plane type id, value is everything else

        for line in airplane_stream:
            line_list = line.strip().split(",") # Get the columns into a list to work with
            
            if len(line_list) == self.CSV_ROWS:
                check_list = [] # This list holds the output from the validator
                new_airplane = self.__modelAPI.get_model('Airplane')
                plane_type = line_list[DLAirplanes.PLANE_TYPE_ID]
                check_list.append(new_airplane.set_insignia(line_list[DLAirplanes.PLANE_INSIGNIA])) # Setting all the nessecariy info into the model instance
                airplane_info_list = type_dict[plane_type] # Gets which type we are dealing with
                check_list.append(new_airplane.set_make(airplane_info_list[self.AIRPLANE_DICT_PLANE_TYPE])) # PlaneType
                check_list.append(new_airplane.set_model(airplane_info_list[self.AIRPLANE_DICT_MODEL])) # Model
                check_list.append(new_airplane.set_capacity(airplane_info_list[self.AIRPLANE_DICT_CAPACITY])) # Capacity
                if False not in check_list: # If the validator returned a false bool anywhere, the instance is not appended and thus not 
                    all_airplanes_list.append(new_airplane) # sent down to the other layers, this excludes the header and "corrupt" lines
        airplane_stream.close()
        type_stream.close()
        return all_airplanes_list
        

    def append_airplane(self, airplane):
        '''Adds a new airplane to the airplane string'''
        airplane_stream = open('./repo/Airplane.csv', 'a')
        airplane_str = airplane.raw_info() # Gets the "raw info" from the instance, which is a csv friendly string
        airplane_stream.write(airplane_str)
        airplane_stream.close()
        return True


    def pull_airplane_types_info(self):
        '''Opens csv files and returns a list of all the airplane types (type ID, make, model, capacity)'''
        filestream = open("./repo/AirplaneType.csv", "r")
        new_airplane_type_list = [] # Holds all the airplane types, later to be returned
        
        for airplane in filestream:
            check_list = [] # This list holds the output from the validator
            new_airplane_type = self.__modelAPI.get_model("AirplaneType")
            airplanes_types_list = airplane.strip().split(",") # Get the columns into a list to work with
            check_list.append(new_airplane_type.set_plane_type_id(airplanes_types_list[self.PLANE_TYPE_ID]))
            check_list.append(new_airplane_type.set_make(airplanes_types_list[self.PLANE_TYPE_MAKE]))
            # Setting all the nessecariy info into the model instance
            check_list.append(new_airplane_type.set_model(airplanes_types_list[self.PLANE_TYPE_MODEL]))
            check_list.append(new_airplane_type.set_capacity(airplanes_types_list[self.PLANE_TYPE_CAPACITY]))      
            if False not in check_list: # If the validator returned a false bool anywhere, the instance is not appended and thus not 
                new_airplane_type_list.append(new_airplane_type) # Sent down to the other layers, this excludes the header and "corrupt" lines
        filestream.close()
        return new_airplane_type_list

