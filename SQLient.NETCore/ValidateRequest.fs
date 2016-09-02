namespace SQLient
open System

module ValidateRequest = 
    open CommonLibrary    

    let validateRequest input =
        if input.servername = "" then Failure "Server needs to be specified"
        else if input.database = "" then Failure "Database needs to be specified"
        else if input.userid = "" then Failure "UserId needs to be specified"
        else Success input

    let validatePassword input =
        if input.password = "" then
            printf "Please enter your password: "
            let pw = Helper.getPassword
            let newOptions = { input with password=pw}
            Success newOptions
        else Success input
    
    let validateAll =
        validateRequest >> bind validatePassword