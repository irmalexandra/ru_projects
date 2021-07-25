#include "ActionCreator.h"


Defensive* ActionCreator::create_defensive(Payload* payload) {
    auto stats = get_base_stats(payload);
    stats->type = "Defensive";
    int* defense_modifier = new int();
    int* attack_modifier = new int();
    int* health_recovery = new int();
    int* duration = new int(1);

    cout << "Enter a value for how many turns this action applies (default value is 1)";
    *duration = get_int_within_range(0, 5, "Duration: ");
    cout << "Enter a value for the defense modifier (enter 0 to not effect this stat)" << endl;
    *defense_modifier = get_int_within_range(0, 10, "Def modifier: ");
    cout << "Enter a value for the attack modifier (enter 0 to not effect this stat)" << endl;
    *attack_modifier = get_int_within_range(0, 10, "Def attack: ");
    cout << "Enter a value for the health recovery (enter 0 to not effect this stat)";
    *health_recovery = get_int_within_range(0, 10, "Recovery: ");

    return new Defensive(stats, defense_modifier, attack_modifier, health_recovery, duration);
}

Offensive* ActionCreator::create_offensive(Payload* payload) {
    auto stats = get_base_stats(payload);
    stats->type = "Offensive";
    int* damage = new int();
    int* hit_modifier = new int();
    cout << "Enter a value for how much damage this action will cause" << endl;
    *damage = get_int_within_range(1, 10, "Damage: ");
    cout << "Enter a value for the hit modifier (higher chance means more likely to hit)" << endl;
    *hit_modifier = get_int_within_range(0, 10, "Hit Modifier: ");
    return new Offensive(stats, hit_modifier, damage);
}


baseActionTemplateStats *ActionCreator::get_base_stats(Payload* payload) {
    string name;
    string is_physical;
    cout << "What is the name of this action?\nName: ";
    cin >> name;
    while(get_index(payload->DHDefensives->get_data(), name) != -1 || get_index(payload->DHOffensives->get_data(), name) != -1 ){
        cout << "That name is already taken\nName: ";
        cin >> name;
    }
    auto stats = new baseActionTemplateStats();
    stats->name = name;
    stats->cooldown = get_int_within_range(1, 5, "How long is the cool down for this action?");
    cout << "Is this a physical action (No would imply that it is a mental action)?\n1. Yes\n2. No" << endl;
    cin >> is_physical;

    if(is_physical == "1"){
        stats->physical = true;
    }
    else{
        stats->physical = false;
    }
    return stats;
}
