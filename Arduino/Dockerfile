FROM aboavobr/arduino-buildenv

LABEL maintainer "benj.huser@gmail.com"

WORKDIR /src
VOLUME /src
COPY . /src/
COPY entrypoint.sh /

ENTRYPOINT ["sh", "/entrypoint.sh"]