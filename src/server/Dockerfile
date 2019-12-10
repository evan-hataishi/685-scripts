FROM ubuntu:16.04

RUN apt-get update -y && \
    apt-get install -y python3 python3-dev python3-pip

# We copy just the requirements.txt first to leverage Docker cache
COPY ./requirements.txt /app/requirements.txt

WORKDIR /app

RUN pip3 install -r requirements.txt

COPY . /app

EXPOSE 8080

ENTRYPOINT [ "python3" ]

CMD [ "server.py" ]

# docker build -t 685-server:latest .
# docker run -it -p 8080:8080 685-server:latest
