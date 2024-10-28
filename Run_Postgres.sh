docker run -d \
    --name postgres_container \
    -e POSTGRES_USERNAME=group2 \
    -e POSTGRES_PASSWORD=group2 \
    -e POSTGRES_DB=book_exchange \
    -v $(pwd)/init.sql/docker-entrypoint-initdb.d/init.sql \
    -p 5432:5432 \
    postgres:16