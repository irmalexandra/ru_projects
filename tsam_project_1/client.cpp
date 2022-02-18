#include <netdb.h>
#include <iostream>
#include <netinet/in.h>
#include <string.h>

typedef int SOCKET_TYPE;
#define TCP_PORT "4033" // Listening port # on the server
#define BUFLEN 16384

void send_and_recieve(SOCKET_TYPE &sock_fd){
    std::string send_buffer;
    char recv_bufffer[BUFLEN];
    while(true)
    {
        memset(recv_bufffer, 0, BUFLEN);
        getline(std::cin, send_buffer); // Read the input until newline
        send(sock_fd, send_buffer.c_str(), sizeof(send_buffer), 0);
        recv(sock_fd, recv_bufffer, BUFLEN, 0); 
        printf("%s", recv_bufffer);
    }
}

int main(int argc, char* argv[])
{
    char *hostname = argv[1];
    addrinfo hints, *result;
    hints.ai_socktype = SOCK_STREAM;
    hints.ai_family = AF_INET;
    hints.ai_flags = 0;
    if (getaddrinfo(hostname, TCP_PORT, &hints, &result) < 0)
    {
        std::cout << "Invalid server information" << std::endl;
    }

    SOCKET_TYPE sock_fd = socket(result->ai_family, result->ai_socktype, result->ai_protocol); // int
    if (connect(sock_fd, result->ai_addr, result->ai_addrlen) < 0){
        std::cout << "Connection failed" << std::endl;
    }
    else{
        send_and_recieve(sock_fd);   
    }
}
