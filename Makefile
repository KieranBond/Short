SHELL := /bin/bash

.SILENT:

PHONY: test build redis run run-rebuild

test:
	@docker-compose -f docker-compose-tests.yml run --rm test .buildscripts/run-tests.sh

build:
	@docker build -f Short/Dockerfile .

redis:
	@docker-compose up redis

run: 
	@docker-compose up

run-rebuild:
	@docker-compose up --build