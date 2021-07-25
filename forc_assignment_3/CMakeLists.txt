cmake_minimum_required(VERSION 3.17)
project(FORC_PA_3)

set(CMAKE_CXX_STANDARD 11)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR})

add_executable(FORC_PA_3 main.cpp Classes/Node.cpp Classes/Node.h Helpers/FileHandler.cpp Helpers/FileHandler.h Classes/Data.cpp Classes/Data.h Helpers/HelperFunctions.cpp Helpers/HelperFunctions.h Classes/Decompressor.cpp Classes/Decompressor.h Classes/HuffmanDecoder.cpp Classes/HuffmanDecoder.h Classes/Compressor.cpp Classes/Compressor.h Classes/HuffmanEncoder.cpp Classes/HuffmanEncoder.h)