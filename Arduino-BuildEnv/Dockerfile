FROM resin/armv7hf-debian-qemu

LABEL maintainer "benj.huser@gmail.com"

RUN [ "cross-build-start" ]

RUN apt-get update && \
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
    arduino-core && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

RUN pip install ino

WORKDIR /src
COPY main.sh /

ENTRYPOINT ["sh", "/main.sh"]

RUN [ "cross-build-end" ] 