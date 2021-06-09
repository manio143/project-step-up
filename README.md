# Project codename "Step Up"

This is a game made in Stride for the GMTK gamejam 2021 by [@manio143](https://github.com/manio143/) and [@ykafia](https://github.com/ykafia/).

## Game

TBD

## Building

You need to setup a github [Personal Access Token](https://docs.github.com/en/github/authenticating-to-github/keeping-your-account-and-data-secure/creating-a-personal-access-token)
and setup a nuget source to pull my Stride [fork](https://github.com/manio143/stride/tree/gmtk-gamejam-2021) for this gamejam:

```
dotnet nuget add source --username USERNAME --password "PAT" --name github-manio143 "https://nuget.pkg.github.com/manio143/index.json"
```

Then simply `restore`, `build` and `publish` or just `dotnet run` the Windows project.
