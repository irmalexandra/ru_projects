#include "HuffmanEncoder.h"


HuffmanEncoder::~HuffmanEncoder() {
    delete this->root;
}

void HuffmanEncoder::encode(vector<char> *file_content){
    this->file_content = file_content;
    make_frequency_table();
    build_tree();
    make_compression_keys();
}

void HuffmanEncoder::make_frequency_table() {
    auto* frequency_map = new map<char, unsigned long>;
    for (auto const letter:*this->file_content){
        // Originally we ignored newlines but we figured the compression would be better if we encoded them as well.
        (*frequency_map)[letter] += 1;
    }
    this->frequency_table = frequency_map;
}

void HuffmanEncoder::build_tree() {
    Node *left;
    Node *right;
    std::vector <Node*> roots;

    for (auto pair:*this->frequency_table){
        roots.push_back(new Node( new Data( pair.second, pair.first)));
    }

    while (roots.size() != 1){
        sort(roots.begin(), roots.end(), comparer);
        right =  roots.back();
        roots.pop_back();
        left = roots.back();
        roots.pop_back();
        Node *new_node = new Node(left, right);
        roots.push_back(new_node);
    }
    this->root = roots.back();
}

void HuffmanEncoder::make_compression_keys(Node* current_node, char *key, map<char, char*> *key_map, int *depth){
    if (current_node->get_left() == nullptr && current_node->get_right() == nullptr){
        char* temp_key = new char[256];
        copy(key, key+256, temp_key);
        key_map->insert(std::pair<char, char*>(current_node->get_data()->get_value(), temp_key));
        return;
    }
    key[*depth] = '0';
    (*depth)++;
    make_compression_keys(current_node->get_left(), key, key_map, depth);
    key[*depth] = 0;
    (*depth)--;
    key[*depth] = '1';
    (*depth)++;
    make_compression_keys(current_node->get_right(), key, key_map, depth);
    key[*depth] = 0;
    (*depth)--;
};

void HuffmanEncoder::make_compression_keys() {
    this->compression_keys = new map<char, char*>;
    char *key = new char[256]{'\0'};
    auto* depth = new int(0);

    make_compression_keys(root, key, this->compression_keys, depth);
    delete depth;
}

EncodeInfo* HuffmanEncoder::get_encode_info(){
   return new EncodeInfo(this->frequency_table, this->compression_keys, this->file_content);
}
