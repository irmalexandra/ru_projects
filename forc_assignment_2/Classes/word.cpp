#include "word.h"
#include <stdlib.h>     /* srand, rand */
#include <time.h>       /* time */
#include <iostream>

Word::Word() {
}

Word::Word(char *word, int word_length){
    this->word_length = word_length;
    this->word = new char [word_length];
    this->word[word_length] = '\0';
    this->scrambled_word = new char [word_length];
    this->scrambled_word[word_length] = '\0';
    this->revealed_letters = new bool[word_length] {false};
    this->revealed_word = new char[word_length];
    this->hints_given = 0;
    this->guessed = false;

    for(int i = 0; i < word_length; i++){
        this->word[i] = word[i];
        this->scrambled_word[i] = word[i];
    }

    this->scramble_word(20);

}

char* Word::get_word() {
    return this->word;
}

void Word::set_guessed() {
    this->guessed = true;
}

bool Word::get_guessed() const {
    return this->guessed;
}

char* Word::get_revealed_word(){
    return this->revealed_word;
}

void Word::do_hint() {
    srand (time(NULL));
    if(this->hints_given != this->word_length){
        int index_1 = rand() % word_length;
        while (this->revealed_letters[index_1]){
            index_1 = rand() % word_length;
        }
        this->revealed_letters[index_1] = true;
        this->hints_given++;
        this->update_revealed_letters();
    }
}

int Word::get_word_score() {
    return (this->word_length - this->hints_given)*1000;
}

void Word:: update_revealed_letters(){

    for(int i = 0; i < this->word_length; i++){
        if(this->revealed_letters[i]){
            this->revealed_word[i] = this->word[i];
        }
        else{
            this->revealed_word[i] =  '-';
        }
    }
    this->revealed_word[word_length] = '\0';

}

char* Word::get_scrambled_word() {
    return this->scrambled_word;
}

bool Word::check_same(){
    for(int i = 0; i < word_length; i++){
        if(scrambled_word[i] != word[i]){
            return false;
        }
    }
    return true;
}

void Word::scramble_word(int number_of_runs) {
    /* initialize random seed: */
    srand (time(NULL));
    char char_holder;
    for(int i = 0; i < number_of_runs; i++){
        int index_1 = rand() % word_length;
        int index_2 = rand() % word_length;

        while(index_1 == index_2){
            index_2 = rand() % word_length;
        }
        char_holder = scrambled_word[index_1];
        this->scrambled_word[index_1] = this->scrambled_word[index_2];
        this->scrambled_word[index_2] = char_holder;
    }
    if(check_same()){
        scramble_word(20);
    }
}

int Word::get_word_length() {
    return this->word_length;
}


void Word::reset() {
    this->revealed_letters = new bool[this->word_length] {false};
    this->revealed_word = new char[this->word_length];
    this->hints_given = 0;
    this->guessed = false;
}


