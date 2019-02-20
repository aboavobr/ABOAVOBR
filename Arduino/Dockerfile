FROM strm/dev-arduino:latest

LABEL maintainer "benj.huser@gmail.com"

WORKDIR /src
VOLUME /src
COPY . /src/

ENTRYPOINT ["/main.sh"]
CMD ["upload"]