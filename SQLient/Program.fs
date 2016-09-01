
namespace SQLient

module Main =
    open CommandLine
    open Helper
    open ValidateRequest
    open RequestHandler
    open System


    let printHelp() =        
        Helper.printHelp()
        0    

    let handleRequest options =
        handle options 
        

    [<EntryPoint>]
    let main args =         
        let arglist = args |> List.ofSeq                
        match arglist.Length with
            | 0 ->                                
                printHelp()                
            | _ ->
                let options = CommandLine.parseCommandLine arglist                
                handleRequest options |> ignore
                0
