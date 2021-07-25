#ifndef FORC_PA_1_HELPER_FUNCS_H
#define FORC_PA_1_HELPER_FUNCS_H

void intToLittleBytes(char byteArr[], int param, int index, int bytes);

int calcFrequency(char readFrequency[]);

double parseData(char srcLines[][32], double parsedData[][2], double bpm, int srcLinesSize);

void makeFileName (char fileName[], char outputFile[], int writeIndex);

void makeWaveHeader(char wave[] ,int sampleRate, int noChannels, int bitsSample, int noSamples);

void addSample(char wave[], double sample, int i);

int readFile(char fileToRead[], char fileName[]);

#endif //FORC_PA_1_HELPER_FUNCS_H
