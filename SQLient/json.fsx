//this file is mostly for debugging and testing feel free to ignore

#r "System.Runtime.Serialization"

open System.IO
open System.Runtime.Serialization
open System.Runtime.Serialization.Json
open System.Xml
open System.Text

let defaultName = "sql.json"

type CommandLineOptions = {
    servername: string;
    port: int;
    database: string;
    userid: string;
    password: string;
    query: string;
    valid: bool        
    }

let internal json<'t> (myObj:'t) =
    use ms = new MemoryStream()
    (new DataContractJsonSerializer(typeof<'t>)).WriteObject(ms, myObj)
    Encoding.Default.GetString(ms.ToArray())

let internal unjson<'t> (jsonString:string)  : 't =  
        use ms = new MemoryStream(ASCIIEncoding.Default.GetBytes(jsonString)) 
        let obj = (new DataContractJsonSerializer(typeof<'t>)).ReadObject(ms) 
        obj :?> 't

let getConfigFile name = 
    let localPath = System.IO.Directory.GetCurrentDirectory()    
    let file =
        if (name = "") then defaultName
        else
        name
    let fileName = localPath + "\\" + file
    let fileExists =    
        System.IO.File.Exists(fileName)
    if (fileExists = false) then
        File.Create(fileName)
    else
        File.Open(fileName,FileMode.Open,FileAccess.ReadWrite)

let readConfig name =
    let configFile = getConfigFile name
    let readConfig = new StreamReader(configFile)
    let config = unjson (readConfig.ReadToEnd())
    config
    readConfig.Dispose()
    
let writeConfig name config =
    let configFile = getConfigFile name
    let writeConfig = new StreamWriter(configFile)
    let myJson = json config
    writeConfig.Write(myJson)
    writeConfig.Dispose()
    


let testOptions = {
    servername = "myserver";
    port = 1433;
    database = "mydb";
    userid = "shannon";
    password = "mypass123";
    query = "";
    valid = true
    }

let myJson = json testOptions
printfn "%A" myJson

writeConfig "my.config" testOptions