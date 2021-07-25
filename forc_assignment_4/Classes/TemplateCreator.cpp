#include "TemplateCreator.h"

speciesStats* TemplateCreator::get_species_stats(){
    auto species_stats = new speciesStats();
    get_base_stats(species_stats);
    int choice = -1;



    while(true){
        cout << "Is this a Eldritch Horror?\n1. Yes\n 2. No" << endl;
        cin >> choice;
        if(cin.fail()){
            cout << "Invalid input" << endl;
            cin.clear();
            cin.ignore(std::numeric_limits<int>::max(),'\n');
            continue;
        }
        switch (choice) {
            case 1:
                species_stats->type = "Eldritch Horror";
                species_stats->is_eldritch = true;

                species_stats->unnatural = true;
                species_stats->dis_min = 10;
                species_stats->dis_max = 10;
                species_stats->trauma_min = get_int_within_range(0, 3, "Enter lower range for traumatism: ");
                species_stats->trauma_max = get_int_within_range(species_stats->trauma_min, 3, "Enter upper range for traumatism: ");
                return species_stats;
            case 2:
                species_stats->type = "Creature";
                species_stats->is_eldritch = false;

                cout << "Unnatural?\n1. Yes\n2. No" << endl;
                cin >> choice;
                species_stats->unnatural = choice == 1;

                species_stats->dis_min = get_int_within_range(0, 10, "Enter lower range for disquiet: ");
                species_stats->dis_max = get_int_within_range(species_stats->dis_min, 10, "Enter upper range for disquiet: ");
                return species_stats;

            default:
                cout << "Invalid input" << endl;
                break;

        }
    }

}

Species *TemplateCreator::create_species() {
    speciesStats* stats = this->get_species_stats();
    return new Species(stats);
}

Role *TemplateCreator::create_role() {
    auto role_stats = new baseStats();
    role_stats->type = "Person";
    get_base_stats(role_stats);

    return new Role(role_stats);
}


void TemplateCreator::get_base_stats(baseStats* base_stats) {
    if (base_stats == nullptr){
       auto base_stats = new baseStats();
    }

    cout << "Enter name (no spaces): ";
    cin >> base_stats->name;


    base_stats->life_min = get_int_within_range(0, 10, "Enter lower range for life: ");
    base_stats->life_max = get_int_within_range(base_stats->life_min, 10, "Enter upper range for life: ");

    base_stats->int_min = get_int_within_range(0, 10, "Enter lower range for intelligence: ");
    base_stats->int_max = get_int_within_range(base_stats->int_min, 10, "Enter upper range for intelligence: ");

    base_stats->str_min = get_int_within_range(0, 10, "Enter lower range for strength: ");
    base_stats->str_max = get_int_within_range(base_stats->str_min, 10, "Enter upper range for strength: ");

}
