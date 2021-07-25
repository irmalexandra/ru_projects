from datetime import datetime
from datetime import timedelta
class LLVoyages:
    def __init__(self, DLAPI, modelAPI):
        self.__dl_api = DLAPI
        self.__modelAPI = modelAPI
        self.__all_voyage_list = []

        self.__ll_employees = None
        self.__ll_destinations = None
        self.__ll_airplanes = None


    def set_ll_employees(self, ll_employee):
        self.__ll_employees = ll_employee


    def set_ll_airplanes(self, ll_airplanes):
        self.__ll_airplanes = ll_airplanes


    def set_ll_destinations(self, ll_destinations):
        self.__ll_destinations = ll_destinations

    # All list functions

    def get_all_voyage_list(self, changed = False):
        '''Gets and returns a list of voyage instances'''
        if changed:
            self.__all_voyage_list = self.__dl_api.pull_all_voyages()
        elif not self.__all_voyage_list:
            self.__all_voyage_list = self.__dl_api.pull_all_voyages()

        self.check_status()
        self.check_staffed()
        return sorted(self.__all_voyage_list, key=lambda voyage: voyage.get_departing_flight_departure_date())


    def filter_all_empty_voyages(self):
        '''Takes a list of all voyage instances and returns a list of filtered voyages by staffed status'''
        self.get_all_voyage_list() 
        empty_voyage_list = []

        for voyage in self.__all_voyage_list:
            if voyage.get_staffed() == "Not staffed":
                empty_voyage_list.append(voyage)

        return sorted(empty_voyage_list, key=lambda voyage: voyage.get_departing_flight_departure_date())


    def filter_all_voyages_by_period(self, start_date, end_date):
        '''Takes a list of all voyage instances and returns a list of voyages filtered by period'''
        self.get_all_voyage_list()
        
        start = self.get_iso_format_date_time(start_date).replace(hour=0, minute= 0, second = 0)
        end = self.get_iso_format_date_time(end_date).replace(hour = 23, minute = 59, second = 59)
        # Time is replaced to ensure both the start day and end day are completely covered
        period_voyage_list = []

        for voyage in self.__all_voyage_list:
            
            departure_date = self.get_iso_format_date_time(voyage.get_departing_flight_departure_date())
            return_date = self.get_iso_format_date_time(voyage.get_return_flight_arrival_date())
            
            if start <= return_date and departure_date <= end:
                period_voyage_list.append(voyage)

        return period_voyage_list

        
    def filter_all_voyages_by_airport(self, airport):
        '''Takes a string input and uses it to filter all voyages, returns a list of voyages'''
        self.get_all_voyage_list()
        airport_voyage_list = []

        for voyage in self.__all_voyage_list:
            if voyage.get_return_flight_departing_from() == airport:
                airport_voyage_list.append(voyage)

        return airport_voyage_list


    def filter_available_employees(self, rank, voyage):
        '''Takes a rank string and voyage instance, returns a list of employee instances'''
        start_date = voyage.get_departing_flight_departure_date()
        end_date = voyage.get_return_flight_arrival_date()
        voyages_in_date_range_list = self.filter_all_voyages_by_period(start_date, end_date)

        all_employee_list = self.__ll_employees.get_all_employee_list() 

        filter_rank_list = [(employee) for employee in all_employee_list if employee.get_rank() == rank]
        # Creates a list of all employees that with the requested rank
        available_employee_list = []

        for other_voyage in voyages_in_date_range_list:   
            voyage_ssn = other_voyage.get_voyage_employee_ssn(rank)
            for employee in filter_rank_list:
                if employee not in available_employee_list:
                    employee_ssn = employee.get_ssn()
                    if type(voyage_ssn).__name__ == "list":
                        if employee_ssn not in voyage_ssn:
                            available_employee_list.append(employee)
                            
                    else:
                        if employee_ssn != voyage_ssn:
                            available_employee_list.append(employee)
        # Creates a list of employees that aren't in other voyages around the same time as the voyage in question

        final_employee_list = available_employee_list    
        
        if rank == "Captain" or rank == "Copilot":
            final_employee_list = []
            for airplane in self.__ll_airplanes.get_all_airplane_list():
                if airplane.get_insignia() == voyage.get_airplane_insignia():
                    selected_airplane = airplane
                    break # Gets the instance of the airplane assigned to the voyage

            airplane_type = "NA" + selected_airplane.get_make() + selected_airplane.get_model()
            # Creates the airplane type that will match with employee licence
            for employee in available_employee_list: 
                if employee.get_licence() == airplane_type:
                    final_employee_list.append(employee)
            # Final filter
        return sorted(final_employee_list, key=lambda employee: employee.get_name())

    # All change functions

    def create_voyage(self, destination, start_date = "00-00-0000", start_time = "00:00:00"):
        '''Takes a destination instance, date and time strings and returns a boolean'''
        current_date = datetime.today()

        self.get_all_voyage_list()

        fixed_date_time = self.get_iso_format_date_time(start_date, start_time)

        if current_date > fixed_date_time: 
            return False # Ensures that no new voyages can be created back in time

        new_voyage = self.__modelAPI.get_model("Voyage")

        new_voyage.set_return_flight_departing_from(destination.get_airport())
        new_voyage.set_departing_flight_departure_date(fixed_date_time.isoformat())
        new_voyage.set_airplane_insignia(".")
        new_voyage.set_captain_ssn(".")
        new_voyage.set_copilot_ssn(".")
        new_voyage.set_fsm_ssn(".")
        new_voyage.set_fa_ssns([".", "."])

        dep_flight_num_str, ret_flight_num_str = self.generate_flight_numbers(start_date, destination) 
        new_voyage.set_flight_numbers(dep_flight_num_str, ret_flight_num_str)

        departing_flight_arrival_date_str, return_flight_departure_date_str, return_flight_arrival_date_str \
            = self.calculate_flight_times(fixed_date_time, destination.get_airport())
        
        new_voyage.set_flight_times(departing_flight_arrival_date_str, \
            return_flight_departure_date_str, return_flight_arrival_date_str)

        start_date = fixed_date_time.isoformat()
        end_date = new_voyage.get_return_flight_arrival_date()

        for voyage in self.__all_voyage_list:
            other_start_date = voyage.get_departing_flight_departure_date()
            other_end_date = voyage.get_return_flight_arrival_date()
            
            if other_start_date == start_date or other_start_date == end_date or\
                other_end_date == start_date or other_end_date == end_date:
                return False # Ensures that no flight can depart or arrive at reykjavik airport at the same time

        if self.__modelAPI.validate_model(new_voyage):
            if self.__dl_api.append_voyage(new_voyage):
                self.get_all_voyage_list(True)
                return True

        return False


    def overwrite_all_voyages(self):
        '''Sends a overwrite request to the data layer'''
        if self.__dl_api.overwrite_all_voyages(self.__all_voyage_list):
            self.get_all_voyage_list(True)
            return True


    def duplicate_voyage(self, voyage, start_date = "00-00-0000", start_time = "00:00:00"):
        '''Takes a voyage instance, a date and time, returns a boolean sent from create voyage function'''
        all_destination_list = self.__ll_destinations.get_all_destination_list()

        for destination in all_destination_list:
            if destination.get_airport() == voyage.get_destination():
                selected_destination = destination

        return self.create_voyage(selected_destination, start_date, start_time)


    def repeat_voyage(self, voyage, repeat_interval, end_date = "00-00-0000"):
        '''Takes a voyage instance, an integer and a date string, returns a boolean after running through duplicate'''
        success = False
        try:
            date = self.get_iso_format_date_time(voyage.get_departing_flight_departure_date())
            end_date = self.get_iso_format_date_time(end_date)
            repeat_interval = int(repeat_interval)
        except ValueError:
            return False

        while date < end_date:
            try:
                date += timedelta(days=repeat_interval)
                # Try except in case SOMEONE decides that the repeat interval should be a very high number
            except OverflowError:
                return False
            success = self.duplicate_voyage(voyage, date)
            date += timedelta(days=repeat_interval)
            # This loop runs until the end date has been met, calling the duplicate function and incrementing the date each time
        return success
            

    def add_employee_to_voyage(self, voyage, employee):
        '''Takes voyage and employee instances and adds employee ssn to voyage, then if successful sends a overwrite request to data layer'''
        rank_dict = {'Captain':voyage.set_captain_ssn, 
                     'Copilot':voyage.set_copilot_ssn, 
                     'Flight Service Manager':voyage.set_fsm_ssn, 
                     'Flight Attendant':voyage.set_fa_ssns}
        # This dictionary matches the return from get_rank() with a function call in the voyage model
        employee_ssn_str = employee.get_ssn()
        employee_rank_str = employee.get_rank()
        check = rank_dict[employee_rank_str](employee_ssn_str)
        # Tries to send the employee ssn to the appropriate rank of the voyage, returns a boolean
        if check:
            return self.overwrite_all_voyages()
        return check
    

    def add_airplane_to_voyage(self, voyage, airplane):
        '''Takes voyage and airplane instances and adds the airplane name to the voyage,\
            sends a overwrite request to data layer'''
        if voyage.set_airplane_insignia(airplane.get_insignia()):
            return self.overwrite_all_voyages()
              
              
    def check_status(self):
        '''Gets the current date and a list of voyage instances, updates the status variable of each instance'''
        current_date = datetime.today()

        for voyage in self.__all_voyage_list:
            departing_flight_departure_date = self.get_iso_format_date_time(voyage.get_departing_flight_departure_date())
            departing_flight_arrival_date = self.get_iso_format_date_time(voyage.get_departing_flight_arrival_date())
            return_flight_departure_date = self.get_iso_format_date_time(voyage.get_return_flight_departure_date())
            return_flight_arrival_date = self.get_iso_format_date_time(voyage.get_return_flight_arrival_date())

            if current_date >= departing_flight_departure_date and \
                ('.' in voyage.get_all_required_crew_ssn() or voyage.get_airplane_insignia() == '.'):
                voyage.set_status("Cancelled")
            elif current_date <= departing_flight_departure_date:
                voyage.set_status("Not started")
            elif departing_flight_departure_date <= current_date <= departing_flight_arrival_date:
                voyage.set_status("Flying to {}".format(voyage.get_return_flight_departing_from()))
            elif departing_flight_arrival_date <= current_date <= return_flight_departure_date:
                voyage.set_status("Currently in {}".format(voyage.get_return_flight_departing_from()))
            elif return_flight_departure_date <= current_date <= return_flight_arrival_date:
                voyage.set_status("Flying to {}".format(voyage.get_departing_flight_departing_from()))
            else:
                voyage.set_status("Voyage completed")
    

    def check_staffed(self):
        '''Gets a list of all voyage instances and checks if voyage is fully staffed, sets voyage status'''

        for voyage in self.__all_voyage_list:
            all_crew_ssn = voyage.get_all_required_crew_ssn()
            for ssn in voyage.get_fa_ssns():
                all_crew_ssn.append(ssn)

            if voyage.get_airplane_insignia() != "." and '.' not in all_crew_ssn:
                voyage.set_staffed("Staffed")
            else:
                voyage.set_staffed("Not staffed")

    # All special functions

    def calculate_flight_times(self, date, airport):
        '''takes a date and an airport string, then alculates the date of the arrival at the airport, the date of which
            they'll depart from the airport and the date of which they'll land back home. Returns all three in iso format'''
        self.__all_voyage_list = self.get_all_voyage_list()
        destinations_list = self.__ll_destinations.get_all_destination_list()
        destinations_dict = dict()
        
        for destination in destinations_list:
            destinations_dict[destination.get_airport()] = int(destination.get_flight_time())
            # Creates a dictionary of airport:flight time

        flight_time = destinations_dict[airport]
        departing_flight_arrival_date = date + timedelta(hours=flight_time)
        return_flight_departure_date = departing_flight_arrival_date + timedelta(hours = 1)
        return_flight_arrival_date = return_flight_departure_date + timedelta(hours = flight_time)
        return departing_flight_arrival_date.isoformat(), return_flight_departure_date.isoformat(), return_flight_arrival_date.isoformat()


    def generate_flight_numbers(self, date, destination):
        '''Takes date and destination and returns two flight numbers, one for departing flight the other 
            returning flight'''
        NEW_FLIGHT_NUM_LEN = 7
        LAST_POSSIBLE_FLIGHT = 999 # The last possible flight number in a three number format
        start_date = self.get_iso_format_date_time(date)
        end_date = start_date + timedelta(hours=23, minutes=59,seconds=59)

        destination_id = destination.get_destination_id()

        self.__all_voyage_list = self.get_all_voyage_list()
        existing_numbers = [] # A list to hold all the flight numbers that have already been made

        for voyage in self.__all_voyage_list:
            voyage_airport = voyage.get_return_flight_departing_from() # Gets the voyage airport
            departing_flight_departure_date = self.get_iso_format_date_time(voyage.get_return_flight_departure_date())
            # Gets the departing flight departure date in a date instance
            if start_date <= departing_flight_departure_date <= end_date and voyage_airport == destination.get_airport():
                # If the departing flight departure date is in the days range and the airport fits the destinations airport
                flight_number = voyage.get_return_flight_num()
                if len(flight_number) == NEW_FLIGHT_NUM_LEN: # Filters out the current flight format from older ones
                    existing_numbers.append(int(flight_number.replace("NA" + destination_id,""))) # Gets an integer format of the number
                                                                                                                                                #section of the flight number
        if LAST_POSSIBLE_FLIGHT in existing_numbers:
            return False # If the maximum amounts of flight numbers reached, returns a false
        elif not existing_numbers: # Makes the first of its kind 
            departing_flight_num = "NA" + destination_id + "000"
            return_flight_num = "NA" + destination_id + "001"
            return departing_flight_num, return_flight_num

        else:
            last_number = max(existing_numbers) # Gets the last voyages number 
            next_departing_number_str  = str(last_number + 1) # +1 to last voyage number, throws into string format
            next_return_number_str = str(last_number + 2)
            while len(next_departing_number_str) < 3: # Fills empty space with 0 to ensure 3 seat numbers format
                next_departing_number_str = "0" + next_departing_number_str
            while len(next_return_number_str) < 3:
                next_return_number_str = "0" + next_return_number_str
            
            departing_flight_num = "NA" + destination_id + str(next_departing_number_str)
            return_flight_num = "NA" + destination_id + str(next_return_number_str)
            return departing_flight_num, return_flight_num


    def get_iso_format_date_time(self, date = "00-00-0000", time = "00:00:00"):
        '''Takes two variables in various date/time formats and returns a datetime instance'''

        if type(date).__name__ != 'datetime':

            if date.find("T") == -1:
                new_date = datetime.strptime(date,'%d-%m-%Y')
                new_time = datetime.strptime(time, '%H:%M:%S').time()
                new_date = datetime.combine(new_date, new_time)
            else:
                new_date = datetime.strptime(date,'%Y-%m-%dT%H:%M:%S')
            return new_date
        return date

    def update_voyage_pointer(self, voyage):
        for updated_voyage in self.__all_voyage_list:
            if voyage.get_departing_flight_departure_date() == updated_voyage.get_departing_flight_departure_date():
                return updated_voyage


    def filter_voyage_by_date(self, date):
        '''Takes a date and returns a list of voyage instances within that date'''
        returned_list = []
        start_range= self.get_iso_format_date_time(date).replace(hour=0, minute=0,second=0,microsecond=0)
        end_range = start_range + timedelta(hours = 23, minutes=59, seconds=59) # To make sure all 24 hours of the day are included
        for voyage in self.get_all_voyage_list():

            departure_date = self.get_iso_format_date_time(voyage.get_departing_flight_departure_date())
            return_date = self.get_iso_format_date_time(voyage.get_return_flight_arrival_date())

            if start_range <= departure_date <= end_range or start_range <= return_date <= end_range:
                returned_list.append(voyage)

        return returned_list