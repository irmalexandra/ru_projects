import os
from Models import Player
from Models.high_score import HighScore


class Writer:
    """
    Handles all write related actions on DB
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

    def push_word_bank(self, new_word: str, word_list: list) -> bool:
        """
        Adds the new_word to the DB
        :param new_word: str
        :param word_list: list
        :return: bool
        """
        write_stream = open(self.file_path_word_bank_temp, "a")
        if word_list:
            for word in word_list:
                write_stream.write(word + "\n")
        write_stream.write(new_word + "\n")
        write_stream.close()
        os.remove(self.file_path_word_bank)
        os.rename(self.file_path_word_bank_temp, self.file_path_word_bank)
        return True

    def push_players(self, new_player: Player, player_dict: dict) -> bool:
        """
        Adds the new_player to the DB
        :param new_player: Player
        :param player_dict: dict
        :return: bool
        """
        write_stream = open(self.file_path_players_temp, "a")
        if player_dict:
            for key, player in player_dict.items():
                write_stream.write(player.raw_data() + "\n")
        write_stream.write(new_player.raw_data() + "\n")
        write_stream.close()
        os.remove(self.file_path_players)
        os.rename(self.file_path_players_temp, self.file_path_players)
        return True

    def push_high_scores(self, new_high_score: HighScore, high_scores: list) -> bool:
        """
        Adds the new_high_score to the DB
        :param new_high_score: HighScore
        :param high_scores: list
        :return: bool
        """
        write_stream = open(self.file_path_high_scores_temp, "a")
        if high_scores:
            for high_score in high_scores:
                write_stream.write(high_score.raw_data() + "\n")
        write_stream.write(new_high_score.raw_data() + "\n")
        write_stream.close()
        os.remove(self.file_path_high_scores)
        os.rename(self.file_path_high_scores_temp, self.file_path_high_scores)
        return True

    def push_saves(self, new_save_game, save_games_list):
        """
        Adds the new_high_score to the DB
        :param new_save_game: HighScore
        :param save_games_list: list
        :return: bool
        """
        write_stream = open(self.file_path_save_games_temp, "a")
        if save_games_list:
            for save_game in save_games_list:
                write_stream.write(save_game.raw_data() + "\n")
        write_stream.write(new_save_game.raw_data() + "\n")
        write_stream.close()
        os.remove(self.file_path_save_games)
        os.rename(self.file_path_save_games_temp, self.file_path_save_games)
        return True
