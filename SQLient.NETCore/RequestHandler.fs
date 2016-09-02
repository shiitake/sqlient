namespace SQLient

module RequestHandler = 
    open CommonLibrary
    open ValidateRequest
    open ServerConnection
    
    let handle =
        validateAll
        >> map (tee connectToServer)
        >> log

