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
if [ -d "$BRANCH" ]; then
    rm .tmp/townsim -rf
fi

DIR=$PWD

git clone https://github.com/CompulsiveCoder/townsim.git .tmp/townsim --branch $BRANCH
cd .tmp/townsim && \
sh init-build-test.sh && \
cd $DIR && \
rm .tmp/townsim -rf
