namespace SQLient
open System

module CommandLine = 

    open CommonLibrary
    open Helper

    let getPassword =        
        let rec readMask pw =
            let k = Console.ReadKey()
            match k.Key with
            | ConsoleKey.Enter -> pw
            | ConsoleKey.Escape -> pw
            | ConsoleKey.Backspace -> 
                match pw with
                | [] -> readMask []
                | _::t ->
                    Console.Write " \b"
                    readMask t
            | _ ->
                Console.Write "\b*"
                readMask (k.KeyChar::pw)
        let password = readMask [] |> Seq.rev |> String.Concat
        Console.WriteLine ()
        password

    let rec parseCommandLineRec args optionsSoFar =
        match args with
            | [] ->                
                optionsSoFar
            | "/S"::xs | "/s"::xs ->
                match xs with
                | x::xss ->
                    let newOptionsSoFar = { optionsSoFar with servername=x}
                    parseCommandLineRec xss newOptionsSoFar
            | "/Q"::xs | "/q"::xs ->
                match xs with
                | x::xss ->
                    let newOptionsSoFar = {optionsSoFar with query=x}
                    parseCommandLineRec xss newOptionsSoFar

            | "/D"::xs | "/d"::xs->
                match xs with
                | x::xss ->
                    let newOptionsSoFar = { optionsSoFar with database=x}
                    parseCommandLineRec xss newOptionsSoFar
            | "/U"::xs | "/u"::xs ->
                match xs with
                | x::xss ->
                    let newOptionsSoFar = { optionsSoFar with userid=x}
                    parseCommandLineRec xss newOptionsSoFar
            | "/P"::xs | "/p"::xs->
                match xs with
                | x::xss ->                    
                    let newOptionsSoFar = { optionsSoFar with port=(int x)}
                    parseCommandLineRec xss newOptionsSoFar                
            | "/PW"::xs | "/pw"::xs->
                match xs with
                | x::xss ->
                    let newOptionsSoFar = { optionsSoFar with password=x}
                    parseCommandLineRec xss newOptionsSoFar
                | _ ->
                    printf "Please enter your password: "
                    let pw = getPassword
                    let newOptionsSoFar = { optionsSoFar with password=pw}
                    parseCommandLineRec xs newOptionsSoFar
            | x::xs ->
                eprintfn "Option '%s' is unrecognized" x
                parseCommandLineRec xs optionsSoFar

    let parseCommandLine args =
        let defaultOptions = {
            servername = "";
            port = 1433;
            database = "";
            userid = "";
            password = "";
            query = "";
            valid = true
            }
        parseCommandLineRec args defaultOptions