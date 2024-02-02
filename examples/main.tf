terraform {
  required_providers {
    download = {
      version = "~> 1.0.0"
      source  = "6651-2601-201-8100-20e0-b910-d175-7f34-9b5f.ngrok-free.app/coding-ia/download"
    }
  }
}

data "download_file" "test" {
  url           = "https://github.com/bjunker99/LambdaHelloWorld/releases/download/v1.1/LambdaHelloWorld.zip"
  output_file   = "LambdaHelloWorld.zip"
}