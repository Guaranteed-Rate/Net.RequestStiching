del *.nupkg
nuget pack Clients.csproj -Prop Platform=AnyCPU -Prop Configuration=Release
nuget push *.nupkg