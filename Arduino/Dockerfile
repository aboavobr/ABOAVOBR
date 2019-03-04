FROM aboavobr/arduino-devenv

LABEL maintainer "benj.huser@gmail.com"

RUN [ "cross-build-start" ]

WORKDIR /src
VOLUME /src
COPY . /src/

RUN [ "cross-build-end" ]