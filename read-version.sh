PROJ_NAME='QueroTruck.Api'
PACKAGE_VERSION=$(sed '/<Version>/!d;s/ *<\/\?Version> *//g' src/$PROJ_NAME/$PROJ_NAME.csproj)
echo $PACKAGE_VERSION
