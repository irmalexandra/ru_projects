from datetime import datetime
class Validator():
    TITLE_LIST = ["Pilot", "Cabincrew"]
    PILOT_RANK_LIST = ["Captain", "Copilot"]
    CABINCREW_RANK_LIST = ["Flight Service Manager", "Flight Attendant"]
    DOMAIN = "nanair.is"
    PHONE_NUMBER = 7
    SSN = 10


    def __validate_string(self, attribute):
        '''Takes a variable, checks if it is in the ascii alphabet, returns a boolean'''
        attribute = str(attribute)
        return attribute.isalpha()


    def __validate_int(self, attribute):
        '''Takes a variable, checks if it can be converted to an integer, retuns a boolean'''
        try:
            int(attribute)
            return True
        except ValueError:
            return False


    def validate_name(self, name):
        '''Takes a variable, tries to split it on space and sends it to string checker, returns boolean'''
        try:
            first, last = name.split(" ")
        except ValueError:
            return False

        name = name.replace(" ", "")           
        if self.__validate_string(name):
            return True
        return False


    def validate_employee_name(self, name):
        '''Takes a variable, returns a boolean'''
        return self.validate_name(name)


    def validate_employee_ssn(self, ssn):
        '''Takes a variable, checks the length and returns a boolean'''
        try:
            if ssn[6] == '-':
                ssn = ssn.replace("-", "")
            if (self.__validate_int(ssn)) and (len(ssn) == self.SSN):
                return True
        except IndexError:
            return False

        return False


    def validate_employee_address(self, address):
        '''Takes a variable, tries to split into string and integer, returns a boolean'''
        try:
            name, number = address.split()
            if self.__validate_string(name) and self.__validate_int(number):
                return True
        except ValueError:
            return False


    def validate_phone_number(self, number):
        '''Takes a variable, checks length and returns a boolean'''
        try:
            if number[3] == '-':
                number = number.replace("-", "")
            if (self.__validate_int(number)) and (len(number) == self.PHONE_NUMBER):
                return True
        except IndexError:
            return False

        return False


    def validate_mobile_number(self, number):
        '''Takes a variable, returns a boolean'''
        return self.validate_phone_number(number)


    def validate_home_number(self, number):
        '''Takes a variable, returns a boolean'''
        return self.validate_phone_number(number)


    def validate_email(self, email):
        '''Takes a variable, compares to a class constand and returns a boolean'''
        try:
            name, domain = email.split("@")
            if (domain == self.DOMAIN) and ("." in name):
                return True
        except ValueError:
            return False


    def validate_title(self, title):
        '''Takes a variable, returns a boolean'''
        if title in self.TITLE_LIST:
            return True

        return False
        

    def validate_pilot_rank(self, rank):
        '''Takes a variable, returns a boolean'''
        if rank in self.PILOT_RANK_LIST:
            return True

        return False


    def validate_cabincrew_rank(self, rank):
        '''Takes a variable, returns a boolean'''
        if rank in self.CABINCREW_RANK_LIST:
            return True

        return False


    def validate_date_time(self, date):
        '''Takes a variable, tries to convert variable to datetime instance, returns a boolean'''
        if type(date).__name__ != datetime:
            try:
                if date.find("T") == -1:
                    datetime.strptime(date,'%d-%m-%Y')
                else:
                    datetime.strptime(date,'%Y-%m-%dT%H:%M:%S')
                return True
            except ValueError:
                return False
        return True


    def validate_airplane_typeid(self, typeid):
        '''Takes a variable, returns a boolean'''
        return typeid[:2] == "NA"


    def validate_airplane_insignia(self, insignia):
        '''Takes a variable, returns a boolean'''
        if insignia[2] == "-":
            if len(insignia) == 6:
                return self.__validate_string(insignia.replace("-", ""))

        return False


    def validate_airplane_make(self, make):
        '''Takes a variable, returns a boolean'''
        return self.__validate_string(make)


    def validate_airplane_model(self, model):
        '''Takes a variable, returns a boolean'''
        if model.strip() == model:
            return True
        return False


    def validate_airplane_capacity(self, capacity):
        '''Takes a variable, returns a boolean'''
        return self.__validate_int(capacity)


    def validate_flight_number(self, flight_num):
        '''Takes a variable, returns a boolean'''
        if (flight_num[:2] == "NA") and (self.__validate_int(flight_num[2:])):
            return True

        return False


    def validate_country(self, country):
        '''Takes a variable, returns a boolean'''
        name = country.replace(" ", "") 
        return self.__validate_string(name)


    def validate_airport(self, airport):
        '''Takes a variable, returns a boolean'''
        name = airport.replace(" ", "") 
        return self.__validate_string(name)


    def validate_id(self, id_num):
        '''Takes a variable, returns a boolean'''
        return self.__validate_int(id_num)


    def validate_flight_time(self, time):
        '''Takes a variable, returns a boolean'''
        return self.__validate_int(time)


    def validate_distance(self, distance):
        '''Takes a variable, returns a boolean'''
        return self.__validate_int(distance)


    def validate_contact_name(self, name):
        '''Takes a variable, returns a boolean'''
        return self.validate_name(name)


    def validate_contact_number(self, number):
        '''Takes a variable, returns a boolean'''
        return self.validate_phone_number(number)
