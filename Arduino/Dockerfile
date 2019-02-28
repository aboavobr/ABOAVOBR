FROM arm64v8/debian

LABEL maintainer "benj.huser@gmail.com"

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
VOLUME /src
COPY . /src/

COPY main.sh /

RUN ["chmod", "+x", "/main.sh"]
ENTRYPOINT ["sh", "/main.sh"]