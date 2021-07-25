#ifndef FORC_PA_3_NODE_H
#define FORC_PA_3_NODE_H
#include "Data.h"
#include "iostream"

using namespace std;

class Node{
public:
    Node(Node *left, Node *right);
    Node(Data *data);
    Node();

    Node* get_left();
    Node* get_right();
    void set_left(Node* node);
    void set_right(Node* node);

    Data* get_data();
    void set_data(Data* data);

    virtual ~Node();

    friend ostream& operator<< (ostream& out, const Node *node);

private:
    Data *data = nullptr;
    Node *left = nullptr;
    Node *right = nullptr;
};

#endif //FORC_PA_3_NODE_H
