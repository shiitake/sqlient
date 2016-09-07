namespace SQLient
open System

module CommandLine = 

    open CommonLibrary
    open Helper

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
            | "/display"::xs ->                   
                let newOptionsSoFar = { optionsSoFar with displayConnection=true}
                parseCommandLineRec xs newOptionsSoFar
            | "/save"::xs ->                   
                match xs with
                | x::xss ->
                    let newOptionsSoFar = { optionsSoFar with saveConnection=x}
                    parseCommandLineRec xss newOptionsSoFar
                | _ ->
                    let newOptionsSoFar = { optionsSoFar with saveConnection="default"}
                    parseCommandLineRec xs newOptionsSoFar                
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
            | "/H"::xs | "/h"::xs ->
                //let's ignore this for now
                parseCommandLineRec xs optionsSoFar
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
            valid = true;
            displayConnection = false;
            saveConnection = ""
            }
        parseCommandLineRec args defaultOptions