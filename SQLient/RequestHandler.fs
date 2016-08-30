namespace SQLient

module RequestHandler = 
    open CommonLibrary
    open ValidateRequest
    open ServerConnection

    let handle request =
        let validatedRequest = ValidateRequest.validateAll request        
        if (validatedRequest.valid = true) then            
            ServerConnection.connectToServer validatedRequest
        else
            eprintfn "Invalid Options"

