#ifndef FORC_PA_2_PLAYER_H
#define FORC_PA_2_PLAYER_H

struct Player{
    Player();
    Player(char **player_info);
    void set_player(char **player_info);
    char initials[4];
    int score;
    float time;
    int longest_streak;
};

#endif //FORC_PA_2_PLAYER_H
