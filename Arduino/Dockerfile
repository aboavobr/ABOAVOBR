FROM aboavobr/arduino-buildenv

LABEL maintainer "benj.huser@gmail.com"

RUN [ "cross-build-start" ]

WORKDIR /src
VOLUME /src
COPY . /src/
COPY entrypoint.sh /

ENTRYPOINT ["sh", "/entrypoint.sh"]

RUN [ "cross-build-end" ]