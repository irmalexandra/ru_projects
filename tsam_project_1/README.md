## Ensure dependencies
Ensure that you have **gcc** installed on your machine


## Using Makefile
In terminal navigate to project directory and type
    
    make
    

## Manual compilation
In case the makefile doesn't work type instead

    g++ -Wall -std=c++11 server.cpp -o server
    g++ -Wall -std=c++11 client.cpp -o client

## Launching server and client
Run these commands in seperate terminals

    ./server 4032

    ./client <server hostname>