echo "Running TileSim project"
echo "Dir: $PWD"

redis-server &

cd bin/Release
mono tilesimConsole.exe
