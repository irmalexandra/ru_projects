#include <fstream>
#include <cstring>
#include <iostream>
#include "file_handler.h"

using namespace std;


Word* make_word_list(char file_to_read[], int *word_count_ptr){

    char single_line[32] = {};
    int line_count = 0;
    int word_length = 32;

    Word *word_list = nullptr;

    ifstream fileIn (file_to_read);

    while (fileIn.getline(single_line, sizeof(single_line))){
        line_count++;
    }

    fileIn.clear();
    fileIn.seekg(0, ios::beg);

    word_list = new Word[line_count];
    Word* temp_word = nullptr;

    int word_count = 0;


    while (fileIn.getline(single_line, sizeof(single_line))) {
        temp_word = new Word(single_line, strlen(single_line));
        word_list[word_count] = *temp_word;
        word_count++;
        delete temp_word;
    }
    delete temp_word;
    *word_count_ptr = word_count;
    return word_list;
}

Player* make_high_score_list(char file_to_read[], int *high_score_length_ptr){
    char single_line[32] = {};
    int line_count = 0;

    Player* high_score_list = nullptr;

    ifstream fileIn (file_to_read);
    delete file_to_read;
    while (fileIn.getline(single_line, sizeof(single_line))){
        line_count++;
    }

    fileIn.clear();
    fileIn.seekg(0, ios::beg);

    high_score_list = new Player[line_count];

    int high_score_count = 0;

    while (fileIn.getline(single_line, sizeof(single_line))) {
        high_score_list[high_score_count] = *new Player(split_array(single_line, ' '));
        high_score_count++;
    }

    *high_score_length_ptr = high_score_count;
    return high_score_list;
}

bool update_high_score_list(int total_score, double seconds, char* initials, int longest_streak, Player* high_score_list, char* high_score_file, int high_score_length){
    bool inserted = false;
    bool new_record = false;
    ofstream fileout;
    fileout.open(high_score_file, std::ofstream::trunc);
    if(high_score_length == 0){
        fileout << initials << " " << total_score << " " << seconds << " " << longest_streak << endl;
        new_record = true;
    }
    else{
        Player *current_player;
        for(int i = 0; i < high_score_length; i++){
            current_player = &high_score_list[i];
            if(total_score >= current_player->score && !inserted){
                if (i == 0){
                    new_record = true;
                }
                fileout << initials << ' ' << total_score << ' ' << seconds << ' ' << longest_streak << endl;
                inserted = true;
            }
            fileout << current_player->initials << ' ' << current_player->score << ' ' << current_player->time << ' ' << current_player->longest_streak <<  endl;
        }
        if(!inserted){
            fileout << initials << ' ' << total_score << ' ' << seconds << ' ' << longest_streak << endl;
        }
        delete current_player;
    }

    fileout.close();
    return new_record;
}