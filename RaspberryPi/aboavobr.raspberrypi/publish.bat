dotnet publish -r linux-arm /p:ShowLinkerSizeComparison=true 
pushd .\bin\Debug\netcoreapp2.1\linux-arm\publish
pscp -pw raspberry -v -r .\* pi@192.168.1.155:/home/pi/aboavobr
popd