DIR=$PWD

echo "Initializing tilesim project"
echo "Dir: $PWD"

git submodule update --init --recursive

cd lib
sh get-libs.sh
cd $DIR

cd mod/datamanager/
INIT_FILE="init.sh"
if [ ! -f "$INIT_FILE" ]; then
  echo "datamanager init file not found: $PWD/$INIT_FILE. Did the submodule fail to clone?"
else
  echo "datamanager submodule found"
  sh init.sh
  cd $DIR
fi
