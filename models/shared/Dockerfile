FROM python:3.10-slim

RUN apt-get update && apt-get install -y git curl && rm -rf /var/lib/apt/lists/*

COPY ./models/shared/requirements.txt /tmp/requirements.txt

RUN pip install --no-cache-dir -r /tmp/requirements.txt