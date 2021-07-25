#include <chrono>

#include "Models/Investigator.h"
#include "Models/EldritchHorror.h"

#include "Classes/IndividualCreator.h"
#include "string"
#include "Helpers/InputHandler.h"

void stuff(InputHandler* thing){
    thing->DHRoles->get_data();
    thing->DHSpecies->get_data();

}

using namespace std;
int main() {
    cout << "Starting... " << endl;
    cout << "Setting random seed" << endl;
    auto randomSeed = std::chrono::system_clock::to_time_t(std::chrono::system_clock::now());
//    auto randomSeed = 1616788278;
    cout << "This is the random seed: " << randomSeed << endl;
    srand(randomSeed);
    auto input_handler = new InputHandler();
    input_handler->main_menu();
    cout << "Exiting" << endl;
    return 0;

}
