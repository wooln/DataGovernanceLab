module foo.client

go 1.12

require (
	Foo_Contracts v0.0.0
	github.com/grpc-ecosystem/grpc-gateway v1.9.0 // indirect
	golang.org/x/net v0.1.0 // indirect
	google.golang.org/genproto v0.0.0-20190605220351-eb0b1bdb6ae6 // indirect
	google.golang.org/grpc v1.21.1
)

replace Foo_Contracts => ../Foo_Contracts
