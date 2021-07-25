#include <cstdlib>
#include <iostream>
#include <cstring>
#include <ctime>

#include "game_handler.h"

int GameHandler::get_streak() {
    return this->streak;
}

void GameHandler::reset_streak() {
    this->streak = 0;
}

void GameHandler::add_streak() {
    this->streak++;
    if (this->streak > this->longest_streak){
        this->longest_streak = this->streak;
    }
}

int GameHandler::get_longest_streak() {
    return this->longest_streak;
}

GameHandler::GameHandler(Word *word_list, int *word_count) {
    this->word_list = word_list;
    this->word_count = *word_count;
    this->words_left = this->word_count;
    this->accumulated_score = 0;
    this->streak = 0;
    this->longest_streak = 0;

    srand (time(NULL));
}

bool GameHandler::compare_guess(char *guess_arr) {
    if(strlen(guess_arr) == current_word->get_word_length()){
        char *current_word_arr = current_word->get_word();
        for (int i = 0; i < (current_word->get_word_length()); i++){
            if (guess_arr[i] != current_word_arr[i]){
                return false;
            }
        }
        this->words_left--;

        return true;
    }
    return false;

}

void GameHandler::add_score(int score) {
    this->accumulated_score += score;
}

int GameHandler::get_accumulated_score(){
    return this->accumulated_score;
}

void GameHandler::set_current_word() {
    int rand_word_index;
    rand_word_index = rand() % word_count;
    this->current_word = &word_list[rand_word_index];
}

Word *GameHandler::get_current_word() {
    return this->current_word;
}

int GameHandler::get_words_left() {
    return this->words_left;
}


void GameHandler::reset(int word_count) {
    for (int i = 0; i < word_count; ++i) {
        word_list[i].reset();
    }
    this->reset_streak();
    this->longest_streak = 0;
    this->words_left = this->word_count;
    this->accumulated_score = 0;
}