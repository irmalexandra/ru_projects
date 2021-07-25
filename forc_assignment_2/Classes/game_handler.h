#ifndef FORC_PA_2_GAME_HANDLER_H
#define FORC_PA_2_GAME_HANDLER_H

#include "word.h"



class GameHandler {
public:
    GameHandler(Word *word_list, int *word_count);
//    void set_initials(char *initials);
    bool compare_guess(char guess_arr[]);
    void set_current_word();
    Word *get_current_word();
    int get_words_left();
    int get_accumulated_score();
    int get_streak();
    void reset_streak();
    void add_streak();
    void add_score(int score);
    int get_longest_streak();
    void reset(int word_count);

private:
    Word *current_word;
    Word *word_list;
//    Player *player;
    int word_count;
    int words_left;
    int accumulated_score;
    int streak;
    int longest_streak;
};

#endif //FORC_PA_2_GAME_HANDLER_H
