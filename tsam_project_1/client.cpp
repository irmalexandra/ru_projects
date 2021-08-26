#include <netdb.h>
#include <iostream>
#include <stdio.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <string>
#include <string.h>

typedef int SOCKET_TYPE;
#define TCP_PORT "5000" // Listening port # on the server
using namespace std;


void send_to_server(SOCKET_TYPE &sock_fd){
    string buffer;
    while(true)
    {
        getline(cin, buffer);
        send(sock_fd, buffer.c_str(), sizeof(buffer), 0); 
    }
}

int main(int argc, char* argv[])
{
    char *hostname = argv[1];
    addrinfo hints, *result;
    hints.ai_socktype = SOCK_STREAM;
    hints.ai_family = AF_INET;
    if (getaddrinfo(hostname, TCP_PORT, &hints, &result) < 0)
    {
        cout << "Invalid server information" << endl;
    }

    SOCKET_TYPE sock_fd = socket(result->ai_family, result->ai_socktype, result->ai_protocol); // int
    if (connect(sock_fd, result->ai_addr, result->ai_addrlen) < 0){
        cout << "Connection failed" << endl;
    }
    else{
        send_to_server(sock_fd);   
    }
    cout << "Hello World" << endl;
}