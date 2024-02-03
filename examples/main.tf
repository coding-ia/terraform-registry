terraform {
  required_providers {
    download = {
      version = "~> 1.0.0"
      source  = "4b50-2601-201-8100-20e0-b910-d175-7f34-9b5f.ngrok-free.app/coding-ia/download"
    }
  }
}

module "iam" {
  source  = "4b50-2601-201-8100-20e0-b910-d175-7f34-9b5f.ngrok-free.app/terraform-aws-modules/iam/aws"
  version = "5.34.0"
}

data "download_file" "test" {
  url           = "https://github.com/bjunker99/LambdaHelloWorld/releases/download/v1.1/LambdaHelloWorld.zip"
  output_file   = "LambdaHelloWorld.zip"
}