namespace SQLient

module Helper =
    let printHelp() =
        printfn "SQLient is a simple SQL Server Client"
        printfn "\n"
        printfn "Usage:\t\tsqlient [options]"
        printfn "\n"
        printfn "Commands:"
        printfn "\t/H /h\t\t\t\tDisplay help"
        printfn "\t/S /s [server]\t\t\tServer name"
        printfn "\t/D /d [database]\t\tDatabase name"
        printfn "\t/U /u [Username]\t\tUser Id (currently only SQL Authentication works)"
        printfn "\t/P /p [Password]\t\tUser password"
        printfn "\t/Q /q [Query]\t\t\tSQL Query to be executed"

