namespace SQLient
open System

module ValidateRequest = 
    open CommonLibrary
    open CommandLine

    let validServer options =
        if options.servername = "" then
            eprintfn "Server needs to be specified"
            false
        else true

    let validDatabase options =
        if options.database = "" then
            eprintfn "Database needs to be specified"
            false
        else true
    
    let validUserId options =
        if options.userid = "" then
            eprintfn "UserId needs to be specified"
            false
        else true

    let validPassword options =
        if options.password = "" then
            printf "Please enter your password: "
            let pw = CommandLine.getPassword
            let newOptions = { options with password=pw}
            newOptions
        else options
            
    
    let validateAll options =        
        if (validServer options 
            && validDatabase options 
            && validUserId options) then            
            let validPw = validPassword options            
            let validOptions = { validPw with valid=true }            
            validOptions            
        else
            let invalidOptions = { options with valid=false}
            invalidOptions