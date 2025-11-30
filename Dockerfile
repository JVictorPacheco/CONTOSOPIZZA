# ============================================
# ESTÁGIO 1: BUILD (compilação)
# ============================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build 
WORKDIR /src

# Copiar apenas os arquivos .csproj primeiro (otimização de cache)
COPY ["src/ContosoPizza.WebApi/ContosoPizza.csproj", "src/ContosoPizza.WebApi/"]
COPY ["src/ContosoPizza.Domain/ContosoPizza.Domain.csproj", "src/ContosoPizza.Domain/"]
COPY ["src/ContosoPizza.Application/ContosoPizza.Application.csproj", "src/ContosoPizza.Application/"]
COPY ["src/ContosoPizza.Infrastructure/ContosoPizza.Infrastructure.csproj", "src/ContosoPizza.Infrastructure/"]

# Restaurar as dependências (NuGet packages)
RUN dotnet restore "src/ContosoPizza.WebApi/ContosoPizza.csproj" 

# Copiar todo o código fonte
COPY . .

# Buildar a aplicação em modo Release
WORKDIR "/src/src/ContosoPizza.WebApi"
RUN dotnet build "ContosoPizza.csproj" -c Release -o /app/build

# ============================================
# ESTÁGIO 2: PUBLISH (preparar para produção)
# ============================================
FROM build AS publish
RUN dotnet publish "ContosoPizza.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ============================================
# ESTÁGIO 3: RUNTIME (imagem final)
# ============================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Expor a porta 8080 (padrão do .NET 9)
EXPOSE 8080

# Copiar os arquivos publicados do estágio anterior
COPY --from=publish /app/publish .

# Definir o comando de inicialização
ENTRYPOINT ["dotnet", "ContosoPizza.dll"]