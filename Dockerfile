FROM mcr.microsoft.com/dotnet/sdk:6.0
LABEL key="Matheus Andrade"
ENV NODE_ENV=development
ENV PORT=3000
COPY . /var/www
WORKDIR /var/www
RUN dotnet build
ENTRYPOINT dotnet run
EXPOSE $PORT
