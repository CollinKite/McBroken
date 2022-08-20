### Setup Backend API ###
Install Docker Desktop

Clone the REPO

open a terminal and CD into the "RegisterAccount" folder in the Backend

install the dockerimage with the command:

docker image build -t flask_docker -f McBrokenBackend\RegisterAccount\Dockerfile .


once installed, run the docker image with the command:

docker run -p 5000:5000 -d flask_docker
