
#include <cstring>
#include <iostream>
#include "display_handler.h"
#include <iomanip>


using namespace std;

void print_char_array(char *array){// this function is living proof for us being awesome at this.
    for (int i = 0; i < strlen(array); i++){
            cout << array[i];
    }
    cout << endl;
}

void print_options_display(){
    cout << "**********************************************************" << endl;
    cout << "*************   \"![h]int\" to get a hint     ****************" << endl;
    cout << "*************   \"![q]uit\" to end the game   ****************" << endl;
    cout << "**********************************************************" << endl;
}

void print_menu_display(){
    cout << "*********************************************************************" << endl;
    cout << "*************   \"![p]lay\" to start playing             ****************" << endl;
    cout << "*************   \"![h]ighscore\" to see high scores      ****************" << endl;
    cout << "*************   \"![t]op5\" to see the top 5 scores      ****************" << endl;
    cout << "*************   \"![q]uit\" to end the game              ****************" << endl;
    cout << "*********************************************************************" << endl;
}

void print_high_scores(Player *scores, int count){
    cout << "" << endl;
    int col_width = 15;
    cout << "NAME"<< setw(col_width) << "SCORE" << setw(col_width) << "TIME" << setw(col_width)  << "LONGEST STREAK" << endl;
    for (int i = 0; i < count; i++){
        //cout << scores[i].initials << "|" << scores[i].score << "" << scores[i].time << "" << scores[i].longest_streak << endl;
        //cout << scores[i].initials << setw(col_width) << "|"<< setw(col_width) << scores[i].score<< "|" << setw(col_width) << scores[i].time << "|" << setw(col_width)  << scores[i].longest_streak << "|" << endl;
        cout << scores[i].initials << setw(col_width+1) << scores[i].score << setw(col_width) << scores[i].time << setw(col_width) << scores[i].longest_streak << endl;
    }
}
