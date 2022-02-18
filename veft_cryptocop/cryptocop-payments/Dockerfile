FROM python:3

LABEL maintainer="Loki Alexander Hopkins"

ENV PYTHON_ENV production

copy . /app
WORKDIR /app

RUN pip install requests pika
RUN pip install card-validator

ENTRYPOINT [ "python", "-u", "app.py" ]
