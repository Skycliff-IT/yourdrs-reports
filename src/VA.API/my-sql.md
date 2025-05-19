In docker desktop
mysql -h 127.0.0.1 -P 3306 -u appuser -p

docker exec -it mysql-container mysql -u appuser -p

docker exec -i mysql-container mysql -u appuser -papppassword -e "USE testdb; SHOW TABLES;"
