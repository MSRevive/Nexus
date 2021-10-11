package log

import (
  "os"
  "io"
  "time"

  "github.com/saintwish/auralog"
)

var (
  Flags = auralog.Ldate | auralog.Ltime | auralog.Lmicroseconds
  FlagsWarn = auralog.Ldate | auralog.Ltime | auralog.Lmicroseconds
  FlagsError = auralog.Ldate | auralog.Ltime | auralog.Lmicroseconds | auralog.Lshortfile
  FlagsDebug = auralog.Ldate | auralog.Ltime | auralog.Lmicroseconds | auralog.Lshortfile

  Core *auralog.Logger
)


// InitDefaultCore(filename string, directory string)
// Initiate core logging with default settings.
func InitDefault(filename string, dir string) {
  file := &auralog.RotateWriter{
    Dir: dir,
    Filename: filename,
    ExTime: 24 * time.Hour,
    MaxSize: 5 * auralog.Megabyte,
  }

  Core = auralog.New(auralog.Config{
    Output: io.MultiWriter(file, os.Stdout),
    Prefix: "[CORE] ",
    Level: auralog.LogLevelDebug,
    Flag: Flags,
    WarnFlag: FlagsWarn,
    ErrorFlag: FlagsError,
    DebugFlag: FlagsDebug,
  })
}
