from Models import Player


class SaveGame:
    def __init__(self, name: str,
                 current_word: str,
                 current_word_length: int,
                 progress_word_list: list,
                 current_word_list: list,
                 current_rem_letters: int,
                 total_tries: int,
                 current_tries: int):
        self.player_name = name
        self.current_word = current_word
        self.current_word_length = str(current_word_length)
        self.progress_word_list = progress_word_list
        self.current_word_list = current_word_list
        self.current_rem_letters = str(current_rem_letters)
        self.total_tries = str(total_tries)
        self.current_tries = str(current_tries)

    def raw_data(self) -> str:
        """
        Returns a string for saving a game state in DB
        :return:
        """
        return self.player_name + "," + self.current_word + "," + self.current_word_length + "," + ".".join(
            self.progress_word_list) + "," + ".".join(
            self.current_word_list) + "," + self.current_rem_letters + "," + self.total_tries + "," + self.current_tries

    def __str__(self):
        return "{}   {}   {}".format(".".join(self.progress_word_list), self.current_tries, self.total_tries)
