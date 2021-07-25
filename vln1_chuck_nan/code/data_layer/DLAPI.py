from data_layer.DLAirplanes import DLAirplanes
from data_layer.DLDestinations import DLDestinations
from data_layer.DlEmployees import DLEmployees
from data_layer.DLVoyages import DLVoyages
from models.ModelAPI import ModelAPI

class DLAPI():
    def __init__(self):
        self.__modelAPI = ModelAPI()
        self.__dl_employees = DLEmployees(self.__modelAPI)
        self.__dl_voyages = DLVoyages(self.__modelAPI)
        self.__dl_destinations = DLDestinations(self.__modelAPI)
        self.__dl_airplanes = DLAirplanes(self.__modelAPI)


    def pull_all_employees(self):
        '''Gets a list of all employees from data layer and returns it'''
        return self.__dl_employees.pull_all_employees()


    def overwrite_all_employees(self, employee_list):
        '''Gets a list of employee instances from the Logic layer and sends it to the Data layer
            to overwrite the employee csv file with the given list '''
        return self.__dl_employees.overwrite_all_employees(employee_list)


    def append_employee(self, employee):
        '''Gets an employee instance, sends it to the data layer to append it into the employee csv file'''
        return self.__dl_employees.append_employee(employee)


    def pull_all_voyages(self):
        '''Gets a list of all voyages from the data layer and returns it'''
        return self.__dl_voyages.pull_all_voyages()


    def overwrite_all_voyages(self, voyage_list):
        '''Gets a list of voyage instances, sends it to the data layer to have the voyage csv file 
            overwritten by the given list   '''
        return self.__dl_voyages.overwrite_all_voyages(voyage_list)


    def append_voyage(self, voyage):
        '''Gets a voyage instance, sends it to the data layer to append it to the voyage csv file  '''
        return self.__dl_voyages.append_voyage(voyage)


    def pull_all_destinations(self):
        '''Gets a list of all destinations from the data layer and returns it'''
        return self.__dl_destinations.pull_all_destinations()


    def overwrite_all_destinations(self, all_destination_list):
        '''Gets a list of destination instances, sends it to the data layer to overwrite 
            the destinations csv file with the given list   '''
        return self.__dl_destinations.overwrite_all_destinations(all_destination_list)
    

    def append_destination(self, destination):
        '''Adds a new destination to the list of destinations'''
        return self.__dl_destinations.append_destination(destination)


    def pull_all_airplanes(self):
        '''Gets a list of all airplanes from the data layer and returns it'''
        return self.__dl_airplanes.pull_all_airplanes()


    def pull_all_airplane_types(self):
        '''Gets a list of all airplane types from the data layer and returns it'''
        return self.__dl_airplanes.pull_airplane_types_info()


    def append_airplane(self, airplane):
        '''Gets an instance of the airplane model, sends it to the data layer to 
            append it to the airplanes csv file  '''
        return self.__dl_airplanes.append_airplane(airplane)
