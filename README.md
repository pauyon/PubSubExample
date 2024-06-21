# PubSubExample

In order to get this project up and running you need to do a couple of things:
  1. Install Docker Desktop
  2. Run the following command in a command prompt window: docker run --name redis-pub-sub -p 6379:6379 -d redis

The docker command name and ports can be configured to whatever you want but be sure to change the port numbers within the producer and consumer classes.
If there are any issues when running the command in a command prompt, running it as an admin might help as it needs to install redis from the internet.
