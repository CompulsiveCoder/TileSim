echo "Starting build for towmsim project"
echo "Dir: $PWD"

DIR=$PWD

cd mod/datamanager/
sh build.sh
cd $DIR

xbuild src/tilesim.sln /p:Configuration=Release
