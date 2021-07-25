import os
from os import path


class DLDestinations():
    COUNTRY = 0
    AIRPORT = 1
    FLIGHT_TIME = 2
    DISTANCE = 3
    CONTACT_NAME = 4
    CONTACT_NUMBER = 5
    ID = 6
    CSV_ROWS = 7


    def __init__(self, modelAPI):
        self.__modelAPI = modelAPI


    def pull_all_destinations(self):
        '''Opens csv files and returns a list of all destinations (country, airport, flight time, distance, contact name, contact number)'''
        if path.exists('./repo/destination.csv') and path.exists('./repo/destinations_temp.csv'):
            filestream = open("./repo/destination.csv", "r") # Makes sure that if something happens in the overwriting process
            os.remove("./repo/destinations_temp.csv")        # it will be covered
        elif path.exists('./repo/destination.csv') and path.exists('./repo/destinations_temp.csv') == False:
            filestream = open("./repo/destination.csv", "r")
        elif path.exists('./repo/destination.csv') == False and path.exists('./repo/destinations_temp.csv'):
            filestream = open("./repo/destinations_temp.csv", "r")
        else:
            print("destination data files not found")
            return

        all_destinations_list = [] # The list that will be returned once it has been filled with instances
        for line in filestream:
            line_list = line.strip().split(",") # Get the columns into a list to work with
            if len(line_list) == self.CSV_ROWS:
                check_list = [] # This list holds the output from the validator
                new_destination = self.__modelAPI.get_model('Destination')
                
                check_list.append(new_destination.set_country(line_list[DLDestinations.COUNTRY]))
                check_list.append(new_destination.set_airport(line_list[DLDestinations.AIRPORT]))
                check_list.append(new_destination.set_flight_time(
                    line_list[DLDestinations.FLIGHT_TIME])) # Setting all the nessecariy info into the model instance
                check_list.append(new_destination.set_distance(line_list[DLDestinations.DISTANCE]))
                check_list.append(new_destination.set_contact_name(
                    line_list[DLDestinations.CONTACT_NAME]))
                check_list.append(new_destination.set_contact_number(
                    line_list[DLDestinations.CONTACT_NUMBER]))
                check_list.append(new_destination.set_destination_id(line_list[DLDestinations.ID]))
                if False not in check_list: # If the validator returned a false bool anywhere, the instance is not appended and thus not 
                    all_destinations_list.append(new_destination) # Sent down to the other layers, this excludes the header and "corrupt" lines
        filestream.closed
        return all_destinations_list


    def append_destination(self, new_destination):
        '''Opens a csv file and adds a new destination to the destination string'''
        destination_stream = open('./repo/Destination.csv', 'a')
        destination_str = new_destination.raw_info() # Gets the "raw info" from the instance, which is a csv friendly string
        destination_stream.write(destination_str)
        destination_stream.close()
        return True


    def overwrite_all_destinations(self, destination_list):
        # employee_file.write(new_emp_str)
        HEADER = "country,airport,flight time,distance,contact name,contact number,id\n"
        filestream = open("./repo/destinations_temp.csv", "a")
        filestream.write(HEADER) # Writes the first line of the temp as the header
        for destination_info in destination_list:
            filestream.write(destination_info.raw_info()) # "Appends" the raw info lines into the temp, raw info being csv friendly strings
        filestream.close()
        os.remove("./repo/destination.csv")
        os.rename("./repo/destinations_temp.csv", "./repo/destination.csv")
        return True
