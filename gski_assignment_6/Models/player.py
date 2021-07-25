class Player:
    """
    Stores all relevant information about the players
    """
    def __init__(self, name):
        self.name = name
        self.save_list = []

    def raw_data(self):
        """
        Converts the player to a CSV string format
        :return: str
        """
        return self.name

    def __eq__(self, name):
        if self.name == name:
            return True
        return False


