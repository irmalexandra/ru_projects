from .uimain import UIMain
from LL import LLAPI


class UITraversal:
    """
    Handles the users navigation of the hangman program
    """

    def __init__(self):
        self.__ll_api = LLAPI()
        self.__ui_main = UIMain()
        self.current_menu = -1
        self.main_menu_dict = {-1: self.__ui_main.print_player_selection,
                               0: self.__ui_main.print_main_menu,
                               1: self.__ui_main.print_new_game_menu,
                               2: self.__ui_main.print_save_game_menu,
                               3: self.__ui_main.print_add_to_db_menu,
                               4: self.__ui_main.print_high_scores_menu,
                               5: self.__ui_main.print_word_progress,
                               6: self.exit_program}
        self.current_profile = None

    def starting_point(self):
        """
        Creates a new player or selects a returning player and calls the necessary functions to get the user to the main menu
        """
        self.__ui_main.print_title_screen()
        self.print_current_menu()
        name_str = self.get_user_str_input('name')
        if self.__ll_api.player_select(name_str):
            print("\nWelcome back {}".format(name_str))
        else:
            print("\nNew player successfully created!")
        self.current_profile = name_str
        self.current_menu = 0
        self.handle_selection()

    def get_user_menu_selection(self, key_word: str = "selection") -> int:
        """
        Prompts user for input, returns int input if possible else asks user for input again
        :param key_word: str
        :return: int
        """
        while True:
            try:
                return_value = int(input("Enter {}: ".format(key_word)))
                if return_value in self.main_menu_dict.keys() and return_value != 5:
                    return return_value
                else:
                    print("Invalid {}".format(key_word))
            except ValueError:
                print("Invalid value for {}".format(key_word))

    def get_user_int_input(self, key_word: str) -> int:
        """
        Prompts user for input, returns int input if possible else asks user for input again
        :param key_word: str
        :return: int
        """
        while True:
            try:
                return_value = int(input("Enter {}: ".format(key_word)))
                return return_value
            except ValueError:
                print("Invalid value for {}".format(key_word))

    def get_user_str_input(self, key_word: str, current_word_length: int = 0):
        """
        Prompts user for input, returns str input if possible else asks user for input again
        :param key_word: str
        :param current_word_length: int
        :return: str
        """
        while True:

            return_value = input("Enter {}: ".format(key_word))
            input_str_length = len(return_value)
            if key_word == 'name':
                return return_value
            elif return_value == "":
                return ":quit"
            elif return_value.isalpha():
                if key_word == 'letter':
                    if input_str_length == 1 or input_str_length == current_word_length:
                        return return_value
                    else:
                        print("Please input only one character or a word of length {}.".format(current_word_length))

                elif key_word == 'word':
                    return return_value
            else:
                print("Invalid value for {}".format(key_word))

    def handle_selection(self):
        """
        Loops until player has selected the option to quith the program
        """
        while True:
            self.print_current_menu()
            if self.current_menu == 0:
                stats_tpl = self.__ll_api.win_ratio()
                self.__ui_main.print_stats_this_session(stats_tpl)
                selection = self.get_user_menu_selection()
                self.current_menu = selection

            elif self.current_menu == 1:
                selection = self.get_user_int_input("number of tries")
                self.current_menu = 5
                self.initialize_game(selection)

            elif self.current_menu == 2:
                self.current_menu = 5
                save_games = self.__ll_api.get_saves()
                if save_games:
                    self.__ui_main.print_save_games(save_games)
                    selection = self.get_user_int_input("Index")
                    save_game = save_games[selection]
                    self.__ll_api.load_game(save_game)
                    self.__ui_main.print_word_progress(" ".join(letter for letter in save_game.progress_word_list),
                                                       save_game.current_rem_letters,
                                                       int(save_game.total_tries) - int(save_game.current_tries))
                    self.play_game(save_game.current_word)
                else:
                    print("No save games found for this user!")
                    self.current_menu = 0

            elif self.current_menu == 3:
                new_word = self.get_user_str_input('word')
                if self.__ll_api.write_to_database(new_word):
                    print("The word '{}' successfully added to the database.".format(new_word))
                else:
                    print("The word '{}' already exists in the database.".format(new_word))
                self.current_menu = 0

            elif self.current_menu == 4:
                selection = self.get_user_int_input("selection")
                score_list = self.__ll_api.get_high_scores(selection)
                if selection == 1:
                    self.__ui_main.print_high_scores(score_list, self.current_profile)
                elif selection == 2:
                    self.__ui_main.print_high_scores(score_list)
                input()
                self.current_menu = 0

            elif self.current_menu == 6:
                self.exit_program()

    def print_current_menu(self):
        """
        Calls the function for the current index number of the main menu dictionary
        """
        self.main_menu_dict[self.current_menu]()

    def initialize_game(self, initial_tries: int) -> None:
        """
        Initializes the game by sending information to the LL
        :param initial_tries: int
        """
        game_info_tpl = self.__ll_api.start_new_game(initial_tries)
        current_word = game_info_tpl[0]
        progress_word_str = game_info_tpl[1]
        rem_letters = game_info_tpl[2]
        self.main_menu_dict[self.current_menu](progress_word_str, rem_letters, initial_tries)
        self.play_game(current_word)

    def play_game(self, current_word: str) -> None:
        """
        Loops until either a win or loss condition is met
        :param current_word: str
        """
        cont = True
        while cont:
            letter = self.get_user_str_input("letter", len(current_word))
            if letter == ":quit":
                self.__ll_api.save_game()
                self.current_menu = 0
                cont = False
            else:
                game_info_tpl = self.__ll_api.handle_word_progress(letter)
                progress_word_str = game_info_tpl[0]
                rem_letters = game_info_tpl[1]
                rem_tries = game_info_tpl[2]

                if self.__ll_api.win_status():
                    self.__ui_main.print_victory_message(current_word, rem_tries)
                    self.current_menu = 0
                    cont = False
                elif self.__ll_api.loss_status():
                    self.__ui_main.print_loss_message(current_word, rem_letters)
                    self.current_menu = 0
                    cont = False
                else:
                    self.main_menu_dict[self.current_menu](progress_word_str, rem_letters, rem_tries)

    def exit_program(self):
        """
        Exits the program
        """
        print("Thank you for playing!")
        exit()
