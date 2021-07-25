#include <cstring>
#include <iostream>
#include <fstream>
#include "helper_funcs.h"


int readFile(char fileToRead[], char fileName[]){
    std::ifstream fileIn (fileToRead);
    char singleLine[7];
    int lineCount = 0;
    int lineLength = 0;
    int srcLineIndex = 0;


    while (fileIn.getline(singleLine, sizeof(singleLine))){
        lineCount++;
    }

    char srcLines[lineCount][32] = {};     // lineCount is not constant, but is the only way to assign a dynamic size to our arrays
    double parsedData[lineCount][2] = {};  // Usually we would use vectors

    fileIn.clear();
    fileIn.seekg(0, std::ifstream::beg);


    while (fileIn.getline(singleLine, sizeof(singleLine))) {
        lineLength = strlen(singleLine);
        for (int i = 0; i < lineLength; i++){
            srcLines[srcLineIndex][i] = singleLine[i];
            singleLine[i] = 0;
        }
        srcLineIndex++;
    }


    for(int i = 0; i < strlen(srcLines[0]); i++){ // Creates the desired filename for the .wav file
        fileName[i] = srcLines[0][i];
    }

    return atoi(srcLines[1]);
}

void intToLittleBytes(char byteArr[], int param, int index, int bytes)
{
    for (int i = 0; i < bytes; i++) {
        byteArr[i + index] = param >> (i * 8);
    }
}

void makeFileName (char fileName[], char outputFile[], int writeIndex){
    for(int i = 0; i < strlen(fileName); i++, writeIndex++){
        if(fileName[i] != '\0'){
            outputFile[writeIndex] = fileName[i];
            continue;
        }
        break;
    }

    for (char letter:".wav"){ // for letter in ".wav"
        outputFile[writeIndex++] = letter;
    }
}

int calcFrequency(char readFrequency[]){
    int frequency;
    switch (readFrequency[0]) {
        case 's': // silence?
            return 0;
        case 'a':
            frequency = 440;
            break;
        case 'A':
            frequency = 466;
            break;
        case 'b':
            frequency = 494;
            break;
        case 'c':
        case 'B':
            frequency = 523;
            break;
        case 'C':
            frequency = 554;
            break;
        case 'd':
            frequency = 587;
            break;
        case 'D':
            frequency = 662;
            break;
        case 'e':
            frequency = 659;
            break;
        case 'f':
        case 'E':
            frequency = 698;
            break;
        case 'F':
            frequency = 740;
            break;
        case 'g':
            frequency = 784;
            break;
        case 'G':
            frequency = 831;
            break;
    }
    switch (readFrequency[2]) {
        case '0':
            frequency /= 2;
            break;
        case '2':
            frequency *= 2;
            break;
        case '3':
            frequency *= 4;
            break;
        case '4':
            frequency *= 8;
            break;
        case '5':
            frequency *= 16;
            break;
    }
    return frequency;
}

double parseData(char srcLines[][32], double parsedData[][2], double bpm, int srcLinesSize) {
    double totalBeats = 0;
    double beats = 0;
    double bps = bpm/60;
    double numerator = 0;
    double denominator = 0;
    int frequency;

    bool isNumerator = true;
    char fractArr[10] = {}; // Temp array for fractions
    int tempIndex = 0;
    int parseIndex = 0;
    int silenceOrNotIndex = 0;
    for (int i = 2; i < srcLinesSize; i++) {
        if (srcLines[i][0] == 0){
            continue;
        }
        frequency = calcFrequency(srcLines[i]);
        if(srcLines[i][0] == 's'){ // index 2 for case s
            silenceOrNotIndex = 2;
            }
            else{ // index 4 for case not s
            silenceOrNotIndex = 4;
            }
        isNumerator = true;
        for(int j = silenceOrNotIndex; j < strlen(srcLines[i]); j++){
            if(srcLines[i][j] != 32){ // 'bil'
                fractArr[tempIndex++] = srcLines[i][j];
            }
            else{
                numerator = atoi(fractArr);
                memset(fractArr, 0, 10);
                isNumerator = !isNumerator;
                tempIndex = 0;
            }
        }
        denominator = atoi(fractArr);

        beats = (numerator / denominator) * 4;
        totalBeats += beats;

        parsedData[parseIndex][0] = frequency;
        parsedData[parseIndex++][1] = beats;

        tempIndex = 0;
        memset(fractArr, 0, 10);

    }
     return totalBeats/bps;
}

void makeWaveHeader(char wave[] ,int sampleRate, int noChannels, int bitsSample, int noSamples){

    // Compute values of header fields
    unsigned char riffList[] = "RIFF";
    for (int i = 0; i < 4; ++i) {
        wave[i] = riffList[i];
    }
    int byteRate = sampleRate * noChannels * bitsSample/8;
    int blockAlign = noChannels * bitsSample/8;

    char waveString[] = "WAVEfmt ";
    char dataString[] = "data";

    for (int i = 0; i < sizeof(waveString); i++){
        wave[i+8] = waveString[i];
    }

    intToLittleBytes(wave, bitsSample, 16, 4);

    intToLittleBytes(wave, 1, 20, 2); // Audio Format PCM(Pulse-Code Modulation)
    intToLittleBytes(wave, noChannels, 22, 2); // No. of Channels, Mono = 1, Stereo = 2

    intToLittleBytes(wave, sampleRate, 24, 4); // Sample Rate
    intToLittleBytes(wave, byteRate, 28, 4); // Byte Rate

    intToLittleBytes(wave, blockAlign, 32, 2);
    intToLittleBytes(wave, bitsSample, 34, 2);

    for (int i = 0; i < sizeof(dataString); i++){
        wave[i+36] = dataString[i];
    }

    int subChunkSize = noSamples * noChannels * bitsSample/8;
    int chunkSize = 4 + (8 + bitsSample) + (8 + subChunkSize);
    chunkSize += 4 + 8 + bitsSample + 8;
    intToLittleBytes(wave, subChunkSize, 40, 4);
    intToLittleBytes(wave, chunkSize, 4, 4);

}

void addSample(char noteArr[], double sample, int i){
    int sample_16 = (sample * 32767);
    intToLittleBytes(noteArr, sample_16, i * 2, 2);
}