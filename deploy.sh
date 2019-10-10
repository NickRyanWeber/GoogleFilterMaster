dotnet publish -c Release 

cp dockerfile ./bin/release/netcoreapp2.2/publish

docker build -t google-filter-master-image ./bin/release/netcoreapp2.2/publish

docker tag google-filter-master-image registry.heroku.com/google-filter-master/web

docker push registry.heroku.com/google-filter-master/web

heroku container:release web -a google-filter-master

# sudo chmod 755 deploy.sh
# ./deploy.sh