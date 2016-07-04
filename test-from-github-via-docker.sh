BRANCH=$1
TEST_CATEGORY=$2

if [ -z "$BRANCH" ]; then
    BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')
fi

if [ -z "$BRANCH" ]; then
    BRANCH="master"
fi

if [ -z "$TEST_CATEGORY" ]; then
    TEST_CATEGORY="Unit"
fi

echo "Branch: $BRANCH"
#echo "Tests: $TEST_CATEGORY"  # TODO: Reimplement or remove support for specifying test type

docker run -it compulsivecoder/ubuntu-mono /bin/bash -c "curl https://raw.githubusercontent.com/CompulsiveCoder/TileSim/$BRANCH/test-from-github.sh | sh -s $BRANCH"
