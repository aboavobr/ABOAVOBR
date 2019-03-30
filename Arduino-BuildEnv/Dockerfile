FROM resin/armv7hf-debian-qemu

LABEL maintainer "benj.huser@gmail.com"

RUN [ "cross-build-start" ]

RUN sed -i '/jessie-updates/d' /etc/apt/sources.list  # Now archived

RUN apt-get update && \
    apt-get upgrade -y && \
    DEBIAN_FRONTEND=noninteractive apt-get install -y \
    # Generic
    build-essential \
    curl \
    wget \
    git \
    tar \
    # Python
    python \
    python-pip \
    picocom \
    gcc-avr \
    avr-libc \
    avrdude \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*
    
# arduino
RUN wget http://downloads.arduino.cc/arduino-1.8.4-linuxarm.tar.xz && \
    tar xf arduino-1.8.4-linuxarm.tar.xz && \
    chown root:root -R arduino-1.8.4/ && \
    sh ./arduino-1.8.4/install.sh

RUN pip install ino

WORKDIR /src
COPY main.sh /

ENTRYPOINT ["sh", "/main.sh"]

RUN [ "cross-build-end" ] 