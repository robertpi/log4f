﻿namespace Strangelights.Log4f
open System
open System.Diagnostics
open log4net

type Logger(logger: ILog) =
    member __.RawILog = logger
    member __.IsDebugEnabled = logger.IsDebugEnabled
    member __.IsInfoEnabled = logger.IsInfoEnabled
    member __.IsWarnEnabled = logger.IsWarnEnabled
    member __.IsErrorEnabled = logger.IsErrorEnabled
    member __.IsFatalEnabled = logger.IsFatalEnabled
    member __.Debug x = Printf.kprintf logger.Debug x
    member __.Info x = Printf.kprintf logger.Info x
    member __.Warn x = Printf.kprintf logger.Warn x
    member __.Warn ((ex: Exception), x) = Printf.kprintf (fun x -> logger.Warn(x, ex)) x
    member __.Error x = Printf.kprintf logger.Error x
    member __.Error ((ex: Exception), x) = Printf.kprintf (fun x -> logger.Error(x, ex)) x
    member __.Fatal x = Printf.kprintf logger.Fatal x
    member __.Fatal ((ex: Exception), x) = Printf.kprintf (fun x -> logger.Fatal(x, ex)) x
    

module LogManager =
    let getNamedLogger (name: string) = new Logger(LogManager.GetLogger(name))
    let getLogger() = 
        let st = StackTrace()
        let frame = st.GetFrame(1)
        let t = frame.GetMethod().DeclaringType
        new Logger(LogManager.GetLogger(t))


// TODO configuration combinator anyone? :)