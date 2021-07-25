#ifndef FORC_PA_2_WORD_H
#define FORC_PA_2_WORD_H

class Word{
public:
    Word();
    Word(char *word, int word_length);

    void scramble_word(int number_of_runs);
    char* get_word();
    char* get_scrambled_word();
    bool check_same();
    int get_word_length();
    void set_guessed();
    bool get_guessed() const;
    void update_revealed_letters();
    void do_hint();
    char* get_revealed_word();
    int get_word_score();
    void reset();


private:
    char *word;
    char *scrambled_word;
    bool *revealed_letters;
    int word_length;
    bool guessed;
    int hints_given;
    char *revealed_word;
};


#endif //FORC_PA_2_WORD_H
