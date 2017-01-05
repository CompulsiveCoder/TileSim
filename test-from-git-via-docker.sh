BRANCH=$1

if [ -z "$BRANCH" ]; then
    BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')
fi

if [ -z "$BRANCH" ]; then
    BRANCH="master"
fi

docker run -it -v $PWD:/tilesim-src compulsivecoder/ubuntu-mono /bin/bash -c "git clone /tilesim-src /tilesim-dest/ && cd /tilesim-dest/ && sh init.sh && sh build-and-test-all.sh $BRANCH"
