BRANCH=$1

DIR=$PWD

if [ -z "$BRANCH" ]; then
    BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')
fi

if [ -z "$BRANCH" ]; then
    BRANCH="master"
fi

echo "Branch: $BRANCH"

# If the .tmp/townsim directory exists then remove it
if [ -d "$BRANCH" ]; then
    rm .tmp/townsim -rf
fi

git clone https://github.com/CompulsiveCoder/townsim.git .tmp/townsim --branch $BRANCH
cd .tmp/townsim && \
sh init-build.sh && \
cd $DIR && \
rm .tmp/townsim -rf
