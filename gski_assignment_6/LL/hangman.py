from Models.player import Player
from Models.high_score import HighScore
from DL import DLAPI
from random import choice
import operator

from Models.save_game import SaveGame


def sort_high_scores_list(high_scores: list) -> list:
    """
    Sorts the list by Current_tries
    :param high_scores:list
    :return: list
     """
    return sorted(high_scores, key=operator.attrgetter("current_tries"))


class Hangman:
    """
    Class handles all the main logic functionality of Hangman, and stores relevant data in class variables
    """

    def __init__(self):
        self.player = None
        self.__dlapi = DLAPI()
        self.players = []
        self.words = []
        self.current_word = ""
        self.current_word_length = 0
        self.progress_word_list = []
        self.current_word_list = []
        self.current_rem_letters = 0
        self.total_tries = 0
        self.current_tries = 0
        self.win = False
        self.win_count = 0
        self.loss = False
        self.loss_count = 0
        self.current_save = None

    def get_high_scores(self, selection: int) -> list:
        """
        If selection is 1 it returns the selected players high scores \n
        If selection is 2 it returns all high scores \n
        Else returns None \n
        :param selection:int
        :return: list
        """
        high_scores = self.__dlapi.pull_high_scores()
        sorted_high_scores = sort_high_scores_list(high_scores)
        if selection == 1:
            player_scores = []
            for high_score in sorted_high_scores:
                if high_score.player.name == self.player.name:
                    player_scores.append(high_score)
            return player_scores
        elif selection == 2:
            return sorted_high_scores
        else:
            return []

    def win_ratio(self) -> tuple:
        """
        Returns a tuple containing the (win_count, loss_count, win_ratio)
        :return :tuple
        """
        if self.win_count > 0 or self.loss_count > 0:
            return self.win_count, self.loss_count, self.win_count / (self.win_count + self.loss_count)
        return 0, 0, 0

    def win_status(self) -> bool:
        """
        Returns a win check bool
        :return: bool
        """
        return self.win

    def loss_status(self) -> bool:
        """
        Returns a loss check bool
        :return: bool
        """
        return self.loss

    def get_all_players(self) -> list:
        """
        Returns a list of all players
        :return: list
        """
        if len(self.players) == 0:
            self.players = self.__dlapi.pull_players()
        return self.players

    def get_all_words(self) -> list:
        """
        Returns a list of all words
        :return: list
        """
        if len(self.words) == 0:
            self.words = self.__dlapi.pull_word_bank()
        return self.words

    def create_player(self, name: str) -> bool:
        """
        Creates a new player and stores it in DB, self.player to new player, returns true on success
        :param name: str
        :return: bool
        """
        new_player = Player(name)
        self.player = new_player
        return self.__dlapi.push_players(new_player)

    def select_player(self, name: str) -> bool:
        """
        If the player already exists, the player is selected, else a new player is created
        :param name: str
        :return: bool
        """
        if len(self.players) == 0:
            self.get_all_players()
        if name in self.players:
            self.player = self.players[name]
            all_save_games = self.__dlapi.pull_all_save_games()
            current_save_games = []
            for save_game in all_save_games:
                if save_game.player_name == self.player.name:
                    current_save_games.append(save_game)
            self.player.save_list = current_save_games
            return True
        self.create_player(name)
        return False

    def select_word(self) -> None:
        """
        Selects a word from randomly from  all words available
        """
        self.get_all_words()
        self.current_word = choice(self.words)
        self.current_word_length = len(self.current_word)

    def progress_word_list_to_str(self) -> str:
        """
        Converts the progress word list to a str for display purposes
        :return: str
        """
        return " ".join(letter for letter in self.progress_word_list)

    def handle_word_progress(self, letter: str) -> tuple:
        """
        Functions handles main logic of hangman, uses helper function to check if word contains letter,
        and checks for win and loss states
        win and loss states
        :param letter: str
        :return: tuple
        """
        self.current_tries += 1
        if len(letter) > 1:
            self.handle_full_word_guess(letter)
        else:
            was_update = self.update_word_progress(letter)
            if was_update:  # Check if a letter was updated
                self.calc_remaining_letters()

        if self.current_tries == self.total_tries:  # Loss  Check
            self.loss = True
            self.loss_count += 1
        elif self.progress_word_list == self.current_word_list:  # Win Check
            self.win = True
            self.post_high_score()
            self.win_count += 1

        progress_word_str = self.progress_word_list_to_str()
        return progress_word_str, self.current_rem_letters, self.total_tries - self.current_tries

    def handle_full_word_guess(self, word_str: str):
        """
        Checks on a exact match for the current word
        :param word_str: str
        """
        if word_str == self.current_word:
            self.progress_word_list = self.current_word_list

    def update_word_progress(self, letter) -> bool:
        """
        Function to check if the word contains the guess letter
        :param letter: str
        :return: bool
        """
        was_update = False
        if letter not in self.progress_word_list:
            for i in range(self.current_word_length):
                if self.current_word_list[i] == letter:
                    self.progress_word_list[i] = letter
                    was_update = True
        return was_update

    def initialize_words(self) -> None:
        """
        Initializes class variables used in the main logic of the game
        """
        self.progress_word_list = ["_" for _ in range(self.current_word_length)]
        self.current_word_list = [letter for letter in self.current_word]

    def calc_remaining_letters(self) -> None:
        """
        Calculates the remaining letters of the word. Stores in class variable
        """
        rem_letters = 0
        for letter in self.progress_word_list:
            if letter == "_":
                rem_letters += 1
        self.current_rem_letters = rem_letters

    def start_game(self, number_of_tries: int = 10) -> str and int:
        """
        Sets up the initial state of the game
        Returns a tuple with (progress_word_str, remaining letters)
        :param number_of_tries: int
        :return: tuple
        """
        self.reset_game()
        self.select_word()
        self.initialize_words()
        self.calc_remaining_letters()
        self.total_tries = number_of_tries
        return self.current_word, self.progress_word_list_to_str(), self.current_rem_letters

    def add_word(self, new_word: str) -> bool:
        """
        Adds the new_word to the DB
        :param new_word:
        :return: bool
        """
        return self.__dlapi.push_word_bank(new_word.lower())

    def post_high_score(self) -> bool:
        """
        Adds new high_score to the DB
        :return:
        """
        new_high_score = HighScore(self.player, self.current_word, str(self.current_tries), str(self.total_tries))
        return self.__dlapi.push_high_score(new_high_score)

    def reset_game(self):
        """
        Resets all relevant class variables to their initial state
        """
        self.current_word = ""
        self.current_word_length = 0
        self.progress_word_list = []
        self.current_word_list = []
        self.current_rem_letters = 0
        self.total_tries = 0
        self.current_tries = 0
        self.win = False
        self.loss = False
        self.current_save = None

    def save_game(self):
        """
        Saves the current game state to DB
        """
        new_save_game = SaveGame(
            self.player.name,
            self.current_word,
            self.current_word_length,
            self.progress_word_list,
            self.current_word_list,
            self.current_rem_letters,
            self.total_tries,
            self.current_tries,
        )
        self.player.save_list.append(new_save_game)
        return self.__dlapi.push_all_save_games(new_save_game)

    def get_save_games(self) -> list:
        """
        returns a list of all save games for player
        :return: list
        """
        return self.player.save_list

    def load_game(self, save_game: SaveGame):
        """
        Loads the selected game state
        :param save_game:
        """
        self.reset_game()
        self.current_save = save_game
        self.current_word = save_game.current_word
        self.current_word_length = int(save_game.current_word_length)
        self.progress_word_list = save_game.progress_word_list
        self.current_word_list = save_game.current_word_list
        self.current_rem_letters = int(save_game.current_rem_letters)
        self.total_tries = int(save_game.total_tries)
        self.current_tries = int(save_game.current_tries)

