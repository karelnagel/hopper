docker kill hopperapi
docker build -t hopperapi .
heroku container:push -a hopperapi web
heroku container:release -a hopperapi web