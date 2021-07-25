
#ifndef FORC_PA_3_HUFFMANDECODER_H
#define FORC_PA_3_HUFFMANDECODER_H

#include <iostream>
#include <fstream>
#include <map>
#include <cmath>
#include <deque>
#include "Node.h"
#include "../Helpers/HelperFunctions.h"

using namespace std;

struct DecodeInfo{
public:
    DecodeInfo(map<char, deque<char>>* compression_keys, int* bit_count, Node* root){
        this->bit_count = bit_count;
        this->compression_keys = compression_keys;
        this->root = root;
    }
    ~DecodeInfo(){
        this->compression_keys->erase(this->compression_keys->begin(), this->compression_keys->end());
        delete this->compression_keys;
        delete this->root;
        delete this->bit_count;
    }
    Node* get_root(){
        return this->root;
    }
    int* get_bit_count(){
        return this->bit_count;
    }
private:
    map<char, deque<char>>*compression_keys;
    int* bit_count = nullptr;
    Node* root;
};

class HuffmanDecoder {
public:
    HuffmanDecoder() = default;
    ~HuffmanDecoder();

    DecodeInfo* get_decode_info();

    void decode(ifstream& stream);

private:
    Node* root = nullptr;
    vector<char>* file_content = nullptr;
    map<char, deque<char>>* compression_keys = nullptr;
    int* bit_count = nullptr;
    void build_tree_from_keys(deque<char> value, char key, Node* current_node);
    void read_head(ifstream& stream);
    void make_decode_tree();
};

#endif //FORC_PA_3_HUFFMANDECODER_H
