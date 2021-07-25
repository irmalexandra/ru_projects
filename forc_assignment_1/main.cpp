#include <iostream>
#include <fstream>
#include <cmath>
#include <cstring>
#include "helper_funcs.h"

using namespace std;

int main(int argC, char *argv[]) {
    char tempBpmArr[32] = {};   // Temporary array to read the bpm value
    char fileToRead[32] = {};   // Argument file name array
    char fileName[32] = {};     // Array for the initial source file name
    char fileToWrite[32] = "../wav_out/"; // Array for the name and location of the output file
    char singleLine[32] = {};

    char header[44];   // Array that houses the header and the sample collection

    int fileInLength = 0;       // Will have the length of the file that is read
    int parsedDataIndex = 0;    // Keeps track of where in the note array the loop has reached
    int lineCount = 0;          // How many lines there are in the read file
    int lineLength = 0;         // The length of a given line
    int srcLineIndex = 0;

    int sampleRate = 44100;     // Sample rate in Hz. (CD quality)
    int noChannels = 1;         // Mono
    int bitsSample = 16;        // Number of bits that each sample uses
    int noSamples = 0;          // Number of samples that need to be created
    int frequency = 0;          // Frequency of each note
    int samplesPerNote = 0;     // Tracks how many samples there are for each note in the audio file

    double BPM = 0;             // Beats per minute
    double duration = 0;        // The total duration of the audio file
    double sample = 0;          // The value of each sample

    if(argC == 2){
        for(int i = 0; i < strlen(argv[1]); i++){
            fileToRead[i] = argv[1][i];
        }
    }
    else if (argC > 2) {

    }
    else{
        cout << "enter file name";
        cin >> fileToRead;
    }

    ifstream fileIn (fileToRead);
    while (fileIn.getline(singleLine, sizeof(singleLine))){
        lineCount++;
    }

    char srcLines[lineCount][32] = {};     // lineCount is not constant, but is the only way to assign a dynamic size to our arrays
    double parsedData[lineCount][2] = {};  // Usually we would use vectors

    fileIn.clear();
    fileIn.seekg(0, ifstream::beg);


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
    for(int i = 0; i < strlen(srcLines[1]); i++){ // Parses the BPM from the source file
            tempBpmArr[i] = srcLines[1][i];
    }

    BPM = atoi(tempBpmArr);
    duration = parseData(srcLines, parsedData, BPM, lineCount); // Fills a 2d array with note information, and calculates the total duration of the audio file
    noSamples = (int)duration * sampleRate; // Total number of samples for file

    makeFileName(fileName, fileToWrite, strlen(fileToWrite));

    ofstream fileOut;
    fileOut.open(fileToWrite , ios::binary);

    while (parsedData[parsedDataIndex][1] != 0) {
        samplesPerNote = (int)(60 / BPM * sampleRate * parsedData[parsedDataIndex][1]);
        char noteArr[samplesPerNote * 2];
        for (int j = 0; j < samplesPerNote; j++) {
            frequency = (int)parsedData[parsedDataIndex][0];
            sample = cos(frequency * j * 3.142 / sampleRate);
            addSample(noteArr, sample, j);
        }
        fileOut.write(noteArr, sizeof(noteArr));
        parsedDataIndex++;
    }

    makeWaveHeader(header, sampleRate, noChannels, bitsSample, noSamples);

    fileOut.seekp(0, ios::beg);
    fileOut.write(header, sizeof(header));
    fileOut.close();

    return 0;
}
