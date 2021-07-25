from .uitraversal import UITraversal
class UIAPI():
    def __init__(self):
        self.ui_traversal = UITraversal()
    
    def start_game(self):
        """
        Starts a game of Hangman
        """
        self.ui_traversal.starting_point()