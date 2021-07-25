#ifndef FORC_PA_3_HUFFMANENCODER_H
#define FORC_PA_3_HUFFMANENCODER_H

#include <iostream>
#include <set>
#include <map>
#include <string>
#include <cstring>
#include <vector>
#include <utility>
#include <algorithm>
#include "../Classes/Node.h"
#include "../Classes/Data.h"
#include "../Helpers/HelperFunctions.h"

struct EncodeInfo{
  EncodeInfo() = default;

  EncodeInfo(map<char, unsigned long>* frequency_table, map<char, char*>* compression_keys, vector<char>* file_content){
      this->frequency_table = frequency_table;
      this->compression_keys = compression_keys;
      this->file_content = file_content;
      this->count_bits();
  }

  ~EncodeInfo(){
      delete this->file_content;
      this->compression_keys->erase(this->compression_keys->begin(), this->compression_keys->end());
      delete this->compression_keys;
      this->frequency_table->erase(this->frequency_table->begin(), this->frequency_table->end());
      delete this->frequency_table;
  }

  void count_bits(){
      for (auto freq:*this->frequency_table){
          this->bit_count += freq.second * strlen(this->compression_keys->find(freq.first)->second);
      }
  }

  map<char, unsigned long>* frequency_table = nullptr;
  map<char, char*>* compression_keys = nullptr;
  vector<char>* file_content = nullptr;
  unsigned int bit_count = 0;
};

class HuffmanEncoder {
public:
    HuffmanEncoder() = default;
    ~HuffmanEncoder();

    void encode(vector<char> *file_content);
    EncodeInfo* get_encode_info();

private:
    Node* root = nullptr;
    vector<char>* file_content = nullptr;
    map<char, unsigned long>* frequency_table = nullptr;
    map<char, char*>* compression_keys = nullptr;
    void make_frequency_table();
    void build_tree();
    void make_compression_keys();
    void make_compression_keys(Node* current_node, char *key, map<char, char*> *key_map, int *depth);
};


#endif //FORC_PA_3_HUFFMANENCODER_H
