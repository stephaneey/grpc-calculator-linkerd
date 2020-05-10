# grpc-calculator-linkerd
 This sample application is intended to help .NET Core developers finding their way with gRPC and [LinkerD2](https://github.com/linkerd/linkerd2). It provides a series of microservices deployed as a calculator.  Each service is a single mathematical operation:
 * Division: this one throws from time to time a division by zero to let LinkerD catch and report them
 * Multiplication
 * Addition
 * Substraction
 * Percentage: this one calls Multiplication followed by Division
 
The MathFanBoy console generates traffic so as to randomly call one of these mathematical operations. The purpose is to watch how the Service Mesh (LinkerD) helps identifying the flows (observability), monitoring the errors, etc. Feel freel to visit the LinkerD web site or [my blog post](https://techcommunity.microsoft.com/t5/azure-developer-community-blog/meshing-with-linkerd2-using-grpc-enabled-net-core-services/ba-p/1377867) that explains the rationale behind using gRPC & LinkerD. 



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
docker build . -t calcpercentage:dev
docker run -p 5005:80 -t calcpercentage:dev

```
* Start the MathFanboy.exe, as a plain .Net Core console. No Docker in order to avoid having to build a local Docker network.

# Deploy to AKS (or any other K8s cluster)

