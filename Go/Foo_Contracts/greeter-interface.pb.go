// Code generated by protoc-gen-go. DO NOT EDIT.
// source: greeter-interface.proto

package Foo_Contracts

import (
	context "context"
	fmt "fmt"
	proto "github.com/golang/protobuf/proto"
	grpc "google.golang.org/grpc"
	math "math"
)

// Reference imports to suppress errors if they are not otherwise used.
var _ = proto.Marshal
var _ = fmt.Errorf
var _ = math.Inf

// This is a compile-time assertion to ensure that this generated file
// is compatible with the proto package it is being compiled against.
// A compilation error at this line likely means your copy of the
// proto package needs to be updated.
const _ = proto.ProtoPackageIsVersion3 // please upgrade the proto package

func init() { proto.RegisterFile("greeter-interface.proto", fileDescriptor_a4c0bfed33d21379) }

var fileDescriptor_a4c0bfed33d21379 = []byte{
	// 186 bytes of a gzipped FileDescriptorProto
	0x1f, 0x8b, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0xff, 0xe2, 0x12, 0x4f, 0x2f, 0x4a, 0x4d,
	0x2d, 0x49, 0x2d, 0xd2, 0xcd, 0xcc, 0x2b, 0x49, 0x2d, 0x4a, 0x4b, 0x4c, 0x4e, 0xd5, 0x2b, 0x28,
	0xca, 0x2f, 0xc9, 0x17, 0xe2, 0x75, 0xcb, 0xcf, 0xd7, 0x73, 0xce, 0xcf, 0x2b, 0x29, 0x4a, 0x4c,
	0x2e, 0x29, 0x96, 0x12, 0x81, 0xa9, 0xcb, 0x4f, 0xca, 0x4a, 0x4d, 0x2e, 0x81, 0x28, 0x32, 0x9a,
	0xc5, 0xc8, 0xc5, 0xee, 0x0e, 0x91, 0x10, 0x72, 0xe1, 0xe2, 0x08, 0x4e, 0xac, 0xf4, 0x48, 0xcd,
	0xc9, 0xc9, 0x17, 0x92, 0xd6, 0x43, 0xd1, 0xad, 0x07, 0x16, 0x0d, 0x4a, 0x2d, 0x2c, 0x4d, 0x2d,
	0x2e, 0x91, 0x92, 0xc4, 0x2e, 0x59, 0x90, 0x53, 0xa9, 0xc4, 0x20, 0xe4, 0xc9, 0xc5, 0x0b, 0x33,
	0xc5, 0x31, 0x3d, 0x31, 0x33, 0x8f, 0x7c, 0xa3, 0x9c, 0xcc, 0xb8, 0xa4, 0x33, 0xf3, 0xf5, 0xd2,
	0x8b, 0x0a, 0x92, 0xf5, 0x52, 0x2b, 0x12, 0x73, 0x0b, 0x72, 0x52, 0x8b, 0xf5, 0x32, 0x40, 0x0a,
	0xca, 0xf3, 0x8b, 0x72, 0x52, 0x9c, 0xf8, 0xc1, 0x8a, 0xc3, 0x41, 0xec, 0x00, 0x90, 0x67, 0x02,
	0x18, 0x17, 0x31, 0x31, 0x7b, 0xf8, 0x84, 0x27, 0xb1, 0x81, 0xfd, 0x66, 0x0c, 0x08, 0x00, 0x00,
	0xff, 0xff, 0xd9, 0x07, 0x85, 0x7f, 0x1b, 0x01, 0x00, 0x00,
}

// Reference imports to suppress errors if they are not otherwise used.
var _ context.Context
var _ grpc.ClientConn

// This is a compile-time assertion to ensure that this generated file
// is compatible with the grpc package it is being compiled against.
const _ = grpc.SupportPackageIsVersion4

// GreeterClient is the client API for Greeter service.
//
// For semantics around ctx use and closing/ending streaming RPCs, please refer to https://godoc.org/google.golang.org/grpc#ClientConn.NewStream.
type GreeterClient interface {
	// Sends a greeting
	SayHello(ctx context.Context, in *HelloRequest, opts ...grpc.CallOption) (*HelloReply, error)
	// Sends another greeting
	SayHelloAgain(ctx context.Context, in *HelloRequest, opts ...grpc.CallOption) (*HelloReply, error)
}

type greeterClient struct {
	cc *grpc.ClientConn
}

func NewGreeterClient(cc *grpc.ClientConn) GreeterClient {
	return &greeterClient{cc}
}

func (c *greeterClient) SayHello(ctx context.Context, in *HelloRequest, opts ...grpc.CallOption) (*HelloReply, error) {
	out := new(HelloReply)
	err := c.cc.Invoke(ctx, "/Foo.Contracts.Greeter/SayHello", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

func (c *greeterClient) SayHelloAgain(ctx context.Context, in *HelloRequest, opts ...grpc.CallOption) (*HelloReply, error) {
	out := new(HelloReply)
	err := c.cc.Invoke(ctx, "/Foo.Contracts.Greeter/SayHelloAgain", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

// GreeterServer is the server API for Greeter service.
type GreeterServer interface {
	// Sends a greeting
	SayHello(context.Context, *HelloRequest) (*HelloReply, error)
	// Sends another greeting
	SayHelloAgain(context.Context, *HelloRequest) (*HelloReply, error)
}

func RegisterGreeterServer(s *grpc.Server, srv GreeterServer) {
	s.RegisterService(&_Greeter_serviceDesc, srv)
}

func _Greeter_SayHello_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(HelloRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(GreeterServer).SayHello(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/Foo.Contracts.Greeter/SayHello",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(GreeterServer).SayHello(ctx, req.(*HelloRequest))
	}
	return interceptor(ctx, in, info, handler)
}

func _Greeter_SayHelloAgain_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(HelloRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(GreeterServer).SayHelloAgain(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/Foo.Contracts.Greeter/SayHelloAgain",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(GreeterServer).SayHelloAgain(ctx, req.(*HelloRequest))
	}
	return interceptor(ctx, in, info, handler)
}

var _Greeter_serviceDesc = grpc.ServiceDesc{
	ServiceName: "Foo.Contracts.Greeter",
	HandlerType: (*GreeterServer)(nil),
	Methods: []grpc.MethodDesc{
		{
			MethodName: "SayHello",
			Handler:    _Greeter_SayHello_Handler,
		},
		{
			MethodName: "SayHelloAgain",
			Handler:    _Greeter_SayHelloAgain_Handler,
		},
	},
	Streams:  []grpc.StreamDesc{},
	Metadata: "greeter-interface.proto",
}
