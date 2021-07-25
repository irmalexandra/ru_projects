#ifndef FORC_PA_2_DISPLAY_HANDLER_H
#define FORC_PA_2_DISPLAY_HANDLER_H

#include "../Classes/word.h"
#include "../Structs/player.h"

void print_char_array(char *array);
void print_int_array(int *array, int count);
void print_options_display();
void print_menu_display();
void print_high_scores(Player *scores, int count);

#endif //FORC_PA_2_DISPLAY_HANDLER_H
