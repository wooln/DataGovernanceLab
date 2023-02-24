module foo.webapi

go 1.12

require (
	Foo_Contracts v0.0.0-00010101000000-000000000000
	github.com/golang/glog v0.0.0-20160126235308-23def4e6c14b
	github.com/grpc-ecosystem/grpc-gateway v1.9.0
	golang.org/x/net v0.1.0 // indirect
	google.golang.org/genproto v0.0.0-20190605220351-eb0b1bdb6ae6 // indirect
	google.golang.org/grpc v1.21.1
)

replace Foo_Contracts => ../Foo_Contracts
