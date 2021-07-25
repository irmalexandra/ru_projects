from Models import Player
from Models.high_score import HighScore
from Models.save_game import SaveGame
from .reader import Reader
from .writer import Writer


class DLAPI:
    """
    API to handle all DB actions and file paths for CSV documents
    """
    FILE_PATH_WORD_BANK = "./DB/WORD_BANK.txt"
    FILE_PATH_PLAYERS = "./DB/PLAYERS.txt"
    FILE_PATH_HIGH_SCORES = "./DB/HIGH_SCORES.txt"
    FILE_PATH_SAVE_GAME = "./DB/SAVE_GAMES.txt"

    FILE_PATH_WORD_BANK_TEMP = "./DB/WORD_BANK_temp.txt"
    FILE_PATH_PLAYERS_TEMP = "./DB/PLAYERS_temp.txt"
    FILE_PATH_HIGH_SCORES_TEMP = "./DB/HIGH_SCORES_temp.txt"
    FILE_PATH_SAVE_GAME_TEMP = "./DB/SAVE_GAMES_temp.txt"

    def __init__(self):
        self.__reader = Reader(DLAPI.FILE_PATH_WORD_BANK,
                               DLAPI.FILE_PATH_WORD_BANK_TEMP,
                               DLAPI.FILE_PATH_PLAYERS,
                               DLAPI.FILE_PATH_PLAYERS_TEMP,
                               DLAPI.FILE_PATH_HIGH_SCORES,
                               DLAPI.FILE_PATH_HIGH_SCORES_TEMP,
                               DLAPI.FILE_PATH_SAVE_GAME,
                               DLAPI.FILE_PATH_SAVE_GAME_TEMP)
        self.__writer = Writer(DLAPI.FILE_PATH_WORD_BANK,
                               DLAPI.FILE_PATH_WORD_BANK_TEMP,
                               DLAPI.FILE_PATH_PLAYERS,
                               DLAPI.FILE_PATH_PLAYERS_TEMP,
                               DLAPI.FILE_PATH_HIGH_SCORES,
                               DLAPI.FILE_PATH_HIGH_SCORES_TEMP,
                               DLAPI.FILE_PATH_SAVE_GAME,
                               DLAPI.FILE_PATH_SAVE_GAME_TEMP)
        self.word_list = self.pull_word_bank()
        self.players = self.pull_players()
        self.high_scores = self.pull_high_scores()
        self.save_games = self.pull_all_save_games()

    def push_word_bank(self, new_word: str) -> bool:
        """
        Chain function to connect to Writer
        Stores the new_word in DB
        :param new_word: str
        :return: bool
        """
        if new_word not in self.word_list:
            self.__writer.push_word_bank(new_word, self.word_list)
            self.word_list = self.pull_word_bank()
            return True
        else:
            return False

    def pull_word_bank(self) -> list:
        """
        Chain function to connect to Reader
        Returns a list of all words from DB
        :return: list
        """
        return self.__reader.pull_word_bank()

    def push_players(self, new_player: Player) -> bool:
        """
        Chain function to connect to Writer
        Stores new_player in the DB
        :param new_player: Player
        :return: bool
        """
        return self.__writer.push_players(new_player, self.players)

    def pull_players(self) -> list:
        """
        Chain function to connect to Reader
        Returns a list of all players in the DB
        :return: list
        """
        return self.__reader.pull_players()

    def push_high_score(self, high_score: HighScore) -> bool:
        """
        Chain function to connect to Writer
        Stores high_score in the DB
        :param high_score: HighScore
        :return: bool
        """
        return self.__writer.push_high_scores(high_score, self.high_scores)

    def pull_high_scores(self) -> list:
        """
        Chain function to connect to Reader
        Returns a list of all high_scores in the DB
        :return: list
        """
        return self.__reader.pull_high_scores()

    def push_all_save_games(self, save_game: SaveGame):
        """
        Chain function to connect to Writer
        Stores save_game in the DB
        :param save_game: HighScore
        :return: bool
        """
        return self.__writer.push_saves(save_game, self.save_games)

    def pull_all_save_games(self) -> list:
        """
        Chain function to connect to Reader
        Returns a list of all save_games in the DB
        :return: list
        """
        return self.__reader.pull_save_games()

