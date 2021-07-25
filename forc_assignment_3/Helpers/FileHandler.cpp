#include "FileHandler.h"

vector <char>* read_from_file(const string& filename) {
    auto* data = new vector<char>();

    char byte;
    ifstream input_file(filename, ios::binary);
    while (input_file.get(byte)) {
        data->push_back(byte);
    }
    input_file.close();
    return data;
}
