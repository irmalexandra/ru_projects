class HighScore:
    """
    Class to store all relevant high score information
    """
    def __init__(self, player, word, current_tries, total_tries):
        self.player = player
        self.word = word
        self.current_tries = current_tries
        self.total_tries = total_tries

    def raw_data(self) -> str:
        """
        Converts the highscore to a CSV string format
        :return: str
        """
        return self.player.name + "," + self.word + "," + self.current_tries + "," + self.total_tries

    def __str__(self):
        return "Name: {} - Word: {} - Tries: {} out of {}".format(self.player.name, self.word, self.current_tries, self.total_tries)