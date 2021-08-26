TARGET=client server 
PP=g++
CFLAGS= -Wall -g -std=c++11
normal: $(TARGET)
client: client.cpp
	$(PP) $(CFLAGS) client.cpp -o client
server: server.cpp
	$(PP) $(CFLAGS) server.cpp -o server
clean:
	$(RM) $(TARGET)