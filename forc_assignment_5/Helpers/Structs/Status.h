#ifndef FORC_PA_5_STATUS_H
#define FORC_PA_5_STATUS_H

#include "iostream"
#include "vector"

using namespace std;

struct Status {
    explicit Status(){
        this->injured = false;
        this->insane = false;
        this->frightened = false;
        this->outnumbered = false;
        this->enraged = false;
        this->fleeing = false;
        this->dead = false;
        this->overcame = false;
    };

    void set_injured(bool maybe){
        this->injured = maybe;
    }

    void set_frightened(bool maybe){
        this->frightened = maybe;
    }

    void set_outnumbered(bool maybe){
        this->outnumbered = maybe;
    }

    void set_enraged(bool maybe){
        this->enraged = maybe;
    }

    void set_insane(bool maybe){
        this->insane = maybe;
    }

    void set_dead(bool maybe){
        this->dead = maybe;
    }

    void set_fleeing(bool maybe){
        this->fleeing = maybe;
    }

    void set_overcame(bool maybe){
        this->overcame = maybe;
    }

    bool injured;
    bool insane;
    bool frightened;
    bool outnumbered;
    bool enraged;
    bool fleeing;
    bool dead;
    bool overcame;
};


#endif //FORC_PA_5_STATUS_H
