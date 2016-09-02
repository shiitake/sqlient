namespace SQLient

module ConnectionString =
    open System
    open System.Data.SqlClient
    open System.Configuration
    open CommonLibrary

    let tcpHeader = "tcp:"

    let getServername request =
        "tcp:" + request.servername + "," + request.port.ToString()

    let buildConnString request =
        let builder = new SqlConnectionStringBuilder()        
        builder.DataSource <- getServername request
        //builder.IntegratedSecurity <- false
        //builder.NetworkLibrary <- "dbmssocn"
        builder.UserID <- request.userid        
        builder.Password <- request.password        
        builder.InitialCatalog <- request.database        
        builder

    let displayConnectionString builder =
        printfn "Connection string: %A" (builder.ToString())