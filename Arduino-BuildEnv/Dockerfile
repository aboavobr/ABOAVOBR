FROM resin/armv7hf-debian-qemu

LABEL maintainer "benj.huser@gmail.com"

RUN [ "cross-build-start" ]

RUN wget http://downloads.arduino.cc/arduino-1.8.4-linuxarm.tar.xz && \
    sudo tar xf arduino-1.8.4-linuxarm.tar.xz && \
    sudo chown root:root -R arduino-1.8.4/ && \
    ./arduino-1.8.4/install
    apt-get update && \
    apt-get upgrade -y && \
    DEBIAN_FRONTEND=noninteractive apt-get install -y \
    # Generic
    build-essential \
    curl \
    wget \
    git \
    # Python
    python \
    python-pip \
    picocom \
    gcc-avr \
    avr-libc \
    avrdude \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

RUN pip install ino

WORKDIR /src
COPY main.sh /

ENTRYPOINT ["sh", "/main.sh"]

RUN [ "cross-build-end" ] 