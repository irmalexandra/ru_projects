class LLDestinations:
    MAX_DESTINATIONS = 99
    def __init__(self, DLAPI, modelAPI):
        self.__dl_api = DLAPI
        self.__modelAPI = modelAPI
        self.__all_destination_list = []

    # All list functions

    def get_all_destination_list(self, changed = True):
        ''' Gets a list of destination instances and returns it '''
        if changed:
            self.__all_destination_list = self.__dl_api.pull_all_destinations()
        if not self.__all_destination_list:
            self.__all_destination_list = self.__dl_api.pull_all_destinations()

        return sorted(self.__all_destination_list, key=lambda destination: destination.get_country())


    def get_destination_list_by_country(self, country):
        '''Gets input from UI layer and returns a list of instances'''

        self.get_all_destination_list()
        found_destination_list = []

        for destination in self.__all_destination_list:
            if destination.get_country() == country:
                found_destination_list.append(destination)
        
        return sorted(found_destination_list, key=lambda airplane: airplane.get_airport())

    # All change functions

    def create_destination(self, destination):
        '''Gets instance and sends it down to the data layer, returns a boolean'''
        destination_id = self.generate_destination_id()
        if destination_id:
            destination.set_destination_id(destination_id)
        else:
            return False

        if self.validate_destination(destination):
            if self.__dl_api.append_destination(destination):
                self.get_all_destination_list(True)
                return True
            
        return False


    def overwrite_all_destinations(self):
        '''Takes a list of destination instances and sends it to the DL''' 
        if self.__dl_api.overwrite_all_destinations(self.__all_destination_list):
            self.get_all_destination_list()
            return True

    # All special functions

    def validate_destination(self, destination):
        '''Gets destination instance and sends it to get validated, returns a boolean'''
        return self.__modelAPI.validate_model(destination)


    def generate_destination_id(self):
        '''Gets a list of instances and returns a generated number based on the newest instance''' 
        self.get_all_destination_list()
        id_list = []
        for destination in self.__all_destination_list:
            id_list.append(destination.get_destination_id())
        
        id_list.sort(reverse=True)

        next_id_int = int(id_list[0]) + 1 # Creates a variable that is highest destination id + 1
        if next_id_int <= 9:
            next_id_str = '0'+ str(next_id_int)
        else:
            next_id_str = str(next_id_int)

        if next_id_int <= self.MAX_DESTINATIONS:
            return next_id_str
        return False
        

