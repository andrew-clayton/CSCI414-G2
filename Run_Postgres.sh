docker run -d \
    --name postgres_container \
    --network my_network \
    -e POSTGRES_USER=group2 \
    -e POSTGRES_PASSWORD=group2 \
    -e POSTGRES_DB=book_exchange \
    -v C:\Users\andre\source\repos\CSCI414-G2\init.sql:/docker-entrypoint-initdb.d/init.sql \
    -p 5432:5432 \
    postgres:16