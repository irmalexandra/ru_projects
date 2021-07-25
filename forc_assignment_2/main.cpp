#include <iostream>

#include <cstring>
#include <ctime>

#include "Classes/word.h"
#include "Classes/game_handler.h"
#include "Helpers/file_handler.h"
#include "Helpers/display_handler.h"
#include "Helpers/general_helpers.h."
#include "Structs/player.h"

using namespace std;

int main(int argC, char *argv[]) {
    int const word_length = 32;
    int word_count = 0;
    char file_to_read[word_length] = {};
    char guess_word[word_length];
    char initials[4];


    GameHandler *game_handler;

    bool cont = true;
    bool game_running = true;

    char high_score_file[16] = {'h', 'i', 'g', 'h', '_', 's', 'c', 'o', 'r', 'e', 's', '.', 't', 'x', 't', '\0'};
    int high_score_length;


    if(argC == 2){
        for(int i = 0; i < strlen(argv[1]); i++){
            file_to_read[i] = argv[1][i];
        }
    }
    else{
        cout << "Enter file name: ";
        cin >> file_to_read;
    }
    game_handler = new GameHandler(make_word_list(file_to_read, &word_count), &word_count);
    game_handler->set_current_word();

    print_menu_display();
    while (game_running){
        Player* high_score_list = make_high_score_list(high_score_file, &high_score_length);
        cout << "Choice: ";
        cin >> guess_word;
        if(compare_char_array(guess_word, (char*)"!play")){
            clock_t begin = clock();
            print_options_display();
            cont = true;
            while (cont){
                cout << "What word is this?" << endl;
                clock_t word_begin_time = clock();
                print_char_array(game_handler->get_current_word()->get_scrambled_word());
                cout << endl;
                cout << "Your guess: ";
                cin >> guess_word;

                if (game_handler->compare_guess(guess_word)){
                    clock_t word_end_time = clock();
                    game_handler->add_streak();
                    int word_score = ((game_handler->get_current_word()->get_word_score()) *
                                     game_handler->get_streak())/ (double(word_end_time - word_begin_time) / CLOCKS_PER_SEC);
                    cout << "Correct!" << endl;
                    game_handler->add_score(word_score);
                    cout << "Current streak: " << game_handler->get_streak() << "!" << endl;
                    cout << "You scored " << word_score << " points!" << endl;
                    game_handler->get_current_word()->set_guessed();
                    if(game_handler->get_words_left() != 0){
                        while(game_handler->get_current_word()->get_guessed()){
                            game_handler->set_current_word();
                        }
                    }
                }
                else if(guess_word[0] == '!'){
                    if(compare_char_array(guess_word, (char*)"!hint")){
                        game_handler->get_current_word()->do_hint();
                        game_handler->reset_streak();
                        cout << "Streak reset!"<< endl;

                        print_char_array(game_handler->get_current_word()->get_revealed_word());
                    }
                    else if(compare_char_array(guess_word, (char*)"!quit")){
                        cont = false;
                    }
                    else{
                        cout << "\"" << guess_word << "\"" << " is not a valid command" << endl;
                    }
                }
                else{
                    cout << "Incorrect guess." << endl;
                }
                if(game_handler->get_words_left() == 0){
                    cout << "No words left, you have beat the game!" << endl;
                    clock_t end = clock();
                    cout << "Your final score is: " << game_handler->get_accumulated_score() << endl << "Total time: "
                         << double(end - begin) / CLOCKS_PER_SEC << " seconds." << endl;
                    cout << "Enter your initials (3 letters)." << endl;
                    cout << "Initials: ";
                    cin >> initials;
                    bool high_score = update_high_score_list((game_handler->get_accumulated_score()), (double(end - begin)
                       / CLOCKS_PER_SEC), initials, game_handler->get_longest_streak(), high_score_list, high_score_file, high_score_length);
                    if (high_score){
                        cout << "You beat the current high score!" << endl;
                        cout << endl;
                    }
                    print_menu_display();
                    cont = false;
                    game_handler->reset(word_count);
                }
            }
        }
        else if (compare_char_array(guess_word, (char*)"!highscore")) {
            print_high_scores(high_score_list, high_score_length);
        }
        else if (compare_char_array(guess_word, (char*)"!top5")){
            print_high_scores(high_score_list, (high_score_length < 5)? high_score_length:5);
        }
        else if (compare_char_array(guess_word, (char*)"!quit")){
            game_running = false;
        }
        else {
            cout << "Invalid input." << endl;
        }
    }
    return 0;
}
