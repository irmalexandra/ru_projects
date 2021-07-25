#include "Node.h"

Node::Node(Node *left, Node *right) {
    this->data = new Data((left->data->get_frequency() + right->data->get_frequency()));
    this->left = left;
    this->right = right;
}

Node::Node(Data *data) {
    this->data = data;
}

Node::~Node() {
    delete this->data;
    delete this->left;
    delete this ->right;
}

Node * Node::get_left() {
    return this->left;
}

Node * Node::get_right() {
    return this->right;
}

Data* Node::get_data(){
    return this->data;
}


ostream& operator<< (ostream& out, const Node *node){
    if(node != nullptr){
        out << node->data;
        out << node->left;
        out << node->right;
    }
    return out;
}

Node::Node() {
    this->right = nullptr;
    this->left = nullptr;
    this->data = new Data();
}

void Node::set_left(Node* node) {
    this->left = node;
}

void Node::set_right(Node *node) {
    this->right = node;
}

void Node::set_data(Data* data) {
    this->data = data;
}


