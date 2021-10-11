package main

import (
  "syscall"
  "os"
  "os/signal"
  "runtime"

  "github.com/msrevive/Nexus/log"
)

func main() {
  //Initiate logging.
  log.InitDefault("log.log", "./")

  //Run the server and wait for signal termination for shutdown.
  log.Core.Println("Nexus central MS server is now running. Press CTRL-C to exit.")
  sc := make(chan os.Signal, 1)
  signal.Notify(sc, syscall.SIGINT, syscall.SIGTERM, os.Interrupt, os.Kill)
  <-sc
}

func panicRecovery() {
  if panic := recover(); panic != nil {
    log.Core.Panicln("Nexus has encountered an unrecoverable error and as crashed.")
    log.Core.Panicln("Crash Information: " + panic.(error).Error())

    stack := make([]byte, 65536)
    l := runtime.Stack(stack, true)

    log.Core.Panic("Stack trace:\n" + string(stack[:l]))

    os.Exit(1)
  }
}
