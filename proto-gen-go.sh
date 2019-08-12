
#1. 生成go版本的stub
protoc ./Foo.Protos/*.proto \
	-I ./Foo.Protos/ \
	-I /usr/local/include \
	-I $GOPATH/src \
	-I $GOPATH/src/github.com/grpc-ecosystem/grpc-gateway/third_party/googleapis \
	--go_out=plugins=grpc:./Go/Foo_Contracts

# webapi gateway 瑕疵，先不生产在相同位置，避免包名问题
#2.v1 使用annotations
# protoc ./Foo.Protos/*.proto \
# 	-I ./Foo.Protos/ \
# 	-I /usr/local/include \
# 	-I $GOPATH/src \
# 	-I $GOPATH/src/github.com/grpc-ecosystem/grpc-gateway/third_party/googleapis \
# 	--grpc-gateway_out=logtostderr=true:./Go/Foo_Contracts

#2.v2 不使用annotations而使用外部声明模式
protoc ./Foo.Protos/*.proto \
	-I ./Foo.Protos/ \
	-I /usr/local/include \
	-I $GOPATH/src \
	-I $GOPATH/src/github.com/grpc-ecosystem/grpc-gateway/third_party/googleapis \
	--grpc-gateway_out=logtostderr=true,grpc_api_configuration=./Foo.Protos/greeter-interface.yaml:./Go/Foo_Contracts

#3 自动生成webapi文档
protoc ./Foo.Protos/*.proto \
	-I ./Foo.Protos/ \
	-I /usr/local/include \
	-I $GOPATH/src \
	-I $GOPATH/src/github.com/grpc-ecosystem/grpc-gateway/third_party/googleapis \
	--swagger_out=logtostderr=true:./Go/foo.webapi/doc


#4，运行rpc server和http josn webapi后post请求进行测试
#curl -X POST -k http://localhost:8081/v1/foo/SayHello -d '{ "name":" Lily"}'

