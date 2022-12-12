# Short
Make that URL short!

## How to use

We use `make` to try and keep things consistent. We try to follow the three-musketeers pattern, too.

To run the project locally to use it, just run `make run`. If you want to force a rebuild of the docker images used and then run the project, run `make run-build`.

When running, check the output from the `short` service. By default, it is setup to be available on port `6380`. You should be able to access the Swagger output on `http://localhost:6380/swagger/index.html`.

## Local Development

### Redis

Short uses Redis, both for local development and when deployed. To run redis for local development, use the command `make redis`. By default it is configured to be available on port `6379`, under `localhost`.

### Tests

To run the projects unit tests, run the command `make test`. You'll have test output in your terminal.

## Technologies

Short currently uses RedisDB to cache the generated url, with the original url as the key.

The current implementation does not guarantee the shortened url is unique.
