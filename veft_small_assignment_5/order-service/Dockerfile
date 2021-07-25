FROM node:latest

LABEL maintainer="Hópur 43"

ENV DBNAME order-db
ENV NODE_ENV production
ENV PORT=80

copy . /app
WORKDIR /app


RUN npm install

ENTRYPOINT [ "node", "index.js" ]
