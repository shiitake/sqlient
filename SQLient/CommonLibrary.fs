﻿namespace SQLient

open System

[<AutoOpen>]
module CommonLibrary =

    type Connection = {
        Name: string;
        ServerName: string;
        Port: int;
        Database: string;
        UserId: string;
        Password: string;
        ConnectionString: string;        
    }
    
    type ConnectionInfo = {
        Connection: Connection;
        SaveConnection: string option;
        DisplayConnection: bool;
    }    

    type Query = {
        name: string;
        SQLQuery: string;
    }

    type Parameters = {
        ConnectionInfo: ConnectionInfo;
        Query: Query;
    }

    type Configuration = {
        //array of connection strings
        Connections: Connection[];
        //array of queries
        Queries: Query[]
    }

    type CommandLineOptions = {
        servername: string;
        port: int;
        database: string;
        userid: string;
        password: string;
        query: string;
        valid: bool;
        displayConnection: bool;
        saveConnection: string        
        }

    //scoffolding for two track execution
    //setting up error handling
    type Result<'TSuccess, 'TFailure> = 
        | Success of 'TSuccess
        | Failure of 'TFailure

    let succeed x =
        Success x
    
    let fail x =
        Failure x

    //adapter function to handle results
    let bind switchFunction twoTrackInput =
        match twoTrackInput with
            | Success s -> switchFunction s
            | Failure f -> Failure f

    //map function (converts single input function to Result type)
    let map oneTrackFunction twoTrackInput =
        match twoTrackInput with
            | Success s -> Success (oneTrackFunction s)
            | Failure f -> Failure f
    
    //dead end function 
    let tee f x =
        f x |> ignore
        x

    //trycatch
    let trycatch f x =
        try 
            f x |> Success
        with
        | ex -> Failure ex.Message

    //handles results
    let doubleMap successFunc failureFunc twoTrackInput =
        match twoTrackInput with
            | Success s -> Success (successFunc s)
            | Failure f -> Failure (failureFunc f)

    //logging
    let log twoTrackInput =
        let success x = printfn "Request Complete."; x
        let failure x = printfn "Error. %A" x; x
        doubleMap success failure twoTrackInput

