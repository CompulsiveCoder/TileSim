echo "Testing datamanager project from github"
echo "  Current directory:"
echo "  $PWD"

BRANCH=$1

if [ -z "$BRANCH" ]; then
    BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')
fi

if [ -z "$BRANCH" ]; then
    BRANCH="master"
fi

echo "  Branch: $BRANCH"

# If the .tmp/datamanager directory exists then remove it
if [ -d ".tmp/tilesim" ]; then
    rm .tmp/tilesim -rf
fi

DIR=$PWD

git clone https://github.com/CompulsiveCoder/tilesim.git .tmp/tilesim --branch $BRANCH
cd .tmp/tilesim && \
sh init-build-test.sh && \
cd $DIR && \
rm .tmp/tilesim -rf
