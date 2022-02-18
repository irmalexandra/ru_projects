FROM ubuntu:latest
COPY server.cpp .
RUN apt-get update && apt-get install -y g++
WORKDIR /
RUN g++ -Wall -std=c++11 server.cpp -o server_build
EXPOSE 4032