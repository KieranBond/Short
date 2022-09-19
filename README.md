# Short
Make that URL short!

## How

Short is pretty simple. Provide a URL and it'll fire back a short one!

## Technologies

Short currently uses RedisDB to cache the generated url, with the original url as the key.

The current implementation does not guarantee the shortened url is unique, and so needs solving!