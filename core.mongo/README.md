# learning-core
A study on DotNet Core samples

## A study on content based trademark retrieval system

```shell

cd core.mongo

dotnet --version
dotnet new globaljson --sdk-version 2.2.108 --force
dotnet new classlib -n core.mongo.trademark
dotnet new webapi   -n core.mongo.webapi

dotnet sln add core.mongo.trademark/core.mongo.trademark.csproj
dotnet sln add core.mongo.webapi/core.mongo.webapi.csproj
dotnet sln list

cd core.mongo.webapi
dotnet add package DnsClient
dotnet add package MongoDB.Bson
dotnet add package MongoDB.Driver
dotnet add package MongoDB.Driver.Core

cd ..
dotnet build
```