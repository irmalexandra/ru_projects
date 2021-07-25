

#include "Compressor.h"

Compressor::Compressor(EncodeInfo* encode_info) {
    this->encode_info = encode_info;
}

Compressor::~Compressor() {
    delete this->encode_info;
}

void Compressor::compress(string file_name) {
    ofstream out_stream;
    out_stream.open(file_name, ios::binary|ios::trunc|ios::out);

    write_header(out_stream);
    write_bytes(out_stream);

    out_stream.close();
}

void Compressor::set_bit(char& byte, char& bit_char, int& byte_index, bool is_overflow){
    if (bit_char == '1'){
        if (is_overflow){
            byte = byte | (1 << (7 - byte_index));
        }
        else{
            byte = byte << 1;
            byte = byte | 1;
        }
    }
    if(bit_char == '0'){
        if (is_overflow){
            byte = byte | (0 << (7 - byte_index));
        }
        else{
            byte = byte << 1;
        }
    }
}

void Compressor::write_bytes(ofstream& out_stream) {
    char byte = 0;
    int byte_index = 0;
    int bytes_inserted = 0;
    int number_of_bytes = (this->encode_info->bit_count) / 8;
    auto current_value = new char;
    for(int x = 0; x < this->encode_info->file_content->size(); x++){

        current_value = this->encode_info->compression_keys->find(this->encode_info->file_content->at(x))->second;

        for (int i = 0; i < strlen(current_value); i++){
            set_bit(byte, current_value[i], byte_index, bytes_inserted == number_of_bytes);
            byte_index++;
            if(byte_index == 8){
                out_stream << byte;
                bytes_inserted++;
                byte = 0;
                byte_index = 0;
            }
        }

    }
    delete current_value;

    out_stream << byte;
}

void Compressor::write_header(ofstream& out_stream) {
    char byte = 0;
    int bits_inserted = 0;
    int key_length = 0;
    int byte_index = 0;
    out_stream << this->encode_info->compression_keys->size() << endl;
    for (auto data:*this->encode_info->compression_keys) {
        key_length = strlen(data.second);

        out_stream << data.first;
        out_stream << (char) key_length; // length of "value"

        for (int i = 0; i < key_length; i++) {
            set_bit(byte, data.second[i], byte_index, byte_index == key_length);
            byte_index++;
            if (byte_index == 8 || byte_index == key_length) {
                byte = byte << (8 - byte_index);
                out_stream << (byte);
                bits_inserted++;
                byte = 0;
                byte_index = 0;
            }
        }
        if (key_length > 8){
            byte = byte << (8-byte_index);
            out_stream << (byte);
            byte = 0;
            byte_index = 0;
        }

    }
    out_stream << this->encode_info->bit_count;
    out_stream << "\\";
}
