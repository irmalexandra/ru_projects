#include "Decompressor.h"

Decompressor::Decompressor(DecodeInfo *decode_info) {
    this->decode_info = decode_info;
}

Decompressor::~Decompressor() {
    delete this->decode_info;
}

bool is_bit_set(char byte, int k){
    if (byte & (1 << (7-k))){
        return true;
    }
    else{
        return false;
    }
}

void Decompressor::decompress(ifstream &in_stream, ofstream &out_stream) {
    auto byte = (char) in_stream.get();
    Node* current_node = this->decode_info->get_root();
    int bit_count = 0;
    while (!in_stream.eof() && bit_count < *this->decode_info->get_bit_count()){
        for(int i = 0; i < 8; i++){

            if(current_node->get_left() == nullptr && current_node->get_right() == nullptr){
                out_stream << current_node->get_data()->get_value();
                current_node = this->decode_info->get_root();
            }

            if(is_bit_set(byte, i)){
                current_node = current_node->get_right();
            }
            else{
                current_node = current_node->get_left();
            }
            if(bit_count == *this->decode_info->get_bit_count()) {
                break;
            }
            bit_count++;
        }
        byte = (char) in_stream.get();
    }
    out_stream << current_node->get_data()->get_value();
}