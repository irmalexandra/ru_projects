from Models.save_game import SaveGame
from .hangman import Hangman


class LLAPI:
    """
    API to handle all main actions on the Hangman class
    """

    def __init__(self):
        self.hangman = Hangman()

    def handle_word_progress(self, letter: str = '') -> tuple:
        """
        Chain function to Hangman
        Functions handles main logic of hangman, uses helper function to check if word contains letter,
        and checks for win and loss states
        win and loss states
        :param letter:
        :return: tuple
        """
        return self.hangman.handle_word_progress(letter)

    def start_new_game(self, total_tries: int) -> tuple:
        """
        Chain function to Hangman
        Sets up the initial state of the game
        Returns a tuple with (progress_word_str, remaining letters)
        :param total_tries: int
        :return: tuple
        """
        return self.hangman.start_game(total_tries)

    def save_game(self) -> bool:
        """
        Chain function to Hangman
        Saves the current game state to DB
        :return: bool
        """
        return self.hangman.save_game()

    def win_status(self) -> bool:
        """
        Chain function to Hangman
        Returns a win check bool
        :return: bool
        """
        return self.hangman.win_status()

    def loss_status(self) -> bool:
        """
        Chain function to Hangman
        Returns a loss check bool
        :return: bool
        """
        return self.hangman.loss_status()

    def write_to_database(self, new_word: str) -> bool:
        """
        Chain function to Hangman
        Adds the new_word to the DB
        :param new_word:
        :return: bool
        """
        return self.hangman.add_word(new_word)

    def player_select(self, name) -> bool:
        """
        Chain function to Hangman
        If the player already exists, the player is selected, else a new player is created
        :param name: str
        :return: bool
        :param name:
        :return:
        """
        return self.hangman.select_player(name)

    def win_ratio(self) -> tuple:
        """
        Chain function to Hangman
        Returns a tuple containing the (win_count, loss_count, win_ratio)
        :return :tuple
        """
        return self.hangman.win_ratio()

    def get_high_scores(self, selection: int) -> list:
        """
        Chain function to Hangman
        If selection is 1 it returns the selected players high scores \n
        If selection is 2 it returns all high scores \n
        Else returns None \n
        :param selection:int
        :return: list
        """
        return self.hangman.get_high_scores(selection)

    def get_saves(self) -> list:
        """
        returns a list of the players save games
        :return: list
        """
        return self.hangman.get_save_games()

    def load_game(self, save_game) -> None:
        """
        Loads the game state from save_game
        :param save_game:
        :return:
        """
        return self.hangman.load_game(save_game)


