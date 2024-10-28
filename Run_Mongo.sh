docker run -d \
    --name mongo_container
    -e MONGO_INITDB_ROOT_USERNAME=group3 \
    -e MONGO_INITDB_ROOT_PASSWORD=group3 \
    -p 27017:27017 \
    mongo:latest