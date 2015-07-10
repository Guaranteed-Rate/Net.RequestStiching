del *.nupkg
nuget pack MessageHandlers.csproj -Prop Platform=AnyCPU -Prop Configuration=Release
nuget push *.nupkg