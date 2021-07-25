//
// Created by rikki on 2/4/2021.
//

#include "general_helpers.h"
#include "cstring"

bool compare_char_array(char* guess, char* command){
    for (int i = 0; i < strlen(guess); ++i) {
        if (guess[i] != command[i]) return false;
    }
    return true;
}



char ** split_array(char* array, char delimiter){
    char* initials = new char[32];
    char* score = new char[32];
    char* time = new char[32];
    char* streak = new char[32];

    char** return_array = new char*[4];

    return_array[0] = initials;
    return_array[1] = score;
    return_array[2] = time;
    return_array[3] = streak;

    bool b_initials = true;
    bool b_score = false;
    bool b_time = false;
    bool b_streak = false;

    int write_index = 0;

    for (int i = 0; i < strlen(array); i++) {
        if (array[i] == delimiter){
            if (b_initials){
                b_initials = false;
                b_score = true;
                initials[write_index] = '\0';
                write_index = 0;
            }else if(b_score){
                b_score = false;
                b_time = true;
                score[write_index] = '\0';
                write_index = 0;
            }else{
                b_time = false;
                b_streak = true;
                time[write_index] = '\0';
                write_index = 0;
            }
        }else{
            if (b_initials){
                initials[write_index] = array[i];
            }
            if (b_score){
                score[write_index] = array[i];
            }
            if (b_time){
                time[write_index] = array[i];
            }
            if (b_streak){
                streak[write_index] = array[i];
            }
            write_index++;
        }
        streak[write_index] = '\0';
    }
    return return_array;
}