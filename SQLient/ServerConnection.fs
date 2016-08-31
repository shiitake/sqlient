﻿namespace SQLient

module ServerConnection = 

    open System
    open System.Data.SqlClient
    open System.Configuration
    open System.Data
    open CommonLibrary

    let sqlCom = "SELECT top 10 * FROM message"
    let tcpHeader = "tcp:"

    let columnHeaders (record : IDataReader) =
        let columnCount = record.FieldCount
        let columnName x = record.GetName(x).ToString()
        let headerList = [
            for i in 0 .. columnCount-1
                do yield columnName i
            ]
        headerList
    
    let printHeaders headerList = 
        for header in headerList do
            printf "%s\t\t" header
        printf "\n" 

    let getReader (command : SqlCommand) =
        try
            printfn "Executing SQL Command."
            command.ExecuteReader()
        with
            | :? System.InvalidCastException as ex ->
                eprintfn "There was an error execting the SQL command."
                eprintfn "%s" ex.Message
                null                
            | :? System.Data.SqlClient.SqlException as ex ->
                eprintfn "There was an error execting the SQL command."
                eprintfn "%s" ex.Message
                null
            | :? System.InvalidOperationException as ex ->
                eprintfn "There was an error execting the SQL command."
                eprintfn "%s" ex.Message
                null            
            | :? System.IO.IOException as ex ->
                eprintfn "There was an error execting the SQL command."
                eprintfn "%s" ex.Message
                null            

    let openConnection (conn: SqlConnection) =
        try
            printfn "Opening connection to server."
            conn.Open()
            printfn "Connection opened."
        with
            | :? System.InvalidOperationException as ex ->
                eprintfn "There was an error opening the connection."
                eprintfn "%s" ex.Message                           
            | :? System.Data.SqlClient.SqlException as ex ->
                eprintfn "There was an error opening the connection."
                eprintfn "%s" ex.Message                
            | :? System.Configuration.ConfigurationErrorsException as ex ->
                eprintfn "There was an error opening the connection."
                eprintfn "%s" ex.Message                        
        conn

    let readRow (record : IDataReader) =
        let columnCount = record.FieldCount    
        let dataType x = record.GetFieldType(x).ToString()    
        let getColumnData x =
            if (record.IsDBNull(x) = true) then "NULL"
            else            
            let columnType = dataType x
            match columnType with
                | "System.Int32" ->                
                    record.GetInt32(x).ToString()
                | "System.String" ->
                    record.GetString(x).ToString()
                | "System.DateTime" ->
                    record.GetDateTime(x).ToString()
                | "System.Boolean" ->
                    record.GetBoolean(x).ToString()
                | _ -> record.GetValue(x).ToString()    
    
        //for each record
        let columnList = [
            for i in 0 .. columnCount-1 do            
                yield getColumnData i            
        ]
        columnList
    
    let getServername request =
        "tcp:" + request.servername + "," + request.port.ToString()

    let buildConnString request =
        let builder = new SqlConnectionStringBuilder()        
        builder.DataSource <- getServername request
        builder.IntegratedSecurity <- false
        builder.NetworkLibrary <- "dbmssocn"
        builder.UserID <- request.userid        
        builder.Password <- request.password        
        builder.InitialCatalog <- request.database        
        builder.ToString()

    let connectToServer request =
        let connString = buildConnString request
        #if DEBUG
        printfn "Connection string: %A" connString
        #endif 
        let query = 
            if request.query = "" then
                sqlCom
            else
                request.query
        use conn = new SqlConnection(connString)
        
        let conn = openConnection conn
        
        let command = new SqlCommand(query, conn)        
        let reader = getReader command           
    
        if (reader = null) then 
            eprintf "Exiting"
        else
        //print Headers
            let headerList = columnHeaders reader
            printHeaders headerList
    
            //print data
            let rowList =
                [
                    while reader.Read() 
                        do yield readRow reader
                ]
            for row in rowList do        
                for column in row do
                    printf "%s\t\t" column
                printf "\n"