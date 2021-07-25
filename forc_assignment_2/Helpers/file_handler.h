#ifndef FORC_PA_2_FILE_HANDLER_H
#define FORC_PA_2_FILE_HANDLER_H

#include "../Classes/word.h"
#include "../Structs/player.h"
#include "../Helpers/general_helpers.h"

Word* make_word_list(char file_to_read[], int *word_count_ptr);
Player* make_high_score_list(char file_to_read[], int* high_score_length_ptr);
bool update_high_score_list(int accumulated_score, double seconds, char* initials, int longest_streak, Player* high_score_list, char* high_score_file, int high_score_length);

#endif //FORC_PA_2_FILE_HANDLER_H
