#!/bin/bash

# Default to development mode
ENV=${1:-inf}

docker network create bbt-development
echo "Starting in DEVELOPMENT mode"
docker-compose up --build
