SHELL := /bin/bash

.SILENT:

PHONY: test redis run run-build

test:
	@docker-compose -f docker-compose-tests.yml run --rm test .buildscripts/run-tests.sh

redis:
	@docker-compose up redis

run: 
	@docker-compose up

run-build:
	@docker-compose up --build