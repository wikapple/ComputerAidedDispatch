#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ComputerAidedDispatchAIDispatcherConsoleApp/ComputerAidedDispatchAIDispatcherConsoleApp.csproj", "ComputerAidedDispatchAIDispatcherConsoleApp/"]
COPY ["ComputerAidedDispatch_UtilityLibrary/ComputerAidedDispatch_UtilityLibrary.csproj", "ComputerAidedDispatch_UtilityLibrary/"]
RUN dotnet restore "ComputerAidedDispatchAIDispatcherConsoleApp/ComputerAidedDispatchAIDispatcherConsoleApp.csproj"
COPY . .
WORKDIR "/src/ComputerAidedDispatchAIDispatcherConsoleApp"
RUN dotnet build "ComputerAidedDispatchAIDispatcherConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ComputerAidedDispatchAIDispatcherConsoleApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ComputerAidedDispatchAIDispatcherConsoleApp.dll"]