open Saturn
open Giraffe
open Fable.Remoting.Giraffe
open Fable.Remoting.Server
open Shared

let serverApi: IServerApi = {
    Ping = fun () -> async { return "Pong!" }
}

let remotingHandler: HttpHandler =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder (sprintf "/api/%s/%s")
    |> Remoting.fromValue serverApi
    |> Remoting.buildHttpHandler

let application = application {
    url "http://*:5000"
    use_router remotingHandler
}

run application
