from logic_layer.LLValidator import Validator
class Voyage():
    def __init__(self , departing_flight_num = "", return_flight_num = "", departing_flight_departing_from = "Reykjavik", 
                departing_flight_departure_date = "", departing_flight_arrival_date = "", return_flight_departing_from = "", 
                return_flight_departure_date = "", return_flight_arrival_date = "", airplane_insignia = "", 
                captain_ssn = "", copilot_ssn = "", fsm_ssn = "", fa_ssns = [], status = "", staffed = ""):
        
        self.__models_validation = Validator()
        self.__departing_flight_num = departing_flight_num
        self.__return_flight_num = return_flight_num


        self.__departing_flight_departing_from = departing_flight_departing_from
        self.__departing_flight_departure_date = departing_flight_departure_date
        self.__departing_flight_arrival_date = departing_flight_arrival_date

        self.__return_flight_departing_from = return_flight_departing_from
        self.__return_flight_departure_date = return_flight_departure_date
        self.__return_flight_arrival_date = return_flight_arrival_date

        self.__airplane_insignia = airplane_insignia
        self.__captain_ssn = captain_ssn
        self.__copilot_ssn = copilot_ssn
        self.__fsm_ssn = fsm_ssn
        self.__fa_ssns = fa_ssns
        self.__status = status
        self.__staffed = staffed


        self.__header_format_dict = {"default": self.get_model_header_default_format}

        self.__list_info_dict = {"default": self.get_model_list_default_info}

        self.__validation_dict = {self.get_destination: self.set_return_flight_departing_from, 
                                         self.get_departing_flight_departure_date: self.set_departing_flight_departure_date}

        self.__voyage_employee_ssn_dict = {"Captain":self.get_captain_ssn, 
                                           "Copilot":self.get_copilot_ssn, 
                                           "Flight Service Manager":self.get_fsm_ssn, 
                                           "Flight Attendant":self.get_fa_ssns}


    def __str__(self):
        return "Departing flight no: {}\nReturning flight no: {}\nDeparting from: {}\nDeparting date/time: {}\nReturning date/time: {}\nDestination: {}\nDeparting date/time: {}\nReturning date/time: {}\nAirplane insignia: {}\nCaptain SSN: {}\nCopilot SSN: {}\nFlight service manager SSN: {}\nCabin crew SSN: {}".format(self.__departing_flight_num,
        self.__return_flight_num,
        self.__departing_flight_departing_from, 
        self.change_date_time_format(self.__departing_flight_departure_date), 
        self.change_date_time_format(self.__departing_flight_arrival_date), 
        self.__return_flight_departing_from, 
        self.change_date_time_format(self.__return_flight_departure_date), 
        self.change_date_time_format(self.__return_flight_arrival_date), 
        self.__airplane_insignia, 
        self.__captain_ssn, 
        self.__copilot_ssn, 
        self.__fsm_ssn, 
        ", ".join(self.__fa_ssns))


    def raw_info(self):
        true_employees = ":".join(self.__fa_ssns)
        return self.__departing_flight_num + "," + self.__return_flight_num + "," + self.__departing_flight_departing_from + "," + self.__departing_flight_departure_date + "," + self.__departing_flight_arrival_date + "," + self.__return_flight_departing_from + "," + self.__return_flight_departure_date + "," + self.__return_flight_arrival_date + "," + self.__airplane_insignia + "," + self.__captain_ssn + "," + self.__copilot_ssn + "," + self.__fsm_ssn + "," + true_employees + "\n"


    def get_validation_dict(self):
        return self.__validation_dict


    def get_departing_flight_num(self):
        return self.__departing_flight_num


    def set_departing_flight_num(self, flight_number):
        if self.__models_validation.validate_flight_number(flight_number):
            self.__departing_flight_num = flight_number
            return True
        return False


    def get_return_flight_num(self):
        return self.__return_flight_num


    def set_return_flight_num(self, flight_number):
        if self.__models_validation.validate_flight_number(flight_number):
            self.__return_flight_num = flight_number
            return True    
        return False


    def set_flight_numbers(self, departing_flight_num, return_flight_num):
        dep_success = self.set_departing_flight_num(departing_flight_num)
        ret_success = self.set_return_flight_num(return_flight_num)
        if dep_success and ret_success:
            return True
        return False


    def get_departing_flight_departing_from(self):
        return self.__departing_flight_departing_from


    def set_departing_flight_departing_from(self, airport):
        if self.__models_validation.validate_airport(airport):
            self.__departing_flight_departing_from = airport
            return True
        return False


    def get_return_flight_departing_from(self):
        return self.__return_flight_departing_from


    def set_return_flight_departing_from(self, airport):
        if self.__models_validation.validate_airport(airport):
            self.__return_flight_departing_from = airport
            return True

        return False


    def get_destination(self):
        return self.__return_flight_departing_from


    def get_departing_flight_departure_date(self):
        return self.__departing_flight_departure_date


    def set_departing_flight_departure_date(self, new_departure_date):
        if self.__models_validation.validate_date_time(new_departure_date):
            self.__departing_flight_departure_date = new_departure_date
            return True
        return False


    def get_departing_flight_arrival_date(self):
        return self.__departing_flight_arrival_date


    def set_departing_flight_arrival_date(self, new_arrival_date):
        if self.__models_validation.validate_date_time(new_arrival_date):
            self.__departing_flight_arrival_date = new_arrival_date
            return True
        return False        


    def get_return_flight_departure_date(self):
        return self.__return_flight_departure_date


    def set_return_flight_departure_date(self, new_departure_date):
        if self.__models_validation.validate_date_time(new_departure_date):
            self.__return_flight_departure_date = new_departure_date
            return True
        return False 


    def get_return_flight_arrival_date(self):
        return self.__return_flight_arrival_date


    def set_return_flight_arrival_date(self, new_arrival_date):
        if self.__models_validation.validate_date_time(new_arrival_date):
            self.__return_flight_arrival_date = new_arrival_date
            return True
        return False 


    def get_status(self):
        return self.__status


    def set_status(self, status):
        self.__status = status


    def get_staffed(self):
        return self.__staffed


    def set_staffed(self, staffed):
        self.__staffed = staffed


    def handle_key_value(self, key, value):
        return value(key())


    def set_flight_times(self, departing_flight_arrival_date, return_flight_departure_date, return_flight_arrival_date):
        self.set_departing_flight_arrival_date(departing_flight_arrival_date)
        self.set_return_flight_departure_date(return_flight_departure_date)
        self.set_return_flight_arrival_date(return_flight_arrival_date)
        pass


    def get_airplane_insignia(self):
        return self.__airplane_insignia


    def set_airplane_insignia(self, new_insignia):
        if new_insignia != ".":
            if self.__models_validation.validate_airplane_insignia(new_insignia):
                self.__airplane_insignia = new_insignia
                return True
            return False   
        else:
            self.__airplane_insignia = new_insignia
            return True


    def get_captain_ssn(self):
        return self.__captain_ssn


    def set_captain_ssn(self, new_ssn):
        if new_ssn != ".":
            if self.__models_validation.validate_employee_ssn(new_ssn):
                self.__captain_ssn = new_ssn
                return True
            return False
        else:
            self.__captain_ssn = new_ssn
            return True


    def get_copilot_ssn(self):
        return self.__copilot_ssn


    def set_copilot_ssn(self, new_ssn):
        if new_ssn != ".":
            if self.__models_validation.validate_employee_ssn(new_ssn):
                self.__copilot_ssn = new_ssn
                return True
            return False
        else:
            self.__copilot_ssn = new_ssn
            return True


    def get_fsm_ssn(self):
        return self.__fsm_ssn


    def set_fsm_ssn(self, new_ssn):
        if new_ssn != ".":
            if self.__models_validation.validate_employee_ssn(new_ssn):
                self.__fsm_ssn = new_ssn
                return True
            return False
        else:
            self.__fsm_ssn = new_ssn
            return True


    def get_fa_ssns(self):
        return self.__fa_ssns


    def set_fa_ssns(self, new_ssn):
        if type(new_ssn).__name__ == "list":
            for ssn in new_ssn:
                if ssn != ".":
                    if not self.__models_validation.validate_employee_ssn(ssn):
                        return False
            self.__fa_ssns = new_ssn
            return True
        
        if not self.__models_validation.validate_employee_ssn(new_ssn):
            return False

        if self.__fa_ssns[0] == '.':
            self.__fa_ssns[0] = new_ssn
        elif self.__fa_ssns[1] == '.':
            self.__fa_ssns[1] = new_ssn
        else:
            self.__fa_ssns.append(new_ssn)
            
        return True
        

    def get_all_required_crew_ssn(self):
        return [self.get_captain_ssn(), self.get_copilot_ssn(), self.get_fsm_ssn()]
        

    def get_voyage_employee_ssn(self, rank):
        return self.__voyage_employee_ssn_dict[rank]()


    def get_model_header_format(self, header_flag):
        return self.__header_format_dict[header_flag]()


    def get_model_list_info(self, header_flag):
        return self.__list_info_dict[header_flag]()


    def get_model_header_default_format(self):
        return "{:7}{:14}{:10}{:22}{:16}{:22}{:16}{:14}{:17}".format("Index:",
                                                            "Destination:",
                                                            "Airplane:",
                                                            "Departing date/time:",
                                                            "Flight number: ",
                                                            "Returning date/time:",
                                                            "Flight number:",
                                                            "Staffed:", 
                                                            "Status:")

 
    def get_model_list_default_info(self):
        return "  {:14}{:10}{:22}{:16}{:22}{:16}{:14}{:17}|\n".format(self.get_return_flight_departing_from(),
                                                                   self.get_airplane_insignia(),  # we should change this to airplane type
                                                                   self.change_date_time_format(self.get_departing_flight_departure_date()),
                                                                   self.get_departing_flight_num(),
                                                                   self.change_date_time_format(self.get_return_flight_arrival_date()),
                                                                   self.get_return_flight_num(),
                                                                   self.get_staffed(),
                                                                   self.get_status())

 
    def change_date_time_format(self, date_string):
        date_string = date_string[:-3].replace("T", " ")
        return date_string