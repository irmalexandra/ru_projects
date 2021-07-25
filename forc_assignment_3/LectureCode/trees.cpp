#include <iostream>

#include "trees.h"

using namespace std;

DataClass::DataClass(int number, char letter){
    this->number = number;
    this->letter = letter;
}

ostream& operator<<(ostream& out, const DataClass *dc){
    if(dc != NULL){
        out << "{" << dc->number << ":" << dc->letter << "}";
    }
    return out;
}

Node::Node(DataClass *data, Node *left, Node *right){
    this->data = data;
    this->left = left;
    this->right = right;
}

Node::~Node(){
    delete data;
    delete left;
    delete right;
}

ostream& operator<<(ostream& out, const Node *node){
    if(node != NULL){
        out << node->left;
        out << " " << node->data << " ";
        out << node->right;
    }
    return out;
}