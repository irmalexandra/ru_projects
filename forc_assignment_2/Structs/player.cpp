#include "iostream"
#include "player.h"

void Player::set_player(char **player_info){
    for (int i = 0; i < 4; i++){
        this->initials[i] = player_info[0][i];
    }
    this->score = atoi(player_info[1]);
    this->time = atof(player_info[2]);
    this->longest_streak = atoi(player_info[3]);
    delete[] player_info;
}

Player::Player() {}

Player::Player(char **player_info) {
    for (int i = 0; i < 4; i++){
        this->initials[i] = player_info[0][i];
    }
    this->score = atoi(player_info[1]);
    this->time = atof(player_info[2]);
    this->longest_streak = atoi(player_info[3]);
    delete[] player_info;
}
