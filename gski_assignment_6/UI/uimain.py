class UIMain:
    """
    Prints the menus and relevant information for the hangman program
    """
    UI_DIVIDER_INT = 100
    CREATORS = ("r1klus", "lokialex")
    NAME = 0
    WORD = 1
    TRIES = 2

    def __init__(self):
        pass

    def print_title_screen(self) -> None:
        """
        Prints the title of the program
        """
        print("-" * self.UI_DIVIDER_INT)
        print("   _    _          _   _  _____ __  __          _   _\n" +
              "| |  | |   /\   | \ | |/ ____|  \/  |   /\   | \ | |\n" +
              "| |__| |  /  \  |  \| | |  __| \  / |  /  \  |  \| |\n" +
              "|  __  | / /\ \ | . ` | | |_ | |\/| | / /\ \ | . ` |\n" +
              "| |  | |/ ____ \| |\  | |__| | |  | |/ ____ \| |\  |\n" +
              "|_|  |_/_/    \_\_| \_|\_____|_|  |_/_/    \_\_| \_|\n")
        print("-" * self.UI_DIVIDER_INT)
        print("Created by {} and {}.".format(self.CREATORS[0], self.CREATORS[1]))
        print("-" * self.UI_DIVIDER_INT)

    def print_player_selection(self) -> None:
        """
        Prints the title of the program
        """
        print("Please enter your name.")

    def print_main_menu(self) -> None:
        """
        Prints the main menu of the program
        """
        print("-" * self.UI_DIVIDER_INT)
        print("1. Start a new game.")
        print("2. Load game")
        print("3. Add words to database.")
        print("4. See high scores.")
        print("6. Quit.")
        print("-" * self.UI_DIVIDER_INT)

    def print_stats_this_session(self, stats_tpl: tuple) -> None:
        """
        Prints the users stats for the current instance of the program
        :param stats_tpl: tuple
        """
        print("You have won {} games, lost {} games. Your Win/Lose ration is {}.".format(stats_tpl[0], stats_tpl[1],
                                                                                         stats_tpl[2]))
        print("-" * self.UI_DIVIDER_INT)

    def print_new_game_menu(self) -> None:
        """
        Prints the new game menu
        """
        print("-" * self.UI_DIVIDER_INT)
        print("Select your preferred difficulty")
        print(" - Easy: 20 tries - " + 5 * " " + " - Medium: 10 tries - " + 5 * " " + " - Hard: 5 tries - ")
        print("P.S. You can choose any amount of tries")
        print("-" * self.UI_DIVIDER_INT)

    def print_word_progress(self, word_str: str, rem_letters: int, rem_tries: int) -> None:
        """
        Prints the progress of the word, remaining letters to guess, and remaining tries
        :param word_str: str
        :param rem_letters: int
        :param rem_tries: int
        """
        print("-" * self.UI_DIVIDER_INT)
        print(word_str)
        print("{} letters remaining and you have {} tries left!".format(rem_letters, rem_tries))
        print("To save and quit, press enter instead of entering a letter.")
        print("-" * self.UI_DIVIDER_INT)

    def print_add_to_db_menu(self) -> None:
        """
        Prints message about addding a word to the database
        """
        print("-" * self.UI_DIVIDER_INT)
        print("Type in a word following UTF8 conventions.")
        print("The word is not case sensitive.")

    def print_high_scores_menu(self):
        """
        Prints the high scores menu
        """
        print("Press '1' for your scores, or '2' for everyone.")

    def print_high_scores(self, scores: list = [], profile: str = "everyone") -> None:
        """
        Iterates through list of objects printing out the information in them
        :param profile: str
        :param scores: list
        """
        print("-" * self.UI_DIVIDER_INT)
        print("Here are the scores for {}:".format(profile))
        for score_info in scores:
            print("\t", score_info)
        print('\nPress enter to return to the main menu')

    def print_victory_message(self, word_str: str, rem_tries: int) -> None:
        """
        Prints victory message if user won the round
        :param word_str: str
        :param rem_tries: int
        """
        print("\nCongratulations! You've won!")
        print("You completed the word '{}' with {} remaining tries.".format(word_str, rem_tries))

    def print_loss_message(self, word_str: str, rem_letters: int) -> None:
        """
        Prints loss message if user lost the round
        :param word_str: str
        :param rem_letters: int
        """
        print("\nSorry but you have lost the game.")
        print("The word was '{}' and you had {} letters left to find.".format(word_str, rem_letters))

    def print_save_games(self, save_games: list) -> None:
        """
        Iterates through list of objects printing out the information in them
        :param save_games: list
        """
        for index, save_game in enumerate(save_games):
            print("{}\t{}".format(index, save_game))

    def print_save_game_menu(self):
        """
        Prints the save game menu
        """
        print("Select which save game you want to load")
        print("{}\t{}".format("Index", "Save File"))

