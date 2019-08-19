
package main // import "foo.webapi"

import (
  "context"  // Use "golang.org/x/net/context" for Golang version <= 1.6
  "flag"
  "net/http"
  "log"

  "github.com/golang/glog"
  "github.com/grpc-ecosystem/grpc-gateway/runtime"
  "google.golang.org/grpc"
  gw "Foo_Contracts"  // Update
)

var (
  // command-line options:
  // gRPC server endpoint
  grpcServerEndpoint = flag.String("grpc-server-endpoint",  "localhost:50051", "gRPC server endpoint")
)

const (
	port = ":8081"
)

func run() error {
  ctx := context.Background()
  ctx, cancel := context.WithCancel(ctx)
  defer cancel()

  // Register gRPC server endpoint
  // Note: Make sure the gRPC server is running properly and accessible
  mux := runtime.NewServeMux()
  opts := []grpc.DialOption{grpc.WithInsecure()}
  err := gw.RegisterGreeterHandlerFromEndpoint(ctx, mux,  *grpcServerEndpoint, opts)
  if err != nil {
    return err
  }

	log.Printf("Greeter grpc gateway server listening on port " + port);
  // Start HTTP server (and proxy calls to gRPC server endpoint)
  return http.ListenAndServe(port, mux)
}

func main() {
  flag.Parse()
  defer glog.Flush()
  
  if err := run(); err != nil {    
    glog.Fatal(err)
  }
}