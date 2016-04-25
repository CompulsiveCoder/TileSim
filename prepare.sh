echo "Preparing for tilesim project"
echo "Dir: $PWD"

sudo apt-get update
sudo apt-get install -y git wget mono-complete mono-xsp4

mozroots --import --sync
