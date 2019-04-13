FROM balenalib/rpi-raspbian

LABEL maintainer "benj.huser@gmail.com"

RUN apt-get update && \
    apt-get upgrade -y && \
    DEBIAN_FRONTEND=noninteractive apt-get install -y \
    # Generic
    git \
    wget \
    bzip2

ENV USER=pi

# Install arduino-cli
RUN wget https://downloads.arduino.cc/arduino-cli/arduino-cli-latest-linuxarm.tar.bz2 && \
    tar xjf *.tar.bz2 && \
    rm *.tar.bz2 && \
    mv arduino-cli* /usr/local/bin/arduino-cli && \
    arduino-cli core update-index && \
    arduino-cli core install arduino:avr

WORKDIR /src
COPY main.sh /

RUN chmod +x /main.sh
ENTRYPOINT ["sh", "/main.sh"]