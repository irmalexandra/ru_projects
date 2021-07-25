import os
from Models import Player
from Models.high_score import HighScore
from Models.save_game import SaveGame


def check_file_path(path: str, path_temp: str) -> str or bool:
    """
    Checks both file paths and checks if they exist. \n
    If both paths exist, it deletes the temp path and returns the path \n
    If Only path exists it returns path
    If Only temp path exists it returns temp path
    :param path: str
    :param path_temp: str
    :return: bool
    """
    if os.path.exists(path) and os.path.exists(path_temp):
        os.remove(path_temp)
        return path
    elif os.path.exists(path) and os.path.exists(path_temp) is False:
        return path
    elif os.path.exists(path) is False and os.path.exists(path_temp):
        return path_temp
    else:
        return False


class Reader:
    """
    Handles all read related actions on the DB
    """

    def __init__(self,
                 word_bank,
                 word_bank_temp,
                 players,
                 players_temp,
                 high_scores,
                 high_scores_temp,
                 save_games,
                 save_games_temp):
        self.file_path_word_bank = word_bank
        self.file_path_word_bank_temp = word_bank_temp

        self.file_path_players = players
        self.file_path_players_temp = players_temp

        self.file_path_high_scores = high_scores
        self.file_path_high_scores_temp = high_scores_temp

        self.file_path_save_games = save_games
        self.file_path_save_games_temp = save_games_temp

    def pull_word_bank(self) -> list or bool:
        """
        Returns a list of all words from DB
        :return: list or false
        """
        file_path = check_file_path(self.file_path_word_bank, self.file_path_word_bank_temp)
        if file_path:
            file_stream = open(file_path)
            word_list = []
            for word in file_stream:
                word_list.append(word.strip())

            return word_list
        return file_path

    def pull_players(self) -> list or bool:
        """
        Returns a list of all players from DB
        :return: list or false
        """
        file_path = check_file_path(self.file_path_players, self.file_path_players_temp)
        if file_path:
            file_stream = open(file_path)
            player_dict = {}
            for player in file_stream:

                player_dict[player.strip()] = Player(player.strip())

            return player_dict
        return file_path

    def pull_high_scores(self) -> list or bool:
        """
        Returns a list of all high scores from DB
        :return: list or false
        """
        file_path = check_file_path(self.file_path_high_scores, self.file_path_high_scores_temp)
        if file_path:
            file_stream = open(file_path)
            high_scores = []
            for high_score in file_stream:
                high_score = high_score.split(',')
                new_high_score = HighScore(Player(high_score[0]), high_score[1], high_score[2], high_score[3].strip())
                high_scores.append(new_high_score)

            return high_scores
        return file_path

    def pull_save_games(self) -> list or bool:
        """
        Returns a list of all save games from DB
        :return: list or false
        """
        file_path = check_file_path(self.file_path_save_games, self.file_path_save_games_temp)
        if file_path:
            file_stream = open(file_path)
            save_games = []
            for save_game in file_stream:
                save_game = save_game.split(',')
                new_save_game = SaveGame(save_game[0],
                                         save_game[1],
                                         save_game[2],
                                         save_game[3].split("."),
                                         save_game[4].split("."),
                                         save_game[5],
                                         save_game[6],
                                         save_game[7].strip())
                save_games.append(new_save_game)

            return save_games
        return file_path
