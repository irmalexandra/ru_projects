from datetime import datetime

class LLAirplanes:
    def __init__(self, DLAPI, modelAPI):
        self.__dl_api = DLAPI
        self.__modelAPI = modelAPI
        self.__all_airplane_list = []
        self.__all_airplane_type_list = []

        self.__ll_voyages = None


    def set_ll_voyages(self, ll_voyage):
        self.__ll_voyages = ll_voyage

    # All list functions


    def get_all_airplane_list(self, changed = False):
        '''Initializes a list of instances after checking if it exists and returns it sorted'''
        if changed: # Various functions can send a flag to this function to force an update
            self.__all_airplane_list = self.__dl_api.pull_all_airplanes()
        
        if not self.__all_airplane_list:
            self.__all_airplane_list = self.__dl_api.pull_all_airplanes()

        self.get_airplane_status()
        
        return sorted(self.__all_airplane_list, key=lambda airplane: airplane.get_model())


    def get_all_airplane_list_by_period(self, date, time):
        '''Takes variables and returns a list'''
        self.get_all_airplane_list()

        date_time = self.get_iso_format_date_time(date, time)

        self.get_airplane_status(date_time)

        return sorted(self.__all_airplane_list, key=lambda airplane: airplane.get_status())


    def get_airplane_type_list(self):
        '''Gets a list of instances and returns it'''
        self.__all_airplane_type_list = self.__dl_api.pull_all_airplane_types()
        return self.__all_airplane_type_list
    

    def filter_available_airplanes(self, voyage):
        '''Takes a voyage instance and returns a list of airplane instances'''
        all_airplane_list = self.get_all_airplane_list()
        
        selected_voyage_start_date = self.get_iso_format_date_time(voyage.get_departing_flight_departure_date())
        selected_voyage_end_date = self.get_iso_format_date_time(voyage.get_return_flight_arrival_date())
        
        self.get_airplane_status(selected_voyage_start_date)

        available_airplane_list = []
        unavailable_voyages = []
        unavailable_airplane_insignias = []

        all_voyage_list = self.__ll_voyages.get_all_voyage_list()

        for voyage in all_voyage_list:        
            voyage_start_date = self.get_iso_format_date_time(voyage.get_departing_flight_departure_date())
            voyage_end_date = self.get_iso_format_date_time(voyage.get_return_flight_arrival_date())

            if selected_voyage_start_date <= voyage_start_date <= selected_voyage_end_date \
                or selected_voyage_start_date <= voyage_end_date <= selected_voyage_end_date:
                unavailable_voyages.append(voyage)

            elif voyage_start_date <= selected_voyage_start_date <= voyage_end_date \
                or voyage_start_date <= selected_voyage_end_date <= voyage_end_date:
                unavailable_voyages.append(voyage)
       
        # Creates a list of voyages that occurr in the same time frame as selected voyage

        for voyage in unavailable_voyages:
            unavailable_airplane_insignias.append(voyage.get_airplane_insignia())

        for airplane in all_airplane_list:
            if airplane.get_insignia() in unavailable_airplane_insignias:
                continue
            else:
                available_airplane_list.append(airplane)

        return sorted(available_airplane_list, key=lambda airplane: airplane.get_make())

    # All change functions

    def create_airplane(self, airplane, airplane_types, insignia):
        '''Gets a list of airplane instances, checks if user created instance exists in list, returns boolean and instance'''
        self.get_all_airplane_list()
        insignia = insignia.upper()
        existing_airplanes_list = [airplane.get_insignia() for airplane in self.__all_airplane_list]
        
        # List comprehension that returns a list of all taken airplane insignias

        if airplane.get_insignia() not in existing_airplanes_list:
            existing_airplane_types = airplane_types
            airplane_make = airplane.get_make()
            airplane_model = airplane.get_model()
            
            for info in existing_airplane_types:
                if info.get_make() == airplane_make and info.get_model() == airplane_model:
                    airplane.set_capacity(info.get_capacity())
                    if self.__modelAPI.validate_model(airplane):
                        if self.__dl_api.append_airplane(airplane):
                            self.get_all_airplane_list(True)
                            return True
        return False

    # All special functions

    def get_airplane_status(self, current_date = datetime.now().replace(microsecond=0)):
        '''Takes a date and gets a list of airplane instances, updates their status'''
        for airplane in self.__all_airplane_list:
            airplane.set_status("Not in use")
            airplane.set_current_destination("N/A")
            airplane.set_date_available("N/A")
            airplane.set_flight_number("N/A")

        all_voyage_list = self.__ll_voyages.get_all_voyage_list()
        
        current_voyages = []

        for voyage in all_voyage_list:
            dep_flight_start = self.get_iso_format_date_time(voyage.get_departing_flight_departure_date())
            ret_flight_end = self.get_iso_format_date_time(voyage.get_return_flight_arrival_date())

            if dep_flight_start <= current_date <= ret_flight_end:
                current_voyages.append(voyage)
                
        for airplane in self.__all_airplane_list:
            for voyage in current_voyages:
                dep_flight_start = self.get_iso_format_date_time(voyage.get_departing_flight_departure_date())
                dep_flight_end = self.get_iso_format_date_time(voyage.get_departing_flight_arrival_date())
                ret_flight_start = self.get_iso_format_date_time(voyage.get_return_flight_departure_date())
                ret_flight_end = self.get_iso_format_date_time(voyage.get_return_flight_arrival_date())

                if airplane.get_insignia() == voyage.get_airplane_insignia():
                    airplane.set_current_destination(voyage.get_return_flight_departing_from())
                    airplane.set_date_available(ret_flight_end.isoformat())

                    if dep_flight_start <= current_date <= dep_flight_end:
                        airplane.set_flight_number(voyage.get_departing_flight_num())
                        airplane.set_status("In air, departing")

                    elif dep_flight_end <= current_date <= ret_flight_start:
                        airplane.set_flight_number("N/A")
                        airplane.set_status("At destination")

                    elif ret_flight_start <= current_date <= ret_flight_end:
                        airplane.set_flight_number(voyage.get_return_flight_num())
                        airplane.set_status("In air, returning")


    def get_iso_format_date_time(self, date = "00-00-0000", time = "00:00:00"):
        '''Takes two variables in various date/time formats and returns a datetime instance'''
        if type(date).__name__ != 'datetime': # Skips everything if what is being sent in is already in datetime

            if date.find("T") == -1:
                new_date = datetime.strptime(date,'%d-%m-%Y')
                new_time = datetime.strptime(time, '%H:%M:%S').time()
                new_date = datetime.combine(new_date, new_time)
            else:
                new_date = datetime.strptime(date,'%Y-%m-%dT%H:%M:%S')
            return new_date
        return date