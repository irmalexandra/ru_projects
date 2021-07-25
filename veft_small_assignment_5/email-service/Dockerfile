FROM python:3

LABEL maintainer="Hópur 43"

ENV PYTHON_ENV production

copy . /app
WORKDIR /app

RUN pip install requests pika

ENTRYPOINT [ "python", "app.py" ]
