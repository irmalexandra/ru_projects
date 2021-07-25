

#ifndef FORC_PA_3_HUFFMANENCODER_H
#include "HuffmanDecoder.h"
#include <iostream>
#include <fstream>
#include <map>
#include <deque>
#include "../Helpers/HelperFunctions.h"

using namespace std;

class Decompressor {

public:
    Decompressor() = default;
    explicit Decompressor(DecodeInfo* decode_info);
    ~Decompressor();
    void decompress(ifstream& in_stream,  ofstream& out_stream);

private:
    DecodeInfo* decode_info = nullptr;
};
#endif //FORC_PA_3_HUFFMANENCODER_H
