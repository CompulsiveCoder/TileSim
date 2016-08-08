BRANCH=$1

if [ -z "$BRANCH" ]; then
    BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')
fi

if [ -z "$BRANCH" ]; then
    BRANCH="master"
fi

docker run -it -p 8085:8085 -v $PWD:/tilesim compulsivecoder/ubuntu-mono-redis /bin/bash -c "cd /tilesim && sh build.sh && sh run-www.sh"
