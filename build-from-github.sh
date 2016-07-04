BRANCH=$1

DIR=$PWD

if [ -z "$BRANCH" ]; then
    BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')
fi

if [ -z "$BRANCH" ]; then
    BRANCH="master"
fi

echo "Branch: $BRANCH"

# If the .tmp/tilesim directory exists then remove it
if [ -d ".tmp/tilesim" ]; then
    rm .tmp/tilesim -rf
fi

git clone https://github.com/CompulsiveCoder/tilesim.git .tmp/tilesim --branch $BRANCH
cd .tmp/tilesim && \
sh init-build.sh && \
cd $DIR && \
rm .tmp/tilesim -rf
