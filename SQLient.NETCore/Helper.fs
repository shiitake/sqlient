namespace SQLient

module Helper =
    let printHelp() =
        printfn "SQLient is a simple SQL Server Client"
        printfn "\n"
        printfn "Usage:\t\tsqlient [options]"
        printfn "\n"
        printfn "Commands:"
        printfn "\t/H /h\t\t\t\tDisplay help"
        printfn "\t/S /s [Server]\t\t\tServer name"
        printfn "\t/P /p [Port]\t\t\tPort number (default 1433)"
        printfn "\t/D /d [Database]\t\tDatabase name"
        printfn "\t/U /u [Username]\t\tUser Id (currently only SQL Authentication works)"
        printfn "\t/PW /pw [Password]\t\tUser password"
        printfn "\t/Q /q [Query]\t\t\tSQL Query to be executed"

    let getPassword =        
        let rec readMask pw =
            let k = System.Console.ReadKey()
            match k.Key with
            | System.ConsoleKey.Enter -> pw
            | System.ConsoleKey.Escape -> pw
            | System.ConsoleKey.Backspace -> 
                match pw with
                | [] -> readMask []
                | _::t ->
                    System.Console.Write " \b"
                    readMask t
            | _ ->
                System.Console.Write "\b*"
                readMask (k.KeyChar::pw)
        let password = readMask [] |> Seq.rev |> System.String.Concat
        System.Console.WriteLine ()
        password