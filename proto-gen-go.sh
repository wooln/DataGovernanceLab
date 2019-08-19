
#1. 生成go版本的stub（包含grpc插件），从./Foo.Protos/*.proto到./Go/Foo_Contracts/
protoc ./Foo.Protos/*.proto \
	-I ./Foo.Protos/ \
	--go_out=plugins=grpc:./Go/Foo_Contracts/

#2. 生成gateway，使用外部yaml描述模式，不使用annotations模式. 从./Foo.Protos/*.proto到./Go/Foo_Contracts/
protoc ./Foo.Protos/*.proto \
	-I ./Foo.Protos/ \
	-I $GOPATH/src/github.com/grpc-ecosystem/grpc-gateway/third_party/googleapis \
	--grpc-gateway_out=logtostderr=true,grpc_api_configuration=./Foo.Protos/greeter-interface.yaml:./Go/Foo_Contracts/

#3 自动生成webapi文档, 从./Foo.Protos/*.proto到./Go/foo.webapi/doc/
protoc ./Foo.Protos/*.proto \
	-I ./Foo.Protos/ \
	-I $GOPATH/src/github.com/grpc-ecosystem/grpc-gateway/third_party/googleapis \
	--swagger_out=logtostderr=true:./Go/foo.webapi/doc/

#4，运行rpc server和http josn webapi后post请求进行测试
# curl -X POST -k http://localhost:8081/v1/foo/SayHello -d '{ "name":" Lily"}'

