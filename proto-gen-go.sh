#v1
#protoc  ./Foo.Protos/*.proto -I ./Foo.Protos/ --go_out=plugins=grpc:./Go/Foo_Contracts

#v2 rpc
protoc ./Foo.Protos/*.proto \
	-I ./Foo.Protos/ \
	-I /usr/local/include \
	-I $GOPATH/src \
	-I $GOPATH/src/github.com/grpc-ecosystem/grpc-gateway/third_party/googleapis \
	--go_out=plugins=grpc:./Go/Foo_Contracts

# webapi gateway(生成的包名不能改，先不生产在相同位置)
# protoc ./Foo.Protos/*.proto \
	# -I ./Foo.Protos/ \
	# -I /usr/local/include \
	# -I $GOPATH/src \
	# -I $GOPATH/src/github.com/grpc-ecosystem/grpc-gateway/third_party/googleapis \
	# --grpc-gateway_out=logtostderr=true:./Go/foo.webapi

# webapi gateway 瑕疵，先不生产在相同位置，避免包名问题
protoc ./Foo.Protos/*.proto \
	-I ./Foo.Protos/ \
	-I /usr/local/include \
	-I $GOPATH/src \
	-I $GOPATH/src/github.com/grpc-ecosystem/grpc-gateway/third_party/googleapis \
	--grpc-gateway_out=logtostderr=true:./Go/Foo_Contracts

#post请求进行测试
#curl -X POST -k http://localhost:8081/v1/foo/SayHello -d '{"Name":" Lilyily"}'

