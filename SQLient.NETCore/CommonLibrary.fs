namespace SQLient

module CommonLibrary =

    type CommandLineOptions = {
        servername: string;
        port: int;
        database: string;
        userid: string;
        password: string;
        query: string;
        valid: bool        
        }

    //setting up error handling
    type Results<'TSuccess, 'TFailure> = 
        | Success of 'TSuccess
        | Failure of 'TFailure


