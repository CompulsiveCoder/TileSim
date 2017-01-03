echo "Starting build for tilesim project"
echo "Dir: $PWD"

DIR=$PWD

cd mod/datamanager/
sh build.sh
cd $DIR

xbuild src/tilesim.sln /p:Configuration=Release
