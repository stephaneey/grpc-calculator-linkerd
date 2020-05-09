# grpc-calculator-linkerd
 .NET Core GRPC & LinkerD

Still under construction

# Testing with plain docker
* Build the docker files that are in the root folder. Rename them one by one to DockerFile. To build them, run: docker build . -t calcadd:dev or whatever tag you want to use. Repeat this operation for all four operators
* Run local docker containers the following way: docker run -p 5001:80 -t calcadd:dev. Make sure to specify the correct corresponding port (5001 for addition, 5002 for division, etc.). 
```
docker build . -t calcadd:dev
docker run -p 5001:80 -t calcadd:dev
docker build . -t calcdivide:dev
docker run -p 5002:80 -t calcdivide:dev
docker build . -t calcmultiply:dev
docker run -p 5003:80 -t calcadd:multiply
docker build . -t calcsubstract:dev
docker run -p 5004:80 -t calcsubstract:dev

```
* Start the MathFanboy, as a Docker container or plain .Net Core console
